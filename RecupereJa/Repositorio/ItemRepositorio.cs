using Microsoft.EntityFrameworkCore;
using RecupereJa.Data;
using RecupereJa.Models;
using RecupereJa.Repository;
using RecupereJa.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecupereJa.Repository
{
    public class ItemRepositorio : IItemRepositorio
    {
        private readonly RecupereJaContext _ctx;
        public ItemRepositorio(RecupereJaContext ctx) => _ctx = ctx;

        public async Task<Item> CriarAsync(Item entidade)
        {
            _ctx.Set<Item>().Add(entidade);
            await _ctx.SaveChangesAsync();
            return entidade;
        }

        public async Task<Item?> BuscarPorIdAsync(int id)
            => await _ctx.Set<Item>().FindAsync(id);

        public async Task<List<Item>> BuscarTodosAsync()
            => await _ctx.Set<Item>().AsNoTracking().ToListAsync();

        public async Task<Item> AtualizarAsync(Item entidade)
        {
            _ctx.Set<Item>().Update(entidade);
            await _ctx.SaveChangesAsync();
            return entidade;
        }

        public async Task<bool> DeletarAsync(int id)
        {
            var e = await _ctx.Set<Item>().FindAsync(id);
            if (e == null) return false;
            _ctx.Remove(e);
            await _ctx.SaveChangesAsync();
            return true;
        }

        public async Task<List<Item>> BuscarOrdenadoDataCriacaoDescAsync()
        {
            return await _ctx.Set<Item>()
                .AsNoTracking()
                .OrderByDescending(i => i.DataCriacao)
                .ToListAsync();
        }

        public async Task<List<Item>> BuscarItemParaHomeAsync()
        {
            // Critério simples: itens ativos e mais recentes
            return await _ctx.Set<Item>()
                .AsNoTracking()
                .Where(i => i.Ativo)
                .OrderByDescending(i => i.DataCriacao)
                .Take(12)
                .ToListAsync();
        }

        Task<Usuario> AtualizarAsync(Usuario entidade)
        {
            return AtualizarAsync(entidade);

        }
    }
}
