using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecupereJa.Repository;
using RecupereJa.Services;
using RecupereJa.ViewModel;

namespace RecupereJa.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IItemService _itens;
        private readonly IItemRepositorio _itemRepositorio;

        public HomeController(ILogger<HomeController> logger, IItemService itens, IItemRepositorio itemRepositorio)
        {
            _logger = logger;
            _itens = itens;
            _itemRepositorio = itemRepositorio;
        }

        public async Task<IActionResult> Index()
        {
            // Busca apenas os aprovados
            var recentes = await _itemRepositorio.BuscarItemParaHomeAsync();

            // Monta ViewModel
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
    }
}