using System.Collections.Generic;
using System.Linq;
using FinTech.Models;
using FinTech.Data;

namespace FinTech
{
    public class Sistema
    {
        IRepoCategoria _catRepo;
        IRepoApuntes _apRepo;
        List<Categoria> _categorias = new() { };
        List<Apunte> _apuntes = new() { };
        public ReglasDeNegocio Validador;

        public Sistema(IRepoCategoria catRepo, IRepoApuntes apRepo)
        {
            _catRepo = catRepo;
            _catRepo.Inicializar();
            _categorias.AddRange(_catRepo.Cargar());

            _apRepo = apRepo;
            _apRepo.Inicializar();
            _apuntes.AddRange(_apRepo.Cargar());

            Validador = new ReglasDeNegocio();
        }
        public List<Categoria> QryCategorias(int id = 0) => _categorias.Where(c => c.IdParent == id).ToList();
        public void CmdRegistrarCategoria(Categoria cat)
        {
            _categorias.Add(cat);
            _categorias = _categorias.OrderBy(c => c.IdGlobal).ToList();
            _catRepo.Guardar(_categorias);
        }
        public void CmdUpdateCategoria(Categoria cat)
        {
            var oldCat = _categorias.Find(c => c.Id == cat.Id);
            if (oldCat is null) return;
            oldCat.Descripcion = cat.Descripcion;
            _categorias = _categorias.OrderBy(c => c.IdGlobal).ToList();
            _catRepo.Guardar(_categorias);
        }
        public void CmdDeleteCategoria(Categoria cat)
        {
            _categorias.Remove(cat);
            _categorias = _categorias.OrderBy(c => c.IdGlobal).ToList();
            _catRepo.Guardar(_categorias);
        }

        public List<Apunte> QryApuntes() => _apuntes.ToList();
        public void CmdRegistrarApunte(Apunte dato)
        {
            _apuntes.Add(dato);
            _apuntes = _apuntes.ToList();
            _apRepo.Guardar(_apuntes);
        }
    }
}
