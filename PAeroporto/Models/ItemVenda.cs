using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    internal class ItemVenda
    {
        public int IdVenda { get; set; }
        public int IdItem { get; set; }
        public int IdPassagem { get; set; }

        public override string ToString()
        {
            return $"Id Venda: {IdVenda:0000}\tId Item: {IdItem:0000}\tId Passagem: PA{IdPassagem:00000}".ToString();
        }
    }
}
