using System.Threading.Tasks;
using System.Collections.Generic;
using RecupereJa.Models;
using RecupereJa.Repository;

namespace RecupereJa.Repositorio
{
    public interface IUsuarioRepositorio : ICRUD<Usuario>
    {
        // Busca usuário apenas pelo email ou identificador
        Task<Usuario?> BuscarPorEmailAsync(string email);
        Task<Usuario?> BuscarPorIdentificadorAsync(string identificador);
    }
}
