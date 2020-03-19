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
        ArrayList AbrirValoresArchivo(string pathArchivo);
        string GetValorPosicion(string tipo, int f, int c);
        string[,] SetValorInicio(ArrayList arrText, string[,] valorInicio);
        LetrasJuego _ContadorIngresado(bool contadorActivado, int numGrilla);
        string[,] SetValorSolucion(ArrayList arrText, string[,] valorSolucion);
        string[,] SetValorIngresado(ArrayList arrText, string[,] valorIngresado);
        string[,] SetValorEliminado(ArrayList arrText, string[,] valorEliminado);
        string[,] SetearTextBoxNumero(string[,] cajaTexto, string[,] vIngresado);
        string[,] SetearTextBoxEliminado(string[,] cajaTexto, string[,] vEliminado);
        DataTable SetearTestC(string[,] vIngresado, string[] solo, string[] oculto);
        string[,] ElejiblesInstantaneos(string[,] valorIngresado, string[,] valorCandidato);
        string[,] CandidatosSinEliminados(string[,] valorIngresado, string[,] valorCandidato, string[,] valorEliminado);
    }
}
