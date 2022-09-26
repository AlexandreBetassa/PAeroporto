using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PAeroporto;

namespace PAeroporto
{
    internal class Voo
    {
        public string IdVoo { get; set; } //V0000
        public string Destino { get; set; }
        public DateTime DataVoo { get; set; } // Data 8 dígitos + 4 dígitos da hora
        public DateTime DataCadastro { get; set; }
        public char Situacao { get; set; } //A Ativo ou C Cancelado
        public Aeronave Aeronave { get; set; }
        public CompanhiaAerea CompanhiaAerea { get; set; }

        public Voo()
        {
            DataVoo = DateTime.Now;
            DataCadastro = DateTime.Now;
            Situacao = 'A';
        }

        public void CadastrarVoo(List<String> listaIatas, List<Aeronave> listaAeronaves, List<Voo> listaVoos)
        {
            // idvoo 
            if (listaVoos.Count > 9999)
            {
                Console.WriteLine("Limite de Voos atingidos!");
                return;
            }
            this.IdVoo = "V" + (listaVoos.Count() + 1).ToString("0000");

            //Nome do Aeroporto
            do
            {
                Console.Write("Informe o AEROPORTO de destino do voo: [EX: GRU]");
                Destino = Console.ReadLine().ToUpper();

                if (ValidarIATA(listaIatas, Destino) == false)
                {
                    Console.WriteLine("\nDESTINO INVÁLIDO.");
                    Console.WriteLine("PRESSIONE QUALQUER TECLA PARA INFORMAR NOVAMENTE!");
                    Console.ReadKey();
                }
            } while (ValidarIATA(listaIatas, Destino) == false);

            //Data e hora do voo
            Console.Write("Informe a data de partida do voo: ");
            DateTime dataVoo;
            while (!DateTime.TryParse(Console.ReadLine(), out dataVoo))
            {
                Console.Write("Informe a data de partida do voo: ");
            }

            Console.Write("Informe a hora de partida do voo: ");
            DateTime horaVoo;
            while (!DateTime.TryParse(Console.ReadLine(), out horaVoo))
            {
                Console.Write("Informe a hora de partida do voo: ");
            }

            //Listar Aeronaves
            Console.WriteLine("Lista de Aeronaves Cadastradas:");
            foreach (Aeronave item in listaAeronaves)
            {
                if (item.Situacao == 'A')
                    Console.WriteLine(item.ToString());
            }

            Console.Write("Informe qual Aeronave pertence a este Voo: ");
            string insc = Console.ReadLine();

            foreach (Aeronave item in listaAeronaves)
            {
                if (item.Situacao == 'A')
                {
                    if (item.Inscricao == insc)
                    this.Aeronave = item;
                }
            }
        }

        #region Validar destino IATA
        public static bool ValidarIATA(List<String> listaIatas, string destino)
        {
            bool achei = false;

            foreach (string d in listaIatas)
            {
                if (destino == d)
                {
                    achei = true;

                }
                else
                {
                    if (achei == false)
                    {
                        Console.WriteLine("Destino não foi localizado. Informe novamente com o destino correto!");
                        Console.WriteLine("Aperte qualquer tecla para continuar...");
                        Console.ReadKey();
                    }
                }
            }
            return true;
        }
        #endregion 

        public void EditarVoo()
        {
            Voo voo = new();

            Console.Write("Editar SITUAÇÃO");

            Console.Write("Infome a NOVA situação do voo:\nA - Ativo OU C - Cancelado: ");
            char situacao = char.Parse(Console.ReadLine());
            voo.Situacao = situacao;

        }
        
        public override string ToString()
        {
            return "\nIdVoo: " + IdVoo + "\nCNPJ da Companhia Aerea: " + CompanhiaAerea.CNPJ + "\nIdAeronave: " + Aeronave.Inscricao + "\nDestino: " + Destino + "\nData do Voo: " + DataVoo.ToString("dd/MM/yyyy HH:mm") + "\nData do Cadastro: " + DataCadastro + "\nSituação: " + Situacao;
        }

    }
}
