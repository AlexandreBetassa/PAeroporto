using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            SituacaoCA = 'A'; //A Ativo ou I Inativo
        }

        public void CadCompAerea(string cnpj)
        {
            Console.Clear();
            Console.WriteLine("### CADASTRO DE COMPANHIA AEREA ###");
            CNPJ = cnpj;
            //Razão Social
            do
            {
                Console.Write("Informe o nome da Razão Social (máximo 50 dígitos):  ");
                RazaoSocial = Console.ReadLine();
                if (RazaoSocial.Length > 50 || String.IsNullOrWhiteSpace(RazaoSocial))
                {
                    Console.WriteLine("\nInforme um nome válido.\nTENTE NOVAMENTE!");
                    Utils.Pause();
                }
            } while (RazaoSocial.Length > 50 || String.IsNullOrWhiteSpace(RazaoSocial));

            //Data de abertura
            DataAbertura = Utils.ColetarData("Informe a data de abertura: (mês/dia/ano)");
        }

        public void EditarCompAerea()
        {
            int op;
            do
            {
                Console.Write("Escolha o dado que você deseja editar: ");
                Console.Write("1 - Editar RAZÃO SOCIAL ");
                Console.Write("2 - Editar DATA DE ABERTURA ");
                Console.Write("3 - Editar ÚLTIMO VOO ");
                Console.Write("4 - Editar NOVA DATA CADASTRO (ALTERAÇÃO) ");
                Console.Write("0 - SAIR ");
                op = int.Parse(Console.ReadLine());

                if (op != 1 && op != 2 && op != 3 && op != 4 && op != 0)
                {
                    Console.WriteLine("Opção inválida!");
                }

            } while (op != 1 && op != 2 && op != 3 && op != 4 && op != 0);

            switch (op)
            {
                case 1:
                    Console.Write("Informe a RAZÃO SOCIAL correta: ");
                    string razaoSocial = Console.ReadLine();
                    RazaoSocial = razaoSocial;
                    break;

                case 2:
                    Console.Write("Informe a DATA DE ABERTURA correta: ");
                    DateTime dataAbertura = DateTime.Parse(Console.ReadLine());
                    DataAbertura = dataAbertura;
                    break;

                case 3:
                    Console.Write("Informe a DATA DO ÚLTIMO VOO correta: ");
                    DateTime ultimoVoo = DateTime.Parse(Console.ReadLine());
                    UltimoVoo = ultimoVoo;
                    break;

                case 4:
                    do
                    {
                        Console.WriteLine("Informe a SITUAÇÃO do cadastro correta (A - Ativo, I - Inativo): ");
                        char situacao = char.Parse(Console.ReadLine());
                        SituacaoCA = situacao;

                    } while (SituacaoCA != 'A' && SituacaoCA != 'I');
                    break;

                case 0:
                    break;
            }
        }

        public override string ToString()
        {
            return "\nCNPJ: " + CNPJ + "\nRazão Social: " + RazaoSocial + "\nData de Abertura: " + DataAbertura + "\nÚltimo Voo: " + UltimoVoo + "\nData de Cadastro: " + DataCadastro + "\nSituação: " + SituacaoCA;
        }


    }
}
