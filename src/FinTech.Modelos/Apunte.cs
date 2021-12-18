using System;
using System.Globalization;

namespace FinTech.Models
{
    public class Apunte
    {
        public DateTime FechaApunte { get; set; }
        public int CategoriaId { get; set; }
        public int SubCategoriaId { get; set; }
        public decimal Importe { get; set; }
        public string Usuario { get; set; }
        public string Detalle { get; set; }

        public Apunte(int categoriaId, int subCategoriaId, decimal importe, String user, String detalle)
            : this(DateTime.Now, categoriaId, subCategoriaId, importe, user, detalle) { }

        public Apunte(DateTime fh, int categoriaId, int subCategoriaId, decimal importe, String user, String detalle)
        {
            FechaApunte = fh;
            CategoriaId = categoriaId;
            SubCategoriaId = subCategoriaId;
            Importe = importe;
            Usuario = user;
            Detalle = detalle;
        }

        public override string ToString() =>
            $"{FechaApunte:dd-MM-yy H:mm} {Usuario,-8} {CategoriaId:00}/{SubCategoriaId:00} {Importe:#.##} {Detalle}";
        public static Apunte ParseCSVRow(string row)
        {
            NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;

            var columns = row.Split(',');
            // FechaApunte, Usuario, Categoria, SubCategoria
            // Importe, Detalle
            return new Apunte(
                DateTime.ParseExact(columns[0].Trim(), "dd-MM-yy H:mm",null),
                Int16.Parse(columns[2].Trim(), nfi),   //cat
                Int16.Parse(columns[3].Trim(), nfi),   //subcat
                Decimal.Parse(columns[4].Trim(), nfi), //importe
                columns[1], //usu
                columns[5]  //det
            );
        }
        public string ToCSVRow() => 
            $"{FechaApunte:dd-MM-yy H:mm},{Usuario},{CategoriaId},{SubCategoriaId},{Importe:0.00},{Detalle}";

    }
}