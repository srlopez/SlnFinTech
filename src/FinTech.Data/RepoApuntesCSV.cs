using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

//#nullable enable

namespace FinTech.Data
{
    using FinTech.Models;

    public class RepoApuntesCSV : IRepoApuntes
    {
        private string _file;

        void IRepoApuntes.Inicializar()
        {
            // TODO: Servicio de configuracion 
            var config = AppServicios.Create<AppConfig>().Get();
            _file = config.DataPath + "apuntes.csv";
        }
        List<Apunte> IRepoApuntes.Cargar()
        {
            // TODO: Verificar que el archivo existe
            return File.ReadAllLines(_file)
                .Skip(1)
                .Where(row => row.Length > 0)
                .Select(i => Apunte.ParseCSVRow(i)).ToList();
        }
        void IRepoApuntes.Guardar(List<Apunte> datos)
        {
            var lines = new List<string> { "FECHA, USUARIO, CAT, SUBCAT, IMPORTE, DETALLE" };
            lines.AddRange(datos.Select(i => i.ToCSVRow()));

            File.WriteAllLines(_file, lines);
        }

    }
}