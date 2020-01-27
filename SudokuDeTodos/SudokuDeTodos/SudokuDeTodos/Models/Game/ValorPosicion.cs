using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SudokuDeTodos.Models.Game
{
    public class ValorPosicion
    {
        public int F { get; set; } // fila 

        public int C { get; set; } // columna

        public string  Id { get; set; }  // identidad textbox 

        public string Valor { get; set; } // valor de la posicion

    }
}