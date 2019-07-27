using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SudokuForAll.Models.Sistema
{
    public class ActivarCliente
    {
        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Password2 { get; set; }

        public DateTime FechaRegistro { get; set; }

        public DateTime FechaActivacion { get; set; }

        public bool Estatus { get; set; }

        public Guid Identidad { get; set; }
    }
}