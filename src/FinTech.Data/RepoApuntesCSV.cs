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
        private string _dataPath;
        private string _file;

        void IRepoApuntes.Inicializar()
        {
            _dataPath = "../../Data/";
            _file = _dataPath + "apuntes.csv";
        }
        List<Apunte> IRepoApuntes.Cargar()
        {
            return File.ReadAllLines(_file)
                .Skip(1)
                .Where(row => row.Length > 0)
                .Select(i => Apunte.ParseRow(i)).ToList();
        }
        void IRepoApuntes.Guardar(List<Apunte> datos)
        {
            var lines = new List<string> { "FECHA, USUARIO, CAT, SUBCAT, IMPORTE, DETALLE" };
            lines.AddRange(datos.Select(i => i.ToCSVRow()));

            File.WriteAllLines(_file, lines);
        }

    }
}