using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SudokuForAll.Models.DbSistema
{
    [Table("ResetPassword")]
    public class ResetPassword
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "INT")]
        public int Id { get; set; }

        [Required]
        [Column(Order = 2, TypeName = "VARCHAR")]
        [StringLength(50)]
        public string Email { get; set; }

        [Column(Order = 3, TypeName = "VARCHAR")]
        [StringLength(10)]
        public string Codigo { get; set; }

        [Column(Order = 4, TypeName = "DATETIME")]
        public DateTime Fecha { get; set; }

        [Column(Order = 5, TypeName = "BIT")]
        public bool Estatus { get; set; }
    }
}