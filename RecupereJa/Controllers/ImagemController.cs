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
        public IActionResult GetImage(int id)
        {
            Usuario? usuario  =  _usuarioService.BuscarPorId(id);

            if (usuario is null)
                return NotFound();

            return File(usuario.FotoUsuario, "image/jpeg");
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage([FromForm] UploadDeImagem model)
        {
            if (model.ImageFile == null || model.ImageFile.Length == 0)
                return BadRequest("Nenhum arquivo enviado");

            // Validações básicas
            var allowedTypes = new[] { "image/jpeg", "image/png", "image/gif" };
            if (!allowedTypes.Contains(model.ImageFile.ContentType))
                return BadRequest("Tipo de arquivo não permitido");

            if (model.ImageFile.Length > 5 * 1024 * 1024) // 5MB
                return BadRequest("Arquivo muito grande");

            Usuario? usuario = _usuarioService.BuscarPorId(1);
            if (usuario is null)
                return Ok(new { success = false });


            MemoryStream memoryStream = new MemoryStream();
            await model.ImageFile.OpenReadStream().CopyToAsync(memoryStream);

            usuario.FotoUsuario = memoryStream.ToArray();

            _usuarioService.Atualizar(usuario);



            return Ok(new { success = true, fileName = "" });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteImage(int id)
        {
            bool deuCerto =  _usuarioService.Deletar(id);

            return NotFound();
        }
    }
}
