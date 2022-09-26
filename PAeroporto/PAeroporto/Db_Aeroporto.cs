using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace PAeroporto
{
    internal class Db_Aeroporto
    {
        string conexao = "Data Source = DESKTOP-49RHHLK\\MSSQL;Initial Catalog=ong_adocao;Persist Security Info=True;User ID=sa;Password=834500";
        SqlConnection conn = new SqlConnection();

        public Db_Aeroporto()
        {
            conn = new SqlConnection(conexao);
        }

        //metodo insert para insercao no banco de dados
        public void InsertTable(string sql)
        {
            int row;
            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                row = cmd.ExecuteNonQuery();
            }
            catch (SqlException msg)
            {
                if (msg.Number == 2627) Console.WriteLine($"Já existe o passageiro cadastrado!!!");
                if (msg.Number == 2628) Console.WriteLine($"Valores truncados da coluna!!!");
            }
            conn.Close();
        }

    }
}
