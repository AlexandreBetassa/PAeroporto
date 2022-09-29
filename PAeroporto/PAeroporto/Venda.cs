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
        public int idVenda { get; set; }
        public DateTime DataVenda { get; set; }
        public String CpfPassageiro { get; set; }
        public float ValorTotal { get; set; }


        public Venda()
        {
            DataVenda = DateTime.Now;
        }

        public void CadastrarVenda()
        {
            int count = 0;
            ValorTotal = 0;
            string cpf;
            Db_Aeroporto db = new Db_Aeroporto();
            Console.Clear();
            Console.WriteLine("### CADASTRO DE VENDAS ###");
            do
            {
                cpf = Utils.ColetarString("Informe o CPF para cadastro do passageiro: ");
                if (!Utils.ValidarCpf(cpf)) Console.WriteLine("CPF inválido");
                else if (!db.VerificarDados($"SELECT cpf FROM dbo. restritos WHERE cpf = '{cpf}'"))
                {
                    Console.WriteLine("Não é possível efetuar vendas para este CPF pois está na lista de restritos");
                    return;
                }
                else if (!db.VerificarDados($"SELECT cpf FROM dbo.passageiro WHERE cpf = '{cpf}'")) Console.WriteLine("CPF não localizado");
                else break;
            } while (true);
            CpfPassageiro = cpf;
            idVenda = db.getValorInt($"SELECT ISNULL(MAX(id),1) FROM dbo.venda");

            db.InsertTable($"INSERT INTO dbo. venda (dataVenda, passageiroCpf) VALUES('{this.DataVenda}', '{this.CpfPassageiro}')");
            Console.Clear();
            do
            {
                if (count == 4)
                {
                    Console.WriteLine("Você atingiu o limite de itens por venda");
                    break;
                }
                Console.WriteLine("### VOOS ATIVOS ###");
                Voo.Buscar('A');
                int idVoo;
                do
                {
                    string numeroVoo = Utils.ColetarString("Informe a identificação do voo EX: (V0000) ou digite 0 para Sair: ").PadRight(5, '0');
                    if (numeroVoo == "0") return;
                    if (!int.TryParse(numeroVoo.Substring(1, 4), out idVoo)) Console.WriteLine("A identificação do voo foi digitada incorretamente");
                    else if (!db.VerificarDados($"SELECT idVoo FROM dbo.voo WHERE idVoo = {idVoo} AND situacao = 'A'")) Console.WriteLine("Voo não localizado");
                    else break;
                } while (true);
                int idPassagem = db.getValorInt($"SELECT MAX(idPassagem) FROM passagem WHERE situacao = 'L' and idVoo = {idVoo}");
                float valor = db.getValorFloat($"SELECT valor FROM dbo.passagem WHERE idPassagem = {idPassagem} AND idVoo = {idVoo} and situacao = 'L'");


                ValorTotal += valor;
                db.InsertTable($"INSERT INTO itemVenda (idVenda, idPassagem) VALUES ({this.idVenda}, {idPassagem})");
                db.UpdateTable($"UPDATE dbo.passagem SET situacao = 'R' WHERE idPassagem = {idPassagem} AND idVoo = {idVoo}");
                int opc;
                do
                {
                    opc = Utils.ColetarValorInt("Deseja comprar outra passagem\n(1 - Sim)\n(2 - Não): ");
                    if (opc != 1 && opc != 2) Console.WriteLine("Opção inválida");
                    else break;
                } while (true);
                if (opc == 2) break;
                count++;
            } while (true);
            db.UpdateTable($"UPDATE dbo.venda SET valorTotal = {ValorTotal} WHERE id = {idVenda}");
        }
    }
}