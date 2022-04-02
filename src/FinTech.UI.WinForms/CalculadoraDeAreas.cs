using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FinTech.Models;


namespace FinTech.UI.WinForms
{

    public class CalculadoraDeAreas
    {
        public class Area
        {
            public int x { get; set; }
            public int y { get; set; }
            public int w { get; set; }
            public int h { get; set; }

            public String toString() => $"Area [{x},{y},{h},{w}]";
        }

        public static Dictionary<Area, ImporteCategoria> CalcularAreas(Area area, IEnumerable<ImporteCategoria> importes) =>
             CalcularSubAreas(
                area,
                // IMPORTANTE Ordenar descendentemente la lista
                importes.OrderByDescending(imp => imp.Importe)
             );

        private static Dictionary<Area, ImporteCategoria> CalcularSubAreas(Area area, IEnumerable<ImporteCategoria> importes)
        {
            var areas = new Dictionary<Area, ImporteCategoria>();

            switch (importes.Count())
            {
                case < 1:
                    break;
                case 1:
                    areas[area] = importes.First();
                    break;
                default:
                    var subLista = DividirListaImportes(importes);
                    var subArea = DividirArea(area, importes.Sum(i => i.Importe), subLista[0].Sum(i => i.Importe));

                    var a = CalcularSubAreas(subArea[0], subLista[0]);
                    var b = CalcularSubAreas(subArea[1], subLista[1]);

                    areas = areas.Concat(a).Concat(b).ToDictionary(x => x.Key, x => x.Value);
                    break;
            };

            return areas;
        }

        private static Area[] DividirArea(Area area, Decimal todo, Decimal parte)
        {
            var subArea = new Area[2];

            // Horizontal o Vertical
            if (area.w < area.h)
            {
                // Vertical
                var h1 = (int)(area.h * parte / todo);
                var h2 = area.h - h1;
                subArea[0] = new Area { x = area.x, y = area.y, w = area.w, h = h1 };
                subArea[1] = new Area { x = area.x, y = area.y + h1, w = area.w, h = h2 };
            }
            else
            {
                // Horizontal
                var w1 = (int)(area.w * parte / todo);
                var w2 = area.w - w1;
                subArea[0] = new Area { x = area.x, y = area.y, w = w1, h = area.h };
                subArea[1] = new Area { x = area.x + w1, y = area.y, w = w2, h = area.h };
            }
            return subArea;
        }
        private static List<ImporteCategoria>[] DividirListaImportes(IEnumerable<ImporteCategoria> importes)
        {
            var listas = new List<ImporteCategoria>[2] { new(), new() };

            decimal mitadImporteTotal = importes.Sum(i => i.Importe) / 2;
            decimal acumImporte = 0M;
            foreach (ImporteCategoria imp in importes)
            {
                var i = (acumImporte < mitadImporteTotal) ? 0 : 1;
                listas[i].Add(imp);
                acumImporte += imp.Importe;
            }
            return listas;
        }

    }

}
