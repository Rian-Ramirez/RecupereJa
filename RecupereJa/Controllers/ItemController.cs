using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecupereJa.Data;
using RecupereJa.Enums;
using RecupereJa.Models;
using RecupereJa.Repository;
using RecupereJa.Services;
using RecupereJa.ViewModel;
using RecupereJa.Filtros;

namespace RecupereJa.Controllers
{
    public class ItemController : Controller
    {
        private readonly IItemService _itemService;
        private readonly IItemRepositorio _itemRepositorio;
        private readonly RecupereJaContext _context;
        private readonly IWebHostEnvironment _env;

        public ItemController(
            IItemService itemService,
            IItemRepositorio itemRepositorio,
            RecupereJaContext context,
            IWebHostEnvironment env)
        {
            _itemService = itemService;
            _itemRepositorio = itemRepositorio;
            _context = context;
            _env = env;
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

        // GET: Item/Details/5
        [IsAdm]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var item = await _context.Items
                .Include(i => i.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (item == null) return NotFound();

            return View(ItemViewModel.FromItem(item));
        }

        // GET: Item/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Item/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ItemViewModel vm, IFormFile imagem)
        {
            if (!ModelState.IsValid)
                return View(vm);

            try
            {
                var item = (Item)vm;
                item.IdUsuario = ObterUsuarioLogadoId();
                item.DataCriacao = DateTime.UtcNow;
                item.Ativo = true;
                item.Aprovado = false;

                if (imagem != null && imagem.Length > 0)
                {
                    var imagesPath = Path.Combine(_env.WebRootPath ?? "wwwroot", "Images");
                    Directory.CreateDirectory(imagesPath);

                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(imagem.FileName)}";
                    var filePath = Path.Combine(imagesPath, fileName);

                    // 🔎 Log para debug
                    Console.WriteLine($"Tentando salvar em: {filePath}");

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imagem.CopyToAsync(stream);
                    }

                    item.ImagemUrl = $"/Images/{fileName}";
                }

                await _itemRepositorio.CriarAsync(item);
                TempData["Sucesso"] = "Item cadastrado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ ERRO AO CRIAR ITEM: {ex}");
                ModelState.AddModelError("", $"Erro ao salvar item: {ex.Message}");
                return View(vm);
            }
        }


        // GET: Item/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var item = await _itemRepositorio.BuscarPorIdAsync(id.Value);
            if (item == null) return NotFound();

            var vm = ItemViewModel.FromItem(item);
            return View(vm);
        }

        // POST: Item/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ItemViewModel vm, IFormFile imagem)
        {
            if (id != vm.Id) return NotFound();
            if (!ModelState.IsValid) return View(vm);

            var item = await _itemRepositorio.BuscarPorIdAsync(id);
            if (item == null) return NotFound();

            item.Titulo = vm.Titulo;
            item.Descricao = vm.Descricao;
            item.Status = vm.Status;
            item.DataEncontrado = vm.DataEncontrado;

            // Se enviou nova imagem, substitui
            if (imagem != null && imagem.Length > 0)
            {
                try
                {
                    var imagesPath = Path.Combine(_env.WebRootPath ?? "wwwroot", "Images");
                    Directory.CreateDirectory(imagesPath);

                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(imagem.FileName)}";
                    var filePath = Path.Combine(imagesPath, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imagem.CopyToAsync(stream);
                    }

                    item.ImagemUrl = $"/Images/{fileName}";
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao atualizar imagem: {ex.Message}");
                    ModelState.AddModelError("", "Erro ao salvar a nova imagem. Tente novamente.");
                    return View(vm);
                }
            }

            await _itemRepositorio.AtualizarAsync(item);
            TempData["Sucesso"] = "Item atualizado com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        // GET: Item/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var item = await _itemRepositorio.BuscarPorIdAsync(id.Value);
            if (item == null) return NotFound();

            return View(ItemViewModel.FromItem(item));
        }

        // POST: Item/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _itemRepositorio.BuscarPorIdAsync(id);
            if (item != null)
            {
                await _itemRepositorio.DeletarAsync(id);
                TempData["Sucesso"] = "Item removido com sucesso!";
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Item/ToggleConcluida/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleConcluida(int id)
        {
            var item = await _itemRepositorio.BuscarPorIdAsync(id);
            if (item == null) return NotFound();

            item.Status = item.Status == ItemStatusEnum.Perdido
                ? ItemStatusEnum.Encontrado
                : ItemStatusEnum.Perdido;

            await _itemRepositorio.AtualizarAsync(item);
            return RedirectToAction(nameof(Index));
        }

        // GET: Item/Pendentes
        [HttpGet]
        public async Task<IActionResult> Pendentes()
        {
            var cargo = HttpContext.Session.GetString("Cargo");
            if (cargo != CargoEnum.Mestre.ToString()) return Unauthorized();

            var itensPendentes = await _itemRepositorio.BuscarPendentesAsync();
            var vms = itensPendentes.Select(ItemViewModel.FromItem).ToList();
            return View(vms);
        }

        // POST: Item/Aprovar/5
        [HttpPost]
        public async Task<IActionResult> Aprovar(int id)
        {
            var cargo = HttpContext.Session.GetString("Cargo");
            if (cargo != CargoEnum.Mestre.ToString()) return Unauthorized();

            var item = await _itemRepositorio.AprovarAsync(id);
            if (item == null) return NotFound();

            TempData["Sucesso"] = "Item aprovado com sucesso!";
            return RedirectToAction(nameof(Pendentes));
        }

        private int ObterUsuarioLogadoId()
        {
            var idString = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (int.TryParse(idString, out int id)) return id;

            throw new Exception("Usuário não autenticado ou ID inválido.");
        }
    }
}