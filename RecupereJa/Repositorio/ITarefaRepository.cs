using RecupereJa.Models;

namespace RecupereJa.Repository
{
    public interface ITarefaRepository : ICRUD<Tarefa>
    {
        public Task<List<Tarefa>> BuscarOrdenadoDataCriacaoDesc();
    }
}
