using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SudokuDeTodos.Models.Game
{
    public class LetrasJuego
    {
        public bool BtnA { get; set; }
        public bool BtnB { get; set; }
        public bool BtnC { get; set; }
        public bool BtnBB { get; set; }
        public string ImagenUrl { get; set; }
        public int ContadorIngresado { get; set; }
        public bool ContadorActivado { get; set; }
        public LetrasJuegoACB LetrasJuegoACB { get; set; }
        public LetrasJuegoFEG LetrasJuegoFEG { get; set; }
        public string[,] ValorTxtSudoku2 { get; set; }
        public string[,] ValorTxtSudoku2Eliminado { get; set; }
    }
}