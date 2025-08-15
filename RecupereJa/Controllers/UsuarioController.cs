using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RecupereJa.ViewModel;
using RecupereJa.Models;
using RecupereJa.Repositorio;

namespace RecupereJa.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioController(ILogger<UsuarioController> logger, IUsuarioRepositorio usuarioRepositorio)
        {
            _logger = logger;
            _usuarioRepositorio = usuarioRepositorio;
        }

        [HttpPost]
        public async Task<IActionResult> Registrar(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                await _usuarioRepositorio.CriarAsync(usuario);
                TempData["Sucesso"] = "Usuário registrado com sucesso!";
                return RedirectToAction("Login");
            }
            return View(usuario);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _usuarioRepositorio.BuscarPorIdentificadorSenhaAsync(model.Identificador, model.Password!);

                if (user != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim("UsuarioId", user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.Nome),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Role, user.Email == "adm@email.com" ? "adm" : "usuario")
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Credenciais inválidas");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Proibidao()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Registrar()
        {
            return View();
        }

        public ActionResult Success()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Perfil()
        {
            var usuarioIdClaim = User.FindFirst("UsuarioId");
            if (usuarioIdClaim == null)
                return Unauthorized();

            var usuarioId = int.Parse(usuarioIdClaim.Value);

            var usuario = await _usuarioRepositorio.BuscarPorIdAsync(usuarioId);
            if (usuario == null)
                return NotFound();

            byte[] fotoBytes = usuario.FotoUsuario;
            if (fotoBytes == null || fotoBytes.Length == 0)
            {
                var caminhoImagem = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/shaq.jpg");
                if (System.IO.File.Exists(caminhoImagem))
                {
                    fotoBytes = System.IO.File.ReadAllBytes(caminhoImagem);
                }
            }

            var perfil = new PerfilUsuarioViewModel
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                FotoPerfilUrl = fotoBytes,
                Nascimento = new DateTime(1990, 7, 15),
                Genero = "Masculino",
                Cidade = "São Paulo",
                Telefone = "11999999999",
                Endereço = "Rua Exemplo, 123",
                Rating = 4.5,
            };

            return View(perfil);
        }

        [HttpPost]
        public async Task<IActionResult> AlterarImagem(int usuarioId, IFormFile formFile)
        {
            if (formFile == null || formFile.Length == 0)
                return RedirectToAction("Perfil");

            var usuario = await _usuarioRepositorio.BuscarPorIdAsync(usuarioId);
            if (usuario == null)
                return NotFound();

            using (var memoryStream = new MemoryStream())
            {
                await formFile.CopyToAsync(memoryStream);
                usuario.FotoUsuario = memoryStream.ToArray();
            }

            await _usuarioRepositorio.AtualizarAsync(usuario);

            TempData["Sucesso"] = "Imagem alterada com sucesso!";
            return RedirectToAction("Perfil");
        }

        public IActionResult Sac()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
