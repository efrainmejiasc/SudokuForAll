using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SudokuDeTodos.Models.Game
{
    public class LetrasJuego
    {
        public int ContadorIngresado { get; set; } 
        public LetrasJuegoACB LetrasJuegoACB { get; set; }

        public LetrasJuegoFEG LetrasJuegoFEG { get; set; }
    }
}