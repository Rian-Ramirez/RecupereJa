using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecupereJa.Data;
using RecupereJa.Filtros;
using RecupereJa.Models;
using RecupereJa.Services;
using RecupereJa.ViewModel;

namespace RecupereJa.Controllers
{
    [RequireAuthentication]
    public class ItemController : Controller
    {
        private readonly IItemService _itemService;
        private readonly RecupereJaContext _recuperejaContext;

        public ItemController(IItemService itemService, RecupereJaContext recuperejaContext)
        {
            _itemService = itemService;
            _recuperejaContext = recuperejaContext;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _itemService.BuscarOrdenadoDataCriacaoDescAsync();
            var itemViewModel = items.Select(ItemViewModel.FromItem).ToList();

            return View(itemViewModel);
        }

        [IsAdm]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var item = await _recuperejaContext.Items.FirstOrDefaultAsync(m => m.Id == id);
            if (item == null) return NotFound();

            var itemViewModel = ItemViewModel.FromItem(item);
            return View(itemViewModel);
        }

        public IActionResult Create()
        {
            return View(new ItemViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ItemViewModel viewModel)
        {
            ModelState.Remove(nameof(ItemViewModel.TemDescricao));

            if (ModelState.IsValid)
            {
                var item = (Item)viewModel;
                _recuperejaContext.Add(item);
                await _recuperejaContext.SaveChangesAsync();
                TempData["Sucesso"] = "Item criado com sucesso!";
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var item = await _recuperejaContext.Items.FindAsync(id);
            if (item == null) return NotFound();

            var viewModel = ItemViewModel.FromItem(item);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ItemViewModel viewModel)
        {
            if (id != viewModel.Id) return NotFound();

            ModelState.Remove(nameof(ItemViewModel.TemDescricao));

            if (ModelState.IsValid)
            {
                try
                {
                    var item = (Item)viewModel;

                    _recuperejaContext.Update(item);
                    await _recuperejaContext.SaveChangesAsync();
                    TempData["Sucesso"] = "Item atualizado com sucesso!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(viewModel.Id)) return NotFound();
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var item = await _recuperejaContext.Items.FirstOrDefaultAsync(m => m.Id == id);
            if (item == null) return NotFound();

            var viewModel = ItemViewModel.FromItem(item);
            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _recuperejaContext.Items.FindAsync(id);
            if (item != null)
            {
                _recuperejaContext.Items.Remove(item);
                await _recuperejaContext.SaveChangesAsync();
                TempData["Sucesso"] = "Item removido com sucesso!";
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleConcluida(int id)
        {
            var item = await _recuperejaContext.Items.FindAsync(id);
            if (item == null) return NotFound();

            // Trocar "Concluída" por "Status"
            item.Status = !item.Status;
            await _recuperejaContext.SaveChangesAsync();


            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(int id)
        {
            return _recuperejaContext.Items.Any(e => e.Id == id);
        }
    }
}
