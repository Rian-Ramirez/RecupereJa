using RecupereJa.Models;
using RecupereJa.Repositorio;
using RecupereJa.Repository;

namespace RecupereJa.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly ItemContext _itemContext;

        public UsuarioRepositorio(ItemContext context)
        {
            _itemContext = context;
        }

        public int Criar(Usuario entidade)
        {
            _itemContext.Usuarios.Add(entidade);
            return _itemContext.SaveChanges();
        }

        public void Atualizar(Usuario entidade)
        {
            _itemContext.Usuarios.Update(entidade);
            _itemContext.SaveChanges();
        }

        public List<Usuario> BuscarTodos()
        {
            return _itemContext.Usuarios.ToList();
        }

        public Usuario BuscarPorId(int id)
        {
            return _itemContext.Usuarios.FirstOrDefault(u => u.Id == id)!;
        }

        public Usuario BuscarPorEmailSenha(string email, string senha)
        {
            return _itemContext.Usuarios.FirstOrDefault(u => u.Email == email && u.Senha == senha)!;
        }

        public void Deletar(int id)
        {
            var usuario = _itemContext.Usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario != null)
            {
                _itemContext.Usuarios.Remove(usuario);
                _itemContext.SaveChanges();
            }
        }
    }
}
