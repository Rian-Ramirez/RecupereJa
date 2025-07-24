using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using RecupereJa.Enums;
using RecupereJa.Filtros;
using RecupereJa.Models;
using RecupereJa.Repository;
using RecupereJa.Services;
using RecupereJa.ViewModels;

namespace RecupereJa.Controllers
{
    [RequireAuthentication]
    public class ItemController : Controller
    {
        private readonly IItemService _itemService;
        private readonly ItemContext _itemContext;

        public ItemController(IItemService itemService, ItemContext itemContext)
        {
            _itemService = itemService;
            _itemContext = itemContext;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _itemService.BuscarOrdenadoDataCriacaoDesc();
            var itemViewModel = items.Select(ItemViewModel.FromItem).ToList();

            return View(itemViewModel);
        }

        [IsAdm]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var item = await _itemContext.Items.FirstOrDefaultAsync(m => m.Id == id);
            if (item == null) return NotFound();

            var itemViewModel = ItemViewModel.FromItem(item);
            return View(itemViewModel);
        }

        public IActionResult Create()
        {
            var viewModel = new ItemViewModel
            {
                Prioridade = PrioridadeEnum.Media,
                Concluida = false
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ItemViewModel viewModel)
        {
            RemoverPropriedadesNaoValidas();

            if (ModelState.IsValid)
            {
                var item = (Item)viewModel;
                _itemContext.Add(item);
                await _itemContext.SaveChangesAsync();
                TempData["Sucesso"] = "Item criado com sucesso!";
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var item = await _itemContext.Items.FindAsync(id);
            if (item == null) return NotFound();

            var viewModel = ItemViewModel.FromItem(item);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ItemViewModel viewModel)
        {
            if (id != viewModel.Id) return NotFound();

            RemoverPropriedadesNaoValidas();

            if (ModelState.IsValid)
            {
                try
                {
                    var item = (Item)viewModel;

                    item.DataConclusao = item.Concluida ? item.DataConclusao ?? DateTime.Now : null;

                    _itemContext.Update(item);
                    await _itemContext.SaveChangesAsync();
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

            var item = await _itemContext.Items.FirstOrDefaultAsync(m => m.Id == id);
            if (item == null) return NotFound();

            var viewModel = ItemViewModel.FromItem(item);
            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _itemContext.Items.FindAsync(id);
            if (item != null)
            {
                _itemContext.Items.Remove(item);
                await _itemContext.SaveChangesAsync();
                TempData["Sucesso"] = "Item removido com sucesso!";
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleConcluida(int id)
        {
            var item = await _itemContext.Items.FindAsync(id);
            if (item == null) return NotFound();

            item.Concluida = !item.Concluida;
            item.DataConclusao = item.Concluida ? DateTime.Now : null;

            _itemContext.Update(item);
            await _itemContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(int id)
        {
            return _itemContext.Items.Any(e => e.Id == id);
        }

        private void RemoverPropriedadesNaoValidas()
        {
            ModelState.Remove(nameof(ItemViewModel.PrioridadeTexto));
            ModelState.Remove(nameof(ItemViewModel.PrioridadeCor));
            ModelState.Remove(nameof(ItemViewModel.StatusTexto));
            ModelState.Remove(nameof(ItemViewModel.StatusCor));
            ModelState.Remove(nameof(ItemViewModel.TemDescricao));
            ModelState.Remove(nameof(ItemViewModel.DataCriacao));
        }
    }
}
