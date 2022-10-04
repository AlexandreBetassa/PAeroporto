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
            sql_insert.Parameters.Add(new SqlParameter("@passageiro", ));
            sql_insert.Parameters.Add(new SqlParameter("@dataNasc", passageiro.DataNascimento));
            sql_insert.Parameters.Add(new SqlParameter("@sexo", passageiro.Sexo));
            sql_insert.Parameters.Add(new SqlParameter("@ultimaCompra", DateTime.Now));
            sql_insert.Parameters.Add(new SqlParameter("@dataCad", DateTime.Now));
            sql_insert.Parameters.Add(new SqlParameter("@situacao", 'A'));

            sql_insert.Connection = DataBase.OpenConnection();
            sql_insert.CommandText = insert;
            sql_insert.ExecuteNonQuery();
            DataBase.CloseConnection(sql_insert.Connection);

            return venda;
        }
        public static void Select(Venda venda)
        {

        }
        public static void Update(Venda venda)
        {

        }
        public static void Delete(Venda venda)
        {

        }

    }
}
