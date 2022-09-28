using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using Microsoft.Data.SqlClient;
using PAeroporto;

namespace PAeroporto
{
    internal class Program
    {
        static void Main(string[] args)
        {

            int op;
            do
            {
                op = Menu();
                switch (op)
                {
                    case 1:
                        Console.Clear();
                        MenuPassageiro();
                        break;
                    case 2:
                        Console.Clear();
                        MenuCompanhia();
                        break;
                    case 3:
                        Console.Clear();
                        MenuAeronave();
                        break;
                    case 4:
                        Console.Clear();
                        MenuVoo();
                        break;
                    //case 5:
                    //    Console.Clear();
                    //    MenuPassagem(listaPassagensVoos, listaVoos, listaPassagemVoo);
                    //    break;
                    //case 6:
                    //    Console.Clear();
                    //    MenuVenda(listaVendas, listaPassageiros);
                    //    break;
                    //case 7:
                    //    Console.Clear();
                    //    MenuItemVenda(listaItemVendas, listaPassagensVoos);
                    //    break;
                    case 0:
                        Console.Clear();
                        Console.WriteLine("### SAIR ###");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Opção Inválida! Digite opção válida.");
                        break;
                }
            } while (true);
        }

        #region Menus
        public static int Menu()
        {
            Console.Clear();
            Console.WriteLine("### MENU PASSAGEIRO ###");
            Console.WriteLine("1 - Menu de Passageiros");
            Console.WriteLine("2 - Menu de Companhias Aéreas ");
            Console.WriteLine("3 - Menu de Aeronaves");
            Console.WriteLine("4 - Menu de Voos");
            Console.WriteLine("5 - Menu de Passagens ");
            Console.WriteLine("6 - Menu de Vendas ");
            Console.WriteLine("7 - Menu de Item Vendas ");
            Console.WriteLine("0 - Sair do Menu Principal");
            return Utils.ColetarValorInt("Informe opção: ");
        }
        static void MenuPassageiro()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("### AREA DO PASSAGEIRO ###");
                int op = Utils.ColetarValorInt("(0 - Retornar ao menu anterior)\n(1 - Cadastrar novo passageiro)\n(2 - Localizar Passageiro)\n" +
                    "(3 - Editar PAssageiro)\n(4 - Listar Passageiros Ativos)\n(5 - Listar Passageiros Inativos)\n(6 - Cadastrar CPF restrito)\n" +
                    "(7 - Remover CPF da lista de restritos)\n(8 - Listar CPFs restritos)\nInforme opção desejada: ");
                switch (op)
                {
                    case 0:
                        return;

                    case 1:
                        Passageiro passageiro = new Passageiro();
                        passageiro.CadastrarPassageiro();
                        break;
                    case 2:
                        Passageiro.Localizar();
                        break;
                    case 3:
                        Passageiro.Editar();
                        break;
                    case 4:
                        Console.Clear();
                        Console.WriteLine("### LISTAR PASSAGEIROS ATIVOS ###\n");
                        Passageiro.Listar('A');
                        break;
                    case 5:
                        Console.Clear();
                        Console.WriteLine("### LISTAR PASSAGEIROS INATIVOS ###\n");
                        Passageiro.Listar('I');
                        break;
                    case 6:
                        Console.Clear();
                        Console.WriteLine("### Cadastrar CPF restrito ###\n");
                        CadastrarCpfRestrito();
                        Utils.Pause();
                        break;
                    case 7:
                        Console.Clear();
                        Console.WriteLine("### REMOVER CPF RESTRITO ###\n");
                        RemoverCpfRestrito();
                        Utils.Pause();
                        break;
                    case 8:
                        Console.Clear();
                        Console.WriteLine("### LISTAR CPFs RESTRITO ###\n");
                        Db_Aeroporto db = new Db_Aeroporto();
                        if (!db.SelectRestritos("SELECT cpf FROM dbo.restritos")) Console.WriteLine("Não há CPFs inscritos nessa lista"); ;
                        Utils.Pause();
                        break;
                    default:
                        Console.WriteLine("Opção inválida...");
                        Utils.Pause();
                        break;
                }
            } while (true);
        }
        public static void MenuCompanhia()
        {
            int opc;
            do
            {
                Console.Clear();
                Console.WriteLine("### MENU OPÇÕES DA COMPANHIA AÉREA ###");
                Console.WriteLine("1 - Cadastrar Companhia");
                Console.WriteLine("2 - Buscar Companhia");
                Console.WriteLine("3 - Editar Companhia");
                Console.WriteLine("4 - Listar Companhias Ativas");
                Console.WriteLine("5 - Listar Companhias Inativas");
                Console.WriteLine("6 - Inserir CNPJ na Llista de Empresas Bloqueados");
                Console.WriteLine("7 - Remover CNPJ na Lista de Empresas Bloqueados");
                Console.WriteLine("8 - Listar CNPJs bloqueado");
                Console.WriteLine("0 - Sair do Menu de Companhias");
                opc = Utils.ColetarValorInt("Informe a opção: ");

                switch (opc)
                {
                    case 1:
                        CompanhiaAerea companhia = new CompanhiaAerea();
                        companhia.CadastrarCompainhaAerea();
                        break;
                    case 2:
                        CompanhiaAerea.LocalizarCompanhiaAerea();
                        break;
                    case 3:
                        CompanhiaAerea.Editar();
                        break;
                    case 4:
                        Console.Clear();
                        Console.WriteLine("### LISTAR COMPANHIAS AÉREAS ATIVAS ###");
                        CompanhiaAerea.ListarCompanhias('A');
                        break;
                    case 5:
                        Console.Clear();
                        Console.WriteLine("### LISTAR COMPANHIAS AÉREAS INATIVAS ###");
                        CompanhiaAerea.ListarCompanhias('I');
                        break;
                    case 6:
                        Console.Clear();
                        Console.WriteLine("### CADASTRAR CNPJ BLOQUEADO ###");
                        CadastrarCnpjBloqueado();
                        break;
                    case 7:
                        Console.Clear();
                        Console.WriteLine("### REMOVER CNPJ BLOQUEADO ###");
                        RemoverCnpjBloqueado();
                        break;
                    case 8:
                        Console.Clear();
                        Console.WriteLine("### LISTAR CNPJs BLOQUEADO ###");
                        Db_Aeroporto db = new Db_Aeroporto();
                        if (!db.SelectBloqueados("SELECT cnpj FROM dbo.bloqueados")) Console.WriteLine("Não há CNPJs inscritos nessa lista");
                        break;
                    case 0:
                        return;
                    default:
                        Console.WriteLine("Opção Inválida! Favor selecionar uma das opções acima!");
                        break;
                }
                Utils.Pause();
            } while (true);
        }
        public static void MenuAeronave()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("### MENU AERONAVE ###");
                Console.WriteLine("0 - Sair do Menu de Aeronaves");
                Console.WriteLine("1 - Cadastrar Aeronaves");
                Console.WriteLine("2 - Listar Aeronaves");
                Console.WriteLine("3 - Editar Aeronave");
                Console.WriteLine("4 - Consultar Aeronave");
                int opc = Utils.ColetarValorInt("Informe opção: ");

                switch (opc)
                {
                    case 1:
                        Aeronave aeronave = new Aeronave();
                        aeronave.CadastroAeronave();
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("### LISTAR TODAS AS AERONAVES ###");
                        Aeronave.Listar();
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("### LISTAR TODAS AS AERONAVES ###");
                        Aeronave.EditarAeronave();
                        break;
                    case 4:
                        Console.Clear();
                        Console.WriteLine("### BUSCAR AERONAVE ###");
                        string inscricao = Utils.ColetarString("Informe a inscrição da aeronave que deseja consultar: ");
                        Aeronave.Listar(inscricao);
                        break;
                    case 0:
                        return;
                    default:
                        Console.WriteLine("Opção Inválida! Favor selecionar uma das opções acima!");
                        break;
                }
                Utils.Pause();
            } while (true);
        }
                public static void MenuVoo()
        {
            do
            {
                Console.WriteLine("1 - Cadastrar Voo");
                Console.WriteLine("2 - Buscar Voo");
                Console.WriteLine("3 - Editar Voo");
                Console.WriteLine("4 - Listar Voos");
                Console.WriteLine("0 - Sair do Menu de Voos");
                Console.Write("Opção: ");
                int opc = int.Parse(Console.ReadLine());

                switch (opc)
                {
                    case 1:

                        break;
                    //case 2:
                    //    Console.Write("Informe o ID do Voo para busca: ");
                    //    string idVoo = Console.ReadLine();
                    //    Console.WriteLine(BuscarVoo(listaVoos, idVoo).ToString());
                    //    break;
                    //case 3:
                    //    EditarVoo(listaVoos);
                    //    break;
                    //case 4:
                    //    foreach (Voo item in listaVoos)
                    //        Console.WriteLine(item.ToString() + "\n");
                    //    break;
                    //case 0:
                    //    Console.WriteLine("Você saiu do Menu de Voos!");
                    //    return;
                    default:
                        Console.WriteLine("Opção Inválida! Favor selecionar uma das opções acima!");
                        break;
                }
            } while (true);
        }

        #endregion Menus

        #region passageiro
        static void CadastrarCpfRestrito()
        {
            string cpf;
            do
            {
                cpf = Utils.ColetarString("Informe o CPF que irá ser cadastrado na lista de restritos: ");
                if (!Utils.ValidarCpf(cpf)) Console.WriteLine("CPF inválido");
                else break;
            } while (true);
            Db_Aeroporto db = new Db_Aeroporto();
            string sql = $"INSERT INTO dbo.restritos (cpf) values ({cpf})";
            if (!db.InsertTable(sql)) Console.WriteLine("Ocorreu um erro na solicitação");
            else Console.WriteLine("Solicitação efetuado com sucesso");
        }

        static void RemoverCpfRestrito()
        {
            string cpf;
            do
            {
                cpf = Utils.ColetarString("Informe o CPF que irá ser removido lista de restritos: ");
                if (!Utils.ValidarCpf(cpf)) Console.WriteLine("CPF inválido");
                else break;
            } while (true);
            Db_Aeroporto db = new Db_Aeroporto();
            string sql = $"DELETE FROM dbo.restritos WHERE cpf='{cpf}'";
            if (!db.InsertTable(sql)) Console.WriteLine("Ocorreu um erro na solicitação");
            else Console.WriteLine("Solicitação efetuado com sucesso");
        }

        #endregion passageiro

        #region Companhia aerea

        static void CadastrarCnpjBloqueado()
        {
            string cnpj;
            do
            {
                cnpj = Utils.ColetarString("Informe o CNPJ que irá ser cadastrado na lista de restritos: ");
                if (!Utils.ValidarCnpj(cnpj)) Console.WriteLine("CNPJ inválido");
                else break;
            } while (true);
            Db_Aeroporto db = new Db_Aeroporto();
            string sql = $"INSERT INTO dbo.bloqueados (cnpj) values ('{cnpj}')";
            if (!db.InsertTable(sql)) Console.WriteLine("Ocorreu um erro na solicitação");
            else Console.WriteLine("Solicitação efetuado com sucesso");
        }

        static void RemoverCnpjBloqueado()
        {
            string cnpj;
            do
            {
                cnpj = Utils.ColetarString("Informe o CNPJ que irá ser removido da lista de bloqueados: ");
                if (!Utils.ValidarCnpj(cnpj)) Console.WriteLine("CNPJ inválido");
                else break;
            } while (true);
            Db_Aeroporto db = new Db_Aeroporto();
            string sql = $"DELETE FROM dbo.bloqueados WHERE cnpj = '{cnpj}'";
            if (!db.InsertTable(sql)) Console.WriteLine("Ocorreu um erro na solicitação");
            else Console.WriteLine("Solicitação efetuado com sucesso");
        }




        #endregion Companhia aerea


    }
}