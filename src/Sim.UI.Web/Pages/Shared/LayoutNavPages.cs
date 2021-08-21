using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Sim.UI.Web.Pages.Shared
{
    public class LayoutNavPages
    {
        public static string Inicio => "Inicio";
        public static string Atendimento => "Atendimento";
        public static string AtendimentoDiario => "AtendimentoDiario";
        public static string AtendimentoAtivo => "AtendimentoAtivo";
        public static string AtendimentoConsulta => "AtendimentoConsulta";
        public static string Clientes => "Clientes";
        public static string Agenda => "Agenda";
        public static string AgendaEventos => "AgendaEventos";
        public static string AgendaPlanner => "AgendaPlanner";
        public static string PowerBI => "PowerBI";
        public static string Configuracoes => "Configuracoes";
        public static string Login => "Login";
        public static string InicioNavClass(ViewContext viewContext) => PageNavClass(viewContext, Inicio);
        public static string AtendimentoNavClass(ViewContext viewContext) => PageNavClass(viewContext, Atendimento);
        public static string AtendimentoDiarioNavClass(ViewContext viewContext) => PageNavClassLi(viewContext, AtendimentoDiario);
        public static string AtendimentoAtivoNavClass(ViewContext viewContext) => PageNavClassLi(viewContext, AtendimentoAtivo);
        public static string AtendimentoConsultaNavClass(ViewContext viewContext) => PageNavClassLi(viewContext, AtendimentoConsulta);
        public static string ClientesNavClass(ViewContext viewContext) => PageNavClass(viewContext, Clientes);
        public static string AgendaNavClass(ViewContext viewContext) => PageNavClass(viewContext, Agenda);
        public static string AgendaEventosNavClass(ViewContext viewContext) => PageNavClassLi(viewContext, AgendaEventos);
        public static string AgendaPlannerNavClass(ViewContext viewContext) => PageNavClassLi(viewContext, AgendaPlanner);
        public static string PowerBINavClass(ViewContext viewContext) => PageNavClass(viewContext, PowerBI);
        public static string ConfiguracoesNavClass(ViewContext viewContext) => PageNavClass(viewContext, Configuracoes);
        public static string LoginNavClass(ViewContext viewContext) => PageNavClass(viewContext, Login);
        private static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }

        private static string PageNavClassLi(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePageLi"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }

    }
}
