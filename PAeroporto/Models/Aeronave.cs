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
        public int Capacidade { get; set; } //3 digitos numericos
        public int AssentosOcupados { get; set; } //3 digitos numericos
        public DateTime UltimaVenda { get; set; } //no cad, dt atual
        public DateTime DataCadastro { get; set; } //dt atual
        public char Situacao { get; set; }
        public CompAerea Companhia { get; set; }
    }
}
