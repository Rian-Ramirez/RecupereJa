using System.Collections.Generic;
using System.Threading.Tasks;
using RecupereJa.Models;
using RecupereJa.Repository;

namespace RecupereJa.Services
{
    public interface IItemService : ICRUD<Item>
    {
        Task<List<Item>> BuscarOrdenadoDataCriacaoDescAsync();
        Task<List<Item>> BuscarItemParaHomeAsync();
    }
}
