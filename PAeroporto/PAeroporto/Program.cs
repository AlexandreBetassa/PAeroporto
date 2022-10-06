using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using Microsoft.Data.SqlClient;
using PAeroporto;
using Models;
using Services;

namespace PAeroporto
{
    internal class Program
    {
        static void Main(string[] args)
        {



            #region
            //    int op;
            //    do
            //    {
            //        op = Menu();
            //        switch (op)
            //        {
            //            case 1:
            //                Console.Clear();
            //                MenuPassageiro();
            //                break;
            //            case 2:
            //                Console.Clear();
            //                MenuCompanhia();
            //                break;
            //            case 3:
            //                Console.Clear();
            //                MenuAeronave();
            //                break;
            //            case 4:
            //                Console.Clear();
            //                MenuVoo();
            //                break;
            //            case 5:
            //                Console.Clear();
            //                MenuVenda();
            //                break;
            //            //case 6:
            //            //    Console.Clear();
            //            //    MenuVenda(listaVendas, listaPassageiros);
            //            //    break;
            //            //case 7:
            //            //    Console.Clear();
            //            //    MenuItemVenda(listaItemVendas, listaPassagensVoos);
            //            //    break;
            //            case 0:
            //                Console.Clear();
            //                Console.WriteLine("### SAIR ###");
            //                Environment.Exit(0);
            //                break;
            //            default:
            //                Console.Clear();
            //                Console.WriteLine("Opção Inválida! Digite opção válida.");
            //                break;
            //        }
            //    } while (true);
            #endregion
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
            Console.WriteLine("5 - Menu de Vendas ");
            Console.WriteLine("7 - Menu de Item Vendas ");
            Console.WriteLine("0 - Sair do Menu Principal");
            return Utils.ColetarValorInt("Informe opção: ");
        }
        //static void MenuPassageiro()
        //{
        //    do
        //    {
        //        Console.Clear();
        //        Console.WriteLine("### AREA DO PASSAGEIRO ###");
        //        int op = Utils.ColetarValorInt("(0 - Retornar ao menu anterior)\n(1 - Cadastrar novo passageiro)\n(2 - Localizar Passageiro)\n" +
        //            "(3 - Editar PAssageiro)\n(4 - Listar Passageiros Ativos)\n(5 - Listar Passageiros Inativos)\n(6 - Cadastrar CPF restrito)\n" +
        //            "(7 - Remover CPF da lista de restritos)\n(8 - Listar CPFs restritos)\nInforme opção desejada: ");
        //        switch (op)
        //        {
        //            case 0:
        //                return;

        //            case 1:
        //                Passageiro passageiro = new Passageiro();
        //                passageiro.CadastrarPassageiro();
        //                break;
        //            case 2:
        //                Passageiro.Localizar();
        //                break;
        //            case 3:
        //                Passageiro.Editar();
        //                break;
        //            case 4:
        //                Console.Clear();
        //                Console.WriteLine("### LISTAR PASSAGEIROS ATIVOS ###\n");
        //                Passageiro.Listar('A');
        //                break;
        //            case 5:
        //                Console.Clear();
        //                Console.WriteLine("### LISTAR PASSAGEIROS INATIVOS ###\n");
        //                Passageiro.Listar('I');
        //                break;
        //            case 6:
        //                Console.Clear();
        //                Console.WriteLine("### Cadastrar CPF restrito ###\n");
        //                CadastrarCpfRestrito();
        //                Utils.Pause();
        //                break;
        //            case 7:
        //                Console.Clear();
        //                Console.WriteLine("### REMOVER CPF RESTRITO ###\n");
        //                RemoverCpfRestrito();
        //                Utils.Pause();
        //                break;
        //            case 8:
        //                Console.Clear();
        //                Console.WriteLine("### LISTAR CPFs RESTRITO ###\n");
        //                Db_Aeroporto db = new Db_Aeroporto();
        //                if (!db.SelectRestritos("SELECT cpf FROM dbo.restritos")) Console.WriteLine("Não há CPFs inscritos nessa lista"); ;
        //                Utils.Pause();
        //                break;
        //            default:
        //                Console.WriteLine("Opção inválida...");
        //                Utils.Pause();
        //                break;
        //        }
        //    } while (true);
        //}
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
                        CompAereaView companhia = new CompAereaView();
                        companhia.CadastrarCompainhaAerea();
                        break;
                    case 2:
                        CompAereaView.LocalizarCompanhiaAerea();
                        break;
                    case 3:
                        CompAereaView.Editar();
                        break;
                    case 4:
                        Console.Clear();
                        Console.WriteLine("### LISTAR COMPANHIAS AÉREAS ATIVAS ###");
                        CompAereaView.ListarCompanhias('A');
                        break;
                    case 5:
                        Console.Clear();
                        Console.WriteLine("### LISTAR COMPANHIAS AÉREAS INATIVAS ###");
                        CompAereaView.ListarCompanhias('I');
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
                        AeronaveView aeronave = new AeronaveView();
                        aeronave.CadastroAeronave();
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("### LISTAR TODAS AS AERONAVES ###");
                        AeronaveView.Listar();
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("### LISTAR TODAS AS AERONAVES ###");
                        AeronaveView.EditarAeronave();
                        break;
                    case 4:
                        Console.Clear();
                        Console.WriteLine("### BUSCAR AERONAVE ###");
                        string inscricao = Utils.ColetarString("Informe a inscrição da aeronave que deseja consultar: ");
                        AeronaveView.Listar(inscricao);
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
                Console.Clear();
                Console.WriteLine("### VOOS ###");
                Console.WriteLine("1 - Cadastrar Voo");
                Console.WriteLine("2 - Buscar Voo");
                Console.WriteLine("3 - Editar Voo");
                Console.WriteLine("4 - Listar Voos Ativos");
                Console.WriteLine("0 - Sair do Menu de Voos");
                int opc = Utils.ColetarValorInt("Opção: ");

                switch (opc)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("### CADASTRAR NOVO VOO ###");
                        VooView voo = new VooView();
                        voo.CadastrarVoo();
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("### CONSULTAR VOO ###");
                        string numeroVoo = Utils.ColetarString("Informe a identificação do voo EX: (V0000): ").PadRight(5, '0');
                        if (!int.TryParse(numeroVoo.Substring(1, 4), out int idVoo)) Console.WriteLine("A identificação do voo foi digitada incorretamente");
                        else VooView.Buscar(idVoo);
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("### EDITAR VOO ###");
                        VooView.EditarVoo();
                        break;
                    case 4:
                        Console.Clear();
                        Console.WriteLine("### LISTAR VOOS ATIVOS ###");
                        VooView.Buscar('A');
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
        public static void MenuVenda()
        {
            do
            {
                Console.WriteLine("1 - Venda de Passagem");
                Console.WriteLine("2 - Buscar Venda");
                Console.WriteLine("3 - Editar Satatus da Passagem");
                Console.WriteLine("4 - Consultar Passagem");
                Console.WriteLine("0 - Sair do Menu de Vendas");
                Console.Write("Opção: ");
                int opc = int.Parse(Console.ReadLine());

                switch (opc)
                {
                    case 1:
                        VendaView venda = new VendaView();
                        venda.CadastrarVenda();
                        break;
                    case 2:
                        VendaView.Buscar();
                        break;
                    case 3:
                        VendaView.EditarPassagem();
                        break;
                    case 4:
                        VendaView.ConsultarPassagem();
                        break;
                    case 0:
                        Console.WriteLine("Você saiu do Menu de Vendas!");
                        return;
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