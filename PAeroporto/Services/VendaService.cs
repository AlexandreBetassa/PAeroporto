using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Models;

namespace Services
{
    internal class VendaService
    {
        public static Venda Insert(Venda venda)
        {
            string insert = $"INSERT INTO dbo.venda (dataVenda, passageiroCpf, valorTotal)" +
                $" VALUES (@dataVenda, @passageiro, @valorTotal);";
            SqlCommand sql_insert = new();
            sql_insert.Parameters.Add(new SqlParameter("@dataVenda", DateTime.Now));
            sql_insert.Parameters.Add(new SqlParameter("@passageiro", venda.Passageiro));
            sql_insert.Parameters.Add(new SqlParameter("@dataNasc", venda.ValorTotal));

            sql_insert.Connection = DataBase.OpenConnection();
            sql_insert.CommandText = insert;
            sql_insert.ExecuteNonQuery();
            DataBase.CloseConnection(sql_insert.Connection);

            return venda;
        }
        public static List<Venda> Select(int id)
        {
            List<Venda> vendaList = new();
            string select = $"SELECT id, dataVenda, passageiroCpf, valorTotal FROM dbo.venda WHERE id = @id";
            SqlCommand sql_select = new()
            {
                CommandText = select,
                Connection = DataBase.OpenConnection()
            };
            sql_select.Parameters.Add(new SqlParameter("@id", id));
            SqlDataReader r = sql_select.ExecuteReader();
            {
                while (r.Read())
                {
                    Venda venda = new()
                    {
                        IdVenda = r.GetInt32(0),
                        DataVenda = r.GetDateTime(1),
                        Passageiro = r.GetString(2),
                        ValorTotal = r.GetFloat(3)
                    };
                    vendaList.Add(venda);
                }
            }
            DataBase.CloseConnection(sql_select.Connection);
            return vendaList;
        }
        public static void Update(Venda venda)
        {
            string update = $"UPDATE dbo.venda SET dataVenda = {new SqlParameter("@dataVenda", venda.DataVenda)}, passageiroCpf = {new SqlParameter("@cpf", venda.Passageiro)}" +
                $"valorTotal = {new SqlParameter("@valor", venda.ValorTotal)} WHERE id = {new SqlParameter("@id", venda.IdVenda)}";
            SqlCommand sql_update = new() { CommandText = update, Connection = DataBase.OpenConnection() };
            sql_update.ExecuteNonQuery();
            DataBase.CloseConnection(sql_update.Connection);
        }
        public static void Delete(Venda venda)
        {
            string delete = $"DELETE FROM dbo.venda WHERE id = {new SqlParameter("@id", venda.IdVenda)}";
            SqlCommand sql_delete = new() { CommandText = delete, Connection = DataBase.OpenConnection() };
            sql_delete.ExecuteNonQuery();
            DataBase.CloseConnection(sql_delete.Connection);
        }

    }
}
