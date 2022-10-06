using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Voo
    {
        public int IdVoo { get; set; }
        public string Destino { get; set; }
        public DateTime DataVoo { get; set; } // Data 8 dígitos + 4 dígitos da hora
        public DateTime DataCadastro { get; set; }
        public char Situacao { get; set; } //A Ativo ou C Cancelado
        public String InscAeronave { get; set; }
        public int AssentosOcupados { get; set; }
    }
}
