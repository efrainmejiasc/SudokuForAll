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

        private string[,] valorIngresado = new string[9, 9] ;
        public void SetValorIngresado(string [,] valor)
        {
            valorIngresado = valor;
        }
        public string[,] GetValorIngresado()
        {
            return (string[,])valorIngresado.Clone();
        }

        private string[,] valorEliminado = new string[9, 9];
        public void SetValorEliminado(string[,] valor)
        {
            valorEliminado = valor;
        }
        public string[,] GetValorEliminado()
        {
            return (string[,])valorEliminado.Clone();
        }
    }
}