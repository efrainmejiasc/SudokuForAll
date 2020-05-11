using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SudokuDeTodos.Models.Sistema
{
    public class Producto
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public double Precio { get; set; }
        public double Impuesto { get; set; }
        public string Moneda { get; set; }
        public string FechaActivacion { get; set; }
        public string FechaExpiracion { get; set; }
        public double Total { get; set; }
    }
}