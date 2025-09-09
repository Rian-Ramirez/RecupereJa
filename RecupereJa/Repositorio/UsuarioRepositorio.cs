using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecupereJa.Data;
using RecupereJa.Models;
using RecupereJa.Repositorio;

namespace RecupereJa.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly RecupereJaContext _ctx;
        public UsuarioRepositorio(RecupereJaContext ctx) => _ctx = ctx;

        public async Task<Usuario> CriarAsync(Usuario entidade)
        {
            _ctx.Set<Usuario>().Add(entidade);
            await _ctx.SaveChangesAsync();
            return entidade;
        }

        public async Task<Usuario?> BuscarPorIdAsync(int id)
            => await _ctx.Set<Usuario>().FindAsync(id);

        public async Task<List<Usuario>> BuscarTodosAsync()
            => await _ctx.Set<Usuario>().AsNoTracking().ToListAsync();

        public async Task<Usuario> AtualizarAsync(Usuario entidade)
        {
            _ctx.Set<Usuario>().Update(entidade);
            await _ctx.SaveChangesAsync();
            return entidade;
        }

        public async Task<bool> DeletarAsync(int id)
        {
            var u = await _ctx.Set<Usuario>().FindAsync(id);
            if (u == null) return false;
            _ctx.Remove(u);
            await _ctx.SaveChangesAsync();
            return true;
        }

        // Busca usuário pelo email
        public async Task<Usuario?> BuscarPorEmailAsync(string email)
        {
            return await _ctx.Set<Usuario>()
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        // Busca usuário pelo identificador
        public async Task<Usuario?> BuscarPorIdentificadorAsync(string identificador)
        {
            return await _ctx.Set<Usuario>()
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Identificador == identificador);
        }
    }
}
