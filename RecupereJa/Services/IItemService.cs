using RecupereJa.Models;
using RecupereJa.Repository;

namespace RecupereJa.Services
{
    public interface IItemService : ICRUD<Item>
    {
        public Task<List<Item>> BuscarOrdenadoDataCriacaoDesc();
    }
}
