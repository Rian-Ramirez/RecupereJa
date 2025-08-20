using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecupereJa.Models;
using RecupereJa.Services;

namespace RecupereJa.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarios;
        public UsuarioController(IUsuarioService usuarios) => _usuarios = usuarios;

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
                new Claim(ClaimTypes.Name, user.Nome ?? user.Email ?? "Usuario")
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
        public async Task<IActionResult> Registrar(Usuario dto)
        {
            if (!ModelState.IsValid) return View(dto);
            // ATENÇÃO: ideal usar hash de senha (ex.: BCrypt). Aqui fica como texto para simplificar.
            var created = await _usuarios.CriarAsync(dto);
            if (created?.Id > 0)
                return RedirectToAction(nameof(Login));
            ModelState.AddModelError(string.Empty, "Falha ao registrar usuário.");
            return View(dto);
        }
    }
}
