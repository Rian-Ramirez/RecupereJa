using System;
using RecupereJa.ViewModel;

namespace RecupereJa.Repository
{
    public interface ICRUD<T>
    {
        Task<int>CriarAsync(T entidade);

        Task AtualizarAsync(T entidade);

        Task<List<T>> BuscarTodosAsync();

        Task<T> BuscarPorIdAsync(int id);

        Task<bool> DeletarAsync(int id);

        Task<List<ItemViewModel>> BuscarItemParaHomeAsync();
    }
}
