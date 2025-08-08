using RecupereJa.Models;
using RecupereJa.Repository;
using RecupereJa.ViewModel;

namespace RecupereJa.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepositorio _itemRepositorio;

        public ItemService(IItemRepositorio itemRepositorio) =>
            _itemRepositorio = itemRepositorio;

        public void Atualizar(Item entidade) => _itemRepositorio.Atualizar(entidade);

        public Item BuscarPorId(int id) => _itemRepositorio.BuscarPorId(id);

        public List<Item> BuscarTodos() => _itemRepositorio.BuscarTodos();

        public int Criar(Item entidade) => _itemRepositorio.Criar(entidade);

        public void Deletar(int id) => _itemRepositorio.Deletar(id);

        public async Task<List<Item>> BuscarOrdenadoDataCriacaoDesc() =>
            await _itemRepositorio.BuscarOrdenadoDataCriacaoDesc();

        public List<ItemViewModel> BuscarItemParaHome()
        {
            var itens = _itemRepositorio.BuscarItemParaHome();
            return itens.Select(i => new ItemViewModel
            {
                Id = i.Id,
                Titulo = i.Titulo,
                Descricao = i.Descricao,
            //    ImagemObjeto = i.ImagemObjeto
            }).ToList();
        }
    }
}
