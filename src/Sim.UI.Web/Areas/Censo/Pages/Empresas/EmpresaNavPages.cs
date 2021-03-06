using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Sim.UI.Web.Areas.Censo.Pages.Empresas
{
    public static class EmpresaNavPages
    {
        public static string Inicio => "Index";
        public static string ConsultaNome => "Consulta-razao-social";
        public static string ConsultaCnpj => "Consulta-nome-socio";
        public static string Relatórios => "Report";
        public static string MigraData => "Migra.data";
        public static string Simples => "SimplesNacional";
        public static string InicioNavClass(ViewContext viewContext) => PageNavClass(viewContext, Inicio);
        public static string ConsultaNomeNavClass(ViewContext viewContext) => PageNavClass(viewContext, ConsultaNome);
        public static string ConsultaCNPJNavClass(ViewContext viewContext) => PageNavClass(viewContext, ConsultaCnpj);
        public static string ReportNavClass(ViewContext viewContext) => PageNavClass(viewContext, Relatórios);
        public static string MigraDataNavClass(ViewContext viewContext) => PageNavClass(viewContext, MigraData);
        public static string SimplesNavClass(ViewContext viewContext) => PageNavClass(viewContext, Simples);
        private static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}
