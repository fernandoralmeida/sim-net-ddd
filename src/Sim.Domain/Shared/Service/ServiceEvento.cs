using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Domain.Shared.Service
{
    using Entity;
    using Domain.Service;
    using Interface;
    public class ServiceEvento : ServiceBase<Evento>, IServiceEvento
    {
        private readonly IRepositoryEvento _evento;
        public ServiceEvento(IRepositoryEvento repositoryEvento)
            :base(repositoryEvento)
        {
            _evento = repositoryEvento;
        }

        public async Task<IEnumerable<Evento>> EventosAtivos(IEnumerable<Evento> eventos)
        {
            var t = Task.Run(() => eventos.Where(s => s.EventosAtivos(s)).OrderBy(o => o.Data));

            await t;

            return t.Result;
        }

        public async Task<IEnumerable<Evento>> EventosPassados(IEnumerable<Evento> eventos)
        {
            var t = Task.Run(() => eventos.Where(s => s.EventosPassados(s)).OrderByDescending(o => o.Data));

            await t;

            return t.Result;
        }

        public Evento GetByCodigo(int codigo)
        {
            return _evento.GetByCodigo(codigo);
        }

        public Evento GetByCodigo_Participantes(int codigo)
        {
            return _evento.GetByCodigo_Participantes(codigo);
        }

        public IEnumerable<Evento> GetByNome(string nome)
        {
            return _evento.GetByNome(nome);
        }

        public IEnumerable<Evento> GetByOwner(string setor)
        {
            return _evento.GetByOwner(setor);
        }

        public int LastCodigo()
        {
            return _evento.LastCodigo();
        }

        public IEnumerable<Evento> ListAll()
        {
            return _evento.ListAll();
        }

        /// <summary>
        /// Lista eventos classificados por mês
        /// </summary>
        /// <param name="eventos">Lista de eventos</param>
        /// <returns>Lista de eventos por mês</returns>
        public async Task<IEnumerable<(string Mes, int Qtde, IEnumerable<Evento>)>> ListarEventosPorMes(IEnumerable<Evento> eventos)
        {
            try
            {
                var t = Task.Run(() =>
                {
                    var _lista_meses = new List<(string Mes, int Qtde, IEnumerable<Evento>)>();
                    var _eventos = new List<Evento>();
                    var _meses = new List<(string Mes, int Qtde)>();
                    var _mes = new List<string>();

                    for (int i = 1; i < 13; i++)
                    {
                        _eventos = eventos.Where(s => s.Data.Value.Month == i).ToList();
                        if(_eventos.Any())
                            _lista_meses.Add((_eventos.FirstOrDefault().Data.Value.ToString("MMM"), _eventos.Count, _eventos));
                    }

                    return _lista_meses;
                });

                await t;

                return t.Result;

            }
            catch
            {
                return null;
            }
        }
    }
}
