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

        public List<Producto> GetProductosParaVenta()
        {
            List<Producto> P = new List<Producto>();
            try
            {
                using (SudokuContext Context = new SudokuContext())
                {
                    P = Context.Producto.ToList();
                }
            }
            catch (Exception ex)
            {
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString().Substring(0, 300) + "*EngineDb/GetProductosParaVenta*" + ""));
            }
            return P;
        }

        public Producto GetProducto(string codigo)
        {
            Producto P = new Producto();
            try
            {
                using (SudokuContext Context = new SudokuContext())
                {
                    P = Context.Producto.Where(x => x.Codigo == codigo).First();
                }
            }
            catch (Exception ex)
            {
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString().Substring(0, 300) + "*EngineDb/GetProducto*" + ""));
            }
            return P;
        }

        public bool PutProducto(Producto m)
        {
            bool resultado = false;
            Producto P = new Producto();
            try
            {
                using (SudokuContext Context = new SudokuContext())
                {
                    P = Context.Producto.Where(x => x.Codigo == m.Codigo).First();
                    if (P != null)
                    {
                        P.Nombre = m.Nombre;
                        P.Descripcion = m.Descripcion;
                        P.Precio = m.Precio;
                        P.Impuesto = m.Impuesto;
                        P.Moneda = m.Moneda;
                        P.Estatus = m.Estatus;
                        P.FechaActualizacion = m.FechaActualizacion;
                        Context.SaveChanges();
                        resultado = true;
                    }
                }
            }
            catch (Exception ex)
            {
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString().Substring(0, 300) + "*EngineDb/PutProducto*" + ""));
            }
            return resultado;
        }


        public bool InsertarNuevoGerente(Gerente model)
        {
            bool resultado = false;
            try
            {
                using (SudokuContext Context = new SudokuContext())
                {
                    Context.Gerente.Add(model);
                    Context.SaveChanges();
                    resultado = true;
                }
            }
            catch (Exception ex)
            {
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString().Substring(0, 300) + "*EngineDb/InsertarNuevoGerente*" + ""));
            }
            return resultado;
        }


        public Guid ObtenerIdentidadGerente(string email)
        {
            Gerente C = new Gerente();
            try
            {
                using (SudokuContext Context = new SudokuContext())
                {
                    C = Context.Gerente.Where(s => s.Email == email).FirstOrDefault();
                    if (C.Identidad != null)
                        return C.Identidad;
                }
            }
            catch (Exception ex)
            {
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString().Substring(0, 300) + "*EngineDb/ObtenerIdentidadGerente*" + email));
            }
            return Guid.Empty;
        }

        public bool PutGerente(Gerente m,string subEjecutada)
        {
            bool resultado = false;
            Gerente P = new Gerente();
            try
            {
                using (SudokuContext Context = new SudokuContext())
                {
                    P = Context.Gerente.Where(x => x.Email == m.Email).First();
                    if (P != null)
                    {
                        if(subEjecutada == "Alto")
                        {
                            P.NombreUsuario = m.NombreUsuario;
                            P.FechaActualizacion = m.FechaActualizacion;
                            P.Estatus = m.Estatus;
                            P.Rol = m.Rol;
                        }
                        else
                        {
                            P.Nombre = m.Nombre;
                            P.NombreUsuario = m.NombreUsuario;
                            P.FechaActualizacion = m.FechaActualizacion;
                            P.Email = m.Email;
                            P.Password = m.Password;
                            P.Estatus = m.Estatus;
                        }
                        Context.SaveChanges();
                        resultado = true;
                    }
                }
            }
            catch (Exception ex)
            {
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString().Substring(0, 300) + "*EngineDb/PutGerente*" + ""));
            }
            return resultado;
        }

        public Gerente GetGerente(string email)
        {
            Gerente P = new Gerente();
            try
            {
                using (SudokuContext Context = new SudokuContext())
                {
                    P = Context.Gerente.Where(x => x.Email == email).First();
                }
            }
            catch (Exception ex)
            {
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString().Substring(0, 300) + "*EngineDb/GetGerente*" + ""));
            }
            return P;
        }

        public Gerente GetLoginGerente(string password)
        {
            Gerente P = new Gerente();
            try
            {
                using (SudokuContext Context = new SudokuContext())
                {
                    P = Context.Gerente.Where(x => x.Password == password).First();
                }
            }
            catch (Exception ex)
            {
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString().Substring(0, 300) + "*EngineDb/GetLoginGerente*" + ""));
            }
            return P;
        }

        public Gerente GetGerenteName(string nombre)
        {
            Gerente P = new Gerente();
            try
            {
                using (SudokuContext Context = new SudokuContext())
                {
                    P = Context.Gerente.Where(x => x.Nombre == nombre).First();
                }
            }
            catch (Exception ex)
            {
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString().Substring(0, 300) + "*EngineDb/GetGerenteName*" + ""));
            }
            return P;
        }

        public Gerente GetGerenteUserName(string nombreUsuario)
        {
            Gerente P = new Gerente();
            try
            {
                using (SudokuContext Context = new SudokuContext())
                {
                    P = Context.Gerente.Where(x => x.NombreUsuario == nombreUsuario).First();
                }
            }
            catch (Exception ex)
            {
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString().Substring(0, 300) + "*EngineDb/GetGerenteUserName*" + ""));
            }
            return P;
        }

        public List<Roles> GetAllGerentes()
        {
            List<Gerente> P = new List<Gerente>();
            List<Roles> G = new List<Roles>();
            try
            {
                using (SudokuContext Context = new SudokuContext())
                {
                    P = Context.Gerente.ToList();
                }
                if (P.Count > 0)
                {
                    int n = 0;
                    foreach (Gerente I in P)
                    {
                        Roles R = new Roles();
                        R.Id = I.Nombre;
                        R.Nombre = I.Nombre;
                        G.Insert(n, R);
                        n++;
                    }
                }
            }
            catch (Exception ex)
            {
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString().Substring(0, 300) + "*EngineDb/GetAllGerentes*" + ""));
            }
            return G;
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