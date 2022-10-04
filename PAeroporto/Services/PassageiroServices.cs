using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Data.SqlClient;
using Models;

namespace Services
{
    public class PassageiroServices
    {

        public static Passageiro Insert(Passageiro passageiro)
        {
            string insert = $"INSERT INTO dbo.passageiro (cpf, nome, dataNasc, sexo, ultimaCompra, dataCad, situacao)" +
                            $" VALUES (@cpf, @nome, @dataNasc, @sexo, @ultimaCompra, @dataCad, @situacao);";
            SqlCommand sql_insert = new();
            sql_insert.Parameters.Add(new SqlParameter("@cpf", passageiro.CPF));
            sql_insert.Parameters.Add(new SqlParameter("@nome", passageiro.Nome));
            sql_insert.Parameters.Add(new SqlParameter("@dataNasc", passageiro.DataNascimento));
            sql_insert.Parameters.Add(new SqlParameter("@sexo", passageiro.Sexo));
            sql_insert.Parameters.Add(new SqlParameter("@ultimaCompra", DateTime.Now));
            sql_insert.Parameters.Add(new SqlParameter("@dataCad", DateTime.Now));
            sql_insert.Parameters.Add(new SqlParameter("@situacao", 'A'));

            sql_insert.Connection = DataBase.OpenConnection();
            sql_insert.CommandText = insert;
            sql_insert.ExecuteNonQuery();
            DataBase.CloseConnection(sql_insert.Connection);

            return passageiro;
        }

        public static List<Passageiro> Select(string sql)
        {
            SqlConnection conn = DataBase.OpenConnection();
            List<Passageiro> list = new();

            SqlCommand cmd = new(sql, conn);
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

        public static void Update(Passageiro passageiro)
        {
            string update = $"UPDATE dbo.passageiro nome = @nome, dataNasc = @dataNasc, sexo = @sexo, situacao = @situacao WHERE cpf = @cpf ;";
            SqlCommand sql_update = new();
            sql_update.Parameters.Add(new SqlParameter("@cpf", passageiro.CPF));
            sql_update.Parameters.Add(new SqlParameter("@nome", passageiro.Nome));
            sql_update.Parameters.Add(new SqlParameter("@dataNasc", passageiro.DataNascimento));
            sql_update.Parameters.Add(new SqlParameter("@sexo", passageiro.Sexo));
            sql_update.Parameters.Add(new SqlParameter("@situacao", passageiro.Situacao));

            sql_update.Connection = DataBase.OpenConnection();
            sql_update.CommandText = update;
            sql_update.ExecuteNonQuery();
            DataBase.CloseConnection(sql_update.Connection);
        }

        public static void Delete(Passageiro passageiro)
        {
            string delete = $"DELETE FROM dbo.passageiro WHERE cpf = @cpf";
            SqlCommand sql_delete = new();
            sql_delete.Parameters.Add(new SqlParameter("@cpf", passageiro.CPF));

            sql_delete.Connection = DataBase.OpenConnection();
            sql_delete.CommandText = delete;
            sql_delete.ExecuteNonQuery();
            DataBase.CloseConnection(sql_delete.Connection);
        }
    }
}
