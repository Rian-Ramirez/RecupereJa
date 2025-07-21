using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace RecupereJa.Filtros
{
    public class IsAdmAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.User.Claims
                .FirstOrDefault(c=> c.Type == ClaimTypes.Role)?.Value != "adm")
            {
                context.Result = new RedirectToActionResult("Proibidao", "Account", null);
            }
        }
    }
}
