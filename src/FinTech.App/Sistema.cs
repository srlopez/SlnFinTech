using System;
using System.Collections.Generic;
using System.Linq;
using FinTech.Models;
using FinTech.Data;


namespace FinTech
{
    public class Sistema
    {
        IRepoCategoria _catRepo; //Dependencia de datos
        IRepoApuntes _apRepo;
        ILog _log;
        public ReglasDeNegocio Validador; // Dependencia no incluida en el constructor

        #region Entidades de Negocio
        List<Categoria> _categorias = new() { };
        List<Apunte> _apuntes = new() { };
        #endregion
        public Sistema(IRepoCategoria catRepo, IRepoApuntes apRepo, ILog log)
        {
            _log = log;

            _catRepo = catRepo;
            _catRepo.Inicializar();
            //_categorias.AddRange(_catRepo.Cargar());
            _categorias = _catRepo.Cargar();

            _apRepo = apRepo;
            _apRepo.Inicializar();
            //_apuntes.AddRange(_apRepo.Cargar());
            _apuntes = _apRepo.Cargar();

            Validador = new ReglasDeNegocio();

            _log.Write("Aplicación iniciada...");
        }

        public List<Categoria> QryCategorias(int id = 0) => _categorias.Where(c => c.IdParent == id).ToList();
        public List<Apunte> QryApuntes() => _apuntes.ToList();
        public List<String> QryUsuarios() => _apuntes.Select(a => a.Usuario).Distinct().ToList();

        #region Categorias CRUD [Metodos]
        public void CmdRegistrarCategoria(Categoria cat)
        {
            _categorias.Add(cat);
            _categorias = _categorias.OrderBy(c => c.IdGlobal).ToList();
            _catRepo.Guardar(_categorias);
            _log.Write($"CmdRegistrarCategoria {cat}");

        }
        public void CmdUpdateCategoria(Categoria cat)
        {
            var oldCat = _categorias.Find(c => c.Id == cat.Id);
            if (oldCat is null) return;
            oldCat.Descripcion = cat.Descripcion;
            _categorias = _categorias.OrderBy(c => c.IdGlobal).ToList();
            _catRepo.Guardar(_categorias);
            _log.Write($"CmdUpdateCategoria {cat}");

        }
        public void CmdDeleteCategoria(Categoria cat)
        {
            _categorias.Remove(cat);
            _categorias = _categorias.OrderBy(c => c.IdGlobal).ToList();
            _catRepo.Guardar(_categorias);
            _log.Write($"CmdDeleteCategoria {cat}");


        }
        #endregion

        #region Apuntes CRUD [Metodos]
        public void CmdRegistrarApunte(Apunte gasto)
        {
            _apuntes.Add(gasto);
            _apuntes = _apuntes.OrderBy(a => a.FechaApunte).ToList();
            _apRepo.Guardar(_apuntes);
            _log.Write($"CmdRegistrarApunte {gasto}");
        }
        #endregion

        // Gastos X Cat + Inyección de predicado        
        public List<GastoPorCategoria> QryImporteDeGastoPorCategoria(Func<Apunte, bool> predicado = null, int id = 0)
            => QryImporteDeGastoPorCategoria(
                    predicado is null ? _apuntes : _apuntes.Where(predicado), id
                );
        private List<GastoPorCategoria> QryImporteDeGastoPorCategoria(IEnumerable<Apunte> apuntes, int id)
        {
            var grp = id switch
            {
                0 => apuntes.GroupBy(ap => ap.CategoriaId),
                _ => apuntes.Where(ap => ap.CategoriaId == id).GroupBy(ap => ap.SubCategoriaId),
            };

            return grp
                .Select(grp => new GastoPorCategoria
                {
                    CategoriaId = grp.Key,
                    Categoria = _categorias.FirstOrDefault(cat => cat.IdParent == id & cat.Id == grp.Key).Descripcion,
                    Importe = grp.Sum(ap => ap.Importe)
                })
                .OrderBy(imp => imp.CategoriaId)
                .ToList();
        }
    }
}
