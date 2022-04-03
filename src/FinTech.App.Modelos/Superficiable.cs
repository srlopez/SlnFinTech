using System;
using System.Globalization;

namespace FinTech.Models
{
    public abstract class Superficiable
    {
        public  int Id { get; init; }
        public  string Nombre { get; set; }
        public  decimal Valor { get; set; }
    }
    public class GastoPorCategoria : Superficiable
    {
        public int CategoriaId { get => base.Id; init => base.Id=value; }
        public string Categoria { get => base.Nombre; init => base.Nombre = value; }
        public decimal Importe { get => base.Valor; init => base.Valor = value; }

        public override string ToString() => $"{CategoriaId:00} {Categoria} {Importe:00.00;;#}";
    }
}