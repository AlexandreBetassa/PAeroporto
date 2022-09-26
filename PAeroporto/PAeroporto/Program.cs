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
            do
            {
                int op = Utils.ColetarValorInt("(1 - Area do passageiro)\nInforme a opção desejada: ");
                switch (op)
                {
                    case 0:
                        Console.Clear();
                        Console.WriteLine("### SAIR ###");
                        Environment.Exit(0);
                        break;
                    case 1:
                        MenuPassageiros();
                        break;
                    default:
                        Console.WriteLine("Opção Inválida...");
                        Utils.Pause();
                        break;
                }
            } while (true);
        }

        static void MenuPassageiros()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("### AREA DO PASSAGEIRO ###");

                int op = Utils.ColetarValorInt("(0 - Retornar ao menu anterior)(1 - Cadastrar novo passageiro)\nInforme opção desejada: ");
                switch (op)
                {
                    case 0:
                        return;

                    case 1:
                        Passageiro passageiro = new Passageiro();
                        passageiro.CadastrarPassageiro();
                        break;

                    default:
                        Console.WriteLine("Opção inválida...");
                        Utils.Pause();
                        break;
                }


            } while (true);
        }
    }
}