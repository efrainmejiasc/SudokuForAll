using SudokuForAll.Models;
using SudokuForAll.Models.DbSistema;
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

        public int ResultadoEntradaAlSitio(string email)
        {
            object obj = new object();
            int resultado = 0;
            SqlConnection Conexion = new SqlConnection(cadenaConexion);
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
            return resultado;
        }

        public int ResultadoLogin(string password)
        {
            object obj = new object();
            int resultado = 0;
            SqlConnection Conexion = new SqlConnection(cadenaConexion);
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
            return resultado;
        }

        public bool InsertarClienteTest(string email, IEngineProyect Funcion)
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
                    string n = ex.ToString();
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
            catch
            {
                Conexion.Close();
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
            catch
            {
                Conexion.Close();
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
            catch
            {
                Conexion.Close();
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
            catch
            {
                Conexion.Close();
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
                using (SudokuContext context = new SudokuContext())
                {
                    C = context.Cliente.Where(s => s.Email == email).FirstOrDefault();
                    if (C.Identidad != null)
                        return C.Identidad;
                }
            }
            catch { }
            return Guid.Empty;
        }

        public int ObtenerIdCliente(string email)
        {
            Cliente C = new Cliente();
            try
            {
                using (SudokuContext context = new SudokuContext())
                {
                    C = context.Cliente.Where(s => s.Email == email).FirstOrDefault();
                    if (C.Id > 0)
                        return C.Id;
                }
            }
            catch { }
            return 0;
        }

        public bool InsertarResetPassword(ResetPassword model)
        {
            bool resultado = false;
            try
            {
                using (SudokuContext context = new SudokuContext())
                {
                    context.ResetPassword.Add(model);
                    context.SaveChanges();
                    resultado = true;
                }
            }
            catch { }
            return resultado;
        }

        public string ObtenerCodigoRestablecerPassword(string email)
        {
            ResetPassword C = new ResetPassword();
            try
            {
                using (SudokuContext context = new SudokuContext())
                {
                    C = context.ResetPassword.OrderByDescending(x => x.Email == email && x.Estatus == false).Take(1).FirstOrDefault();
                    if (C.Codigo != string.Empty && C.Codigo != null)
                        return C.Codigo;
                }
            }
            catch { }
            return string.Empty;
        }
    }
}