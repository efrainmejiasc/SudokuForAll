using SudokuForAll.Models;
using SudokuForAll.Models.DbSistema;
using SudokuForAll.Models.Sistema;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SudokuForAll.Engine
{
    public class EngineDb : IEngineDb
    {
        private static string cadenaConexion = ConfigurationManager.ConnectionStrings["CnxSudoku"].ToString();
        private readonly IEngineProyect Funcion;

        public EngineDb( IEngineProyect _Funcion)
        {
            this.Funcion = _Funcion;
        }


        public int ResultadoEntradaAlSitio(string email)
        {
            object obj = new object();
            int resultado = 0;
            SqlConnection Conexion = new SqlConnection(cadenaConexion);
            try
            {
                using (Conexion)
                {
                    Conexion.Open();
                    SqlCommand command = new SqlCommand("Sp_GetResultToIntro", Conexion);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@Email", email);
                    obj = command.ExecuteScalar();
                    Conexion.Close();
                }
                if (obj != DBNull.Value && obj != null)
                {
                    resultado = Convert.ToInt32(obj);
                }
            }
            catch(Exception ex)
            {
                Conexion.Close();
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString().Substring(0,300) + "*EngineDb/ResultadoEntradaAlSitio*" + email));
            }
            return resultado;
        }

        public int ResultadoLogin(string password)
        {
            object obj = new object();
            int resultado = 0;
            SqlConnection Conexion = new SqlConnection(cadenaConexion);
            try
            {
                using (Conexion)
                {
                    Conexion.Open();
                    SqlCommand command = new SqlCommand("Sp_LoginCliente", Conexion);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@Password", password);
                    obj = command.ExecuteScalar();
                    Conexion.Close();
                }
                if (obj != DBNull.Value && obj != null)
                {
                    resultado = Convert.ToInt32(obj);
                }
            }
            catch (Exception ex)
            {
                Conexion.Close();
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString().Substring(0,300) + "*EngineDb/ResultadoLogin*" + Funcion.DecodeBase64(password)));
            }
            return resultado;
        }

        public bool InsertarClienteTest(IEngineProyect Funcion,string email)
        {
            bool resultado = false;
            Cliente model = new Cliente();
            model = Funcion.ConstruirInsertarClienteTest(email);
            using (SudokuContext Context = new SudokuContext())
            {
                try
                {
                    Context.Cliente.Add(model);
                    Context.SaveChanges();
                    resultado = true;
                }
                catch (Exception ex)
                {
                    InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString().Substring(0,300) + "*EngineDb/InsertarClienteTest*" + email));
                }
            };
            return resultado;
        }

        public int UpdateClienteTest(Cliente model)
        {
            int resultado = -1;
            object obj = new object();
            SqlConnection Conexion = new SqlConnection(cadenaConexion);
            SqlCommand command = new SqlCommand("Sp_PutCliente", Conexion);
            try
            {
                using (Conexion)
                {
                    Conexion.Open();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@FechaActivacionPrueba", model.FechaActivacionPrueba);
                    command.Parameters.AddWithValue("@Email", model.Email);
                    command.Parameters.AddWithValue("@EstatusEnvioNotificacion", model.EstatusEnvioNotificacion);
                    command.Parameters.AddWithValue("@Identidad", model.Identidad);
                    obj = command.ExecuteScalar();
                    Conexion.Close();
                }
                if (obj != DBNull.Value && obj != null)
                {
                    resultado = Convert.ToInt32(obj);
                }
            }
            catch (Exception ex)
            {
                Conexion.Close();
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString().Substring(0,300) + "*EngineDb/UpdateClienteTest*" + model.Email));
            }
            return resultado;
        }

        public int ClienteRegistro(ActivarCliente model)
        {
            int resultado = -1;
            object obj = new object();
            SqlConnection Conexion = new SqlConnection(cadenaConexion);
            SqlCommand command = new SqlCommand("Sp_PutRegistrarCliente", Conexion);
            try
            {
                using (Conexion)
                {
                    Conexion.Open();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@Nombre", model.Nombre);
                    command.Parameters.AddWithValue("@Apellido", model.Apellido);
                    command.Parameters.AddWithValue("@Email", model.Email);
                    command.Parameters.AddWithValue("@Password", model.Password);
                    command.Parameters.AddWithValue("@FechaRegistro", model.FechaRegistro);
                    command.Parameters.AddWithValue("@Estatus", model.Estatus);
                    obj = command.ExecuteScalar();
                    Conexion.Close();
                }
                if (obj != DBNull.Value && obj != null)
                {
                    resultado = Convert.ToInt32(obj);
                }
            }
            catch (Exception ex)
            {
                Conexion.Close();
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString().Substring(0,300) + "*EngineDb/ClienteRegistro*" + model.Email));
            }
            return resultado;
        }

        public int ClienteRegistroActivacion(ActivarCliente model)
        {
            int resultado = -1;
            object obj = new object();
            SqlConnection Conexion = new SqlConnection(cadenaConexion);
            SqlCommand command = new SqlCommand("Sp_PutActivarCliente", Conexion);
            try
            {
                using (Conexion)
                {
                    Conexion.Open();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@Email", model.Email);
                    command.Parameters.AddWithValue("@Password", model.Password);
                    command.Parameters.AddWithValue("@FechaActivacion", model.FechaActivacion);
                    command.Parameters.AddWithValue("@Estatus", model.Estatus);
                    command.Parameters.AddWithValue("@Identidad", model.Identidad);
                    obj = command.ExecuteScalar();
                    Conexion.Close();
                }
                if (obj != DBNull.Value && obj != null)
                {
                    resultado = Convert.ToInt32(obj);
                }
            }
            catch (Exception ex)
            {
                Conexion.Close();
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString().Substring(0,300) + "*EngineDb/ClienteRegistroActivacion*" + model.Email));
            }
            return resultado;
        }

        public int UpdateResetPassword(string email, string codigo, bool estatus)
        {
            int resultado = -1;
            object obj = new object();
            SqlConnection Conexion = new SqlConnection(cadenaConexion);
            SqlCommand command = new SqlCommand("Sp_PutResetPassword", Conexion);
            try
            {
                using (Conexion)
                {
                    Conexion.Open();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Codigo", codigo);
                    command.Parameters.AddWithValue("@Estatus", estatus);
                    obj = command.ExecuteScalar();
                    Conexion.Close();
                }
                if (obj != DBNull.Value && obj != null)
                {
                    resultado = Convert.ToInt32(obj);
                }
            }
            catch (Exception ex)
            {
                Conexion.Close();
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString().Substring(0,300) + "*EngineDb/UpdateResetPassword*" + email));
            }
            return resultado;
        }

        public int ClienteUpdatePassword(ActivarCliente model)
        {
            int resultado = -1;
            object obj = new object();
            SqlConnection Conexion = new SqlConnection(cadenaConexion);
            SqlCommand command = new SqlCommand("Sp_PutPassword", Conexion);
            try
            {
                using (Conexion)
                {
                    Conexion.Open();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@Email", model.Email);
                    command.Parameters.AddWithValue("@Password", model.Password);
                    obj = command.ExecuteScalar();
                    Conexion.Close();
                }
                if (obj != DBNull.Value && obj != null)
                {
                    resultado = Convert.ToInt32(obj);
                }
            }
            catch(Exception ex)
            {
                Conexion.Close();
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString().Substring(0,300) + "*EngineDb/ClienteUpdatePassword*" + model.Email));
            }
            return resultado;
        }

        public DataTable SeleccionProductosAll()
        {
            DataTable dataTabla = new DataTable();
            SqlConnection Conexion = new SqlConnection(cadenaConexion);
            using (Conexion)
            {
                Conexion.Open();
                SqlCommand command = new SqlCommand("Sp_SeleccionProductosAll", Conexion);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter dataAdaptador = new SqlDataAdapter(command);
                dataAdaptador.Fill(dataTabla);
                Conexion.Close();
            }
            return dataTabla;
        }

        //************************************EntityFramework***********************************************************************************

        public Guid ObtenerIdentidadCliente(string email)
        {
            Cliente C = new Cliente();
            try
            {
                using (SudokuContext Context = new SudokuContext())
                {
                    C = Context.Cliente.Where(s => s.Email == email).FirstOrDefault();
                    if (C.Identidad != null)
                        return C.Identidad;
                }
            }
            catch (Exception ex)
            {
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString().Substring(0,300) + "*EngineDb/ObtenerIdentidadCliente*" + email));
            }
            return Guid.Empty;
        }

        public int ObtenerIdCliente(string email)
        {
            Cliente C = new Cliente();
            try
            {
                using (SudokuContext Context = new SudokuContext())
                {
                    C = Context.Cliente.Where(s => s.Email == email).FirstOrDefault();
                    if (C.Id > 0)
                        return C.Id;
                }
            }
            catch (Exception ex)
            {
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString().Substring(0,300) + "*EngineDb/ObtenerIdCliente*" + email));
            }
            return 0;
        }

        public int ObtenerIdCliente(string email,bool estatus)
        {
            Cliente C = new Cliente();
            try
            {
                using (SudokuContext Context = new SudokuContext())
                {
                    C = Context.Cliente.Where(s => s.Email == email && s.Estatus == estatus).FirstOrDefault();
                    if (C.Id > 0)
                        return C.Id;
                }
            }
            catch(Exception ex)
            {
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString().Substring(0,300) + "*EngineDb/ObtenerIdCliente2*" + email));
            }
            return 0;
        }

        public string ObtenerPasswordCliente(string email)
        {
            Cliente C = new Cliente();
            try
            {
                using (SudokuContext Context = new SudokuContext())
                {
                    C = Context.Cliente.Where(s => s.Email == email).FirstOrDefault();
                    if (C.Password != string.Empty || C.Password != null)
                        return C.Password;
                }
            }
            catch (Exception ex)
            {
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString().Substring(0,300) + "*EngineDb/ObtenerPasswordCliente*" + email));
            }
            return string.Empty;
        }

        public bool InsertarResetPassword(ResetPassword model)
        {
            bool resultado = false;
            try
            {
                using (SudokuContext Context = new SudokuContext())
                {
                    Context.ResetPassword.Add(model);
                    Context.SaveChanges();
                    resultado = true;
                }
            }
            catch (Exception ex)
            {
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString().Substring(0,300) + "*EngineDb/InsertarResetPassword*" + model.Email));
            }
            return resultado;
        }

        public string ObtenerCodigoRestablecerPassword(string email)
        {
            ResetPassword C = new ResetPassword();
            try
            {
                using (SudokuContext Context = new SudokuContext())
                {
                    C = Context.ResetPassword.Where(x => x.Email == email && x.Estatus == false) .OrderByDescending(x => x.Id).Take(1).FirstOrDefault();
                    if (C != null)
                        return C.Codigo;
                }
            }
            catch (Exception ex)
            {
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString().Substring(0,300) + "*EngineDb/ObtenerCodigoRestablecerPassword*" + email));
            }
            return string.Empty;
        }

        public int ObtenerNumeroDePago()
        {
            int numero = 0;
            try
            {
                using (SudokuContext Context = new SudokuContext())
                {
                    numero = Context.PagoCliente.Max(x => x.Id) + 1;
                }
            }
            catch (Exception ex)
            {
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString().Substring(0, 300) + "*EngineDb/ObtenerNumeroDePago*" + ""));
            }
            return numero;

        }

        public bool InsertarProductoParaVenta(Producto model)
        {
            bool resultado = false;
            try
            {
                using (SudokuContext Context = new SudokuContext())
                {
                    Context.Producto.Add(model);
                    Context.SaveChanges();
                    resultado = true;
                }
            }
            catch (Exception ex)
            {
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString().Substring(0, 300) + "*EngineDb/InsertarProductoParaVenta*" + ""));
            }
            return resultado;
        }

        public List<Producto> ProductosParaVenta()
        {
            List<Producto> P = new List<Producto>();
            try
            {
                using (SudokuContext Context = new SudokuContext())
                {
                   int id = Context.Producto.Where(u => u.Estatus == true ).Max(u => u.Id);
                   P = Context.Producto.Where(x => x.Id == id).ToList();
                }
            }
            catch (Exception ex)
            {
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString().Substring(0, 300) + "*EngineDb/ProductosParaVenta*" + ""));
            }
            return P;
        }

        public bool InsertarSucesoLog(SucesoLog model)
        {
            bool resultado = false;
            try
            {
                using (SudokuContext Context = new SudokuContext())
                {
                    Context.SucesoLog.Add(model);
                    Context.SaveChanges();
                    resultado = true;
                }
            }
            catch {}
            return resultado;
        }
    }
}