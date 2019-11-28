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

        public static string Zero= "0";
        public const string Right = "Right";
        public const string Left = "Left";

        public string [,] valorIngresado { get;set; }
        public string[,] valorCandidato { get;set; } 
        public string[,] valorEliminado { get; set; }
        public string[,] valorCandidatoSinEliminados { get; set; }
        public string[,] valorInicio { get; set; }
        public string[,] valorSolucion { get; set; }
    }
}