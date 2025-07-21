using RecupereJa.Models;
using RecupereJa.Repository;

namespace RecupereJa.Services
{
    public interface ITarefaService : ICRUD<Tarefa>
    {
        public Task<List<Tarefa>> BuscarOrdenadoDataCriacaoDesc();
    }
}
