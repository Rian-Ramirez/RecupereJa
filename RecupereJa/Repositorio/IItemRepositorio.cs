using RecupereJa.Models;


namespace RecupereJa.Repository
{
    public interface IItemRepositorio : ICRUD<Item>
    {
        public Task<List<Item>> BuscarOrdenadoDataCriacaoDesc();

        List<Item> BuscarItemParaHome();
    }
}
