using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SudokuDeTodos.Models.Sistema
{
    public class Respuesta
    {
        public int Id { get; set; }
        public bool Status { get; set; }
        public string Descripcion { get; set; }
        public string Email { get; set; }
        public string Type { get; set; }
        public string Metodo { get; set; }
    }
}