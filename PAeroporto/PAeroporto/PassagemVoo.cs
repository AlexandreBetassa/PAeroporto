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
        {
            DataUltimaOperacao = DateTime.Now;
            Situacao = 'L';
        }

        public void CadastrarPassagemVoo()
        {
            Db_Aeroporto db = new Db_Aeroporto();
            //int qtd = db.getIntTable($"SELECT capacidade FROM dbo.aeronave, dbo.voo WHERE aeronave.inscAeronave = voo.aeronave and voo.idVoo = {IdVoo}");
            db.conn.Open();
            Valor = Utils.ColetarValorFloat("Informe o valor das passagens: ");
            SqlCommand sql_cmnd = new SqlCommand($"dbo.CadastroPassagens {this.Valor};", db.conn);
            sql_cmnd.CommandType = CommandType.StoredProcedure;
            sql_cmnd.ExecuteNonQuery();
            db.conn.Close();
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
