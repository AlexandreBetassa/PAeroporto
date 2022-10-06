using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Models;

namespace Services
{
    internal class IatasService
    {
        public void Insert(Iata iata)
        {
            string insert = $"INSERT INTO dbo.iatas (nomeAeroporto, sigla) Values ({new SqlParameter("@aeroporto", iata.NomeAeroporto)}, {new SqlParameter("@sigla", iata.Sigla)})";
            SqlCommand sql_insert = new() { Connection = DataBase.OpenConnection(), CommandText = insert };
            sql_insert.ExecuteNonQuery();
            DataBase.CloseConnection(sql_insert.Connection);
        }
        public List<String> Select()
        {
            string select = $"SELECT nomeAeroporto, sigla FROM dbo.iatas";
            List<String> listIata = new List<String>();
            SqlCommand sql_insert = new() { Connection = DataBase.OpenConnection(), CommandText = select };
            SqlDataReader reader = sql_insert.ExecuteReader();
            listIata = (List<string>)reader.GetEnumerator();
            DataBase.CloseConnection(sql_insert.Connection);
            return listIata;
        }
    }
}
