using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SudokuForAll.Models.DbSistema
{
    [Table("Gerente")]
    public class Gerente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "INT")]
        public int Id { get; set; }

        [Column(Order = 2, TypeName = "VARCHAR")]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Column(Order = 3, TypeName = "VARCHAR")]
        [StringLength(50)]
        public string NombreUsuario { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [Column(Order = 4, TypeName = "VARCHAR")]
        [StringLength(50)]
        public string Email { get; set; }

        [Column(Order = 5, TypeName = "VARCHAR")]
        [StringLength(100)]
        public string Password { get; set; }

        [Column(Order = 6, TypeName = "VARCHAR")]
        [StringLength(50)]
        public string Rol { get; set; }

        [Column(Order = 7, TypeName = "DATETIME")]
        public DateTime FechaRegistro{ get; set; }

        [Column(Order = 8, TypeName = "DATETIME")]
        public DateTime FechaActualizacion { get; set; }

        [Column(Order = 9, TypeName = "BIT")]
        public bool Estatus { get; set; }

        [Required]
        [Column(Order = 10, TypeName = "UNIQUEIDENTIFIER")]
        public Guid Identidad { get; set; }
    }
}