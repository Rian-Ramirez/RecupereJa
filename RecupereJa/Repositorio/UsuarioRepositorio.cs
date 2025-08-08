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

        public async Task<Usuario?> BuscarPorEmailSenhaAsync(string email, string senha)
        {
            return await _itemContext.Usuarios.FirstOrDefaultAsync(u => u.Email == email && u.Senha == senha)!;
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



        public List<ItemViewModel> BuscarItemParaHome()
        {
            throw new NotImplementedException();
        }
    }
}
