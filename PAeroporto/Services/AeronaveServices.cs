using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using Models;

namespace Services
{
    internal class AeronaveServices
    {
        public static void Insert(Aeronave aeronave)
        {
            string insert = $"INSERT INTO dbo.aeronave (inscAeronave, cnpjCompAerea, capacidade, ultimaVenda, situacao, dataCadastro)" +
                            $" VALUES ({new SqlParameter("@inscricao", aeronave.Inscricao)}, {new SqlParameter("@companhia", aeronave.Companhia)}, " +
                            $"{new SqlParameter("@capacidade", aeronave.Capacidade)}, {new SqlParameter("@ultimaVenda", DateTime.Now)}, {new SqlParameter("@situacao", 'A')}, " +
                            $"{new SqlParameter("@dataCadastro", DateTime.Now)});";
            SqlCommand sql_insert = new() { Connection = DataBase.OpenConnection(), CommandText = insert };
            sql_insert.ExecuteNonQuery();
            DataBase.CloseConnection(sql_insert.Connection);
        }
        public static List<Aeronave> Select(int inscricao)
        {
            List<Aeronave> lstAeronave = new();
            string select = $"SELECT inscAeronave, cnpjCompAerea, capacidade, ultimaVenda, situacao, dataCadastro FROM dbo.aeronave WHERE inscAeronave = @inscricao";

            SqlCommand sql_select = new() { CommandText = select, Connection = DataBase.OpenConnection() };
            sql_select.Parameters.Add(new SqlParameter("@inscricao", inscricao));
            SqlDataReader reader = sql_select.ExecuteReader();
            {
                while (reader.Read())
                {
                    lstAeronave.Add(new()
                    {
                        Inscricao = reader.GetString(0),
                        Companhia = reader.GetString(1),
                        Capacidade = reader.GetInt32(2),
                        UltimaVenda = reader.GetDateTime(3),
                        Situacao = reader.GetChar(4),
                        DataCadastro = reader.GetDateTime(5)
                    });
                };
            }
            DataBase.CloseConnection(sql_select.Connection);
            return lstAeronave;
        }
        public static void Update(Aeronave aeronave)
        {
            string update = $"UPDATE dbo.aeronave SET capacidade = @capacidade, situacao = @situacao WHERE inscAeronave = @incricao;";
            SqlCommand sql_update = new();
            sql_update.Parameters.Add(new SqlParameter("@inscricao", aeronave.Inscricao));
            sql_update.Parameters.Add(new SqlParameter("@capacidade", aeronave.Capacidade));
            sql_update.Parameters.Add(new SqlParameter("@situacao", aeronave.Situacao));

            sql_update.Connection = DataBase.OpenConnection();
            sql_update.CommandText = update;
            var aviao = sql_update.ExecuteScalar();
            DataBase.CloseConnection(sql_update.Connection);
        }
        public static void Delete(Aeronave aeronave)
        {
            string delete = $"DELETE FROM dbo.aeronave WHERE inscAeronave = @incricao;";
            SqlCommand sql_delete = new();
            sql_delete.Parameters.Add(new SqlParameter("@inscricao", aeronave.Inscricao));

            sql_delete.Connection = DataBase.OpenConnection();
            sql_delete.CommandText = delete;
            sql_delete.ExecuteNonQuery();
            DataBase.CloseConnection(sql_delete.Connection);
        }
    }
}
