using SudokuDeTodos.Models.DbSistema;
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
        private readonly EngineContext Context;

        public EngineDb(IEngineProyect _Funcion,EngineContext _Context)
        {
            this.Funcion = _Funcion;
            this.Context = _Context;
        }

        public int ObtenerIdCliente(string email)
        {
            Cliente C = new Cliente();
            try
            {
                using (this.Context)
                {
                    C = Context.Cliente.Where(s => s.Email == email).FirstOrDefault();
                    if (C != null && C.Id > 0)
                        return C.Id;
                }
            }
            catch (Exception ex)
            {
              InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString() + "*EngineDb/ObtenerIdCliente*" + email));
            }
            return 0;
        }

        public bool InsertarSucesoLog(SucesoLog model)
        {
            bool resultado = false;
            try
            {
                using (this.Context)
                {
                    Context.SucesoLog.Add(model);
                    Context.SaveChanges();
                    resultado = true;
                }
            }
            catch { }
            return resultado;
        }
    }
}