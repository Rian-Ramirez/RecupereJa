using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecupereJa.Data;
using RecupereJa.Models;
using RecupereJa.Services;
using RecupereJa.ViewModel;

namespace RecupereJa.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly RecupereJaContext _context;
        private readonly IUsuarioService _usuarios;

        public UsuarioController(RecupereJaContext context, IUsuarioService usuarios)
        {
            _context = context;
            _usuarios = usuarios;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string identificadorOuEmail, string senha, string? returnUrl = null)
        {
            var user = await _usuarios.AutenticarAsync(identificadorOuEmail, senha);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Credenciais inválidas.");
                return View();
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Nome ?? user.Email ?? "Usuario"),
                new Claim(ClaimTypes.Role, user.Cargo.ToString())
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Item");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Registrar() => View();

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registrar(Usuario dto, IFormFile? foto)
        {
            if (!ModelState.IsValid) return View(dto);

            if (foto != null && foto.Length > 0)
            {
                if (foto.ContentType != "image/jpeg" && foto.ContentType != "image/png")
                {
                    ModelState.AddModelError(string.Empty, "Apenas imagens JPEG ou PNG são permitidas.");
                    return View(dto);
                }

                using var ms = new MemoryStream();
                await foto.CopyToAsync(ms);
                dto.FotoUsuario = ms.ToArray();
                dto.FotoUsuarioMimeType = foto.ContentType;
            }

            dto.Senha = BCrypt.Net.BCrypt.HashPassword(dto.Senha); // hash da senha

            var created = await _usuarios.CriarAsync(dto);
            if (created?.Id > 0) return RedirectToAction(nameof(Login));

            ModelState.AddModelError(string.Empty, "Falha ao registrar usuário.");
            return View(dto);
        }

        private int? ObterUsuarioLogadoId()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (claim == null) return null;
            if (int.TryParse(claim.Value, out int userId)) return userId;
            return null;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Perfil()
        {
            var usuarioId = ObterUsuarioLogadoId();
            if (usuarioId == null) return RedirectToAction("Login");

            var viewModel = new PerfilUsuarioViewModel
            {
                Usuario = _context.Usuarios.FirstOrDefault(u => u.Id == usuarioId.Value),
                Itens = _context.Items.Where(i => i.IdUsuario == usuarioId.Value).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditarPerfilAjax(
            int usuarioId,
            IFormFile? formFile,
            [FromForm] string nome,
            [FromForm] string email,
            [FromForm] string? telefone)
        {
            var usuario = await _context.Usuarios.FindAsync(usuarioId);
            if (usuario == null)
                return Json(new { sucesso = false, mensagem = "Usuário não encontrado" });

            usuario.Nome = nome;
            usuario.Email = email;
            usuario.Telefone = telefone;

            if (formFile != null && formFile.Length > 0)
            {
                if (formFile.ContentType != "image/jpeg" && formFile.ContentType != "image/png")
                    return Json(new { sucesso = false, mensagem = "Apenas JPEG ou PNG são permitidas" });

                using var ms = new MemoryStream();
                await formFile.CopyToAsync(ms);
                usuario.FotoUsuario = ms.ToArray();
                usuario.FotoUsuarioMimeType = formFile.ContentType;
            }

            _context.Update(usuario);
            await _context.SaveChangesAsync();

            return Json(new { sucesso = true });
        }
    }
}
