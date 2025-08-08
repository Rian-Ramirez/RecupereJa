using RecupereJa.Models;
using RecupereJa.Repository;

namespace RecupereJa.Services
{
    public interface IUsuarioService : ICRUD<Usuario>
    {
        public Task< Usuario?> BuscarPorEmailSenhaAsync(string email, string senha);

    }
}
