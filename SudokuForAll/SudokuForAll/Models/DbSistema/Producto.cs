using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SudokuForAll.Models.DbSistema
{
    [Table("Producto")]
    public class Producto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "INT")]
        public int Id { get; set; }

        [Required]
        [Column(Order = 2, TypeName = "VARCHAR")]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Required]
        [Column(Order = 3, TypeName = "VARCHAR")]
        [Index(IsUnique = true)]
        [StringLength(10)]
        public string Codigo { get; set; }

        [Column(Order = 4, TypeName = "VARCHAR")]
        [StringLength(250)]
        public string Descripcion { get; set; }

        [Required]
        [Column(Order = 5, TypeName = "DATETIME")]
        public DateTime Fecha { get; set; }

        [Required]
        [Column(Order = 6, TypeName = "FLOAT")]
        public double Precio { get; set; }

        [Column(Order = 7, TypeName = "VARCHAR")]
        [StringLength(5)]
        public string Moneda { get; set; }

        [Required]
        [Column(Order = 8, TypeName = "FLOAT")]
        public double Impuesto { get; set; }

        [Column(Order = 9, TypeName = "BIT")]
        public bool Estatus { get; set; }

        [Required]
        [Column(Order = 10, TypeName = "DATETIME")]
        public DateTime FechaActualizacion { get; set; }

    }
}