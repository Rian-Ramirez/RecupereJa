using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;

using RecupereJa.Models;
using RecupereJa.Services;

namespace RecupereJa.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public ImageController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetImage(int id)
        {
            Usuario? usuario = await _usuarioService.BuscarPorIdAsync(id);

            if (usuario is null)
                return NotFound();

            return File(usuario.FotoUsuario, "image/jpeg");
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> UploadImage(int id, [FromForm] UploadDeImagem model)
        {
            if (model.ImageFile == null || model.ImageFile.Length == 0)
                return BadRequest("Nenhum arquivo enviado");

            // Validações básicas
            var allowedTypes = new[] { "image/jpeg", "image/png", "image/gif" };
            if (!allowedTypes.Contains(model.ImageFile.ContentType))
                return BadRequest("Tipo de arquivo não permitido");

            if (model.ImageFile.Length > 5 * 1024 * 1024) // 5MB
                return BadRequest("Arquivo muito grande");

            Usuario? usuario = await _usuarioService.BuscarPorIdAsync(id);
            if (usuario is null)
                return Ok(new { success = false });


            MemoryStream memoryStream = new MemoryStream();
            await model.ImageFile.OpenReadStream().CopyToAsync(memoryStream);

            usuario.FotoUsuario = memoryStream.ToArray();

            await _usuarioService.AtualizarAsync(usuario);
            return Ok(new { success = true, fileName = "" });
        }


        //Não entendi o erro 
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, IUsuarioService _usuarioService)
        {
            bool deuCerto = await _usuarioService.DeletarAsync(id);
            
            if (!deuCerto)
                return NotFound();

            return NoContent();
        }
    }
}
