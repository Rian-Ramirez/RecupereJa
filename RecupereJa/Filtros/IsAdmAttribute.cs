using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RecupereJa.Enums;

namespace RecupereJa.Filtros
{
    public class IsAdmAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var cargo = context.HttpContext.Session.GetString("Cargo");

            if (cargo != CargoEnum.Mestre.ToString())
            {
                context.Result = new RedirectToActionResult("Proibidao", "Usuario", null);
            }
        }
    }
}