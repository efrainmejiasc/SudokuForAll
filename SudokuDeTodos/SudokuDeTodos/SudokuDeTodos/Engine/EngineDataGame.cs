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

        public const string Btn1 = "btn1";
        public const string Btn2 = "btn2";
        public const string Btn3 = "btn3";
        public const string Btn4 = "btn4";
        public const string Btn5 = "btn5";
        public const string Btn6 = "btn6";
        public const string Btn7 = "btn7";
        public const string Btn8 = "btn8";
        public const string Btn9 = "btn9";

        public const string eliminar = "btnEE";
        public const string restablecer = "btnRR";

        public const string btnIzquierda = "btnIzquierda";
        public const string btnDerecha = "btnDerecha";

        public const string BtnRes23 = "btnRes23";
        public const string BtnDos = "btnDos";
        public const string BtnTres = "btnTres";
        public const string BtnN = "btnN";

        public const bool Falso = false;
        public const bool Verdadero = true;

        public string PathArchivo { get; set; }
        public string PathSource { get; set; }
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

        private string[] f1 = { "11", "12", "13", "14", "15", "16", "17", "18", "19" };
        private string[] f2 = { "21", "22", "23", "24", "25", "26", "27", "28", "29" };
        private string[] f3 = { "31", "32", "33", "34", "35", "36", "37", "38", "39" };
        private string[] f4 = { "41", "42", "43", "44", "45", "46", "47", "48", "49" };
        private string[] f5 = { "51", "52", "53", "54", "55", "56", "57", "58", "59" };
        private string[] f6 = { "61", "62", "63", "64", "65", "66", "67", "68", "69" };
        private string[] f7 = { "71", "72", "73", "74", "75", "76", "77", "78", "79" };
        private string[] f8 = { "81", "82", "83", "84", "85", "86", "87", "88", "89" };
        private string[] f9 = { "91", "92", "93", "94", "95", "96", "97", "98", "99" };

        public string[] GetNumeroFila(int f)
        {
            string[] fila = new string[9];
            switch (f)
            {
                case (0):
                    fila = f1;
                    break;
                case (1):
                    fila = f2;
                    break;
                case (2):
                    fila = f3;
                    break;
                case (3):
                    fila = f4;
                    break;
                case (4):
                    fila = f5;
                    break;
                case (5):
                    fila = f6;
                    break;
                case (6):
                    fila = f7;
                    break;
                case (7):
                    fila = f8;
                    break;
                case (8):
                    fila = f9;
                    break;
            }
            return fila;
        }

        private string[] c1 = { "11", "21", "31", "41", "51", "61", "71", "81", "91" };
        private string[] c2 = { "12", "22", "32", "42", "52", "62", "72", "82", "92" };
        private string[] c3 = { "13", "23", "33", "43", "53", "63", "73", "83", "93" };
        private string[] c4 = { "14", "24", "34", "44", "54", "64", "74", "84", "94" };
        private string[] c5 = { "15", "25", "35", "45", "55", "65", "75", "85", "95" };
        private string[] c6 = { "16", "26", "36", "46", "56", "66", "76", "86", "96" };
        private string[] c7 = { "17", "27", "37", "47", "57", "67", "77", "87", "97" };
        private string[] c8 = { "18", "28", "38", "48", "58", "68", "78", "88", "98" };
        private string[] c9 = { "19", "29", "39", "49", "59", "69", "79", "89", "99" };

        public string[] GetNumeroColumna(int c)
        {
            string[] columna = new string[9];
            switch (c)
            {
                case (0):
                    columna = c1;
                    break;
                case (1):
                    columna = c2;
                    break;
                case (2):
                    columna = c3;
                    break;
                case (3):
                    columna = c4;
                    break;
                case (4):
                    columna = c5;
                    break;
                case (5):
                    columna = c6;
                    break;
                case (6):
                    columna = c7;
                    break;
                case (7):
                    columna = c8;
                    break;
                case (8):
                    columna = c9;
                    break;
            }
            return columna;
        }

        private string[] r1 = { "11", "12", "13", "21", "22", "23", "31", "32", "33" };
        private string[] r2 = { "14", "15", "16", "24", "25", "26", "34", "35", "36" };
        private string[] r3 = { "17", "18", "19", "27", "28", "29", "37", "38", "39" };
        private string[] r4 = { "41", "42", "43", "51", "52", "53", "61", "62", "63" };
        private string[] r5 = { "44", "45", "46", "54", "55", "56", "64", "65", "66" };
        private string[] r6 = { "47", "48", "49", "57", "58", "59", "67", "68", "69" };
        private string[] r7 = { "71", "72", "73", "81", "82", "83", "91", "92", "93" };
        private string[] r8 = { "74", "75", "76", "84", "85", "86", "94", "95", "96" };
        private string[] r9 = { "77", "78", "79", "87", "88", "89", "97", "98", "99" };

        public string[] GetNumeroRecuadro(int r)
        {
            string[] recuadro = new string[9];
            switch (r)
            {
                case (0):
                    recuadro = r1;
                    break;
                case (1):
                    recuadro = r2;
                    break;
                case (2):
                    recuadro = r3;
                    break;
                case (3):
                    recuadro = r4;
                    break;
                case (4):
                    recuadro = r5;
                    break;
                case (5):
                    recuadro = r6;
                    break;
                case (6):
                    recuadro = r7;
                    break;
                case (7):
                    recuadro = r8;
                    break;
                case (8):
                    recuadro = r9;
                    break;
            }
            return recuadro;
        }

    }
}