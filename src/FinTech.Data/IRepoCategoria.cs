using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

namespace FinTech.Data
{
    using FinTech.Models;

    public interface IRepoCategoria
    {
        void Inicializar();
        List<Categoria> Cargar();
        void Guardar(List<Categoria> categorias);
    }
}