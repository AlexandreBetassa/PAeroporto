using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using PAeroporto;

namespace PAeroporto
{
    internal class PassagemVoo
    {
        public int IdVoo { get; set; }
        public DateTime DataUltimaOperacao { get; set; }
        public float Valor { get; set; } //maximo 9.999,99
        public char Situacao { get; set; }
        public PassagemVoo()
        { }

        public static void Buscar()
        {
            string passagem = Utils.ColetarString("Informe a identificação da passagem (ex: PA0000): ");
            string sql = "";

        }




    }
}
