using RecupereJa.Models;
using RecupereJa.Repository;

namespace RecupereJa.Repositorio
{
    public interface IUsuarioRepositorio : ICRUD<Usuario>
    {
        Usuario BuscarPorEmailSenha(string email, string senha);
    }
}
