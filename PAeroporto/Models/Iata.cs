using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Iata
    {
        public string NomeAeroporto { get; set; }
        public string Sigla { get; set; }
        public override string ToString() => $"Aeroporto: {NomeAeroporto}\tIata: {Sigla}".ToString();
    }
}
