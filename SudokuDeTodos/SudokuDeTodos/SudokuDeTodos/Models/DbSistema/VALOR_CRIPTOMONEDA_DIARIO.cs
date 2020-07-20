using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SudokuDeTodos.Models.DbSistema
{
    [Table("VALOR_CRIPTOMONEDA_DIARIO")]
    public class VALOR_CRIPTOMONEDA_DIARIO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "INT")]
        public int ID { get; set; }

        [Column(Order = 2, TypeName = "VARCHAR")]
        [StringLength(50)]
        public string NOMBRECRIPTOMONEDA { get; set; }

        [Column(Order = 3, TypeName = "VARCHAR")]
        [StringLength(50)]
        public string  FECHA { get; set; }

        [Column(Order = 4, TypeName = "FLOAT")]
        public float VALORCRIPTOMONEDA { get; set; }

        [Column(Order = 5, TypeName = "VARCHAR")]
        [StringLength(50)]
        public string MONEDA { get; set; }
    }
}