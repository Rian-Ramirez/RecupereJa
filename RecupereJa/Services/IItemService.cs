using RecupereJa.Models;
using RecupereJa.Repository;
using RecupereJa.ViewModel;

namespace RecupereJa.Services
{
    public interface IItemService : ICRUD<Item>
    {
        public Task<List<Item>> BuscarOrdenadoDataCriacaoDescAsync();

        public Task<List<ItemViewModel>> BuscarItemParaHomeAsync();
    }
}
