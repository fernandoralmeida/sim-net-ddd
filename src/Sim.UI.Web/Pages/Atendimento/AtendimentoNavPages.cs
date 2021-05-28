using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Sim.UI.Web.Pages.Atendimento
{
    public static class AtendimentoNavPages
    {
        public static string Inicio => "Index";
        public static string Atendimento => "Iniciar";
        public static string AtendimentoNovo => "Novo";
        public static string Periodo => "Consulta";
        public static string Pessoas => "Consulta.ByPF";
        public static string Empresa => "Consulta.ByPJ";
        public static string InicioNavClass(ViewContext viewContext) => PageNavClass(viewContext, Inicio);
        public static string AtendiementoNavClass(ViewContext viewContext) => PageNavClass(viewContext, Atendimento);
        public static string AtendiementoNovoNavClass(ViewContext viewContext) => PageNavClass(viewContext, AtendimentoNovo);
        public static string ByPeriodoNavClass(ViewContext viewContext) => PageNavClass(viewContext, Periodo);
        public static string ByCPFNavClass(ViewContext viewContext) => PageNavClass(viewContext, Pessoas);
        public static string ByCNPJNavClass(ViewContext viewContext) => PageNavClass(viewContext, Empresa);
        private static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}
