using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SudokuForAll.Models.DbSistema
{
    [Table("Cliente")]
    public class Cliente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "INT")]
        public int Id { get; set; }

        [Column(Order = 2, TypeName = "VARCHAR")]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Column(Order = 3, TypeName = "VARCHAR")]
        [StringLength(50)]
        public string Apellido { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [Column(Order = 4, TypeName = "VARCHAR")]
        [StringLength(50)]
        public string Email { get; set; }

        [Column(Order = 5, TypeName = "VARCHAR")]
        [StringLength(50)]
        public string Password { get; set; }

        [Column(Order = 6, TypeName = "DATETIME")]
        public DateTime FechaRegistroPrueba { get; set; }

        [Column(Order = 7, TypeName = "DATETIME")]
        public DateTime FechaActivacionPrueba { get; set; }

        [Column(Order = 8, TypeName = "DATETIME")]
        public DateTime FechaRegistro { get; set; }

        [Column(Order = 9, TypeName = "DATETIME")]
        public DateTime FechaActivacion { get; set; }

        [Column(Order = 10, TypeName = "BIT")]
        public bool Estatus { get; set; }

        [Column(Order = 11, TypeName = "BIT")]
        public bool EstatusEnvioNotificacion { get; set; }

        [Required]
        [Column(Order = 12, TypeName = "UNIQUEIDENTIFIER")]
        public Guid Identidad { get; set; }
    }
}