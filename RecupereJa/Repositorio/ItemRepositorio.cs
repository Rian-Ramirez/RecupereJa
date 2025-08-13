using Microsoft.EntityFrameworkCore;
using RecupereJa.Models;
using RecupereJa.ViewModel;

namespace RecupereJa.Repository
{
    public class ItemRepositorio(RecupereJaContext itemContext) : IItemRepositorio
    {
        private readonly RecupereJaContext _itemContext = itemContext;

        public async Task<int> CriarAsync(Item entidade)
        {
            _itemContext.Items.Add(entidade);
            return await _itemContext.SaveChangesAsync();
        }

        public async Task<List<Item>> BuscarOrdenadoDataCriacaoDesc()
        {
            return await _itemContext.Items
                .OrderByDescending(i => i.DataEncontrado)
                .ToListAsync();
        }

        // Implementações opcionais se você usar:
        public async Task<Item> BuscarPorIdAsync(int id)
        {
            return (await _itemContext.Items.FirstOrDefaultAsync(i => i.Id == id))!;
            //return await _itemContext.Items.FirstOrDefaultAsync(i => i.Id == id)!;
        }

        public async Task<List<Item>> BuscarTodosAsync()
        {
            return await _itemContext.Items.ToListAsync();
        }

        public async Task AtualizarAsync(Item entidade)
        {
            _itemContext.Items.Update(entidade);
            await _itemContext.SaveChangesAsync();
        }

        public async Task<bool> DeletarAsync(int id)
        {
            var item = await _itemContext.Items.FirstOrDefaultAsync(i => i.Id == id);
            if (item != null)
            {
                _itemContext.Items.Remove(item);
                await _itemContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<Item>> BuscarItemParaHomeAsync() => 
            await _itemContext.Items.OrderByDescending(i => i.Id).Take(10).ToListAsync();

        Task<List<ItemViewModel>> ICRUD<Item>.BuscarItemParaHomeAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<Item>> BuscarOrdenadoDataCriacaoDescAsync()
        {
            throw new NotImplementedException();
        }
    }
}
