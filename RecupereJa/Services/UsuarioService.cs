using RecupereJa.Models;
using RecupereJa.Repositorio;
using RecupereJa.Repository;
using RecupereJa.ViewModel;

namespace RecupereJa.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        public UsuarioService(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public async Task AtualizarAsync(Usuario entidade)
        {
           await _usuarioRepositorio.AtualizarAsync(entidade);
        }

        public List<ItemViewModel> BuscarItemParaHomeAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Usuario?> BuscarPorEmailSenhaAsync(string email, string senha)
        {
          return await _usuarioRepositorio.BuscarPorEmailSenhaAsync(email, senha);
        }

        public Task<Usuario> BuscarPorIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Usuario>> BuscarTodosAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> CriarAsync(Usuario entidade)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeletarAsync(int id)
           => await _usuarioRepositorio.DeletarAsync(id);

        Task<List<ItemViewModel>> ICRUD<Usuario>.BuscarItemParaHomeAsync()
        {
            throw new NotImplementedException();
        }
    }
}
