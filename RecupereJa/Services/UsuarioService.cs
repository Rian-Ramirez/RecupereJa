using System.Threading.Tasks;
using System.Collections.Generic;
using RecupereJa.Models;
using RecupereJa.Repositorio;

namespace RecupereJa.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepositorio _repo;
        public UsuarioService(IUsuarioRepositorio repo) => _repo = repo;

        public Task<Usuario?> BuscarPorIdAsync(int id) => _repo.BuscarPorIdAsync(id);

        public Task<List<Usuario>> BuscarTodosAsync() => _repo.BuscarTodosAsync();

        public Task<Usuario> AtualizarAsync(Usuario entidade) => _repo.AtualizarAsync(entidade);

        public Task<bool> DeletarAsync(int id) => _repo.DeletarAsync(id);

        // Cria usuário
        public Task<Usuario> CriarAsync(Usuario entidade) => _repo.CriarAsync(entidade);

        // ✅ Autenticação ajustada: busca usuário pelo email ou identificador e valida senha com hash
        public async Task<Usuario?> AutenticarAsync(string identificadorOuEmail, string senhaEmTexto)
        {
            // Busca usuário pelo email ou identificador
            var user = await _repo.BuscarPorEmailAsync(identificadorOuEmail)
                       ?? await _repo.BuscarPorIdentificadorAsync(identificadorOuEmail);

            if (user == null) return null;

            // Valida a senha usando BCrypt
            bool senhaValida = BCrypt.Net.BCrypt.Verify(senhaEmTexto, user.Senha);
            if (!senhaValida) return null;

            return user;
        }
    }
}




//Esses abaixo são códigos comentados que podem ser removidos ou mantidos para referência futura.

//private readonly IUsuarioRepositorio _usuarioRepositorio;

//public UsuarioService(IUsuarioRepositorio usuarioRepositorio)
//{
//    _usuarioRepositorio = usuarioRepositorio;
//}

//public async Task<Usuario?> AutenticarAsync(string identificadorOuEmail, string senha)
//{
//    var usuario = await _usuarioRepositorio.BuscarPorEmailSenhaAsync(identificadorOuEmail, senha);

//    if (usuario == null)
//    {
//        usuario = await _usuarioRepositorio.BuscarPorIdentificadorSenhaAsync(identificadorOuEmail, senha);
//    }

//    return usuario;
//}

//public async Task<int> CriarAsync(Usuario usuario)
//{
//    return await _usuarioRepositorio.CriarAsync(usuario);
//}

//public async Task<Usuario?> BuscarPorIdAsync(int id)
//{
//    return await _usuarioRepositorio.BuscarPorIdAsync(id);
//}

//public async Task<List<Usuario>> BuscarTodosAsync()
//{
//    return await _usuarioRepositorio.BuscarTodosAsync();
//}

//public async Task<Usuario?> AtualizarAsync(Usuario usuario)
//{
//    return await _usuarioRepositorio.AtualizarAsync(usuario);
//}

//public async Task<bool> DeletarAsync(int id)
//{
//    return await _usuarioRepositorio.DeletarAsync(id);
//}