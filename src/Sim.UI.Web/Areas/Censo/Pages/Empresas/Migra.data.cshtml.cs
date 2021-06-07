using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel;
using AutoMapper;
using System.Text;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Diagnostics;

namespace Sim.UI.Web.Areas.Censo.Pages.Empresas
{
    using Sim.Application.SDE.Interface;
    using Sim.Domain.SDE.Entity;
    using Sim.Service.CNPJ.WebService;
    using Sim.Service.CNPJ.Entity;

    [Authorize(Roles = "Administrador")]
    public class MigradataModel : PageModel
    {
        private readonly IAppServiceEmpresa _appServiceEmpresa;
        private readonly IMapper _mapper;
        private readonly IReceitaWS _receitaWS;

        private List<Domain.Cnpj.Entity.Estabelecimento> lista = new List<Domain.Cnpj.Entity.Estabelecimento>();
        private List<TempCNPJ> linha = new List<TempCNPJ>();

        public class TempCNPJ { public int Id { get; set; } public string CNPJ { get; set; } }

        public MigradataModel(IAppServiceEmpresa appServiceEmpresa, IMapper mapper, IReceitaWS receitaWS)
        {
            _appServiceEmpresa = appServiceEmpresa;
            _mapper = mapper;
            _receitaWS = receitaWS;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [TempData]
        public string StatusMessage1 { get; set; }

        [TempData]
        public string StatusMigration { get; set; }

        [TempData]
        public string StatusMessage2 { get; set; }

        [BindProperty(SupportsGet = true)]
        public VMEmpresa Input { get; set; }


        private string Select_All
        {
            get
            {
                var sql = @"SELECT Campo1, Campo2, Campo3 FROM Estabelecimentos WHERE (Campo21 = '6607')";
                return sql;
            }
        }

        private async Task<DataTable> LoadAsync()
        {
            DataTable dt = new DataTable();

            var t = Task.Run(() => {
                string connetionString;
                SqlConnection cnn;
                connetionString = @"Data Source=DESKTOP-Q9FHB3H;Initial Catalog=RFBDataBaseEmpresas;Trusted_Connection=True;MultipleActiveResultSets=true";
                cnn = new SqlConnection(connetionString);
                cnn.Open();
                SqlCommand dbCommand = cnn.CreateCommand();
                dbCommand.CommandType = CommandType.Text;
                dbCommand.CommandText = Select_All;                
                new SqlDataAdapter(dbCommand).Fill(dt);
                cnn.Close();
            });

            await t;

            return  dt;
        }

        public void OnGet()
        {
            try
            {
                //var res = await LoadAsync();

                StatusMessage = string.Format("Iniciar Migração");
            }
            catch (Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
            }
        }

        private async Task AddCNPJ(string cnpj)
        {
            var rws = await _receitaWS.ConsultarCPNJAsync(cnpj);
            Input = _mapper.Map<VMEmpresa>(rws);

            foreach (var at in rws.AtividadePrincipal)
            {
                Input.CNAE_Principal = at.Code;
                Input.Atividade_Principal = at.Text;
            }

            StringBuilder sb = new StringBuilder();
            foreach (var at in rws.AtividadesSecundarias)
            {
                sb.AppendLine(string.Format("{0} - {1}", at.Code, at.Text));
            }

            Input.Atividade_Secundarias = sb.ToString().Trim();

            var t = Task.Run(() =>
            {
                var empresa = _mapper.Map<Empresa>(Input);
                _appServiceEmpresa.Add(empresa);
            });
            await t;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var rows = await LoadAsync();
            int cont = 0;

            var t = Task.Run(() => {
                foreach (DataRow dr in rows.Rows)
                {
                    cont++;
                    lista.Add(new Domain.Cnpj.Entity.Estabelecimento((string)dr[0], (string)dr[1], (string)dr[2],null
                        ,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null));
                }
            });

            await t;

            stopwatch.Stop();

            StatusMessage1 = string.Format("Total: {0} | Tempo: {1}", lista.Count.ToString(), stopwatch.Elapsed);
            ModelState.AddModelError(string.Empty, string.Format("Total: {0} | Tempo: {1}", lista.Count.ToString(), stopwatch.Elapsed));

            System.Threading.Thread.Sleep(6000);

            cont = 0;

            foreach (var l in lista)
            {
                cont++;
                linha.Add(new TempCNPJ() { Id = cont, CNPJ = string.Format("{0}{1}{2}", l.CNPJBase, l.CNPJOrdem, l.CNPJDV) });
            }

            System.Threading.Thread.Sleep(6000);

            stopwatch.Reset();
            stopwatch.Start();

            cont = 0;

            foreach (var row in linha)
            {
                await AddCNPJ(row.CNPJ);

                cont++;

                System.Threading.Thread.Sleep(21000);

                StatusMigration = string.Format("{0} CNPJ: {1}", cont, row.CNPJ);
                ModelState.AddModelError(string.Empty, string.Format("{0} CNPJ: {1}", cont, row.CNPJ));
            }

            stopwatch.Stop();
            ModelState.AddModelError(string.Empty, string.Format("Total: {0} | Tempo: {1}", linha.Count.ToString(), stopwatch.Elapsed));
            StatusMessage2 = string.Format("Total: {0} | Tempo: {1}", linha.Count.ToString(), stopwatch.Elapsed);

            return Page();
        }

    }
}
