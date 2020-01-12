using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SudokuDeTodos.Models.DbSistema
{
    [Table("ClientePago")]
    public class ClientePago
    {
        [Key]
        [Column(Order = 1, TypeName = "INT")]
        public int Id { get; set; }

        [Column(Order = 2, TypeName = "INT")]
        public int IdCliente { get; set; }

        [Column(Order = 3, TypeName = "DATETIME")]
        public DateTime FechaPago { get; set; }

        [Column(Order = 4, TypeName = "DATETIME")]
        public DateTime FechaVencimiento { get; set; }

        [Column(Order = 5, TypeName = "FLOAT")]
        public double MontoPago { get; set; }

        [Column(Order = 6, TypeName = "FLOAT")]
        public double Impuesto { get; set; }

        [Column(Order = 7, TypeName = "FLOAT")]
        public double MontoTotal { get; set; }
    }
}