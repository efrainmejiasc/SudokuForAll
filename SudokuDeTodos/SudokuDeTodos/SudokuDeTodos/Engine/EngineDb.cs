using SudokuDeTodos.Engine.Interfaces;
using SudokuDeTodos.Models.DbSistema;
using SudokuDeTodos.Models.Sistema;
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
            DateTime time = DateTime.UtcNow;
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
                    else if (C.Id >= 1 )
                    {
                        if (C.EstatusEnvioNotificacion == false)
                            return 1; //Cuenta de prueba no activada
                        else if (C.EstatusEnvioNotificacion == true && C.FechaActivacionPrueba.AddHours(60) > time)
                            return 2; //Cuenta activada con tiempo de prueba 
                        else if (C.EstatusEnvioNotificacion == true && C.FechaActivacionPrueba.AddHours(60) <= time)
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
                    clientePago = this.Context.ClientePago.Where(x => x.IdCliente == cliente.Id).ToList().LastOrDefault();
                    if (clientePago == null || clientePago.Id == 0)
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


        public bool Autentificacion(string password)
        {
            bool resultado = false; //Error en la consulta
            Cliente cliente = new Cliente();
            ClientePago clientePago = new ClientePago();
            try
            {
                using (this.Context = new EngineContext())
                {
                    cliente = this.Context.Cliente.Where(s => s.Password == password).FirstOrDefault();
                    clientePago = this.Context.ClientePago.Where(x => x.IdCliente == cliente.Id).ToList().LastOrDefault();
                    if (clientePago.Id > 0)
                        resultado = true; //Autenticado
                }
            }
            catch (Exception ex)
            {
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString() + "*EngineDb/Autentificacion*" + Funcion.DecodeBase64(password)));
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
                    if (C == null)
                        return Guid.Empty;
                    else if (C.Identidad != null)
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

        public bool UpdateClienteTest(string email,int status)
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
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString() + "*EngineDb/UpdateClienteTest*" + email));
            }
            return resultado;
        }


        public bool EditarClientePagoFechaVencimiento(int id, DateTime fechaVencimiento)
        {
            bool resultado = false;
            ClientePago C = new ClientePago();
            try
            {
                using (this.Context = new EngineContext())
                {
                    C = Context.ClientePago.Where(s => s.Id == id).FirstOrDefault();
                    Context.ClientePago.Attach(C);
                    C.FechaVencimiento = fechaVencimiento;
                    Context.Configuration.ValidateOnSaveEnabled = false;
                    Context.SaveChanges();
                    resultado = true;
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString() + "*EngineDb/EditarClientePagoFechaVencimiento*" + id.ToString()));
            }
            return resultado;
        }

        public bool UpdateClienteRegister(string email, int status)
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
                    C.FechaActivacion = DateTime.UtcNow;
                    C.Estatus = resultado;
                    Context.Configuration.ValidateOnSaveEnabled = false;
                    Context.SaveChanges();
                    resultado = true;
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString() + "*EngineDb/UpdateClienteRegister*" + email));
            }
            return resultado;
        }

        public int  UpdatePasswordCliente (string email,string password)
        {
            Cliente cliente = new Cliente();
            int idClient = 0;
            try
            {
                using (this.Context = new EngineContext())
                {
                    cliente = Context.Cliente.Where(s => s.Email == email).FirstOrDefault();
                    idClient = cliente.Id;
                    Context.Cliente.Attach(cliente);
                    cliente.Password = password;
                    cliente.FechaRegistro = DateTime.UtcNow;
                    Context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString() + "*EngineDb/UpdatePasswordCliente*" + email));
            }
            return idClient;
        }

        public bool UpdatePassword(string email, string password)
        {
            Cliente cliente = new Cliente();
            bool resultado = false;
            try
            {
                using (this.Context = new EngineContext())
                {
                    cliente = Context.Cliente.Where(s => s.Email == email).FirstOrDefault();
                    Context.Cliente.Attach(cliente);
                    cliente.Password = password;
                    Context.SaveChanges();
                    resultado = true;
                }
            }
            catch (Exception ex)
            {
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString() + "*EngineDb/UpdatePassword*" + email));
            }
            return resultado;
        }

        public bool InsertarClientePago(ClientePago model)
        {
            bool resultado = false;
            try 
            {
                using (this.Context = new EngineContext())
                {
                    Context.ClientePago.Add(model);
                    Context.SaveChanges();
                    resultado = true;
                }
            }
            catch (Exception ex)
            {
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString() + "*EngineDb/InsertarClientePago*" + model.IdCliente.ToString()));
            }
            return resultado;
        }

        public bool InsertarCodigoResetPassword(ResetPassword model)
        {
            bool resultado = false;
            try
            {
                using (this.Context = new EngineContext())
                {
                    Context.ResetPassword.Add(model);
                    Context.SaveChanges();
                    resultado = true;
                }
            }
            catch (Exception ex)
            {
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString() + "*EngineDb/InsertarClientePago*" + model.Email));
            }
            return resultado;
        }

        public bool ValidarCodigoResetPassword (string email , string codigo)
        {
            ResetPassword C = new ResetPassword();
            try
            {
                using (this.Context = new EngineContext())
                {
                    C = Context.ResetPassword.Where(s => s.Email == email && s.Codigo == codigo).FirstOrDefault();
                    if (C == null)
                        return false;
                    else if (C.Estatus == false)
                        return true;
                    else if (C.Estatus == true)
                        return false;
                }
            }
            catch (Exception ex)
            {
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString() + "*EngineDb/ValidarCodigoResetPassword*" + email));
            }
            return false;
        }

        public bool DeleteCodigoResetPassword(string email)
        {
            List<ResetPassword> C = new List<ResetPassword>();
            try
            {
                using (this.Context = new EngineContext())
                {
                   Context.ResetPassword.RemoveRange(Context.ResetPassword.Where(s => s.Email == email && s.Estatus == false));
                   Context.SaveChanges();
                   return true;
                }
            }
            catch (Exception ex)
            {
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString() + "*EngineDb/DeleteCodigoResetPassword*" + email));
            }
            return false;
        }

        public bool UpdateCodigoResetPassword(string email, string codigo)
        {
            ResetPassword model = new ResetPassword();
            bool resultado = false;
            try
            {
                using (this.Context = new EngineContext())
                {
                    model = Context.ResetPassword.Where(s => s.Email == email && s.Codigo == codigo && s.Estatus == false).FirstOrDefault();
                    Context.ResetPassword.Attach(model);
                    model.Estatus = true;
                    Context.SaveChanges();
                    resultado = true;
                }
            }
            catch (Exception ex)
            {
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString() + "*EngineDb/UpdateCodigoResetPassword*" + email));
            }
            return resultado;
        }

        public List<ConsultaReporte> ConsultaReporte(DateTime fechaInicial , DateTime fechaFinal)
        {
            List<ConsultaReporte> consulta = new List<ConsultaReporte>();
            try
            {
                using (this.Context = new EngineContext())
                {

                    consulta = (from Cliente in Context.Cliente
                              join ClientePago in Context.ClientePago
                              on Cliente.Id equals ClientePago.IdCliente
                              where ClientePago.FechaPago >= fechaInicial && ClientePago.FechaPago <= fechaFinal
                              select new ConsultaReporte()
                              {
                                  Id = Cliente.Id,
                                  IdClientePago = ClientePago.Id,
                                  Email = Cliente.Email,
                                  FP = ClientePago.FechaPago,
                                  FV = ClientePago.FechaVencimiento,
                                  MontoPago = ClientePago.MontoPago,
                                  Impuesto = ClientePago.Impuesto,
                                  MontoTotal =ClientePago.MontoTotal
                              }).ToList();
                }

                foreach (ConsultaReporte item in consulta)
                {
                    if (DateTime.UtcNow > item.FV)
                        item.Estado = "Vencido";
                    else
                        item.Estado = "Activo";

                    item.FechaPago = item.FP.ToString("dd/MM/yyyy");
                    item.FechaVencimiento = item.FV.ToString("dd/MM/yyyy");
                }
            }
            catch (Exception ex)
            {
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString() + "*EngineDb/ConsultaReporte*" + ""));
            }
            return consulta ;
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