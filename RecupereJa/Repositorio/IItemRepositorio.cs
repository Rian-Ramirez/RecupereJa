using System.Collections.Generic;
using System.Threading.Tasks;
using RecupereJa.Models;

namespace RecupereJa.Repository
{
    public interface IItemRepositorio : ICRUD<Item>
    {
        Task<List<Item>> BuscarOrdenadoDataCriacaoDescAsync();

        Task<List<Item>> BuscarItemParaHomeAsync();
    }
}
