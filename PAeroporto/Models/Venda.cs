using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Venda
    {
        public int idVenda { get; set; }
        public DateTime DataVenda { get; set; }
        public Passageiro Passageiro { get; set; }
        public float ValorTotal { get; set; }
        public ItemVenda Item { get; set; }
    }
}
