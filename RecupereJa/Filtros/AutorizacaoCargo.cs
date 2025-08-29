using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecupereJa.Enums;
using RecupereJa.Models;

namespace RecupereJa.Filtros
{
    public class AutorizacoaCargo : AuthorizeAttribute
    {
        private readonly CargoEnum[] _cargosPermitidos;

        public AutorizacoaCargo(params CargoEnum[] cargos)
        {
            _cargosPermitidos = cargos;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var usuario = (Usuario)httpContext.Session["UsuarioLogado"];

            if (usuario == null)
                return false;

            return _cargosPermitidos.Contains(usuario.Cargo);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("/Erro/AcessoNegado");
        }
    }
}
