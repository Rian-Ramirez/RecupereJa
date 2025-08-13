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


        public async Task<Item>BuscarPorIdAsync(int id)
        {
            return await _itemRepositorio.BuscarPorIdAsync(id);
        }

        public async Task<List<Item>> BuscarTodosAsync() => 
            await _itemRepositorio.BuscarTodosAsync();

        public async Task<int> CriarAsync(Item entidade) => 
            await _itemRepositorio.CriarAsync(entidade);

        public async Task<bool> DeletarAsync(int id) =>
            await _itemRepositorio.DeletarAsync(id);

        public async Task<List<Item>> BuscarOrdenadoDataCriacaoDescAsync() =>
            await _itemRepositorio.BuscarOrdenadoDataCriacaoDescAsync();

        public async Task<List<ItemViewModel>> BuscarItemParaHomeAsync()
        {
            var itens = await _itemRepositorio.BuscarItemParaHomeAsync();
            return itens.Select(i => new ItemViewModel
            {
                Id = i.Id,
                Titulo = i.Titulo,
                Descricao = i.Descricao,
            //    ImagemObjeto = i.ImagemObjeto
            }).ToList();
        }



        //Não entendi os métodos abaixo | Colocar lógica no outros métodos faltantes
        //public async Task<AtualizarAsync>(Item entidade)
        //{
        //    return await _itemRepositorio.AtualizarAsync(entidade);
        //}
        public Task AtualizarAsync(Item entidade)
        {
            return _itemRepositorio.AtualizarAsync(entidade);
        }

        public Task<List<Item>> BuscarOrdenadoDataCriacaoDesc()
        {
            throw new NotImplementedException();
        }

        public Task<List<ItemViewModel>> BuscarItemParaHome()
        {
            throw new NotImplementedException();
        }

    }
}
