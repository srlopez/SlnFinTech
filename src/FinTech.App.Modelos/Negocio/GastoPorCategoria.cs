using System;
using System.Globalization;

namespace FinTech.Models
{
    public class GastoPorCategoria : FinTech.Tools.Superficiable
    {
        public int CategoriaId { get => base.Id; init => base.Id=value; }
        public string Categoria { get => base.Nombre; init => base.Nombre = value; }
        public decimal Importe { get => base.Valor; init => base.Valor = value; }

        public override string ToString() => String.Format("{0,2:00} {1,-21} {2,8:00.00;;#}€",CategoriaId,Categoria,Importe);
    }
}