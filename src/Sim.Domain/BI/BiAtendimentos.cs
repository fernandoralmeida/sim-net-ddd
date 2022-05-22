﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Domain.BI
{
    public class BiAtendimentos
    {
        public (string Titulo, int Atendimentos, int Servicos) Cliente { get; set; }
        public (string Titulo, int Atendimentos, int Servicos) ClientePF { get; set; }
        public (string Titulo, int Atendimentos, int Servicos) ClientePJ { get; set; }
        public List<(string Mes, int Atendimentos, int Servicos)> ListaMensal { get; set; }
        public List<(string Nome, int Atendimentos, int Servicos)> ListaAppUser { get; set; }
    }
}
