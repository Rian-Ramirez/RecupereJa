using Microsoft.EntityFrameworkCore;
using RecupereJa.Models;
using RecupereJa.ViewModel;

namespace RecupereJa.Repository
{
    public class ItemRepositorio(RecupereJaContext itemContext) : IItemRepositorio
    {
        private readonly RecupereJaContext _itemContext = itemContext;

        public int Criar(Item entidade)
        {
            _itemContext.Items.Add(entidade);
            return _itemContext.SaveChanges();
        }

        public async Task<List<Item>> BuscarOrdenadoDataCriacaoDesc()
        {
            return await _itemContext.Items
                .OrderByDescending(i => i.DataEncontrado)
                .ToListAsync();
        }

        // Implementações opcionais se você usar:
        public Item BuscarPorId(int id)
        {
            return _itemContext.Items.FirstOrDefault(i => i.Id == id)!;
        }

        public List<Item> BuscarTodos()
        {
            return _itemContext.Items.ToList();
        }

        public void Atualizar(Item entidade)
        {
            _itemContext.Items.Update(entidade);
            _itemContext.SaveChanges();
        }

        public void Deletar(int id)
        {
            var item = _itemContext.Items.FirstOrDefault(i => i.Id == id);
            if (item != null)
            {
                _itemContext.Items.Remove(item);
                _itemContext.SaveChanges();
            }
        }

        public List<Item> BuscarItemParaHome()
        {
            return _itemContext.Items.OrderByDescending(i => i.Id).Take(10).ToList();
        }

        List<ItemViewModel> ICRUD<Item>.BuscarItemParaHome()
        {
            throw new NotImplementedException();
        }
    }
}
