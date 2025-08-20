using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecupereJa.Services;

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
            return View(recentes);
        }
    }
}
