using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Data.SqlClient;
using Models;

namespace Services
{
    public class PassageiroServices
    {
        public static List<Passageiro> SelectTable(string sql)
        {
            SqlConnection conn = DataBase.OpenConnection();
            List<Passageiro> list = new ();

            SqlCommand cmd = new (sql, conn);
            SqlDataReader r = cmd.ExecuteReader();
            {
                while (r.Read())
                {
                    Passageiro passageiro = new()
                    {
                        Nome = r.GetString(1),
                        CPF = r.GetString(0),
                        DataNascimento = r.GetDateTime(2),
                        Sexo = r.GetString(3),
                        UltimaCompra = r.GetDateTime(4),
                        DataCadastro = r.GetDateTime(5),
                        Situacao = r.GetString(6)
                    };
                    list.Add(passageiro);
                }
            }
            conn.Close();
            return list;
        }

        public static void Insert(Passageiro passageiro)
        {
           
        }

        public static void Update(Passageiro passageiro)
        {

        }

        public static void Delete(Passageiro passageiro)
        {

        }
    }
}
