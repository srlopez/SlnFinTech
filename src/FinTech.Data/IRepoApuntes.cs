using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

namespace FinTech.Data
{
    using FinTech.Models;

    public interface IRepoApuntes
    {
        void Inicializar();
        List<Apunte> Cargar();
        void Guardar(List<Apunte> apuntes);
    }
}