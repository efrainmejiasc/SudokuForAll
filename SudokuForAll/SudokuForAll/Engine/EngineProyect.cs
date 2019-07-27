using SudokuForAll.Models.DbSistema;
using SudokuForAll.Models.Sistema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace SudokuForAll.Engine
{
    public class EngineProyect : IEngineProyect
    {
        public string ConvertirBase64(string cadena)
        {
            var comprobanteXmlPlainTextBytes = Encoding.UTF8.GetBytes(cadena);
            var cadenaBase64 = Convert.ToBase64String(comprobanteXmlPlainTextBytes);
            return cadenaBase64;
        }

        public static string ConvertirBase642(string cadena)
        {
            var comprobanteXmlPlainTextBytes = Encoding.UTF8.GetBytes(cadena);
            var cadenaBase64 = Convert.ToBase64String(comprobanteXmlPlainTextBytes);
            return cadenaBase64;
        }

        public string DecodeBase64(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static string DecodeBase642(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public bool CadenaBase64Valida(string cadena)
        {
            return (cadena.Length % 4 == 0) && Regex.IsMatch(cadena, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);
        }

        public bool EmailEsValido(string email)
        {
            string expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            bool resultado = false;
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, string.Empty).Length == 0)
                {
                    resultado = true;
                }
            }
            return resultado;
        }

        public bool CompareString(string a, string b)
        {
            bool resultado = false;
            if (a == b)
            {
                resultado = true;
            }
            return resultado;
        }


        public Guid IdentificadorReg()
        {
            Guid g = CrearGuid();
            while (g == Guid.Empty)
            {
                g = CrearGuid();
            }
            return g;
        }

        private Guid CrearGuid()
        {
            return Guid.NewGuid();
        }

        public string EncodeMd5(string a)
        {
            MD5CryptoServiceProvider encripte = new MD5CryptoServiceProvider();
            Byte[] vector = encripte.ComputeHash(Encoding.Default.GetBytes(a));
            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < vector.Length; i++)
            {
                sBuilder.Append(vector[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        public bool EstatusLink(DateTime fechaEnvio, DateTime fechaActivacion)
        {
            bool resultado = false;
            if (fechaEnvio.Date != fechaActivacion.Date)
                return resultado;

            int horaEnvio = fechaEnvio.Hour;
            int horaActivacion = fechaActivacion.Hour;
            int diferenciaHora = horaActivacion - horaEnvio;
            if (diferenciaHora <= 3)
                resultado = true;
            return resultado;
        }

        //********************************METODOS DEL SITIO *********************************************************************************
        public bool EnviarNuevaNotificacion(IEngineNotificacion Notificacion, IEngineDb Metodo, string email, string type, string password = "")
        {
            bool resultado = false;
            EstructuraMail model = new EstructuraMail();
            email = DecodeBase64(email);
            if (type == EngineData.Test)
            {
                string enlaze = CrearEnlazePrueba(Metodo, email);

                model = SetEstructuraMailTest(enlaze, email, model);
            }
            else if (type == EngineData.Register)
            {
                password = DecodeBase64(password);
                string enlaze = CrearEnlazeRegistro(Metodo, email, password);
                model = SetEstructuraMailRegister(enlaze, email, model);
            }
            resultado = Notificacion.EnviarMailNotificacion(model);
            return resultado;
        }


        public string CrearEnlazePrueba(IEngineDb Metodo, string email)
        {
            string fecha = Convert.ToString(DateTime.UtcNow).Replace(" ", "*");
            fecha = fecha.Replace(".", "+");
            string link = string.Empty;
            link = EngineData.EndPointValidation;
            link = link + "email=" + ConvertirBase64(email);
            link = link + "&identidad=" + EncodeMd5(Metodo.ObtenerIdentidadCliente(email).ToString());
            link = link + "&status=" + "1";
            link = link + "&date=" + fecha;
            link = link + "&type=" + EngineData.Test;
            return link;
        }

        public string CrearEnlazeRegistro(IEngineDb Metodo, string email, string password)
        {
            string fecha = Convert.ToString(DateTime.UtcNow).Replace(" ", "*");
            fecha = fecha.Replace(".", "+");
            string link = string.Empty;
            link = EngineData.EndPointValidation;
            link = link + "email=" + ConvertirBase64(email);
            link = link + "&ide=" + ConvertirBase64(password);
            link = link + "&identidad=" + EncodeMd5(Metodo.ObtenerIdentidadCliente(email).ToString());
            link = link + "&status=" + "1";
            link = link + "&date=" + fecha;
            link = link + "&type=" + EngineData.Register;
            return link;
        }

        public string CrearEnlazeRestablecerPassword(IEngineDb Metodo, string email, string codigo)
        {
            string fecha = Convert.ToString(DateTime.UtcNow).Replace(" ", "*");
            fecha = fecha.Replace(".", "+");
            string link = string.Empty;
            link = EngineData.EndPointValidation;
            link = link + "email=" + ConvertirBase64(email);
            link = link + "&ide=" + ConvertirBase64(codigo);
            link = link + "&identidad=" + EncodeMd5(Metodo.ObtenerIdentidadCliente(email).ToString());
            link = link + "&status=" + "0";
            link = link + "&date=" + fecha;
            link = link + "&type=" + EngineData.ResetPassword;
            return link;
        }

        public EstructuraMail SetEstructuraMailTest(string enlaze, string email, EstructuraMail model)
        {
            model.Link = enlaze;
            model.EmailDestinatario = email;
            model.Fecha = DateTime.UtcNow.ToString();
            model.Descripcion = EngineData.DescripcionTest;
            model.ClickAqui = EngineData.ClickAqui;
            model.Asunto = EngineData.AsuntoTest;
            model.Observacion = EngineData.ObservacionTest;
            model.PathLecturaArchivo = EngineData.PathLecturaArchivoTest;
            return model;
        }

        public EstructuraMail SetEstructuraMailRegister(string enlaze, string email, EstructuraMail model)
        {
            model.Link = enlaze;
            model.EmailDestinatario = email;
            model.Fecha = DateTime.UtcNow.ToString();
            model.Descripcion = EngineData.DescripcionRegistro;
            model.ClickAqui = EngineData.ClickAqui2;
            model.Asunto = EngineData.AsuntoRegistro;
            model.Observacion = EngineData.ObservacionRegistro;
            model.PathLecturaArchivo = EngineData.PathLecturaArchivoRegistro;
            return model;
        }

        public EstructuraMail SetEstructuraMailResetPassword(string enlaze, string email, string codigo, EstructuraMail model)
        {
            model.Link = enlaze;
            model.EmailDestinatario = email;
            model.Fecha = DateTime.UtcNow.ToString();
            model.Descripcion = EngineData.DescripcionRestablecerPassword;
            model.ClickAqui = EngineData.ClickAqui3;
            model.Asunto = EngineData.AsuntoResetPassword;
            model.Observacion = EngineData.ObservacionRestablecerPassword;
            model.PathLecturaArchivo = EngineData.PathLecturaArchivoRestablecerPassword;
            model.CodigoResetPassword = codigo;
            return model;
        }

        public ResetPassword SetResetPassword(string email, string codigo)
        {
            ResetPassword model = new ResetPassword
            {
                Email = email,
                Codigo = codigo,
                Fecha = DateTime.UtcNow,
                Estatus = false
            };
            return model;
        }

        public Respuesta RespuestaProceso(string nombreAccion="", string nombreControlador="", string respuesta="", string email="", string codigo="", string descripcion="")
        {
            Respuesta resultado = new Respuesta()
            {
                NombreAccion = nombreAccion,
                NombreControlador = nombreControlador,
                RespuestaAccion = respuesta,
                Email = email,
                CodigoResetPassword = codigo,
                Descripcion = descripcion
            };
            return resultado;
        }

        public Cliente ConstruirInsertarClienteTest(string email)
        {
            Cliente R = new Cliente()
            {
                Email = email,
                FechaRegistroPrueba = DateTime.UtcNow,
                FechaActivacion = Convert.ToDateTime("01/01/1900"),
                FechaActivacionPrueba = Convert.ToDateTime("01-01-1900"),
                FechaRegistro = Convert.ToDateTime("01-01-1900"),
                Estatus = false,
                EstatusEnvioNotificacion = false,
                Identidad = IdentificadorReg()
            };
            return R;
        }

        public Cliente ConstruirActualizarClienteTest(string email, string identidad)
        {
            EngineDb Metodo = new EngineDb();
            Guid guid = Metodo.ObtenerIdentidadCliente(email);
            Cliente R = new Cliente()
            {
                Email = email,
                FechaActivacionPrueba = DateTime.UtcNow,
                EstatusEnvioNotificacion = true,
                Identidad = guid
            };
            return R;
        }

        public ActivarCliente ConstruirActivarCliente(string email, string password)
        {
            EngineDb Metodo = new EngineDb();
            Guid guid = Metodo.ObtenerIdentidadCliente(email);
            ActivarCliente R = new ActivarCliente()
            {
                Email = email,
                Password = password,
                FechaActivacion = DateTime.UtcNow,
                Estatus = true,
                Identidad = guid
            };
            return R;
        }

        public string ConstruirCodigo()
        {
            string codigo = string.Empty;
            for (int i = 0; i <= 5; i++)
            {
                if (i % 2 == 0)
                    codigo = codigo + AleatorioLetra(i + DateTime.Now.Millisecond);
                else
                    codigo = codigo + AleatorioNumero(i + DateTime.Now.Millisecond);
            }
            return codigo.Trim();
        }

        private string AleatorioLetra(int semilla)
        {
            string letra = string.Empty;
            Random rnd = new Random(semilla);
            int n = rnd.Next(0, 26);
            double d = AleatorioDoble(semilla);
            if (d >= 0.5)
                letra = EngineData.AlfabetoG[n];
            else
                letra = EngineData.AlfabetoP[n];

            return letra;
        }

        private string AleatorioNumero(int semilla)
        {
            Random rnd = new Random(semilla);
            int n = rnd.Next(0, 9);
            return n.ToString();
        }

        private double AleatorioDoble(int semilla)
        {
            Random rnd = new Random(semilla);
            double n = rnd.NextDouble();
            return n;
        }
    }
}