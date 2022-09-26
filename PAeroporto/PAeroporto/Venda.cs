using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PAeroporto;

namespace PAeroporto
{
    internal class Venda
    {
        public String IdVenda { get; set; }
        public DateTime DataVenda { get; set; }
        public Passageiro Passageiro { get; set; }
        public float ValorTotal { get; set; }


        public Venda()
        {

        }

        public void CadastrarVenda(List<Passageiro> listaPassageiros)
        {
            Console.WriteLine(">>>CADASTRO DE VENDAS<<<");

            //Deverá ser um código que irá identificar a venda.Deverá
            //ser um número sequencial que iniciará em 1 e, através
            //deste sistema poderá ir apenas até a venda 99.999.
            //Sendo assim, será numérico, mas deverá ter no máximo
            //5 dígitos.

            Console.WriteLine("Informe o código de IDENTIFICAÇÃO DA VENDA: ");
            IdVenda = Console.ReadLine();

            //Passageiro Deverá armazenar apenas o CPF do Passageiro, mas ao
            //ser informado, deverá trazer o nome e a data de
            //nascimento do cliente, para uma verificação junto ao
            //mesmo.
            //for each, comparar valor do cpf se for igual ao informado
            //var int id, começando em 1 e cada vez que , incrementar 1

            //Lista de Passageiros
            Console.WriteLine("Lista de Passageiros:");
            foreach (Passageiro item in listaPassageiros)
            {
                if (item.Situacao == 'A')
                    Console.WriteLine(item.ToString());
            }

            Console.Write("Informe qual Passageiro pertence esta Venda: ");
            string cpf = Console.ReadLine();

            foreach (Passageiro item in listaPassageiros)
            {
                if (item.Situacao == 'A')
                {
                    if (item.CPF == cpf)
                        this.Passageiro = item;
                }
            }



        }
    }
}