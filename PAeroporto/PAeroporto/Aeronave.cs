﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PAeroporto;

namespace PAeroporto
{
    internal class Aeronave
    {
        //CHAVE, PADRÃO ANAC, 6 DIGITOD ALFANUMERICOS
        public String Inscricao { get; set; }
        public int Capacidade { get; set; } //3 digitos numericos
        public int AssentosOcupados { get; set; } //3 digitos numericos
        public DateTime UltimaVenda { get; set; } //no cad, dt atual
        public DateTime DataCadastro { get; set; } //dt atual
        public char Situacao { get; set; }
        public String CNPJCompAerea { get; set; }

        public Aeronave()
        {
            DataCadastro = DateTime.Now;
            UltimaVenda = DateTime.Now;
            AssentosOcupados = 0;
            Situacao = 'A';
        }
        bool aux;

        public static String SufixoAeronave()
        {
            string sufixo;
            bool aux;
            do
            {
                Console.Write("Informe as 3 últimas letras da inscrição da aeronave: ");
                sufixo = Console.ReadLine();
                aux = VerificarSufixo(sufixo);
                if (!aux) Console.WriteLine("SUFIXO INVÁLIDO");
            } while (sufixo.Length != 3 || !aux);
            return sufixo.ToUpper();
        }
        public static bool VerificarSufixo(String sufixo)
        {
            for (int i = 0; i < 3; i++)
            {
                char aux = sufixo[i];
                if (char.IsLetter(aux)) ;
                else return false;
            }
            return true;
        }
        public static String SelecionarPrefixo()
        {
            int prefixo;
            do
            {
                Console.WriteLine("Informe o prefixo da aeronave\n1 - PP\n2 - PT\n3 - PR\n4 - PS\n5 - BR\n0 - Sair");
                int.TryParse(Console.ReadLine(), out prefixo);
                switch (prefixo)
                {
                    case 1:
                        return "PP";
                    case 2:
                        return "PT";
                    case 3:
                        return "PR";
                    case 4:
                        return "PS";
                    case 5:
                        return "BR";
                    default:
                        Console.WriteLine("PREFIXO INVÁLIDO");
                        break;
                }
            } while (prefixo != 0);
            return "0";
        }
        public void CadastroAeronave()
        {
            Console.Clear();
            Console.WriteLine("### CADASTRO DE AERONAVE ###");
            Db_Aeroporto db = new Db_Aeroporto();
            string cnpj;
            do
            {
                string prefixo = SelecionarPrefixo();
                if (prefixo == "0")
                {
                    Console.WriteLine("CADASTRAMENTO CANCELADO!!!");
                    Utils.Pause();
                    Console.Clear();
                    return;
                }
                Inscricao = prefixo + "-" + SufixoAeronave();
                if (db.VerificarDados($"SELECT inscAeronave FROM dbo.aeronave WHERE inscAeronave = '{Inscricao}'")) Console.WriteLine("AERONAVE JÁ CADASTRADA");
                else break;
            } while (true);

            do
            {
                Capacidade = Utils.ColetarValorInt("Informe a capacidade de pessoas que a AERONAVE comporta: ");
                if (Capacidade < 0 || Capacidade > 999) Console.WriteLine("No momento não aceitamos aviões com mais de 999 passageiros");
                else break;
            } while (true);

            //Lista de Companhias
            Console.Clear();
            Console.WriteLine("### LISTA DE COMPANHIAS CADASTRADAS E ATIVAS ###");
            db.SelectTableCA($"SELECT cnpj, razaoSocial,dataAbertura, dataCadastro,ultimoVoo,situacao FROM dbo.companhiaAerea WHERE situacao = 'A';");
            do
            {
                cnpj = Utils.ColetarString("Informe o CNPJ da companhia a aeronave pertence na lista acima: ");
                if (!Utils.ValidarCnpj(cnpj)) Console.WriteLine("Informe um CNPJ válido...");
                else if (!db.VerificarDados($"SELECT cnpj FROM dbo.companhiaAerea WHERE cnpj = '{cnpj}'")) Console.WriteLine("Informe um CNPJ da lista acima ou verifique o cadastro da Companhia Aérea");
                else break;
            } while (true);
            CNPJCompAerea = cnpj;

            string sql = $"INSERT INTO dbo.aeronave (inscAeronave, cnpjCompAerea, capacidade, ultimaVenda, situacao, dataCadastro) " +
                $"VALUES ('{this.Inscricao}','{this.CNPJCompAerea}','{this.Capacidade}','{this.UltimaVenda}','{this.Situacao}','{this.DataCadastro}');";
            if (!db.InsertTable(sql)) Console.WriteLine("Houve um erro na solicitação");
            else Console.WriteLine("Solicitação efetuada com sucesso");
        }
        public void EditarAeronave()
        {
            Console.WriteLine("Escolha entre as opções, o/os dados que deseja editar em seu cadastro: ");
            Console.WriteLine("1 - Editar CAPACIDADE cadastrada");
            Console.WriteLine("2 - Editar ASSENTOS OCUPADOS cadastrado");
            Console.WriteLine("3 - Editar ÚLTIMA VENDA cadastrada");
            Console.WriteLine("4 - Editar DATA DO CADASTRO cadastrada");
            Console.WriteLine("5 - Editar SITUAÇÃO DO CADASTRO");
            int op = Utils.ColetarValorInt("Informe a operação: ");
            switch (op)
            {
                case 1:
                    Capacidade = Utils.ColetarValorInt("Informe a CAPACIDADE correta da aeronave: ");
                    break;
                case 2:
                    AssentosOcupados = Utils.ColetarValorInt("Informe a quantidade de ASSENTOS OCUPADOS correta: ");
                    break;
                case 3:
                    UltimaVenda = Utils.ColetarData("Informe a DATA correta da ÚLTIMA VENDA: ");
                    break;
                case 4:
                    DataCadastro = Utils.ColetarData("Informe a DATA DO CADASTRO correta: ");
                    break;
                case 5:
                    do Situacao = Utils.ColetarValorChar("Informe a SITUAÇÃO do cadastro correta (A - Ativo, I - Inativo): ");
                    while (Situacao != 'A' && Situacao != 'I');
                    break;
                default:
                    Console.WriteLine("Opção inválida...");
                    Utils.Pause();
                    break;
            }
        }

        public static void Listar()
        {
            Db_Aeroporto db = new Db_Aeroporto();
            string sql = $"SELECT aeronave.inscAeronave, companhiaAerea.razaoSocial, aeronave.capacidade, aeronave.ultimaVenda, aeronave.dataCadastro, aeronave.situacao" +
                $" FROM dbo.aeronave, dbo.companhiaAerea WHERE aeronave.cnpjCompAerea = companhiaAerea.cnpj";
            db.SelectAeronave(sql);
        }

    }
}
