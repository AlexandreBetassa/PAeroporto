using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    internal class Venda
    {
        public int idVenda { get; set; }
        public DateTime DataVenda { get; set; }
        public String CpfPassageiro { get; set; }
        public float ValorTotal { get; set; }
    }
}
