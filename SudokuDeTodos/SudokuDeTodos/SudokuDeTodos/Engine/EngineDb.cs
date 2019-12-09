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
        private  EngineContext Context;

        public EngineDb(IEngineProyect _Funcion)
        {
            this.Funcion = _Funcion;
        }

        public int VerificarEmail (string email)
        {
            Cliente C = new Cliente();
            try
            {
                using (this.Context = new EngineContext())
                {
                    C = Context.Cliente.Where(s => s.Email == email).FirstOrDefault();
                    if (C == null)
                    {
                        return 0;
                    }
                    else if (C.Id >=1 )
                    {
                        if (C.EstatusEnvioNotificacion == false)
                            return 1;
                        else if (C.EstatusEnvioNotificacion == true && C.FechaActivacion.AddDays(3) < DateTime.UtcNow)
                            return 2;
                        else if (C.EstatusEnvioNotificacion == true && C.FechaActivacion.AddDays(3) > DateTime.UtcNow)
                            return 3;
                    }
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
                using (this.Context = new EngineContext())
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