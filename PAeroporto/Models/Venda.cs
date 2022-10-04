using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Venda
    {
        public int IdVenda { get; set; }
        public DateTime DataVenda { get; set; }
        public String Passageiro { get; set; }
        public float ValorTotal { get; set; }
    }
}
