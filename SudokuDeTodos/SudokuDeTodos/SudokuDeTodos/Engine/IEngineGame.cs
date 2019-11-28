using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuDeTodos.Engine
{
    public interface IEngineGame
    {
        TextBox[,] SetearTextBoxJuegoInicio(TextBox[,] cajaTexto, string[,] vIngresado, string[,] vInicio);
    }
}
