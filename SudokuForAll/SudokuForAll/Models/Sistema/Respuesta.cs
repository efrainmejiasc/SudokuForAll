using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SudokuForAll.Models.Sistema
{
    public class Respuesta
    {
        public int Id { get; set; }

        public string RespuestaAccion { get; set; }

        public string Email { get; set; }

        public string CodigoResetPassword { get; set; }

        public string Descripcion { get; set; }
    }
}