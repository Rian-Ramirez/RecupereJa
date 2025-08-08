using RecupereJa.Models;
using RecupereJa.Repository;
using RecupereJa.ViewModel;

namespace RecupereJa.Services
{
    public interface IItemService : ICRUD<Item>
    {
        public Task<List<Item>> BuscarOrdenadoDataCriacaoDesc();

        public List<ItemViewModel> BuscarItemParaHome();
    }
}
