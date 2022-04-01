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
            public int x;
            public int y;
            public int w;
            public int h;

            public Area(int x, int y, int w, int h)
            {
                this.x = x;
                this.y = y;
                this.w = w;
                this.h = h;
            }

            public String toString() => $"Area [{x},{y},{h},{w}]";
            
        }

        public static Dictionary<Area, ImporteCategoria> calcularAreas(Area area, IEnumerable<ImporteCategoria> list)
        {
            Dictionary<Area, ImporteCategoria> result = new Dictionary<Area, ImporteCategoria>();
            var listImportes = list.OrderByDescending(imp => imp.Importe);
            calcularAreaResult(area, listImportes, result);
            return result;
        }

        private static void calcularAreaResult(Area area, IEnumerable<ImporteCategoria> list, Dictionary<Area, ImporteCategoria> result)
        {
            // Manejar el fin de la recursividad
            if (list.Count() < 1)
                return;

            if (list.Count() == 1)
            {
                result[area]= list.First();
                return;
            }

            // Crear dos listas
            (var list1, var list2 ) = dividirLista(list);
            // Pintar los dos paneles
            Decimal sum = list.Sum(i=>i.Importe);
            Decimal sum1 = list1.Sum(i => i.Importe);
            // Horizontal o vertical
            int w = area.w;
            int h = area.h;
            int h1, h2, w1, w2;
            Area area1;
            Area area2;
            if (w < h)
            {
                // Vertical
                h1 = (int)(h * sum1 / sum);
                h2 = h - h1;
                area1 = new Area(area.x, area.y, w, h1);
                area2 = new Area(area.x, area.y + h1, w, h2);
            }
            else
            {
                // Horizontal
                w1 = (int)(w * sum1 / sum);
                w2 = w - w1;
                area1 = new Area(area.x, area.y, w1, h);
                area2 = new Area(area.x + w1, area.y, w2, h);
            }
            calcularAreaResult(area1, list1, result);
            calcularAreaResult(area2, list2, result);
        }

        private static (IEnumerable<ImporteCategoria>, IEnumerable<ImporteCategoria>) dividirLista(IEnumerable<ImporteCategoria> list)
        {
            var list1 = new List<ImporteCategoria>();
            var list2 = new List<ImporteCategoria>();

            Decimal sum1 = 0M;
            Decimal mitad = list.Sum(i => i.Importe) / 2;
            int i = 0;
            do
            {
                list1.Add(list.ElementAt(i));
                sum1 += list.ElementAt(i).Importe;
                i++;
            } while (sum1 + list.ElementAt(i).Importe < mitad);

            for (; i < list.Count(); i++)
                list2.Add(list.ElementAt(i));

            return (list1, list2);
        }

    }

}
