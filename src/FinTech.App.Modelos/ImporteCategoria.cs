using System;
using System.Globalization;

namespace FinTech.Models
{
    public class ImporteCategoria
    {
        public int CategoriaId { get; init; }
        public string Categoria { get; init; }
        public decimal Importe { get; init; }

        public override string ToString() => $"{CategoriaId:00} {Categoria} {Importe:00.00;;#}";
    }
}