using RecupereJa.Models;
using RecupereJa.Repositorio;
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

        public void Atualizar(Usuario entidade)
        {
            _usuarioRepositorio.Atualizar(entidade);
        }

        public List<ItemViewModel> BuscarItemParaHome()
        {
            throw new NotImplementedException();
        }

        public async Task<Usuario?> BuscarPorEmailSenhaAsync(string email, string senha)
        {
          return await _usuarioRepositorio.BuscarPorEmailSenhaAsync(email, senha);
        }

        public Usuario BuscarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Usuario> BuscarTodos()
        {
            throw new NotImplementedException();
        }

        public int Criar(Usuario entidade)
        {
            throw new NotImplementedException();
        }

        public void Deletar(int id)
        {
            throw new NotImplementedException();
        }
    }
}
