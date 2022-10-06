using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PAeroporto;

namespace PAeroporto
{
    internal class VendaView
    {
        public int idVenda { get; set; }
        public DateTime DataVenda { get; set; }
        public String CpfPassageiro { get; set; }
        public float ValorTotal { get; set; }


        public VendaView()
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
                else if (db.VerificarDados($"SELECT cpf FROM dbo. restritos WHERE cpf = '{cpf}'"))
                {
                    Console.WriteLine("Não é possível efetuar vendas para este CPF pois está na lista de restritos");
                    return;
                }
                else if (!db.getValorDateTime($"SELECT dataNasc FROM dbo.passageiro WHERE cpf = '{cpf}'"))
                {
                    Console.WriteLine("Não é possivel venda de passagens para menores de 18 anos");
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
                VooView.Buscar('A');
                int idVoo;
                do
                {
                    string numeroVoo = Utils.ColetarString("Informe a identificação do voo EX: (V0000) ou digite 0 para Sair: ").PadRight(5, '0');
                    if (numeroVoo == "0") return;
                    if (!int.TryParse(numeroVoo.Substring(1, 4), out idVoo)) Console.WriteLine("A identificação do voo foi digitada incorretamente");
                    else if (!db.VerificarDados($"SELECT idVoo FROM dbo.voo WHERE idVoo = {idVoo} AND situacao = 'A'")) Console.WriteLine("Voo não localizado");
                    else break;
                } while (true);
                int idPassagem = db.getValorInt($"SELECT MIN(idPassagem) FROM passagem WHERE situacao = 'L' and idVoo = {idVoo}");
                float valor = db.getValorFloat($"SELECT valor FROM dbo.passagem WHERE idPassagem = {idPassagem} AND idVoo = {idVoo} and situacao = 'L'");

                string sql = ($"SELECT passagem.idPassagem, passagem.idVoo, voo.aeronave, voo.dataVoo, passagem.dataCadastro, passagem.valor, " +
                    $"passagem.situacao FROM dbo.passagem, dbo.voo " +
                    $"WHERE passagem.idPassagem = {idPassagem} AND passagem.idVoo = {idVoo} AND voo.IdVoo = passagem.idVoo ");
                db.SelectPassagem(sql);


                int confirmar = Utils.ColetarValorInt("Deseja confirmar a seleção da passagem? (1 - Sim) (2 - Não)");
                if (confirmar == 1)
                {
                    ValorTotal += valor;
                    db.InsertTable($"INSERT INTO itemVenda (idVenda, idPassagem) VALUES ({this.idVenda}, {idPassagem})");
                    db.UpdateTable($"UPDATE dbo.passagem SET situacao = 'R' WHERE idPassagem = {idPassagem} AND idVoo = {idVoo}");
                    count++;
                }

                Console.WriteLine("VALOR TOTAL ATÉ AGORA: R$" + ValorTotal.ToString("F"));
                int opc;
                do
                {
                    opc = Utils.ColetarValorInt("Deseja comprar outra passagem\n(1 - Sim)\n(2 - Não): ");
                    if (opc != 1 && opc != 2) Console.WriteLine("Opção inválida");
                    else break;
                } while (true);
                if (opc == 2) break;
            } while (true);
            db.UpdateTable($"UPDATE dbo.venda SET valorTotal = {ValorTotal} WHERE id = {idVenda}");

            Console.Clear();
            Console.WriteLine("### VALOR TOTAL DA TRANSAÇÃO ###");
            db.SelectTableVenda($"SELECT venda.id, venda.dataVenda, passageiro.nome, passageiro.dataNasc, venda.valorTotal" +
                $" FROM passageiro, venda WHERE venda.id = {idVenda}");

        }

        public static void Buscar()
        {
            Db_Aeroporto db = new Db_Aeroporto();
            int venda = Utils.ColetarValorInt("Informe o número da venda realizada: ");
            if (!db.SelectTableVenda($"SELECT venda.id, venda.dataVenda, passageiro.nome, passageiro.dataNasc, venda.valorTotal" +
                $" FROM passageiro, venda WHERE venda.id = {venda}")) Console.WriteLine("Dados não localizados");
        }

        public static void EditarPassagem()
        {
            Db_Aeroporto db = new Db_Aeroporto();
            int idVoo, idPassagem;
            do
            {
                string AuxIdVoo = Utils.ColetarString("Informe a identificação do voo ou 0 para sair: ");
                if (AuxIdVoo == "0") return;
                else if (!int.TryParse(AuxIdVoo.Substring(1, 4), out idVoo)) Console.WriteLine("A identificação do voo foi digitada incorretamente");
                else break;
            } while (true);
            do
            {
                string auxIdPassagem = Utils.ColetarString("Informe a identificação da passagem ou 0 para sair: ");
                if (auxIdPassagem == "0") return;
                else if (!int.TryParse(auxIdPassagem.Substring(2, 5), out idPassagem)) Console.WriteLine("A identificação da passagem foi digitada incorretamente");
                else break;
            } while (true);

            if (!db.VerificarDados($"SELECT idPassagem, idVoo FROM dbo.passagem WHERE idVoo = {idVoo} AND idPassagem = {idPassagem};"))
            {
                Console.WriteLine("Passage, não localizada!!!");
                return;
            }

            do
            {
                int op = Utils.ColetarValorInt("Deseja cancelar a reserva da passagem ou confirmar? (0 - Retornar ao Menu) (1 - Confirmar) (2 - Cancelar): ");
                switch (op)
                {
                    case 0:
                        return;
                    case 1:
                        if (!db.UpdateTable($"UPDATE dbo.passagem SET situacao = 'C' WHERE idVoo = {idVoo} AND idPassagem = {idPassagem}")) Console.WriteLine("Ocorreu um erro na solicitação");
                        else Console.WriteLine("Operação efetuada com sucesso");
                        return;
                    case 2:
                        if (!db.UpdateTable($"UPDATE dbo.passagem SET situacao = 'L' WHERE idVoo = {idVoo} AND idPassagem = {idPassagem}")) Console.WriteLine("Ocorreu um erro na solicitação");
                        else Console.WriteLine("Operação efetuada com sucesso");
                        return;
                    default:
                        Console.WriteLine("Operação inválida");
                        break;
                }

            } while (true);
        }


        public static void ConsultarPassagem()
        {
            Console.Clear();
            Console.WriteLine("### CONSULTA DE PASSAGEM ###");
            Db_Aeroporto db = new Db_Aeroporto();
            int idVoo, idPassagem;
            do
            {
                string AuxIdVoo = Utils.ColetarString("Informe a identificação do voo ou 0 para sair: ");
                if (AuxIdVoo == "0") return;
                else if (!int.TryParse(AuxIdVoo.Substring(1, 4), out idVoo)) Console.WriteLine("A identificação do voo foi digitada incorretamente");
                else break;
            } while (true);
            do
            {
                string auxIdPassagem = Utils.ColetarString("Informe a identificação da passagem ou 0 para sair: ");
                if (auxIdPassagem == "0") return;
                else if (!int.TryParse(auxIdPassagem.Substring(2, 5), out idPassagem)) Console.WriteLine("A identificação da passagem foi digitada incorretamente");
                else break;
            } while (true);

            string sql = ($"SELECT passagem.idPassagem, passagem.idVoo, voo.aeronave, voo.dataVoo, passagem.dataCadastro, passagem.valor, " +
    $"passagem.situacao FROM dbo.passagem, dbo.voo " +
    $"WHERE passagem.idPassagem = {idPassagem} AND passagem.idVoo = {idVoo} AND voo.IdVoo = passagem.idVoo ");
            db.SelectPassagem(sql);

        }

    }
}