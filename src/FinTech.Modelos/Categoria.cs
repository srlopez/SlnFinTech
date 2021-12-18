using System;
using System.Globalization;

namespace FinTech.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public int IdParent { get; set; }
        public string Descripcion { get; set; }
        public int IdGlobal { get => IdParent * 100 + Id; }

        public Categoria(int id, string descripcion) : this(0, id, descripcion) { }

        public Categoria(int idParent, int id, string descripcion)
        {
            if (idParent < 0 || idParent > 9) throw new ArgumentException($"Identificador #{idParent} no permitido para categoría");
            if (id < idParent * 10 || id > idParent * 10 + 9) throw new ArgumentException($"Identificador #{id} no permitido para subcategoría");
            Id = id;
            IdParent = idParent;
            Descripcion = descripcion.Replace(',', '_'); //<- No permito ,
        }

        public override string ToString() => $"{IdGlobal:0000} {Descripcion}";
        public static Categoria ParseRow(string row)
        {
            NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;

            var columns = row.Split(',');
            // IdParent, Id, Descripción
            return new Categoria(
                 Int32.Parse(columns[0].Trim(), nfi),
                 Int32.Parse(columns[1].Trim(), nfi),
                 columns[2]
            );
        }
        public string ToCSVRow() => $"{IdParent},{Id},{Descripcion}";
    }
}