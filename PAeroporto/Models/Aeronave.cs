using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Aeronave
    {
        public String Inscricao { get; set; }
        public int Capacidade { get; set; } 
        public DateTime UltimaVenda { get; set; } 
        public DateTime DataCadastro { get; set; } 
        public char Situacao { get; set; }
        public String Companhia { get; set; }
    }
}
