using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using PAeroporto;

namespace PAeroporto
{
    internal class CompanhiaAerea
    {
        public string CNPJ { get; set; }
        public string RazaoSocial { get; set; }
        public DateTime DataAbertura { get; set; }
        public DateTime UltimoVoo { get; set; }
        public DateTime DataCadastro { get; set; }
        public char SituacaoCA { get; set; }

        public CompanhiaAerea()
        {
            UltimoVoo = DateTime.Now;
            DataCadastro = DateTime.Now;
            SituacaoCA = 'A';
        }

        public void CadastrarCompainhaAerea()
        {
            Db_Aeroporto db = new Db_Aeroporto();
            Console.Clear();
            Console.WriteLine("### CADASTRO DE COMPANHIA AEREA ###");
            do
            {
                CNPJ = Utils.ColetarString("Informe o CNPJ da empresa que deseja se efetur o cadastro: ");
                if (!Utils.ValidarCnpj(CNPJ)) Console.WriteLine("CNPJ inválido");
                else break;
            } while (true);
            if (db.VerificarDados($"SELECT cnpj FROM dbo.bloqueados WHERE cnpj = '{CNPJ}';"))
            {
                Console.WriteLine("O CNPJ da sua empresa se encontra em nossa lista de CNPJ bloqueados\nContate a administração\nOperação cancelada");
                return;
            }
            DataAbertura = Utils.ColetarData("Informe a data de abertura da empresa: ");
            TimeSpan result = DateTime.Now - DataAbertura;
            if (result.Days / 30 < 6)
            {
                Console.WriteLine("EMPRESAS COM MENOS DE 6 MESES DE EXISTÊNCIA NÃO PODEM SER CADASTRADAS");
                return;
            }
            do
            {
                RazaoSocial = Utils.ColetarString("Informe a razão social da empresa: ");
                if (RazaoSocial.Length > 50) Console.WriteLine("O nome deve possuir 50 caracteres ou menos!!!");
                else break;
            } while (true);

            string sql = "INSERT INTO dbo.companhiaAerea (cnpj, razaoSocial, dataAbertura, dataCadastro, ultimoVoo, situacao)" +
                $"VALUES ('{this.CNPJ}','{this.RazaoSocial}', '{this.DataAbertura}','{this.DataCadastro}','{this.UltimoVoo}','{this.SituacaoCA}')";
            if (!db.InsertTable(sql)) Console.WriteLine("Erro na solicitação");
            else Console.WriteLine("Cadastro efetuado com sucesso!!!");
        }

        public static void Editar()
        {
            string cnpj; do
            {
                cnpj = Utils.ColetarString("Informe o CNPJ da Empresa a ser alterado ou digite 0 para Sair: ");
                if (cnpj == "0") return;
                else if (!Utils.ValidarCnpj(cnpj)) Console.WriteLine("CNPJ inválido");
                else break;
            } while (true);
            do
            {
                Console.Clear();
                Console.WriteLine("### EDITAR COMPANHIA AÉREA ###");
                Console.WriteLine("(0 - Sair)\n(1 - Editar Razão Social)\n(2 - Inativar Cadastro)\n(3 - Ativar Cadastro)");
                int op = Utils.ColetarValorInt("Informe opção: ");

                Db_Aeroporto db = new Db_Aeroporto();
                switch (op)
                {
                    case 0:
                        return;
                    case 1:
                        string razaoSocial = Utils.ColetarString("Informe a nova Razão Social: ");
                        if (!db.UpdateTable($"UPDATE dbo.companhiaAerea set razaoSocial = '{razaoSocial}' WHERE cnpj = '{cnpj}'")) Console.WriteLine("Erro na solicitação");
                        else Console.WriteLine("Solicitação efetuada com sucesso!!!");
                        break;
                    case 2:
                        InativarCadastro(cnpj, db);
                        break;
                    case 3:
                        AtivarCadastro(cnpj, db);
                        break;
                    default:
                        Console.WriteLine("Opção inválida");
                        break;
                }
                Utils.Pause();
            } while (true);
        }

        static void InativarCadastro(string cnpj, Db_Aeroporto db)
        {
            int confirmacao;
            do
            {
                confirmacao = Utils.ColetarValorInt("Confirmar inativação (1 - Sim) (2 - Não): ");
                if (confirmacao != 2 && confirmacao != 1) Console.WriteLine("Opção inválida!!!");
                else break;
            } while (true);
            if (confirmacao == 2) return;
            else
            {
                if (!db.UpdateTable($"UPDATE dbo.companhiaAerea SET situacao = 'I' WHERE cnpj = '{cnpj}'")) Console.WriteLine("Erro na solicitação");
                else Console.WriteLine("Solicitação efetuada com sucesso!!!");
            }
        }

        public static void LocalizarCompanhiaAerea()
        {
            string cnpj;
            do
            {
                cnpj = Utils.ColetarString("Informe o CNPJ para busca: ");
                if (!Utils.ValidarCnpj(cnpj)) Console.WriteLine("CNPJ Inválido!!!");
                else break;
            } while (true);
            Db_Aeroporto db = new Db_Aeroporto();
            string sql = $"SELECT cnpj, razaoSocial, dataAbertura, dataCadastro, ultimoVoo,situacao FROM dbo.companhiaAerea WHERE cnpj = '{cnpj}'";
            db.SelectTableCA(sql);
        }

        public static void ListarCompanhias(char situacao)
        {
            Db_Aeroporto db = new Db_Aeroporto();
            string sql = $"SELECT cnpj, razaoSocial,dataAbertura, dataCadastro,ultimoVoo,situacao FROM dbo.companhiaAerea WHERE situacao = '{situacao}';";
            db.SelectTableCA(sql);
        }

        public static void AtivarCadastro(string cnpj, Db_Aeroporto db)
        {
            int confirmacao;
            do
            {
                confirmacao = Utils.ColetarValorInt("Confirmar Ativação (1 - Sim) (2 - Não): ");
                if (confirmacao != 2 && confirmacao != 1) Console.WriteLine("Opção inválida!!!");
                else break;
            } while (true);
            if (confirmacao == 2) return;
            else
            {
                if (!db.UpdateTable($"UPDATE dbo.companhiaAerea SET situacao = 'A' WHERE cnpj = '{cnpj}'")) Console.WriteLine("Erro na solicitação");
                else Console.WriteLine("Solicitação efetuada com sucesso!!!");
            }
        }

        public override string ToString()
        {
            return "\nCNPJ: " + CNPJ + "\nRazão Social: " + RazaoSocial + "\nData de Abertura: " + DataAbertura + "\nÚltimo Voo: " + UltimoVoo + "\nData de Cadastro: " + DataCadastro + "\nSituação: " + SituacaoCA;
        }


    }
}
