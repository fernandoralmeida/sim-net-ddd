using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.SqlClient;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Diagnostics;

namespace Sim.UI.Desktop
{

    using Sim.Domain.Cnpj.Entity;
    using Sim.Service.CNPJ.Entity;
    using Sim.Cross.Data.Repository.Cnpj;


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            WindowReceitaWS w = new WindowReceitaWS();
            w.Show();
        }

        private string Select_CNAE
        {
            get { return @"SELECT [Coluna 0]
,[Coluna 1]
FROM [dbo].[Cnae]"; }
        }

        private string Select_All
        {
            get
            {
                var sql = @"SELECT        a.Campo1, a.Campo2, a.Campo3, a.Campo4, a.Campo5, a.Campo6, a.Campo7, a.Campo8, a.Campo9, a.Campo10, a.Campo11, a.Campo12, a.Campo13, a.Campo14, a.Campo15, a.Campo16, a.Campo17, a.Campo18, a.Campo19, 
                         a.Campo20, a.Campo21, a.Campo22, a.Campo23, a.Campo24, a.Campo25, a.Campo26, a.Campo27, a.Campo28, a.Campo29, a.Campo30, cn.[Coluna 1] AS AtivPrim,  
                         b.Campo1 AS Expr8, b.Campo2 AS Expr9, b.Campo3 AS Expr10, b.Campo4 AS Expr11, b.Campo5 AS Expr12, b.Campo6 AS Expr13, b.Campo7 AS Expr14, sc.[Coluna 1] as MotivoSc, mn.[Coluna 1] AS Munic,
						 nj.[Coluna 1] as NatJur, qr.[Coluna 1] as QuaResp

FROM            Estabelecimentos AS a INNER JOIN
                         Empresas AS b ON b.Campo1 = a.Campo1 INNER JOIN
                         Cnae AS cn ON cn.[Coluna 0] = a.Campo12 INNER JOIN
						 MotivoSC AS sc ON sc.[Coluna 0] = a.Campo8 INNER JOIN
						 Municipios AS mn ON mn.[Coluna 0] = a.Campo21 INNER JOIN
						 NaturezaJuridica AS nj ON nj.[Coluna 0] = b.Campo3 INNER JOIN
						 TipoQsa AS qr ON qr.[Coluna 0] = b.Campo4 

WHERE        (a.Campo21 = '6607')";

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
                var lista = new List<CNAE>();

                foreach(DataRow dr in ShowData().Rows)
                {
                    cont++;
                    lista.Add(new CNAE(cont, (string)dr[0], (string)dr[30]));
                }

                dataGrid.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, new Action(delegate() {
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
            /*
            var pessoa = new _DS_Sim_Data_03TableAdapters.SDT_SE_PFTableAdapter();
            var pessoaSQL = new _DS_Sim_AppTableAdapters.PessoaTableAdapter();
            var t = Task.Factory.StartNew(() =>
            {

                var genero = string.Empty;

                foreach (var s in pessoa.GetData())
                {
                    if (s.Ativo == true)
                    {

                        if (s.Sexo == 1)
                            genero = "Feminino";
                        else
                            genero = "Masculino";

                        pessoaSQL.InsertQuery(Guid.NewGuid(),
                            ToTitleCase(s.Nome),
                            null,
                            s.Nascimento,
                            CPF(s.CPF),
                            s.RG,
                            null,
                            null,
                            genero,
                            null,
                            CEP(s.CEP),
                            ToTitleCase(s.Logradouro),
                            s.Numero,
                            ToTitleCase(s.Complemento),
                            ToTitleCase(s.Bairro),
                            ToTitleCase(s.Municipio),
                            s.UF,
                            Tel(s.Telefones),
                            string.Empty,
                            s.Email.ToLower(),
                            s.Cadastro,
                            s.Atualizado,
                            s.Ativo);
                    }
                }
            });
            t.Wait();
            var pessoa_result = new _DS_Sim_AppTableAdapters.PessoaTableAdapter();

            dataGrid_Destino.ItemsSource = pessoa_result.GetData();
            labelrotulo2.Content = pessoa_result.GetData().Count();
            */
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //var pessoa_result = new _DS_Sim_AppTableAdapters.PessoaTableAdapter();

            //dataGrid_Destino.ItemsSource = pessoa_result.GetData();
            //labelrotulo2.Content = pessoa_result.GetData().Count();
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
