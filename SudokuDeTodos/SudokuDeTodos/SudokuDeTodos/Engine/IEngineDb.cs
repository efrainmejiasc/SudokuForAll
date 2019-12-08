using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SudokuDeTodos.Engine
{
    public interface  IEngineDb
    {
        int ObtenerIdCliente(string email);
    }
}