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
        public static List<Venda> Select(Venda venda)
        {
            List<Venda> vendaList = new();
            
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
            return vendaList;


        }
        public static void Update(Venda venda)
        {

        }
        public static void Delete(Venda venda)
        {

        }

    }
}
