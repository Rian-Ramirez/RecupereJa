using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecupereJa.Enums;
using RecupereJa.Services;
using RecupereJa.ViewModel;

namespace RecupereJa.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IItemService _itens;

        public HomeController(ILogger<HomeController> logger, IItemService itens)
        {
            _logger = logger;
            _itens = itens;
        }

        public async Task<IActionResult> Index()
        {
            var recentes = await _itens.BuscarOrdenadoDataCriacaoDescAsync();

            var viewModel = recentes.Select(i => new ItemViewModel
            {
                Id = i.Id,
                Titulo = i.Titulo,
                DataEncontrado = i.DataEncontrado?.ToString("dd/MM/yyyy HH:mm") ?? string.Empty,
            }).ToList();

            return View(viewModel);
        }

        //[Authorize(Roles = "Gerente")]
        //public ActionResult Relatorio()
        //{
        //    return View();
        //}

        [Authorize(CargoEnum == CargoEnum.Mestre)]
        public IActionResult AdministraçãoGeral()
        {
            return View();
        }
    }
}
