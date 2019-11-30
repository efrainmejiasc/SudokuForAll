using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SudokuDeTodos.Engine
{
    public class EngineDb: IEngineDb
    {
        private static string cadenaConexion = ConfigurationManager.ConnectionStrings["CnxSudoku"].ToString();
        private readonly IEngineProyect Funcion;

        public EngineDb(IEngineProyect _Funcion)
        {
            this.Funcion = _Funcion;
        }
    }
}