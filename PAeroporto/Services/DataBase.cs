using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Services
{
    internal class DataBase
    {
        static string conexao = "Data Source = DESKTOP-49RHHLK\\MSSQL;TrustServerCertificate=True;Initial Catalog=aeroporto;User ID=sa;Password=834500";
        //SqlConnection conn;
        static public SqlConnection conn { get; set; } = new SqlConnection(conexao);
        

        static public SqlConnection OpenConnection()
        {
            conn.Open();
            return conn;
        }

        static public void CloseConnection(SqlConnection conn)
        {
            conn.Close();
        }
    }
}
