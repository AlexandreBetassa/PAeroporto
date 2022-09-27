using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
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
                    //case 2:
                    //    Console.Clear();
                    //    MenuCompanhia(ListaCompanhiaAereas, listaCnpjRestrito);
                    //    break;
                    //case 3:
                    //    Console.Clear();
                    //    MenuAeronave(listaAeronaves, ListaCompanhiaAereas);
                    //    break;
                    //case 4:
                    //    Console.Clear();
                    //    MenuVoo(listaVoos, listaIatas, listaAeronaves);
                    //    break;
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
                int op = Utils.ColetarValorInt("(0 - Retornar ao menu anterior)\n(1 - Cadastrar novo passageiro)\n(2 - Localizar Passageiro)\nInforme opção desejada: ");
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

                    default:
                        Console.WriteLine("Opção inválida...");
                        Utils.Pause();
                        break;
                }


            } while (true);
        }

        #endregion Menus
    }
}