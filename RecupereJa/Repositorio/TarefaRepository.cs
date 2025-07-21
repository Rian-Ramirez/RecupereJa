using Microsoft.EntityFrameworkCore;

using RecupereJa.Models;
namespace RecupereJa.Repository
{
    public class TarefaRepository(TarefaContext tarefaContext) : ITarefaRepository
    {
        private readonly TarefaContext _tarefaContext = tarefaContext;

        public void Atualizar(Tarefa entidade)
        {
            throw new NotImplementedException();
        }

        public Tarefa BuscarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Tarefa> BuscarTodos()
        {
            throw new NotImplementedException();
        }

        public int Criar(Tarefa entidade)
        {
            _tarefaContext.Add(entidade);
            return _tarefaContext.SaveChanges();
        }


        public void Deletar(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Tarefa>> BuscarOrdenadoDataCriacaoDesc()
        {
            return await _tarefaContext.Tarefas
                    .OrderByDescending(t => t.DataCriacao)
                    .ToListAsync();
        }
    }
}
