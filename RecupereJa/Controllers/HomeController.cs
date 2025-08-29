using Microsoft.AspNetCore.Mvc;
using RecupereJa.Data;
using RecupereJa.ViewModel;
using System.Linq;

namespace RecupereJa.Controllers
{
    public class HomeController : Controller
    {
        private readonly RecupereJaContext _context;

        public HomeController(RecupereJaContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var itens = _context.Items
                .Select(i => new ItemViewModel
                {
                    Id = i.Id,
                    Titulo = i.Titulo,
                    Descricao = i.Descricao,
                    Status = i.Status,
                    DataEncontrado = i.DataEncontrado   // ✅ DateTime? sem ToString()
                })
                .ToList();

            return View(itens);
        }
    }
}
