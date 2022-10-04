﻿using System;

namespace Models
{
    public class Passageiro
    {
        public String CPF { get; set; } //prop CHAVE com 11 dígitos
        public String Nome { get; set; } // < 50 digitos
        public DateTime DataNascimento { get; set; }
        public String Sexo { get; set; } //M F N
        public DateTime UltimaCompra { get; set; } //no cadastro, data atual
        public DateTime DataCadastro { get; set; }
        public String Situacao { get; set; } //A - Ativo I - Inativo
    }
}