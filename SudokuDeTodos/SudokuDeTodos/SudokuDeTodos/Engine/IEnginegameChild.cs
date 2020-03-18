using SudokuDeTodos.Models.Game;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuDeTodos.Engine
{
    public interface IEngineGameChild
    {
        void ReadValuesFile();
        List<TableTest> _ProcesosContables();
        string GetValorPosicion(string tipo, int f, int c);
        LetrasJuego _ContadorIngresado(bool contadorActivado, int numGrilla);
        string[,] SetearTextBoxNumero(string[,] cajaTexto, string[,] vIngresado);
        string[,] SetearTextBoxEliminado(string[,] cajaTexto, string[,] vEliminado);
        DataTable SetearTestC(string[,] vIngresado, string[] solo, string[] oculto);
        string[,] CandidatosSinEliminados(string[,] valorIngresado, string[,] valorCandidato, string[,] valorEliminado);
        string[,] ElejiblesInstantaneos(string[,] valorIngresado, string[,] valorCandidato);
        ArrayList AbrirValoresArchivo(string pathArchivo);
        string[,] SetValorIngresado(ArrayList arrText, string[,] valorIngresado);
        string[,] SetValorEliminado(ArrayList arrText, string[,] valorEliminado);
        string[,] SetValorInicio(ArrayList arrText, string[,] valorInicio);
        string[,] SetValorSolucion(ArrayList arrText, string[,] valorSolucion);
    }
}
