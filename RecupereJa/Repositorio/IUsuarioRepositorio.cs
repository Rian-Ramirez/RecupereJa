using System.Threading.Tasks;
using System.Collections.Generic;
using RecupereJa.Models;
using RecupereJa.Repository;

namespace RecupereJa.Repositorio
{
    public interface IUsuarioRepositorio : ICRUD<Usuario>
    {
        Task<Usuario?> BuscarPorEmailSenhaAsync(string email, string senhaHashOuTexto);

        Task<Usuario?> BuscarPorIdentificadorSenhaAsync(string identificador, string senhaHashOuTexto);
    }
}
