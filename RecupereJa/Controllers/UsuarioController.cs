using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RecupereJa.ViewModel;
using RecupereJa.Models;
using RecupereJa.Repositorio;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace RecupereJa.Controllers
{
    public class UsuarioController : Controller
    {
        //private readonly 

        private readonly ILogger<UsuarioController> _logger;

        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioController(ILogger<UsuarioController> logger, IUsuarioRepositorio usuarioRepositorio)
        {
            _logger = logger;
            _usuarioRepositorio = usuarioRepositorio;
        }

        [HttpPost]

        public ActionResult Registrar(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _usuarioRepositorio.Criar(usuario);
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


        public IActionResult Perfil()
        {
            var caminhoImagem = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/shaq.jpg");
            byte[] bytesImagem = null;

            if (System.IO.File.Exists(caminhoImagem))
            {
                bytesImagem = System.IO.File.ReadAllBytes(caminhoImagem);
            }

            var perfil = new PerfilUsuarioViewModel
            {
                Nome = "João da Silva",
                Email = "joao@email.com",
                FotoPerfilUrl = bytesImagem,
                Nascimento = new DateTime(1990, 7, 15),
                Genero = "Masculino",
                Cidade = "São Paulo",
                Telefone = "11999999999",
                Endereço = "Rua Exemplo, 123",
                Rating = 4.5,
            };

            return View(perfil);
        }






        public IActionResult Sac()
        {
            return View();
        }




        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _usuarioRepositorio.BuscarPorEmailSenha(model.Username!, model.Password!);
                if (user != null)
                {
                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Nome),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Email == "adm@email.com" ? "adm" : "usuario"),
                new Claim("UserId", user.Id.ToString())
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


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}