using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
        public String InscAeronave { get; set; }
        public int AssentosOcupados { get; set; }

        public Voo()
        {
            DataCadastro = DateTime.Now;
            Situacao = 'A';
            AssentosOcupados = 0;
        }

        public void CadastrarVoo()
        {
            Db_Aeroporto db = new Db_Aeroporto();
            //Nome do Aeroporto
            db.SelectIatas($"SELECT nomeAeroporto, sigla FROM dbo.iatas");
            do
            {
                Console.Write("Informe o AEROPORTO de destino do voo [EX: GRU] ou 0 para cancelar: ");
                Destino = Console.ReadLine().ToUpper();
                if (Destino == "0") return;
                else if (!db.VerificarDados($"SELECT sigla FROM dbo.iatas WHERE sigla = '{Destino}';")) Console.WriteLine("Destino não localizado");
                else break;
            } while (true);

            //Data e hora do voo
            DataVoo = Utils.ColetarData("Informe a data e hora de partida do voo: ");
            do
            {
                InscAeronave = Utils.ColetarString("Informe qual Aeronave para esse voo: ");
                if (!db.VerificarDados($"SELECT inscAeronave FROM dbo.aeronave WHERE inscAeronave = '{InscAeronave}'")) Console.WriteLine("Aeronave indisponivel");
                else break;
            } while (true);

            string sql = $"INSERT INTO dbo.voo (assentosOcupado, destino, aeronave, dataVoo, dataCadastro, situacao) " +
                $"VALUES ('{this.AssentosOcupados}','{this.Destino}','{this.InscAeronave}','{this.DataVoo}','{this.DataCadastro}','{this.Situacao}');";
            if (!db.InsertTable(sql)) Console.WriteLine("Ocorreu um erro na solicitação");
            else Console.WriteLine("Solicitação efetuada com sucesso!!");
        }

        public static void Buscar(int idVoo)
        {
            Db_Aeroporto db = new Db_Aeroporto();
            string sql = $"SELECT voo.IdVoo, voo.assentosOcupado, iatas.nomeAeroporto, aeronave.inscAeronave, companhiaAerea.razaoSocial, voo.dataVoo, voo.dataCadastro, voo.situacao " +
                $"FROM dbo.voo, dbo.aeronave,dbo.companhiaAerea ,dbo.iatas WHERE idVoo = {idVoo} AND iatas.sigla = voo.destino AND companhiaAerea.cnpj = aeronave.cnpjCompAerea AND " +
                $"aeronave.inscAeronave = voo.aeronave ";
            if (!db.SelectVoo(sql)) Console.WriteLine("Voo não localizado!!! Verifique se informou corretamente a identificação do voo");
        }

        public static void EditarVoo()
        {
            Voo voo = new();

            Console.Write("Editar SITUAÇÃO");

            Console.Write("Infome a NOVA situação do voo:\nA - Ativo OU C - Cancelado: ");
            char situacao = char.Parse(Console.ReadLine());
            voo.Situacao = situacao;

        }

        //public override string ToString()
        //{
        //    return "\nIdVoo: " + IdVoo + "\nCNPJ da Companhia Aerea: " + CompanhiaAerea.CNPJ + "\nIdAeronave: " + Aeronave.Inscricao + "\nDestino: " + Destino + "\nData do Voo: " + DataVoo.ToString("dd/MM/yyyy HH:mm") + "\nData do Cadastro: " + DataCadastro + "\nSituação: " + Situacao;
        //}

    }
}
