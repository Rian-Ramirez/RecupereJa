using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RecupereJa.Enums;
using System.Linq;
using System.Security.Claims;

namespace RecupereJa.Filtros
{
    public class AutorizaCargoAttribute : ActionFilterAttribute
    {
        private readonly CargoEnum[] _cargosPermitidos;

        public AutorizaCargoAttribute(params CargoEnum[] cargos)
        {
            _cargosPermitidos = cargos;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var claimCargo = context.HttpContext.User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (claimCargo == null || !_cargosPermitidos.Any(c => c.ToString() == claimCargo))
            {
                context.Result = new RedirectToActionResult("Proibidao", "Usuario", null);
            }
        }
    }
}
