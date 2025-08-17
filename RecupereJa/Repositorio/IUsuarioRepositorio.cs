using RecupereJa.Models;
using RecupereJa.Repository;

namespace RecupereJa.Repositorio
{
    public interface IUsuarioRepositorio : ICRUD<Usuario>
    {
        Task<Usuario?> BuscarPorEmailSenhaAsync(string email, string senha);
        Task<Usuario?> BuscarPorIdentificadorSenhaAsync(string identificador, string senha);
    }
}
