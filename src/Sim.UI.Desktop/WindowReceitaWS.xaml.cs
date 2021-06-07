using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Diagnostics;
using AutoMapper;

namespace Sim.UI.Desktop
{
    using Sim.Domain.SDE.Entity;
    using Sim.Service.CNPJ.Entity;
    using Sim.Service.CNPJ.WebService;
    using Sim.Cross.Data.Context;
    /// <summary>
    /// Lógica interna para ReceitaWS.xaml
    /// </summary>
    public partial class WindowReceitaWS : Window
    {
        private readonly IMapper _mapper;
        private List<Domain.Cnpj.Entity.Estabelecimento> lista = new List<Domain.Cnpj.Entity.Estabelecimento>();
        private List<TempCNPJ> linha = new List<TempCNPJ>();
        private ReceitaWS wS = new ReceitaWS();
        private ApplicationContext _empresa = new ApplicationContext();
        private Empresa Input = new Empresa();
        public WindowReceitaWS()
        {
            InitializeComponent();
            dataGrid_Destino.ItemsSource = _empresa.Empresa.ToList();
        }

        public class TempCNPJ { public int Id { get; set; } public string CNPJ { get; set; } }


        private string Select_All
        {
            get
            {
                var sql = @"SELECT        Campo1, Campo2, Campo3

FROM            Estabelecimentos 

WHERE        (Campo21 = '6607')";

                return sql;
            }
        }

        private DataTable ShowData()
        {
            string connetionString;
            SqlConnection cnn;
            connetionString = @"Data Source=DESKTOP-Q9FHB3H;Initial Catalog=RFBDataBaseEmpresas;Trusted_Connection=True;MultipleActiveResultSets=true";
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            SqlCommand dbCommand = cnn.CreateCommand();
            dbCommand.CommandType = CommandType.Text;
            dbCommand.CommandText = Select_All;
            DataTable dt = new DataTable();
            new SqlDataAdapter(dbCommand).Fill(dt);
            cnn.Close();
            return dt;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            btnclk_1.IsEnabled = false;
            var t = Task.Factory.StartNew(() =>
            {
                int cont = 0;                

                foreach (DataRow dr in ShowData().Rows)
                {
                    cont++;
                    lista.Add(new Domain.Cnpj.Entity.Estabelecimento(cont, (string)dr[0], (string)dr[1], (string)dr[2]));
                }

                dataGrid.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, new Action(delegate () {
                    dataGrid.ItemsSource = lista;
                    labelrotulo1.Content = lista.Count;
                    stopwatch.Stop();
                    Title = string.Format("Tempo: {0}", stopwatch.Elapsed);
                    btnclk_1.IsEnabled = true;
                }));

            });
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            btn_migra.IsEnabled = false;

            dataGrid.ItemsSource = null;
            int c = 0;
            foreach(var l in lista)
            {
                c++;
                linha.Add(new TempCNPJ() { Id = c, CNPJ = string.Format("{0}{1}{2}", l.CNPJBase, l.CNPJOrdem, l.CNPJDV) });
            }

            dataGrid.ItemsSource = linha;

            var t = Task.Run(() =>
            {

                foreach (var l in linha)
                {
                    var rws = wS.ConsultarCPNJ(l.CNPJ);

                    Input.CNPJ = rws.Cnpj;
                    Input.Tipo = rws.Tipo;
                    Input.Data_Abertura = Convert.ToDateTime(rws.Data_Abertura);
                    Input.Nome_Empresarial = rws.Nome_Empresarial;
                    Input.Nome_Fantasia = rws.Nome_Fantasia;
                    Input.Porte = rws.Porte;
                    
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

                    Input.Natureza_Juridica = rws.Natureza_Juridica;
                    Input.CEP = rws.Cep;
                    Input.Logradouro = rws.Logradouro;
                    Input.Numero = rws.Numero;
                    Input.Complemento = rws.Complemento;
                    Input.Bairro = rws.Bairro;
                    Input.Municipio = rws.Municipio;
                    Input.UF = rws.Uf;
                    Input.Email = rws.Email;
                    Input.Ente_Federativo_Resp = rws.Ente_Federativo_Resp;
                    Input.Situacao_Cadastral = rws.Situacao_Cadastral;
                    Input.Data_Situacao_Cadastral = Convert.ToDateTime(rws.Data_Situacao_Cadastral);
                    Input.Motivo_Situacao_Cadastral = rws.Motivo_Situacao_Cadastral;
                    Input.Situacao_Especial = rws.Situacao_Especial;
                    Input.Data_Situacao_Especial = rws.Data_Situacao_Especial;
                    Input.Capital_Social = rws.Capital_Social;

                    var qsa = new List<QSA>();

                    foreach (var at in rws.Qsa)
                    {
                        qsa.Add(new QSA() { Nome = at.Nome, NomeRepLegal = at.NomeRepLegal, PaisOrigem = at.PaisOrigem, Qual = at.Qual, QualRepLegal = at.QualRepLegal });
                    }

                    Input.QSA = qsa;

                    _empresa.Empresa.Add(Input);
                    _empresa.SaveChanges();

                    System.Threading.Thread.Sleep(21000);

                    dataGrid_Destino.ItemsSource = _empresa.Empresa.ToList();
                }


            });
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //var pessoa_result = new _DS_Sim_AppTableAdapters.PessoaTableAdapter();

            //dataGrid_Destino.ItemsSource = pessoa_result.GetData();
            //labelrotulo2.Content = pessoa_result.GetData().Count();
            dataGrid_Destino.ItemsSource = _empresa.Empresa.ToList();
        }

        public string CPF(string cpf)
        {
            return Convert.ToUInt64(cpf).ToString(@"000\.000\.000\-00");
        }

        public string CEP(string cep)
        {
            if (cep != string.Empty)
            {
                return Convert.ToUInt64(cep).ToString(@"00000\-000");
            }
            return cep;
        }

        public string Tel(string tel)
        {
            var res = tel;

            if (tel != string.Empty)
            {
                res = tel;
            }

            try
            {
                if (res.Length == 12)
                    return Convert.ToUInt64(res).ToString(@"(00) 00000\-0000");
                else
                    return tel;


            }
            catch
            {
                return tel;
            }

        }

        public string Remove(string valor)
        {
            try
            {
                var str = valor;
                str = new string((from c in str
                                  where char.IsWhiteSpace(c) || char.IsLetterOrDigit(c)
                                  select c
                       ).ToArray());

                return str;
            }
            catch
            {
                return valor;
            }
        }

        public string ToTitleCase(string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;
            else
                return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
        }

    }
}
