using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SudokuDeTodos.Models.DbSistema
{
    [Table("TransaccionPaypal")]
    public class TransaccionPaypal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "INT")]
        public int Id { get; set; }

        [Required]
        [Column(Order = 2, TypeName = "VARCHAR")]
        [StringLength(8000)]
        public string Descripcion { get; set; }

        [Required]
        [Column(Order = 3, TypeName = "DATETIME")]
        public DateTime Fecha { get; set; }

    }
}