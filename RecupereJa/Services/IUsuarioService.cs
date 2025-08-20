using System.Threading.Tasks;
using System.Collections.Generic;
using RecupereJa.Models;
using RecupereJa.Repository;

namespace RecupereJa.Services
{
    public interface IUsuarioService : ICRUD<Usuario>
    {
        Task<Usuario?> AutenticarAsync(string identificadorOuEmail, string senhaEmTexto);
    }
}