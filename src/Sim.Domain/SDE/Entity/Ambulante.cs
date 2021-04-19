﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Domain.SDE.Entity
{   
    public class Ambulante
    {
        public Ambulante()
        {

        }
        public Guid Id { get; set; }
        public string Protocolo { get; set; }
        public Guid Titular_Pessoa_ID { get; set; }
        public Guid Auxiliar_Pessoa_ID { get; set; }
        public string FormaAtuacao { get; set; }
        public string Local { get; set; }
        public string Atividade { get; set; }
        public DateTime? Data_Cadastro { get; set; }
        public DateTime? Ultima_Alteracao { get; set; }
        public bool Ativo { get; set; }
        public virtual int Contador { get; set; }

        public virtual ICollection<DIA> DIAs { get; set; }
        public virtual Pessoa Titular { get; set; }
        public virtual Pessoa Auxiliar { get; set; }
    }
}
