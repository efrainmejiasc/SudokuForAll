using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SudokuDeTodos.Models.DbSistema
{
    [Table("Administrador")]
    public class Administrador
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "INT")]
        public int Id { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [Column(Order = 2, TypeName = "VARCHAR")]
        [StringLength(50)]
        public string Email { get; set; }

        [Column(Order = 3, TypeName = "BIT")]
        public bool Estado { get; set; }

        [Column(Order = 4, TypeName = "DATETIME")]
        public DateTime CreateDate { get; set; }

        [Column(Order = 5, TypeName = "VARCHAR")]
        [StringLength(50)]
        public string CreadorEmail { get; set; }

        [Column(Order = 6, TypeName = "INT")]
        public int Nivel { get; set; }
    }
}