using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RecupereJa.ViewModel;
using RecupereJa.Models;

namespace RecupereJa.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(ILogger<UsuarioController> logger)
        {
            _logger = logger;
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

        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                // Aqui você pode adicionar o usuário no banco, etc.
                // Ex: db.Users.Add(user); db.SaveChanges();

                ViewBag.Message = "Usuário registrado com sucesso!";
                return RedirectToAction("Success");
            }

            return View(usuario);
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
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Validar credenciais (exemplo simples)
                if (ValidateUser(model.Username, model.Password))
                {
                    // Criar claims do usuário
                    List<Claim>? claims = new()
                        {
                            new Claim(ClaimTypes.Name, model.Username),
                            new Claim(ClaimTypes.Email, "usuario@email.com"),
                            new Claim(ClaimTypes.Role, "Usuario"),
                            new Claim("UserId", "123")
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

        private bool ValidateUser(string username, string password)
        {
            // Implementar uma lógica válida aqui
            return username == "adm" && password == "123";
        }
    }
}