using Microsoft.AspNetCore.Mvc;
using RecupereJa.Services;

namespace RecupereJa.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        public ImageController(IUsuarioService usuarioService) => _usuarioService = usuarioService;

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _usuarioService.DeletarAsync(id);
            return ok ? NoContent() : NotFound();
        }
    }
}