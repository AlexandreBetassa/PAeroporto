using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Models;
using PAeroporto;

namespace PAeroporto
{
    internal class VooView
    {
        public static void CadastrarVoo()
        {
            Voo voo = new();
            do
            {
                Console.Write("Informe o AEROPORTO de destino do voo [EX: GRU] ou 0 para cancelar: ");
                voo.Destino = Console.ReadLine().ToUpper();
                if (voo.Destino == "0") return;
                else break;
            } while (true);

            voo.DataVoo = Utils.ColetarData("Informe a data e hora de partida do voo: ");
            do
            {
                voo.InscAeronave = Utils.ColetarString("Informe qual Aeronave para esse voo: ");
                //if (!db.VerificarDados($"SELECT inscAeronave FROM dbo.aeronave WHERE inscAeronave = '{InscAeronave}'")) Console.WriteLine("Aeronave indisponivel");
                //else break;
            } while (true);

            float valor = Utils.ColetarValorFloat("Informe o valor das passagens: ");
            #region
            //    string sql = $"INSERT INTO dbo.voo (assentosOcupado, destino, aeronave, dataVoo, dataCadastro, situacao) " +
            //        $"VALUES ('{this.AssentosOcupados}','{this.Destino}','{this.InscAeronave}','{this.DataVoo}','{this.DataCadastro}','{this.Situacao}'); " +
            //        $"EXEC dbo.CadastroPassagens {valor};";
            //    if (!db.InsertTable(sql)) Console.WriteLine("Ocorreu um erro na solicitação");
            //    else Console.WriteLine("Solicitação efetuada com sucesso!!");
        }
        //public static void Buscar(char situacao)
        //{
        //    //Db_Aeroporto db = new Db_Aeroporto();
        //    //string sql = $"SELECT voo.IdVoo, voo.assentosOcupado, iatas.nomeAeroporto, aeronave.inscAeronave, companhiaAerea.razaoSocial, voo.dataVoo, voo.dataCadastro, voo.situacao " +
        //    //    $"FROM dbo.voo, dbo.aeronave,dbo.companhiaAerea ,dbo.iatas WHERE voo.situacao = '{situacao}' AND iatas.sigla = voo.destino AND companhiaAerea.cnpj = aeronave.cnpjCompAerea AND " +
        //    //    $"aeronave.inscAeronave = voo.aeronave ";
        //    //if (!db.SelectVoo(sql)) Console.WriteLine("Voos não localizados!!!");
        //}
        //public static void Buscar(int idVoo)
        //{
        //    //Db_Aeroporto db = new Db_Aeroporto();
        //    //string sql = $"SELECT voo.IdVoo, voo.assentosOcupado, iatas.nomeAeroporto, aeronave.inscAeronave, companhiaAerea.razaoSocial, voo.dataVoo, voo.dataCadastro, voo.situacao " +
        //    //    $"FROM dbo.voo, dbo.aeronave,dbo.companhiaAerea ,dbo.iatas WHERE idVoo = {idVoo} AND iatas.sigla = voo.destino AND companhiaAerea.cnpj = aeronave.cnpjCompAerea AND " +
        //    //    $"aeronave.inscAeronave = voo.aeronave ";
        //    //if (!db.SelectVoo(sql)) Console.WriteLine("Voo não localizado!!! Verifique se informou corretamente a identificação do voo");
        //}
        //public static void EditarVoo()
        ////{
        ////    int idVoo;
        ////    Db_Aeroporto db = new Db_Aeroporto();
        ////    do
        ////    {
        ////        string numeroVoo = Utils.ColetarString("Informe a identificação do voo EX: (V0000) ou digite 0 para Sair: ").PadRight(5, '0');
        ////        if (numeroVoo == "0") return;
        ////        if (!int.TryParse(numeroVoo.Substring(1, 4), out idVoo)) Console.WriteLine("A identificação do voo foi digitada incorretamente");
        ////        else if (!db.VerificarDados($"SELECT idVoo FROM dbo.voo WHERE idVoo = {idVoo}")) Console.WriteLine("Voo não localizado");
        ////        else break;
        ////    } while (true);

        ////    do
        ////    {
        ////        Console.WriteLine("Informe o que deseja alterar do voo\n(0 - Retornar ao menu anterior)\n(1 - Cancelar Voo)\n(2 - Trocar Aeronave)");
        ////        int op = Utils.ColetarValorInt("Informe opção: ");
        ////        switch (op)
        ////        {
        ////            case 0:
        ////                return;
        ////            case 1:
        ////                CancelarVoo(db, idVoo);
        ////                break;
        ////            case 2:
        ////                TrocarAeronave(db, idVoo);
        ////                break;
        ////            default:
        ////                Console.WriteLine("Opção inválida");
        ////                break;
        ////        }
        ////    } while (true);
        //}
        //public static void TrocarAeronave(Db_Aeroporto db, int idVoo)
        //{
        //    int confirmar;
        //    string aeronave;
        //    Console.Clear();
        //    Console.WriteLine("### TROCAR DE AERONAVE ###");
        //    do
        //    {
        //        aeronave = Utils.ColetarString("Informe a inscrição da nova aeronave para o voo ou digite 0 para cancelar: ");
        //        if (aeronave == "0") return;
        //        else if (!db.VerificarDados($"SELECT inscAeronave FROM dbo.aeronave WHERE situacao = 'A' AND inscAeronave = '{aeronave}'")) Console.WriteLine("Aeronave não localizada");
        //        else break;
        //    } while (true);
        //    do
        //    {
        //        confirmar = Utils.ColetarValorInt("Confirmar troca\n(1 - Sim)\n(2 - Não)\nInforme opção: ");
        //        if (confirmar == 2) return;
        //        else if (confirmar != 1) Console.WriteLine("Opção inválida");
        //        else break;
        //    } while (true);

        //    if (!db.UpdateTable($"UPDATE dbo.voo SET aeronave = '{aeronave}' WHERE idVoo = {idVoo}")) Console.WriteLine("Ocorreu um erro na solicitação");
        //    else Console.WriteLine("Solicitação efetuada com sucesso");
        //}
        //public static void CancelarVoo(Db_Aeroporto db, int idVoo)
        //{
        //    Console.Clear();
        //    Console.WriteLine("### CANCELAMENTO DE VOOS ###");
        //    int confirmar;
        //    do
        //    {
        //        confirmar = Utils.ColetarValorInt("(1 - Confirmar Cancelamento)\n(2 - Cancelar Operação de Cancelamento)\nInforme opção: ");
        //        if (confirmar == 2) return;
        //        else if (confirmar != 1) Console.WriteLine("Opção inválida!!!");
        //        else break;
        //    } while (true);

        //    if (!db.UpdateTable($"UPDATE dbo.voo SET situacao = 'C' WHERE idVoo = {idVoo}")) Console.WriteLine("Ocorreu um erro na solicitação");
        //    else Console.WriteLine("Solicitação efetuada com sucesso!!!");
        //}
        #endregion
    }
}
