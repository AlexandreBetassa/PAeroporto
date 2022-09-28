using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Console.WriteLine("### VOOS ATIVOS ###");
            Voo.Buscar('A');
            int id;
            do
            {
                string numeroVoo = Utils.ColetarString("Informe a identificação do voo EX: (V0000) ou digite 0 para Sair: ").PadRight(5, '0');
                if (numeroVoo == "0") return;
                if (!int.TryParse(numeroVoo.Substring(1, 4), out id)) Console.WriteLine("A identificação do voo foi digitada incorretamente");
                else if (!db.VerificarDados($"SELECT idVoo FROM dbo.voo WHERE situacao = 'A' and idVoo = {id}")) Console.WriteLine("Voo não localizado");
                else break;
            } while (true);
            IdVoo = id;
            Valor = Utils.ColetarValorFloat("Informe o valor das passagens: ");
            int qtd = db.getIntTable($"SELECT capacidade FROM dbo.aeronave, dbo.voo WHERE aeronave.inscAeronave = voo.aeronave and voo.idVoo = {IdVoo}");
            db.conn.Open();
            for (int i = 1; i <= qtd; i++) db.InsertTablePassagem($"INSERT INTO dbo.passagem (idVoo, valor, situacao) VALUES ({this.IdVoo}, {this.Valor}, '{this.Situacao}')");
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
