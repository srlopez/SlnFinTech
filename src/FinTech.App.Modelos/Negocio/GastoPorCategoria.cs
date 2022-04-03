using System;
using System.Globalization;

namespace FinTech.Models
{
    public class GastoPorCategoria : FinTech.Tools.Superficiable
    {
        public int CategoriaId { get => base.Id; init => base.Id=value; }
        public string Categoria { get => base.Nombre; init => base.Nombre = value; }
        public decimal Importe { get => base.Valor; init => base.Valor = value; }

        public override string ToString() => $"{CategoriaId:00} {Categoria} {Importe:00.00;;#}";
    }
}