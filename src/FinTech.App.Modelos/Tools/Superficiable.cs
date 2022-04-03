using System;
using System.Globalization;

namespace FinTech.Tools
{
    public abstract class Superficiable
    {
        public  int Id { get; init; }
        public  string Nombre { get; set; }
        public  decimal Valor { get; set; }

        public override string ToString() => $"{Id:00} {Nombre} {Valor:00.00}";

    }
}