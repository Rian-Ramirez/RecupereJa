using Microsoft.AspNetCore.Mvc;
using RecupereJa.Enums;
using RecupereJa.Models;
using RecupereJa.Repository;
using RecupereJa.Services;
using Microsoft.EntityFrameworkCore;
using RecupereJa.Data;
using RecupereJa.Models;
using RecupereJa.ViewModel;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RecupereJa.Filtros;
using RecupereJa.Data;

namespace RecupereJa.Controllers
{
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
        private readonly RecupereJaContext _context;

        public ItemController(RecupereJaContext context)
        {
            _context = context;
        }

        // GET: Item
        public async Task<IActionResult> Index()
        {
            var itens = await _context.Items
                .Include(i => i.Usuario)
                .ToListAsync();

            var vms = itens.Select(ItemViewModel.FromItem).ToList();
            return View(vms);
        }

        // Details - Exibe detalhes de um item
        [IsAdm]
        // GET: Item/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var item = await _context.Items
                .Include(i => i.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (item == null) return NotFound();

            return View(ItemViewModel.FromItem(item));
        }


        // Create - Exibe o formulário para criar um novo item

        // GET: Item/Create

        public IActionResult Create()
        {
            return View();
        }


        // Create (POST) - Processa a criação de um item

        // POST: Item/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ItemViewModel vm)
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

                var item = (Item)vm;

                // 🔹 Atribui usuário fixo (teste)
                item.IdUsuario = 1;

                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            return View(vm);
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

        // GET: Item/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var item = await _context.Items.FindAsync(id);
            if (item == null) return NotFound();

            return View(ItemViewModel.FromItem(item));
        }

        // POST: Item/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ItemViewModel vm)
        {

            if (id != viewModel.Id) return NotFound();

            if (id != vm.Id) return NotFound();


            if (ModelState.IsValid)
            {
                try
                {

                    var item = (Item)viewModel;
                    item.IdUsuario = ObterUsuarioLogadoId();

                    await _itemRepositorio.AtualizarAsync(item); // Usar repositório para atualizar

                    TempData["Sucesso"] = "Item atualizado com sucesso!";

                    var item = (Item)vm;

                    // 🔹 Garante usuário válido
                    item.IdUsuario = 1;

                    _context.Update(item);
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Items.Any(e => e.Id == vm.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }


        // Delete - Exibe o formulário para deletar um item
        [HttpGet]

        // GET: Item/Delete/5

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();


            var item = await _itemRepositorio.BuscarPorIdAsync(id.Value);

            var item = await _context.Items
                .Include(i => i.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);


            if (item == null) return NotFound();

            return View(ItemViewModel.FromItem(item));
        }


        // DeleteConfirmed (POST) - Processa a exclusão de um item

        // POST: Item/Delete/5

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

            var item = await _context.Items.FindAsync(id);
            if (item != null)
            {
                _context.Items.Remove(item);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

    }
}