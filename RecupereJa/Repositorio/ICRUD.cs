using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecupereJa.Repository
{
    public interface ICRUD<T>
    {
        Task<T> CriarAsync(T entidade);

        Task<T?> BuscarPorIdAsync(int id);

        Task<List<T>> BuscarTodosAsync();

        Task<T> AtualizarAsync(T entidade);

        Task<bool> DeletarAsync(int id);
    }
}
