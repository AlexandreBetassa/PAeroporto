using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PAeroporto;

namespace PAeroporto
{
    internal class ItemVenda
    {
        public string IdItemVenda { get; set; }
        public String PassagemVoo { get; set; }
        public float ValorUnit { get; set; }

        public ItemVenda()
        { }



        public override string ToString()
        {
            return $"Id Venda: {IdItemVenda}\nId Passagem: {PassagemVoo}\nValor Unitário: R${ValorUnit}".ToString();
        }

    }
}
