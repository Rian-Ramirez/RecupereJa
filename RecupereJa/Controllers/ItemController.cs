using Microsoft.AspNetCore.Mvc;
using RecupereJa.Enums;
using RecupereJa.Models;
using RecupereJa.Repository;
using RecupereJa.Services;
using RecupereJa.ViewModel;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RecupereJa.Filtros;
using RecupereJa.Data;

namespace RecupereJa.Controllers
{
    [RequireAuthentication]
    public class ItemController : Controller
    {
        private readonly IItemService _itemService;
        private readonly IItemRepositorio _itemRepositorio;
        private readonly RecupereJaContext _recuperejaContext;

        public ItemController(IItemService itemService, IItemRepositorio itemRepositorio, RecupereJaContext recuperejaContext)
        {
            _itemService = itemService;
            _itemRepositorio = itemRepositorio;
            _recuperejaContext = recuperejaContext; 
        }

        // Index - Mostra os itens na tela principal
        public async Task<IActionResult> Index()
        {
            var items = await _itemService.BuscarOrdenadoDataCriacaoDescAsync();
            var itemViewModel = items.Select(ItemViewModel.FromItem).ToList();

            return View(itemViewModel);
        }

        // Details - Exibe detalhes de um item
        [IsAdm]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var item = await _recuperejaContext.Items.FirstOrDefaultAsync(m => m.Id == id);
            if (item == null) return NotFound();

            var itemViewModel = ItemViewModel.FromItem(item);
            return View(itemViewModel);
        }


        // Create - Exibe o formulário para criar um novo item
        public IActionResult Create()
        {
            return View(new ItemViewModel());
        }

        // Create (POST) - Processa a criação de um item
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ItemViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var item = (Item)viewModel;

                    // Adiciona o ID do usuário logado
                    item.IdUsuario = ObterUsuarioLogadoId();

                    await _itemRepositorio.CriarAsync(item); // Usar repositório para criar

                    TempData["Sucesso"] = "Item criado com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Erro ao salvar: " + ex.Message);
                }
            }

            return View(viewModel);
        }

        // Edit - Exibe o formulário para editar um item
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _itemRepositorio.BuscarPorIdAsync(id);
            if (item == null) return NotFound();

            var viewModel = ItemViewModel.FromItem(item);
            return View(viewModel);
        }

        // Edit (POST) - Processa a edição de um item
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ItemViewModel viewModel)
        {
            if (id != viewModel.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var item = (Item)viewModel;
                    item.IdUsuario = ObterUsuarioLogadoId();

                    await _itemRepositorio.AtualizarAsync(item); // Usar repositório para atualizar

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

        // Delete - Exibe o formulário para deletar um item
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var item = await _itemRepositorio.BuscarPorIdAsync(id.Value);
            if (item == null) return NotFound();

            var viewModel = ItemViewModel.FromItem(item);
            return View(viewModel);
        }

        // DeleteConfirmed (POST) - Processa a exclusão de um item
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _itemRepositorio.BuscarPorIdAsync(id);
            if (item != null)
            {
                await _itemRepositorio.DeletarAsync(id); // Usar repositório para deletar
                TempData["Sucesso"] = "Item removido com sucesso!";
            }

            return RedirectToAction(nameof(Index));
        }

        // ToggleConcluida (POST) - Altera o status de um item
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleConcluida(int id)
        {
            var item = await _itemRepositorio.BuscarPorIdAsync(id);
            if (item == null) return NotFound();

            item.Status = !item.Status; // Alterna o status
            await _itemRepositorio.AtualizarAsync(item); // Atualiza o status no repositório

            return RedirectToAction(nameof(Index));
        }

        // Verifica se o item existe
        private bool ItemExists(int id)
        {
            return _itemRepositorio.BuscarTodosAsync().Result.Any(e => e.Id == id); // Usar repositório para verificar
        }

        // Retorna o ID do usuário logado (simula o Identity)
        private int ObterUsuarioLogadoId()
        {
            var idString = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (int.TryParse(idString, out int id))
            {
                return id;
            }

            throw new Exception("Usuário não autenticado ou ID inválido.");
        }

        // Pendentes - Exibe os itens pendentes para aprovação do Mestre
        [HttpGet]
        public async Task<IActionResult> Pendentes()
        {
            var cargo = HttpContext.Session.GetString("Cargo");
            if (cargo != CargoEnum.Mestre.ToString())
                return Unauthorized();

            var itensPendentes = await _itemRepositorio.BuscarPendentesAsync();
            return View(itensPendentes); // Exibe a lista de pendentes
        }

        // Aprovar - Aprova um item
        [HttpPost]
        public async Task<IActionResult> Aprovar(int id)
        {
            var cargo = HttpContext.Session.GetString("Cargo");
            if (cargo != CargoEnum.Mestre.ToString())
                return Unauthorized();

            var item = await _itemRepositorio.AprovarAsync(id);
            if (item == null)
                return NotFound();

            TempData["Sucesso"] = "Item aprovado com sucesso!";
            return RedirectToAction("Pendentes"); // Redireciona de volta para a tela de pendentes
        }
    }
}