using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Data.SqlClient;
using Models;

namespace Services
{
    public class PassageiroServices
    {
        public static void Insert(Passageiro passageiro)
        {
            string insert = $"INSERT INTO dbo.passageiro (cpf, nome, dataNasc, sexo, ultimaCompra, dataCad, situacao)" +
                            $" VALUES ({new SqlParameter("@cpf", passageiro.CPF)}, {new SqlParameter("@nome", passageiro.Nome)}, {new SqlParameter("@dataNasc", passageiro.DataNascimento)}, " +
                            $"{new SqlParameter("@sexo", passageiro.Sexo)}, {new SqlParameter("@ultimaCompra", DateTime.Now)}, {new SqlParameter("@dataCad", DateTime.Now)}, " +
                            $"{new SqlParameter("@situacao", 'A')});";
            SqlCommand sql_insert = new() { Connection = DataBase.OpenConnection(), CommandText = insert };

            sql_insert.ExecuteNonQuery();
            DataBase.CloseConnection(sql_insert.Connection);
        }
        public static List<Passageiro> Select(string cpf)
        {
            List<Passageiro> list = new();

            string select = $"SELECT cpf, nome, dataNasc, sexo, ultimaCompra, dataCad, situacao FROM dbo.passageiro WHERE cpf = @cpf";
            SqlCommand sql_select = new()
            {
                CommandText = select,
                Connection = DataBase.OpenConnection()
            };
            sql_select.Parameters.Add(new SqlParameter("@cpf", cpf));
            SqlDataReader r = sql_select.ExecuteReader();
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
            DataBase.CloseConnection(sql_select.Connection);
            return list;
        }
        public static void Update(Passageiro passageiro)
        {
            string update =
                $"UPDATE dbo.passageiro " +
                $"SET nome = {new SqlParameter("@nome", passageiro.Nome)}, dataNasc = {new SqlParameter("@dataNasc", passageiro.DataNascimento)}," +
                $"sexo = {new SqlParameter("@sexo", passageiro.Sexo)}, situacao = {new SqlParameter("@situacao", passageiro.Situacao)} " +
                $"WHERE cpf = {new SqlParameter("@cpf", passageiro.CPF)};";
            SqlCommand sql_update = new()
            {
                CommandText = update,
                Connection = DataBase.OpenConnection()
            };
            sql_update.ExecuteNonQuery();
            DataBase.CloseConnection(sql_update.Connection);
        }
        public static void Delete(Passageiro passageiro)
        {
            string delete = $"DELETE FROM dbo.passageiro WHERE cpf = {new SqlParameter("@cpf", passageiro.CPF)}";
            SqlCommand sql_delete = new()
            {
                Connection = DataBase.OpenConnection(),
                CommandText = delete
            };

            sql_delete.ExecuteNonQuery();
            DataBase.CloseConnection(sql_delete.Connection);
        }
    }
}
