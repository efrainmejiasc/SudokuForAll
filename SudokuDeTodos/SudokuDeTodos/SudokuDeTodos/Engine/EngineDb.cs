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
                        return 0; //Cuenta no existente
                    }
                    else if (C.Id >=1 )
                    {
                        if (C.EstatusEnvioNotificacion == false)
                            return 1; //Cuenta de prueba no activada
                        else if (C.EstatusEnvioNotificacion == true && C.FechaActivacion.AddHours(36) < DateTime.UtcNow)
                            return 2; //Cuenta activada con tiempo de prueba
                        else if (C.EstatusEnvioNotificacion == true && C.FechaActivacion.AddHours(36) >= DateTime.UtcNow)
                            return 3; //Cuenta activada sin tiempo de prueba 
                    }
                }
            }
            catch (Exception ex)
            {
              InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString() + "*EngineDb/ObtenerIdCliente*" + email));
            }
            return 0;
        }

        public int VerificarClientePago(string email)
        {
            ClientePago clientePago = new ClientePago();
            int resultado = -2;
            try
            {
                using (this.Context = new EngineContext())
                {
                   var r = (from cp in Context.ClientePago
                                   join c in Context.Cliente on cp.IdCliente equals c.Id
                                   where c.Email == email && cp.IdCliente == (Context.ClientePago.Max(x => x.Id))
                                   select new
                                   {
                                    IdCliente =  cp.IdCliente,
                                    FechaPago =  cp.FechaPago,
                                    FechaVencimiento = cp.FechaVencimiento,
                                    MontoPago = cp.MontoPago,
                                    Impuesto =  cp.Impuesto,
                                    MontoTotal = cp.MontoTotal
                                   }).FirstOrDefault(); 
                }
                if (clientePago == null)
                    resultado = -1 ;
                else if (clientePago.FechaVencimiento.AddHours(24) >= DateTime.UtcNow)
                    resultado = 0;
                else if (clientePago.FechaVencimiento.AddHours(24) < DateTime.UtcNow)
                    resultado = 1;

            }
            catch (Exception ex)
            {
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString() + "*EngineDb/VerificarClientePago*" + email));
            }
            return resultado;
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