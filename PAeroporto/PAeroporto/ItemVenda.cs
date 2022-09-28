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

        //public void CadastrarItemVenda(List<PassagemVoo> listaPassagensVoos, String idItemVenda)
        //{
        //    Console.WriteLine(">>>CADASTRO DE VENDA DE ITEM<<<");

        //    IdItemVenda = idItemVenda;

        //    //Lista de Passagens
        //    Console.WriteLine("Lista de Passagens:");
        //    foreach (PassagemVoo item in listaPassagensVoos)
        //    {
        //        Console.WriteLine(item.ToString());
        //    }

        //    Console.Write("Informe qual ID da Passagem pertence a venda deste Item: ");
        //    string iditemvenda = Console.ReadLine();

        //    foreach (PassagemVoo item in listaPassagensVoos)
        //    {
        //        if (item.IdPassagem == iditemvenda)
        //            this.PassagemVoo = item.IdPassagem;
        //    }
        //}

        public override string ToString()
        {
            return $"Id Venda: {IdItemVenda}\nId Passagem: {PassagemVoo}\nValor Unitário: R${ValorUnit}".ToString();
        }

    }
}
