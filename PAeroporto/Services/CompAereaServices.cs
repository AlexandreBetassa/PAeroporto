using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Models;

namespace Services
{
    internal class CompAereaServices
    {
        public static void Insert(CompAerea CompAerea)
        {
            string insert = $"INSERT INTO dbo.companhiaAerea (cnpj, razaoSocial, dataAbertura, dataCadastro, ultimoVoo, situacao)" +
                            $" VALUES ({new SqlParameter("@cnpj", CompAerea.CNPJ)}, {new SqlParameter("@razao", CompAerea.RazaoSocial)}, " +
                            $"{new SqlParameter("@dataAbertura", CompAerea.DataAbertura)}, {new SqlParameter("@dataCadastro", DateTime.Now)}, " +
                            $"{new SqlParameter("@ultimoVoo", DateTime.Now)}, {new SqlParameter("@situacao", 'A')});";
            SqlCommand sql_insert = new() { Connection = DataBase.OpenConnection(), CommandText = insert };
            sql_insert.ExecuteNonQuery();
            DataBase.CloseConnection(sql_insert.Connection);
        }
        public static List<CompAerea> Select(string cnpj)
        {
            List<CompAerea> lstComp = new();
            string select = $"SELECT cnpj, razaoSocial, dataAbertura, dataCadastro, ultimoVoo, situacao " +
                            $"FROM dbo.companhiaAerea " +
                            $"WHERE cnpj = @cnpj";
            SqlCommand sql_select = new() { CommandText = select, Connection = DataBase.OpenConnection() };
            sql_select.Parameters.Add(new SqlParameter("@cnpj", cnpj));
            SqlDataReader reader = sql_select.ExecuteReader();
            {
                while (reader.Read())
                {
                    lstComp.Add(new()
                    {
                        CNPJ = reader.GetString(0),
                        RazaoSocial = reader.GetString(1),
                        DataAbertura = reader.GetDateTime(2),
                        DataCadastro = reader.GetDateTime(3),
                        UltimoVoo = reader.GetDateTime(4),
                        SituacaoCA = reader.GetChar(5)
                    });
                };
            }
            DataBase.CloseConnection(sql_select.Connection);
            return lstComp;
        }
        public static void Update(CompAerea compAerea)
        {
            string update = $"UPDATE dbo.companhiaAerea SET razaoSocial = {new SqlParameter("@razao", compAerea.RazaoSocial)}, " +
                            $"ultimoVoo = {new SqlParameter("@ultimoVoo", compAerea.UltimoVoo)}, situacao = {new SqlParameter("@situacao", compAerea.SituacaoCA)} " +
                            $"WHERE cnpj = {new SqlParameter("@cnpj", compAerea.CNPJ)}";
            SqlCommand sql_update = new() { CommandText = update, Connection = DataBase.OpenConnection() };
            sql_update.ExecuteNonQuery();
            DataBase.CloseConnection(sql_update.Connection);
        }
        public static void Delete(CompAerea compAerea)
        {
            string update = $"DELETE FROM dbo.companhiaAerea " +
                            $"WHERE cnpj = {new SqlParameter("@cnpj", compAerea.CNPJ)}";
            SqlCommand sql_update = new() { CommandText = update, Connection = DataBase.OpenConnection() };
            sql_update.ExecuteNonQuery();
            DataBase.CloseConnection(sql_update.Connection);
        }
    }
}
