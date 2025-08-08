using RecupereJa.Models;
using RecupereJa.Repository;

namespace RecupereJa.Repositorio
{
    public interface IUsuarioRepositorio : ICRUD<Usuario>
    {

        public Task<Usuario?> BuscarPorEmailSenhaAsync(string email, string senha);
        //public Usuario? BuscarPorEmailSenha(string email, string senhaDigitada)
        //{
        //    var usuario = BuscarPorEmailSenha(email);
        //    if (usuario != null && BCrypt.Net.BCrypt.Verify(senhaDigitada, usuario.Senha))
        //    {
        //        return usuario;
        //    }

        //    return null;
        //}
    }
}
