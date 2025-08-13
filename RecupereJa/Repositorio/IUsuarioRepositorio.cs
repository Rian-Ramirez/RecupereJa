using RecupereJa.Models;
using RecupereJa.Repository;

namespace RecupereJa.Repositorio
{
    public interface IUsuarioRepositorio : ICRUD<Usuario>
    {

        public Task<Usuario?> BuscarPorEmailSenhaAsync(string email, string senha);
    }
}
