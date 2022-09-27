using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PAeroporto;

namespace PAeroporto
{
    internal class Passageiro
    {
        public String CPF { get; set; } //prop CHAVE com 11 dígitos
        public String Nome { get; set; } // < 50 digitos
        public DateTime DataNascimento { get; set; }
        public Char Sexo { get; set; } //M F N
        public DateTime UltimaCompra { get; set; } //no cadastro, data atual
        public DateTime DataCadastro { get; set; }
        public Char Situacao { get; set; } //A - Ativo I - Inativo

        public Passageiro()
        {
            UltimaCompra = DateTime.Now;
            DataCadastro = DateTime.Now;
            Situacao = 'A';
        }
        public void CadastrarPassageiro()
        {

            Console.WriteLine(">>>CADASTRO DE PASSAGEIRO<<<");

            do
            {
                CPF = Utils.ColetarString("Informe o número do CPF da pessoa a ser cadastrada: ");
                if (!Utils.ValidarCpf(CPF)) Console.WriteLine("CPF Inválido");
                else break;
                Utils.Pause();
            } while (true);

            do
            {
                Nome = Utils.ColetarString("Informe o seu nome (Máximo 50 digítos): ");
                if (Nome.Length > 50) Console.WriteLine("\nO nome deve possuir no máximo 50 caracteres!!!");
                else break;
                Utils.Pause();
            } while (true);

            DataNascimento = Utils.ColetarData("Informe sua Data de Nascimento: ");
            Sexo = Utils.ColetarValorChar("Informe seu genero: (M - Masculino, F - Feminino, N - Não desejo informar): ");

            Db_Aeroporto db = new Db_Aeroporto();
            string sql = "INSERT INTO dbo.passageiro (cpf, nome, dataNasc, sexo, ultimaCompra, dataCad, situacao)" +
                $"VALUES ('{this.CPF}','{this.Nome}','{this.DataNascimento}','{this.Sexo}','{this.UltimaCompra}','{this.DataCadastro}','{this.Situacao}')";
            db.InsertTable(sql);

        }
        public static void Editar()
        {
            string cpf = Utils.ColetarString("Informe CPF: ");
            Db_Aeroporto db = new Db_Aeroporto();
            string sql = $"SELECT cpf, nome, dataNasc, sexo, ultimaCompra, dataCad, situacao from dbo.passageiro where cpf = '{cpf}'";
            if (!db.SelectTable(sql)) return;
            Console.WriteLine("Escolha entre as opções, o/os dados que deseja editar em seu cadastro: ");
            Console.WriteLine("1 - Editar NOME cadastrado");
            Console.WriteLine("2 - Editar SEXO cadastrado");
            Console.WriteLine("3 - Inativar CADASTRO");
            int op = Utils.ColetarValorInt("Informe opção que deseja editar: ");
            switch (op)
            {
                case 1:
                    string nome = Utils.ColetarString("Informe o novo nome: ");
                    sql = $"UPDATE dbo.passageiro SET nome = '{nome}' WHERE cpf = {cpf}";
                    if (!db.UpdateTable(sql)) Console.WriteLine("Erro na solicitação");
                    else Console.WriteLine("Solicitação efetuada com sucesso!!!");
                    break;
                case 2:
                    char sexo = Utils.ColetarValorChar("Informe o gênero correto (M- Masculino, F - Feminino, N - Não desejo informar): ");
                    sql = $"UPDATE dbo.passageiro SET sexo = '{sexo}' WHERE cpf = {cpf}";
                    if (!db.UpdateTable(sql)) Console.WriteLine("Erro na solicitação");
                    else Console.WriteLine("Solicitação efetuada com sucesso!!!");
                    break;
                case 3:
                    InativarCadastro(db, cpf);
                    break;
                default:
                    Console.WriteLine("Operação inválida");
                    break;
            }
            Utils.Pause();
        }
        public static void InativarCadastro(Db_Aeroporto db, string cpf)
        {
            int confirmar;
            do
            {
                confirmar = Utils.ColetarValorInt("Confirmar inativação do passageiro\n(1 - Sim)\n(2 - Não)\nInforme Opção: ");
                if (confirmar != 1 && confirmar != 2) Console.WriteLine("Opção inválida");
                else break;
            } while (confirmar != 1 && confirmar != 2);
            if (confirmar == 2) return;
            string sql = $"UPDATE dbo.passageiro set situacao = 'I' WHERE cpf = {cpf}";
            if (!db.UpdateTable(sql)) Console.WriteLine("Erro na solicitação");
            else Console.WriteLine("Solicitação efetuada com sucesso!!!");
        }
        public static string Localizar()
        {
            Console.Clear();
            string cpf;
            Console.WriteLine("### LOCALIZAR PASSAGEIRO ###");
            do cpf = Utils.ColetarString("Informe o CPF do Passageiro para busca: ");
            while (!Utils.ValidarCpf(cpf));
            string sql = $"SELECT cpf, nome, dataNasc, sexo, ultimaCompra, dataCad, situacao from dbo.passageiro where cpf = '{cpf}'";
            Db_Aeroporto db = new Db_Aeroporto();
            db.SelectTable(sql);
            Utils.Pause();
            return cpf;
        }
        public static void Listar(char situacao)
        {
            string sql = $"SELECT cpf, nome, dataNasc, sexo, ultimaCompra, dataCad, situacao from dbo.passageiro WHERE situacao = '{situacao}'";
            Db_Aeroporto db = new Db_Aeroporto();
            db.SelectTable(sql);
            Utils.Pause();
        }

        public override string ToString()
        {
            return ($"CPF: {CPF}\nNOME: {Nome}\nDATA DE NASCIMENTO: {DataNascimento}\nSEXO: {Sexo}\nÚLTIMA COMPRA: {UltimaCompra}\nDATA EM QUE O CADASTRO FOI REALIZADO: {DataCadastro}\nSITUAÇÃO DO CADASTRO (A - ATIVO, I - INATIVO): {Situacao}").ToString();
        }
    }
}


