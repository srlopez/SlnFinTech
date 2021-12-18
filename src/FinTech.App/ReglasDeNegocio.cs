/**
 * Requisitos Lógicos - NO FUNCIONALES
 * REGLAS DE NEGOCIO
 */

using System;
using FinTech.Models;

namespace FinTech
{
    public class ReglasDeNegocio
    {

        public  bool ValidarCategoriaDescripcion(Categoria categoria) =>
            rethrow((categoria.Descripcion.Contains("ER")),
            $"'ValidarCategoriaDescripcion' no se cumple que para '{categoria}'");

        public  bool ValidarCategoriaFechaRegistro()
        {
            int hora = DateTime.Now.Hour;
            return rethrow(false && (hora < 8 || hora > 15), $"'ValidarCategoriaFechaRegistro' fuera de horario {hora}");
        }

        private static bool rethrow(bool exprCierta, string msg)
        {
            if (exprCierta) throw new Exception(msg);
            return true;
        }

    }
}