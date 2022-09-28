using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using PAeroporto;

namespace PAeroporto
{
    internal class PassagemVoo
    {
        public int IdVoo { get; set; }
        public DateTime DataUltimaOperacao { get; set; }
        public float Valor { get; set; } //maximo 9.999,99
        public char Situacao { get; set; }
        public PassagemVoo()
        {}

        public static void Buscar()
        {
            string passagem = Utils.ColetarString("Informe a identificação da passagem (ex: PA0000): ");
            string sql = "";

        }

        public void EditarPassagemVoo()
        {
            int op;

            do
            {
                Console.Write("Escolha o item que você deseja editar: ");
                Console.Write("0 - Sair");
                Console.Write("1 - Valor");
                Console.Write("2 - Situação");
                op = int.Parse(Console.ReadLine());

                if (op != 1 && op != 2 && op != 0)
                {
                    Console.WriteLine("Opção inválida!");
                }

            } while (op != 1 && op != 2 && op != 0);

            switch (op)
            {
                case 0:
                    Console.WriteLine("SAINDO...");
                    break;

                case 1:
                    Console.Write("Informe o NOVO valor da passagem: ");
                    Valor = float.Parse(Console.ReadLine());
                    if (Valor > 9999.99 || Valor < 0)
                    {
                        Console.WriteLine("Valor de Passagem excedeu o limite!");
                        break;
                    }
                    else
                    {
                        Valor = Valor;
                        Console.WriteLine("Novo valor gerado com sucesso!");
                    }
                    break;

                case 2:
                    Console.Write("Informe A NOVA Situação: ");
                    char situacao = char.Parse(Console.ReadLine());
                    Situacao = situacao;
                    Console.WriteLine("Passagem editada com sucesso!");
                    break;

                default:
                    Console.WriteLine("Opção Inválida!");
                    break;
            }
        }

    }
}
