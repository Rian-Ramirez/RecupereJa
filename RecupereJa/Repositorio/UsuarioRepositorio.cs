using Microsoft.EntityFrameworkCore;
using RecupereJa.Models;
using RecupereJa.Repositorio;
using RecupereJa.Repository;
using RecupereJa.ViewModel;

namespace RecupereJa.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly RecupereJaContext _itemContext;

        public UsuarioRepositorio(RecupereJaContext context)
        {
            _itemContext = context;
        }

        public async Task<int> CriarAsync(Usuario entidade)
        {
            _itemContext.Usuarios.Add(entidade);
            return await _itemContext.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Usuario entidade)
        {
            _itemContext.Usuarios.Update(entidade);
            await _itemContext.SaveChangesAsync();
        }

        public async Task<List<Usuario>> BuscarTodosAsync()
        {
            return await _itemContext.Usuarios.ToListAsync();
        }

        public async Task<Usuario?> BuscarPorIdentificadorSenhaAsync(string identificador, string senha)
        {
            return await _itemContext.Usuarios.FirstOrDefaultAsync(u =>
            (u.Email == identificador || u.Nome == identificador) && u.Senha == senha);
        }

        public async Task<Usuario?> BuscarPorEmailSenhaAsync(string email, string senha)
        {
            return await _itemContext.Usuarios
                .FirstOrDefaultAsync(u => u.Email == email && u.Senha == senha);
        }

        public async Task<bool> DeletarAsync(int id)
        {
            var usuario = await _itemContext.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
            if (usuario != null)
            {
                _itemContext.Usuarios.Remove(usuario);
                await _itemContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<Usuario> BuscarPorIdAsync(int id)
        {
            return await _itemContext.Usuarios.FirstOrDefaultAsync(u => u.Id == id)!;
        }
        public async Task<List<ItemViewModel>> BuscarItemParaHomeAsync()
        {
            throw new NotImplementedException();
        }

        Task ICRUD<Usuario>.AtualizarAsync(Usuario entidade)
        {
            throw new NotImplementedException();
        }

        public Task<Usuario?> BuscarPorEmailSenhaAsync(string email, string senha)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeletarAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
