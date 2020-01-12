using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuDeTodos.Engine
{
    public interface IEngineGameProcess
    {
        void GuardarValoresInicio(string pathArchivo, string[,] valorInicio);
        void GuardarValoresSolucion(string pathArchivo, string[,] valorSolucion);
        void GuardarValoresEliminados(string pathArchivo, string[,] valorEliminado);
        void GuardarValoresIngresados(string pathArchivo, string[,] valorIngresado);
    }
}
