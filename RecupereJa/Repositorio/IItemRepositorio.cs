using RecupereJa.Models;

namespace RecupereJa.Repository
{
    public interface IItemRepositorio : ICRUD<Item>
    {
        public Task<List<Item>> BuscarOrdenadoDataCriacaoDescAsync();

        public Task<List<Item>> BuscarItemParaHomeAsync();
    }
}
