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
        public char Sexo { get; set; } //M F N
        public DateTime UltimaCompra { get; set; } //no cadastro, data atual
        public DateTime DataCadastro { get; set; }
        public char Situacao { get; set; } //A - Ativo I - Inativo

        public Passageiro()
        {
            UltimaCompra = DateTime.Now; //data atual do sistema
            DataCadastro = DateTime.Now; //data atual do sistema
            Situacao = 'A';
        }
        public void CadastrarPassageiro(String cpf)
        {
            Console.WriteLine(">>>CADASTRO DE PASSAGEIRO<<<");
            CPF = cpf;
            do
            {
                Console.WriteLine("Informe o seu nome (Máximo 50 digítos): ");
                Nome = Console.ReadLine();
                if (Nome.Length > 50)
                {
                    Console.WriteLine("\nIMPOSSÍVEL CADASTRAR! \nTENTE NOVAMENTE!");

                }
            } while (Nome.Length > 50);

            //Fazer o tratamento de possíveis erros
            Console.WriteLine("Informe sua Data de Nascimento: ");
            DataNascimento = DateTime.Parse(Console.ReadLine());

            do
            {
                Console.WriteLine("Informe seu genero: (M - Masculino, F - Feminino, N - Não desejo informar) : ");
                Sexo = char.Parse(Console.ReadLine().ToUpper());
                if (Sexo != 'M' && Sexo != 'F' && Sexo != 'N')
                {
                    Console.WriteLine("OPÇÃO INVÁLIDA! INFORME (M, F OU N) ");
                }
            } while (Sexo != 'M' && Sexo != 'F' && Sexo != 'N');

            //A data de última compra já foi declarada no método construtor
            //Não há necessidade de ler as informações novamente
            //Data de cadastro e situação também já foram declaradas.                  

        }


        public void EditarPassageiro()
        {
            Console.WriteLine("Escolha entre as opções, o/os dados que deseja editar em seu cadastro: ");
            Console.WriteLine("1 - Editar NOME cadastrado");
            Console.WriteLine("2 - Editar DATA DE NASCIMENTO cadastrado");
            Console.WriteLine("3 - Editar SEXO cadastrado");
            Console.WriteLine("4 - Editar DATA DO CADASTRO");
            Console.WriteLine("5 - Editar SITUAÇÃO do CADASTRO ");
            int op = int.Parse(Console.ReadLine());
            switch (op)
            {
                case 1:
                    Console.WriteLine("Informe o NOME correto: ");
                    Nome = Console.ReadLine();
                    break;

                case 2:
                    Console.WriteLine("Informe a DATA DE NASCIMENTO correta: ");
                    DataNascimento = DateTime.Parse(Console.ReadLine());

                    break;

                case 3:
                    Console.WriteLine("Informe o gênero correto (M- Masculino, F - Feminino, N - Não desejo informar) : ");
                    Sexo = char.Parse(Console.ReadLine());
                    break;

                case 4:
                    Console.WriteLine("Informe a DATA DO CADASTRO correta: ");
                    DataCadastro = DateTime.Parse(Console.ReadLine());
                    break;

                case 5:
                    do
                    {
                        Console.WriteLine("Informe a SITUAÇÃO do cadastro correta (A - Ativo, I - Inativo): ");
                        Situacao = char.Parse(Console.ReadLine());

                    } while (Situacao != 'A' && Situacao != 'I');
                    break;

                default:
                    break;
            }
        }
        public override string ToString()
        {
            return ($"CPF: {CPF}\nNOME: {Nome}\nDATA DE NASCIMENTO: {DataNascimento}\nSEXO: {Sexo}\nÚLTIMA COMPRA: {UltimaCompra}\nDATA EM QUE O CADASTRO FOI REALIZADO: {DataCadastro}\nSITUAÇÃO DO CADASTRO (A - ATIVO, I - INATIVO): {Situacao}").ToString();
        }

    }
}


