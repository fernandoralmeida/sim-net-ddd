using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Sim.UI.Web.Pages.Empresa.Consulta
{
    using Sim.Domain.Cnpj.Entity;
    using Sim.Application.SDE.Interface;
    using Functions;

    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IAppServiceEmpresa _appServiceEmpresa;
        
        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty(SupportsGet = true)]
        public InputModel Input { get; set; }

        public SelectList Municipios { get; set; }
        
        public class InputModel
        {
            [DisplayName("Data Inicial")]
            [DataType(DataType.Date)]
            public DateTime? DataI { get; set; }

            [DisplayName("Data Final")]
            [DataType(DataType.Date)]
            public DateTime? DataF { get; set; }

            public string Base { get; set; }

            public string CNAE { get; set; }            

            public string Situacao { get; set; }
            
            public string Logradouro { get; set; }

            public string Bairro { get; set; }

            public string Municipio { get; set; }

            public string Socio { get; set; }

            [DisplayName("Regime Tributário")]
            public string RegimeTributario { get; set; }

            public ICollection<BaseReceitaFederal> ListaEmpresas { get; set; }
        }

        public IndexModel(IAppServiceEmpresa appServiceEmpresa)
        {
            _appServiceEmpresa = appServiceEmpresa;
        }

        private async Task LoadMunicipios()
        {
            var t = Task.Run(() => _appServiceEmpresa.MicroRegiaoJahu());
            await t;

            if (t != null)
            {
                Municipios = new SelectList(t.Result, nameof(Municipio.Codigo), nameof(Municipio.Descricao), null);
            }
        }

        public void OnGet()
        {
            LoadMunicipios().Wait();
        }
    }
}
