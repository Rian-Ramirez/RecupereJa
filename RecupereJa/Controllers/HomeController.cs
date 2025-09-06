using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecupereJa.Repository;
using RecupereJa.Services;
using RecupereJa.Data;
using RecupereJa.ViewModel;
using System.Linq;
using System.Threading.Tasks;

namespace RecupereJa.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IItemService _itens;
        private readonly IItemRepositorio _itemRepositorio;
        private readonly RecupereJaContext _context;

        public HomeController(
            ILogger<HomeController> logger,
            IItemService itens,
            IItemRepositorio itemRepositorio,
            RecupereJaContext context)
        {
            _logger = logger;
            _itens = itens;
            _itemRepositorio = itemRepositorio;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var recentes = await _itemRepositorio.BuscarItemParaHomeAsync();

            var viewModel = recentes.Select(i => new ItemViewModel
            {
                Id = i.Id,
                Titulo = i.Titulo,
                DataEncontrado = i.DataEncontrado
            }).ToList();

            return View(viewModel);
        }

        public IActionResult TodosItens()
        {
            var itens = _context.Items
                .Select(i => new ItemViewModel
                {
                    Id = i.Id,
                    Titulo = i.Titulo,
                    Descricao = i.Descricao,
                    Status = i.Status,
                    DataEncontrado = i.DataEncontrado
                })
                .ToList();

            return View(itens);
        }
    }
}
