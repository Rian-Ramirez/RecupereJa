using Microsoft.EntityFrameworkCore;

using RecupereJa.Models;
namespace RecupereJa.Repository
{
    public class ItemRepositorio(ItemContext itemContext) : IItemRepositorio
    {
        private readonly ItemContext _itemContext = itemContext;

        public void Atualizar(Item entidade)
        {
            throw new NotImplementedException();
        }

        public Item BuscarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Item> BuscarTodos()
        {
            throw new NotImplementedException();
        }

        public int Criar(Item entidade)
        {
            _itemContext.Add(entidade);
            return _itemContext.SaveChanges();
        }


        public void Deletar(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Item>> BuscarOrdenadoDataCriacaoDesc()
        {
            return await _itemContext.Item
                    .OrderByDescending(t => t.DataCriacao)
                    .ToListAsync();
        }
    }
}
