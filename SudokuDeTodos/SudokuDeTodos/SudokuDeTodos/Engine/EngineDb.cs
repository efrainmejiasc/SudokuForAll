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
                        else if (C.EstatusEnvioNotificacion == true && C.FechaActivacion.AddHours(60) > DateTime.UtcNow)
                            return 2; //Cuenta activada con tiempo de prueba 
                        else if (C.EstatusEnvioNotificacion == true && C.FechaActivacion.AddHours(60) <= DateTime.UtcNow)
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
            int resultado = -2; //Error en la consulta
            Cliente cliente = new Cliente();
            ClientePago clientePago = new ClientePago();
            try
            {
                using (this.Context = new EngineContext())
                {
                    cliente = this.Context.Cliente.Where(s => s.Email == email).FirstOrDefault();
                    clientePago = this.Context.ClientePago.Where(x => x.IdCliente == cliente.Id).ToList().Last();
                    if (clientePago == null)
                        resultado = -1; //No existe ningun pago realizado
                    else if (clientePago.FechaVencimiento.AddHours(60) > DateTime.UtcNow)
                        resultado = 1; //Pago activo
                    else if (clientePago.FechaVencimiento.AddHours(60) <= DateTime.UtcNow)
                        resultado = 0; //Pago vencido
                }
            }
            catch (Exception ex)
            {
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString() + "*EngineDb/VerificarClientePago*" + email));
            }
            return resultado;
        }

        public bool InsertarCliente(Cliente model)
        {
            bool resultado = false;
            try
            {
                using (this.Context = new EngineContext())
                {
                    Context.Cliente.Add(model);
                    Context.SaveChanges();
                    resultado = true;
                }
            }
            catch (Exception ex)
            {
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString() + "*EngineDb/InsertarCliente*" + model.Email));
            }
            return resultado;
        }

        public Guid GetIdentidadCliente(string email)
        {
            Cliente C = new Cliente();
            try
            {
                using (this.Context = new EngineContext())
                {
                    C = Context.Cliente.Where(s => s.Email == email).FirstOrDefault();
                    if (C.Identidad != null)
                        return C.Identidad;
                    else
                        return Guid.Empty;
                }
            }
            catch (Exception ex)
            {
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString() + "*EngineDb/ObtenerIdentidadCliente*" + email));
            }
            return Guid.Empty;
        }

        public bool UpdateCliente(string email,int status)
        {
            bool resultado = false;
            if (status == 1)
                resultado = true;
            else
                resultado = false;
            Cliente C = new Cliente();
            try
            {
                using (this.Context = new EngineContext())
                {
                    C = Context.Cliente.Where(s => s.Email == email).FirstOrDefault();
                    Context.Cliente.Attach(C);
                    C.FechaActivacionPrueba = DateTime.UtcNow;
                    C.EstatusEnvioNotificacion = resultado;
                    Context.Configuration.ValidateOnSaveEnabled = false;
                    Context.SaveChanges();
                    resultado = true;
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString() + "*EngineDb/UpdateCliente*" + email));
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