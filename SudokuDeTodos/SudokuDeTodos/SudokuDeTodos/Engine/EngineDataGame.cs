using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SudokuDeTodos.Engine
{
    public class EngineDataGame
    {
        private static EngineDataGame valor;
        public static EngineDataGame Instance()
        {
            if ((valor == null))
            {
                valor = new EngineDataGame();
            }
            return valor;
        }


        public const string Right = "Right";
        public const string Left = "Left";

        public static string Zero = "0";
        public const string uno = "1";
        public const string dos = "2";
        public const string tres = "3";
        public const string cuatro = "4";
        public const string cinco = "5";
        public const string seis = "6";
        public const string siete = "7";
        public const string ocho = "8";
        public const string nueve = "9";

        public const bool Falso = false;
        public const bool Verdadero = true;

        public string PathArchivo { get; set; }
        public string [,] valorIngresado { get;set; }
        public string[,] valorCandidato { get;set; } 
        public string[,] valorEliminado { get; set; }
        public string[,] valorCandidatoSinEliminados { get; set; }
        public string[,] valorInicio { get; set; }
        public string[,] valorSolucion { get; set; }

        //**************************************************************

        public int contadorIngresado  { get; set; }
        public int contadorCandidatos { get; set; }

        //*************************************************************

        private string[,] numFiltro = new string[9, 9];
        public string[,] GetNumFiltro()
        {
            return numFiltro;
        }
        public void SetNumFiltro(string[,] vFiltro)
        {
            this.numFiltro = new string[9, 9];
            this.numFiltro = vFiltro;
        }

        //*************************************************************
        private string nombreIdioma = string.Empty;

        public void SetNombreIdioma(string v) { nombreIdioma = v; }

        public string GetNombreIdioma()
        {
            return nombreIdioma;
        }
    }
}