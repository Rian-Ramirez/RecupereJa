using RecupereJa.Models;

namespace RecupereJa.Repository
{
    public interface InterfaceItemRepositorio : ICRUD<Item>
    {
        public Task<List<Item>> BuscarOrdenadoDataCriacaoDesc();
    }
}
