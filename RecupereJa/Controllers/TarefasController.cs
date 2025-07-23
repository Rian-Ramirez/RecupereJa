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
    public class CardControler : Controller
    {
        private readonly ITarefaService _tarefaService;
        private readonly TarefaContext _context;

        public TarefasController(ITarefaService tarefaService, TarefaContext tarefaContext)
        {
            _tarefaService = tarefaService;
            _context = tarefaContext;
        }


        public async Task<IActionResult> Index()
        
        {
            List<Item> items = await _tarefaService.BuscarOrdenadoDataCriacaoDesc();

            // Converte para ViewModel
            var tarefasViewModel = tarefas.Select(t => TarefaViewModel.FromTarefa(t)).ToList();

            return View(tarefasViewModel);
        }

        [IsAdmAttribute]
        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarefa = await _context.Tarefas
                .FirstOrDefaultAsync(m => m.Id == id);

            if (tarefa == null)
            {
                return NotFound();
            }

            // Converte para ViewModel
            var tarefaViewModel = TarefaViewModel.FromTarefa(tarefa);

            return View(tarefaViewModel);
        }

        [RequireAuthentication]
        public IActionResult Create()
        {
            var viewModel = new TarefaViewModel
            {
                Prioridade = PrioridadeEnum.Media,
                Concluida = false
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TarefaViewModel viewModel)
        {
            // Remove validações das propriedades complementares
            ExtrairPropriedadesInuteis();

            if (ModelState.IsValid)
            {
                // Converte ViewModel para Model
                var tarefa = (Tarefa)viewModel;

                _context.Add(tarefa);
                object value = await _context.SaveChangesAsync();
                TempData["Sucesso"] = "Tarefa criada com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        private void ExtrairPropriedadesInuteis()
        {
            ModelState.Remove(nameof(TarefaViewModel.PrioridadeTexto));
            ModelState.Remove(nameof(TarefaViewModel.PrioridadeCor));
            ModelState.Remove(nameof(TarefaViewModel.StatusTexto));
            ModelState.Remove(nameof(TarefaViewModel.StatusCor));
            ModelState.Remove(nameof(TarefaViewModel.TemDescricao));
            ModelState.Remove(nameof(TarefaViewModel.DataCriacao));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa == null)
            {
                return NotFound();
            }

            // Converte para ViewModel
            var viewModel = TarefaViewModel.FromTarefa(tarefa);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TarefaViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            // Remove validações das propriedades complementares
            ExtrairPropriedadesInuteis();

            if (ModelState.IsValid)
            {
                try
                {
                    // Converte ViewModel para Model
                    var tarefa = (Tarefa)viewModel;

                    // Se marcar como concluída, definir data de conclusão
                    if (tarefa.Concluida && tarefa.DataConclusao == null)
                    {
                        tarefa.DataConclusao = DateTime.Now;
                    }
                    // Se desmarcar como concluída, remover data de conclusão
                    else if (!tarefa.Concluida)
                    {
                        tarefa.DataConclusao = null;
                    }

                    _context.Update(tarefa);
                    await _context.SaveChangesAsync();
                    TempData["Sucesso"] = "Tarefa atualizada com sucesso!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TarefaExists(viewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarefa = await _context.Tarefas
                .FirstOrDefaultAsync(m => m.Id == id);

            if (tarefa == null)
            {
                return NotFound();
            }

            // Converte para ViewModel
            var tarefaViewModel = TarefaViewModel.FromTarefa(tarefa);

            return View(tarefaViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa != null)
            {
                _context.Tarefas.Remove(tarefa);
                await _context.SaveChangesAsync();
                TempData["Sucesso"] = "Tarefa removida com sucesso!";
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleConcluida(int id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa == null)
            {
                return NotFound();
            }

            tarefa.Concluida = !tarefa.Concluida;
            tarefa.DataConclusao = tarefa.Concluida ? DateTime.Now : null;

            _context.Update(tarefa);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool TarefaExists(int id)
        {
            return _context.Tarefas.Any(e => e.Id == id);
        }
    }
}
