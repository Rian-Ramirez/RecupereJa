using System.Collections.Generic;
using System.Threading.Tasks;
using RecupereJa.Models;
using RecupereJa.Repository;

namespace RecupereJa.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepositorio _iitemRepositorio;

        public ItemService(IItemRepositorio repo) => _iitemRepositorio = repo;

        public Task<Item> CriarAsync(Item entidade) => _iitemRepositorio.CriarAsync(entidade);
        public Task<Item?> BuscarPorIdAsync(int id) => _iitemRepositorio.BuscarPorIdAsync(id);
        public Task<System.Collections.Generic.List<Item>> BuscarTodosAsync() => _iitemRepositorio.BuscarTodosAsync();
        public Task<Item> AtualizarAsync(Item entidade) => (Task<Item>)_iitemRepositorio.AtualizarAsync(entidade);
        public Task<bool> DeletarAsync(int id) => _iitemRepositorio.DeletarAsync(id);

        public Task<System.Collections.Generic.List<Item>> BuscarOrdenadoDataCriacaoDescAsync() => _iitemRepositorio.BuscarOrdenadoDataCriacaoDescAsync();
        public Task<System.Collections.Generic.List<Item>> BuscarItemParaHomeAsync() => _iitemRepositorio.BuscarItemParaHomeAsync();
    }
}
