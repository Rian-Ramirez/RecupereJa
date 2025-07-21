using RecupereJa.Models;
using RecupereJa.Repository;

namespace RecupereJa.Services
{
    public class TarefaService : ITarefaService
    {
        private readonly ITarefaRepository _tarefaRepository;

        public TarefaService(ITarefaRepository tarefaRepository) =>
            _tarefaRepository = tarefaRepository;

        public void Atualizar(Tarefa entidade) => _tarefaRepository.Atualizar(entidade);

        public Tarefa BuscarPorId(int id) => _tarefaRepository.BuscarPorId(id);

        public List<Tarefa> BuscarTodos() => _tarefaRepository.BuscarTodos();

        public int Criar(Tarefa entidade) => _tarefaRepository.Criar(entidade);

        public void Deletar(int id) => _tarefaRepository.Deletar(id);

        public async Task<List<Tarefa>> BuscarOrdenadoDataCriacaoDesc() =>
            await _tarefaRepository.BuscarOrdenadoDataCriacaoDesc();
    }
}
