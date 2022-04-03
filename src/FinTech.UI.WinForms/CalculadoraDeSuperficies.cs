using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FinTech.Models;


namespace FinTech.UI.WinForms
{

    public class CalculadoraDeSuperficies<T> where T : Superficiable

    {
        public record Area
        {
            public int x { get; set; }
            public int y { get; set; }
            public int w { get; set; }
            public int h { get; set; }

            public String toString() => $"Area [{x},{y},{h},{w}]";
        }

        public static Dictionary<Area, T> CalcularAreas(Area area, IEnumerable<T> datos) =>
             CalcularSubAreas(
                area,
                // IMPORTANTE Ordenar descendentemente la lista
                datos.OrderByDescending(i => i.Valor)
             );
        private static Dictionary<Area, T> CalcularSubAreas(Area area, IEnumerable<T> datos)
        {
            var areas = new Dictionary<Area, T>();

            switch (datos.Count())
            {
                case < 1:
                    break;
                case 1:
                    areas[area] = datos.First();
                    break;
                default:
                    var subLista = DividirListaValores(datos);
                    var subArea = DividirArea(area, subLista[0].Sum(i => i.Valor), subLista[1].Sum(i => i.Valor));

                    var a = CalcularSubAreas(subArea[0], subLista[0]);
                    var b = CalcularSubAreas(subArea[1], subLista[1]);

                    areas = areas.Concat(a).Concat(b).ToDictionary(x => x.Key, x => x.Value);
                    break;
            };

            return areas;
        }
        private static Area[] DividirArea(Area area, Decimal a, Decimal b)
        {
            var subArea = new Area[2];

            if (area.w < area.h)
            {
                // Proporción en Vertical
                var ha = (int)(area.h * a / (a + b));
                var hb = area.h - ha;
                subArea[0] = area with { h = ha };
                subArea[1] = area with { h = hb, y = area.y + ha };
            }
            else
            {
                // Proporción en Horizontal
                var wa = (int)(area.w * a / (a + b));
                var wb = area.w - wa;
                subArea[0] = area with { w = wa };
                subArea[1] = area with { w = wb, x = area.x + wa };
            }
            return subArea;
        }
        private static List<T>[] DividirListaValores(IEnumerable<T> datos)
        {
            var listas = new List<T>[2] { new(), new() };

            decimal puntoDeCorte = datos.Sum(i => i.Valor) / 2;
            decimal valorAcumulado = 0M;
            foreach (T dato in datos)
            {
                var i = (valorAcumulado < puntoDeCorte) ? 0 : 1;
                listas[i].Add(dato);
                valorAcumulado += dato.Valor;
            }
            return listas;
        }

    }

}
