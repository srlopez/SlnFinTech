using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Configuration;
using System.Collections.Specialized;

namespace FinTech.Data
{
    using FinTech.Models;
    public class RepoCategoriaCSV : IRepoCategoria
    {
        private string _dataPath;
        private string _file;

        void IRepoCategoria.Inicializar()
        {
            // TODO: Parametizar 
            _dataPath = "../../data/";
            _file = _dataPath + "categorias.csv";
        }
        List<Categoria> IRepoCategoria.Cargar()
        {
            // TODO: Verificar que el archivo existe
            return File.ReadAllLines(_file)
                .Skip(1)
                .Where(row => row.Length > 0)
                .Select(i => Categoria.ParseRow(i)).ToList();
        }
        void IRepoCategoria.Guardar(List<Categoria> categorias)
        {
            var lines = new List<string> { "ID,SUBID,DESCRIPCION" };
            lines.AddRange(categorias.Select(i => i.ToCSVRow()));

            File.WriteAllLines(_file, lines);
        }

    }
}