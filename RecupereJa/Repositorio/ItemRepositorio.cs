using Microsoft.EntityFrameworkCore;
using RecupereJa.Models;

namespace RecupereJa.Repository
{
    public class ItemRepositorio(ItemContext itemContext) : IItemRepositorio
    {
        private readonly ItemContext _itemContext = itemContext;

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
    }
}
