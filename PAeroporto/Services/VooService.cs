using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Models;

namespace Services
{
    internal class VooService
    {
        static List<Voo> Insert(Voo voo)
        {
            string insert = $"INSERT INTO dbo.venda (assentosOcupado, destino, aeronave, dataVoo, dataCadastro, situacao)" +
                            $" VALUES ({new SqlParameter("@assentosOcupados", 0)}, {new SqlParameter("@destino", voo.Destino)}, " +
                            $"{new SqlParameter("@aeronave", voo.InscAeronave)}, {new SqlParameter("@dataVoo", voo.DataVoo)}, " +
                            $"{new SqlParameter("@dataCadastro", voo.DataCadastro)}, {new SqlParameter("@situacao", voo.Situacao)});";
            SqlCommand sql_insert = new() { Connection = DataBase.OpenConnection(), CommandText = insert };
            var retorno = sql_insert.ExecuteScalar();
            DataBase.CloseConnection(sql_insert.Connection);
            return (List<Voo>)retorno;
        }
        static List<Voo> Select(int id)
        {
            List<Voo> vooList = new();
            string select = $"SELECT idVoo, assentosOcupado, destino, aeronave, dataVoo, dataCadastro, situacao" +
                            $" FROM dbo.voo " +
                            $"WHERE idVoo = @id";
            SqlCommand sql_select = new() { CommandText = select, Connection = DataBase.OpenConnection() };
            sql_select.Parameters.Add(new SqlParameter("@id", id));
            SqlDataReader r = sql_select.ExecuteReader();
            {
                while (r.Read())
                {
                    vooList.Add(new()
                    {
                        IdVoo = r.GetInt32(0),
                        AssentosOcupados = r.GetInt32(1),
                        Destino = r.GetString(2),
                        InscAeronave = r.GetString(3),
                        DataVoo = r.GetDateTime(4),
                        DataCadastro = r.GetDateTime(5),
                        Situacao = r.GetChar(6)
                    });
                }
            }
            DataBase.CloseConnection(sql_select.Connection);
            return vooList;
        }
        static void Update(Voo voo)
        {
            string update = $"UPDATE dbo.voo " +
                            $"SET dataVoo = {new SqlParameter("@dataVoo", voo.DataVoo)}, situacao = {new SqlParameter("@situacao", voo.Situacao)}" +
                            $" WHERE id = {new SqlParameter("@id", voo.IdVoo)}";
            SqlCommand sql_update = new() { CommandText = update, Connection = DataBase.OpenConnection() };
            sql_update.ExecuteNonQuery();
            DataBase.CloseConnection(sql_update.Connection);
        }
        static void Delete(Voo voo)
        {
            string delete = $"DELETE FROM dbo.voo WHERE id = {new SqlParameter("@id", voo.IdVoo)}";
            SqlCommand sql_delete = new() { CommandText = delete, Connection = DataBase.OpenConnection() };
            sql_delete.ExecuteNonQuery();
            DataBase.CloseConnection(sql_delete.Connection);
        }
    }
}
