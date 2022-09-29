using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Win32.SafeHandles;

namespace PAeroporto
{
    internal class Db_Aeroporto
    {
        string conexao = "Data Source = DESKTOP-49RHHLK\\MSSQL;TrustServerCertificate=True;Initial Catalog=aeroporto;User ID=sa;Password=834500";
        //SqlConnection conn;
        public SqlConnection conn { get; set; }
        public Db_Aeroporto()
        {
            conn = new SqlConnection(conexao);
        }

        //metodo insert para insercao no banco de dados
        public bool InsertTable(string sql)
        {
            bool aux = false;
            int row;
            try
            {
                if (conn.State == 0) conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                row = cmd.ExecuteNonQuery();
                if (row != 0) aux = true;
                else aux = false;
            }
            catch (SqlException msg)
            {
                if (msg.Number == 2627) Console.WriteLine($"Já existe o passageiro cadastrado!!!");
                else if (msg.Number == 2628) Console.WriteLine($"Valores truncados da coluna!!!");
                else Console.WriteLine($"Erro código: {msg.Number}");
                Utils.Pause();
            }
            conn.Close();
            return aux;
        }
        public bool InsertTablePassagem(string sql)
        {
            bool aux = false;
            int row;
            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                row = cmd.ExecuteNonQuery();
                if (row != 0) aux = true;
                else aux = false;
            }
            catch (SqlException msg)
            {
                if (msg.Number == 2627) Console.WriteLine($"Já existe o passageiro cadastrado!!!");
                else if (msg.Number == 2628) Console.WriteLine($"Valores truncados da coluna!!!");
                else Console.WriteLine($"Erro código: {msg.Number}");
                Utils.Pause();
            }
            return aux;
        }
        public bool SelectTable(string sql)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader r = cmd.ExecuteReader();
                if (!r.HasRows)
                {
                    Console.WriteLine("CPF não encontrado!!!");
                    conn.Close();
                    return false;
                }
                else
                {
                    while (r.Read())
                    {
                        Console.WriteLine($"Nome: {r.GetString(1)}");
                        Console.WriteLine($"CPF: {r.GetString(0)}");
                        Console.WriteLine($"Data de nascimento: {r.GetDateTime(2).ToShortDateString()}");
                        Console.WriteLine($"Sexo: {r.GetString(3)}");
                        Console.WriteLine($"Ultima compra: {r.GetDateTime(4).ToShortDateString()}");
                        Console.WriteLine($"Ultima data de cadastramento: {r.GetDateTime(5).ToShortDateString()}");
                        Console.WriteLine($"Status (A - Ativo) (I - Inativo): {r.GetString(6)}");
                        Console.WriteLine();
                    }
                }
            }
            catch (SqlException msg)
            {
                Console.WriteLine(msg.Number);
            }
            conn.Close();
            return true;
        }
        public bool SelectTableCA(string sql)
        {
            bool aux = false;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader r = cmd.ExecuteReader();
                if (!r.HasRows)
                {
                    Console.WriteLine("CNPJ não encontrado!!!");
                    conn.Close();
                }
                else
                {
                    while (r.Read())
                    {
                        Console.WriteLine($"Razão Social: {r.GetString(1)}");
                        Console.WriteLine($"CNPJ: {r.GetString(0)}");
                        Console.WriteLine($"Data de abertura: {r.GetDateTime(2).ToShortDateString()}");
                        Console.WriteLine($"Data de cadastro no sistema: {r.GetDateTime(3).ToShortDateString()}");
                        Console.WriteLine($"Ultima Voo: {r.GetDateTime(4).ToShortDateString()}");
                        Console.WriteLine($"Status (A - Ativo) (I - Inativo): {r.GetString(5)}");
                        Console.WriteLine();
                    }
                    aux = true;
                }
            }
            catch (SqlException msg)
            {
                Console.WriteLine(msg.Number);
            }
            conn.Close();
            return aux;
        }
        public bool SelectAeronave(string sql)
        {
            bool aux = false;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader r = cmd.ExecuteReader();
                if (!r.HasRows) aux = false;
                else
                {
                    while (r.Read())
                    {
                        Console.WriteLine($"Inscrição da aeronave: {r.GetString(0)}");
                        Console.WriteLine($"Nome Companhia aérea: {r.GetString(1)}");
                        Console.WriteLine($"Capacidade da aeronave: {r.GetInt32(2)}");
                        Console.WriteLine($"Data de última venda de passagem: {r.GetDateTime(3)}");
                        Console.WriteLine($"Data de cadastro no sistema: {r.GetDateTime(4)}");
                        Console.WriteLine($"Situação da aeronave: {r.GetString(5)}");
                        Console.WriteLine();
                        aux = true;
                    }
                }
            }
            catch (SqlException msg)
            {
                Console.WriteLine($"Erro código {msg.Number}");
            }
            return aux;
        }
        public bool SelectPassagem(string sql)
        {
            bool aux = false;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader r = cmd.ExecuteReader();
                if (!r.HasRows) aux = false;
                else
                {
                    while (r.Read())
                    {
                        Console.WriteLine($"Destino: {r.GetString(0)}\n");
                        Console.WriteLine($"Sigla (Iata): {r.GetString(1)}\n");
                        aux = true;
                    }
                }
            }
            catch (SqlException msg)
            {
                Console.WriteLine($"Erro código {msg.Number}");
            }
            conn.Close();
            return aux;
        }
        public bool VerificarDados(string sql)
        {
            bool aux;
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader r = cmd.ExecuteReader();
            if (!r.HasRows) aux = false;
            else aux = true;
            conn.Close();
            return aux;
        }
        public bool UpdateTable(string sql)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                if (cmd.ExecuteNonQuery() == 0)
                {
                    conn.Close();
                    return false;
                }
            }
            catch
            {

            }
            conn.Close();
            return true;
        }
        public bool SelectRestritos(string sql)
        {
            bool aux = false;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader r = cmd.ExecuteReader();
                if (!r.HasRows) aux = false;
                else
                {
                    while (r.Read())
                    {
                        Console.WriteLine($"CPF restrito número: {r.GetString(0)}\n");
                        aux = true;
                    }
                }
            }
            catch (SqlException msg)
            {
                Console.WriteLine($"Erro código {msg.Number}");
            }
            return aux;
        }
        public bool SelectBloqueados(string sql)
        {
            bool aux = false;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader r = cmd.ExecuteReader();
                if (!r.HasRows) aux = false;
                else
                {
                    while (r.Read())
                    {
                        Console.WriteLine($"CNPJ bloqueado número: {r.GetString(0)}\n");
                        aux = true;
                    }
                }
            }
            catch (SqlException msg)
            {
                Console.WriteLine($"Erro código {msg.Number}");
            }
            return aux;
        }
        public bool SelectVoo(string sql)
        {
            bool aux = false;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader r = cmd.ExecuteReader();
                if (!r.HasRows) aux = false;
                else
                {
                    while (r.Read())
                    {
                        Console.WriteLine($"Identificação do Voo: V{r.GetInt32(0):0000}");
                        Console.WriteLine($"Assentos Ocupados: {r.GetInt32(1)}");
                        Console.WriteLine($"Destino: {r.GetString(2)}");
                        Console.WriteLine($"Aeronave: {r.GetString(3)}");
                        Console.WriteLine($"Companhia Aérea: {r.GetString(4)}");
                        Console.WriteLine($"Data do voo: {r.GetDateTime(5)}");
                        Console.WriteLine($"Data de cadastro do voo: {r.GetDateTime(6)}");
                        Console.WriteLine($"Situação voo (A - Ativo) (C - Cancelado): {r.GetString(7)}");
                        aux = true;
                    }
                }
            }
            catch (SqlException msg)
            {
                Console.WriteLine($"Erro código {msg.Number}");
            }
            return aux;
        }
        public bool SelectIatas(string sql)
        {
            bool aux = false;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader r = cmd.ExecuteReader();
                if (!r.HasRows) aux = false;
                else
                {
                    while (r.Read())
                    {
                        Console.WriteLine($"Destino: {r.GetString(0)}\n");
                        Console.WriteLine($"Sigla (Iata): {r.GetString(1)}\n");
                        aux = true;
                    }
                }
            }
            catch (SqlException msg)
            {
                Console.WriteLine($"Erro código {msg.Number}");
            }
            conn.Close();
            return aux;
        }
        public int getValorInt(string sql)
        {
            int valor = 0;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader r = cmd.ExecuteReader();
                r.Read();
                valor = r.GetInt32(0);
                Console.WriteLine(valor);
            }
            catch (SqlException msg)
            {
                Console.WriteLine($"Erro código {msg.Number}");
            }
            conn.Close();
            return valor;
        }
        public float getValorFloat(string sql)
        {
            float valor = 0;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader r = cmd.ExecuteReader();
                r.Read();
                valor = Convert.ToSingle(r[0]);
                Console.WriteLine(valor);
            }
            catch (SqlException msg)
            {
                Console.WriteLine($"Erro código {msg.Number}");
            }
            conn.Close();
            return valor;
        }
    }
}
