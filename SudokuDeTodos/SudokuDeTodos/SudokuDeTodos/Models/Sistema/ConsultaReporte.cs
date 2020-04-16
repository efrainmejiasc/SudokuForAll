using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SudokuDeTodos.Models.Sistema
{
    public class ConsultaReporte
    {
        public int Id { get; set; }
        public int IdClientePago { get; set; }
        public string Email { get; set; }
        public string FechaPago { get; set; }
        public string FechaVencimiento { get; set; }
        public DateTime FP { get; set; }
        public DateTime FV { get; set; }
        public double MontoPago { get; set; }
        public double Impuesto { get; set; }
        public double MontoTotal { get; set; }
        public string Estado { get; set; }
    }
}