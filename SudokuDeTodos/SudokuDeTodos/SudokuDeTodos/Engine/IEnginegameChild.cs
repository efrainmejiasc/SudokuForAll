using SudokuDeTodos.Models.Game;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuDeTodos.Engine
{
    public interface IEngineGameChild
    {
        void ReadValuesFile();
        string GetValorPosicion(string tipo, int f, int c);
        LetrasJuego _ContadorIngresado(bool contadorActivado, int numGrilla);
        string[,] SetearTextBoxNumero(string[,] cajaTexto, string[,] vIngresado);
        string[,] SetearTextBoxEliminado(string[,] cajaTexto, string[,] vEliminado);
    }
}
