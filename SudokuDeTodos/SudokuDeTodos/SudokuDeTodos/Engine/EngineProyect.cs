﻿using SudokuDeTodos.Models.DbSistema;
using SudokuDeTodos.Models.Sistema;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;

namespace SudokuDeTodos.Engine
{
    public class EngineProyect : IEngineProyect
    {
        private IEngineNotificacion Notificacion;

        public EngineProyect (IEngineNotificacion _Notificacion)
        {
            Notificacion = _Notificacion;
        }
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

        public SucesoLog ConstruirSucesoLog(string cadena)
        {
            string[] x = cadena.Split('*');
            SucesoLog modelo = new SucesoLog()
            {
                Fecha = DateTime.UtcNow,
                Excepcion = x[0],
                Metodo = x[1],
                Email = x[2]
            };
            return modelo;
        }

        public Respuesta ConstruirRespuesta(int id = 0, bool status = false, string descripcion = "", string email = "")
        {
            Respuesta modelo = new Respuesta()
            {
               Id = id,
               Status = status,
               Descripcion = descripcion,
               Email = email
            };
            return modelo;
        }

        public Respuesta ConstruirRespuesta(int id = 0, bool status = false, string descripcion = "", string email = "",string type ="")
        {
            Respuesta modelo = new Respuesta()
            {
                Id = id,
                Status = status,
                Descripcion = descripcion,
                Email = email,
                Type = type
            };
            return modelo;
        }

        public Cliente ConstruirCliente (string email)
        {
            Cliente R = new Cliente()
            {
                Email = email,
                FechaRegistroPrueba = DateTime.UtcNow,
                FechaActivacion = Convert.ToDateTime("01/01/1900 00:00:00.000"),
                FechaActivacionPrueba = DateTime.UtcNow,
                FechaRegistro = Convert.ToDateTime("01-01-1900 00:00:00.000"),
                Estatus = false,
                EstatusEnvioNotificacion = false,
                Identidad = IdentificadorReg(),
                Cultura = EngineData.GetCultura()
            };
            return R;
        }

        public Cliente ConstruirCliente(string email,Guid identidad)
        {
            Cliente R = new Cliente()
            {
                Email = email,
                FechaRegistroPrueba = DateTime.UtcNow,
                FechaActivacion = Convert.ToDateTime("01/01/1900 00:00:00.000"),
                FechaActivacionPrueba = DateTime.UtcNow,
                FechaRegistro = Convert.ToDateTime("01-01-1900 00:00:00.000"),
                Estatus = false,
                EstatusEnvioNotificacion = false,
                Identidad = identidad,
                Cultura = EngineData.GetCultura()
            };
            return R;
        }

        public string ConstruirEnlazePrueba(string email,Guid identidad)
        {
            string link = string.Empty;
            link = EngineData.EndPointValidation;
            link = link + "id=" + "0";
            link = link + "&email=" + ConvertirBase64(email);
            link = link + "&identidad=" + EncodeMd5(identidad.ToString());
            link = link + "&status=" + "1";
            link = link + "&date=" + DateTime.UtcNow.ToString();
            link = link + "&type=" + EngineData.Test;
            link = link + "&cultureInfo=" + EngineData.GetCultura();
            return link;
        }

        public string ConstruirEnlazeRegistro(string email, string password,Guid identidad)
        {
            string link = string.Empty;
            link = EngineData.EndPointValidation;
            link = link + "id=" + "0";
            link = link + "&email=" + ConvertirBase64(email);
            link = link + "&ide=" + ConvertirBase64(password);
            link = link + "&identidad=" + EncodeMd5(identidad.ToString());
            link = link + "&status=" + "1";
            link = link + "&date=" + DateTime.UtcNow.ToString();
            link = link + "&type=" + EngineData.Register;
            link = link + "&cultureInfo=" + EngineData.GetCultura();
            return link;
        }

        public string ConstruirEnlazeRestablecerPassword(string email, string codigo,Guid identidad)
        {
            string link = string.Empty;
            link = EngineData.EndPointValidation;
            link = link + "id=" + "1";
            link = link + "&email=" + ConvertirBase64(email);
            link = link + "&ide=" + ConvertirBase64(codigo);
            link = link + "&identidad=" + EncodeMd5(identidad.ToString());
            link = link + "&status=" + "0";
            link = link + "&date=" + DateTime.UtcNow.ToString();
            link = link + "&type=" + EngineData.ResetPassword;
            link = link + "&cultureInfo=" + EngineData.GetCultura();
            return link;
        }

        public string ConstruirEnlazeRegistroGerente(string email,Guid identidad)
        {
            string link = string.Empty;
            link = EngineData.EndPointValidation;
            link = link + "id=" + "0";
            link = link + "&email=" + ConvertirBase64(email);
            link = link + "&identidad=" + EncodeMd5(identidad.ToString());
            link = link + "&status=" + "1";
            link = link + "&date=" + DateTime.UtcNow.ToString();
            link = link + "&type=" + EngineData.RegisterManager;
            link = link + "&cultureInfo=" + EngineData.GetCultura();
            return link;
        }

        public EstructuraMail SetEstructuraMailTest(string enlaze, string email)
        {
            EstructuraMail model = new EstructuraMail();
            model.Link = enlaze;
            model.Saludo = EngineData.Saludo();
            model.EmailDestinatario = email;
            model.Fecha = DateTime.UtcNow.ToString();
            model.Descripcion = EngineData.DescripcionTest();
            model.ClickAqui = EngineData.ClickAqui();
            model.Asunto = EngineData.AsuntoTest();
            model.Observacion = EngineData.ObservacionTest();
            model.PathLecturaArchivo = EngineData.PathLecturaArchivoTest;
            return model;
        }

        public EstructuraMail SetEstructuraMailRegister(string enlaze, string email)
        {
            EstructuraMail model = new EstructuraMail();
            model.Link = enlaze;
            model.Saludo = EngineData.Saludo();
            model.EmailDestinatario = email;
            model.Fecha = DateTime.UtcNow.ToString();
            model.Descripcion = EngineData.DescripcionRegistro();
            model.ClickAqui = EngineData.ClickAqui2();
            model.Asunto = EngineData.AsuntoRegistro();
            model.Observacion = EngineData.ObservacionRegistro();
            model.PathLecturaArchivo = EngineData.PathLecturaArchivoRegistro;
            return model;
        }

        public EstructuraMail SetEstructuraMailResetPassword(string enlaze, string email, string codigo)
        {
            EstructuraMail model = new EstructuraMail();
            model.Link = enlaze;
            model.Saludo = EngineData.Saludo();
            model.EmailDestinatario = email;
            model.Fecha = DateTime.UtcNow.ToString();
            model.Descripcion = EngineData.DescripcionRestablecerPassword();
            model.ClickAqui = EngineData.ClickAqui3();
            model.Asunto = EngineData.AsuntoResetPassword();
            model.Observacion = EngineData.ObservacionRestablecerPassword();
            model.PathLecturaArchivo = EngineData.PathLecturaArchivoRestablecerPassword;
            model.CodigoResetPassword = codigo;
            return model;
        }

        public EstructuraMail SetEstructuraMailRegisterManager(string enlaze, string email)
        {
            EstructuraMail model = new EstructuraMail();
            model.Link = enlaze;
            model.Saludo = EngineData.Saludo();
            model.EmailDestinatario = email;
            model.Fecha = DateTime.UtcNow.ToString();
            model.Descripcion = EngineData.DescripcionRegistroGerente();
            model.ClickAqui = EngineData.ClickAqui4();
            model.Asunto = EngineData.AsuntoRegistroGerente();
            model.Observacion = EngineData.ObservacionRegistroGerente();
            model.PathLecturaArchivo = EngineData.PathLecturaArchivoRegistro;
            return model;
        }

        public bool ValidacionIdentidad (string email,string identidad,IEngineDb Metodo)
        {
            bool resultado = false;
            Guid guidClient = Metodo.GetIdentidadCliente(email);
            if (guidClient == Guid.Empty)
                return false;
            string ident = EncodeMd5(guidClient.ToString());
            resultado = CompareString(identidad, ident);
            if (!resultado)
                return resultado;
            return resultado;
        }

        public void SetCultureInfo(string cultura)
        {
            if (cultura == string.Empty)
                cultura = "es-ES";
            CultureInfo ci = new CultureInfo(cultura);
            Thread.CurrentThread.CurrentUICulture = ci;
            Thread.CurrentThread.CurrentCulture = ci;
            System.Web.HttpContext.Current.Session["Cultura"] = cultura;
        }

        public bool ValidacionTypeTransaccion(string type)
        {
            bool resultado = true;
            if (type != EngineData.Register && type != EngineData.Test && type != EngineData.ResetPassword && type != EngineData.RegisterManager)
                resultado = false;
            return resultado;
        }

        public bool EstatusLink(DateTime fechaEnvio)
        {
            bool resultado = false;
            DateTime fechaActivacion = DateTime.UtcNow;
            if (fechaEnvio.Date != fechaActivacion.Date)
                return resultado;

            int horaEnvio = fechaEnvio.Hour;
            int horaActivacion = fechaActivacion.Hour;
            int diferenciaHora = horaActivacion - horaEnvio;
            if (diferenciaHora <= 3)
                resultado = true;
            return resultado;
        }

        public bool EnviarNuevaNotificacion(Guid identidad , string email = "", string type = "", string password = "")
        {
            bool resultado = false;
            EstructuraMail model = new EstructuraMail();
            resultado = CadenaBase64Valida(email);
            if (resultado)
                email = DecodeBase64(email);

            if (type == EngineData.Test)
            {
                string enlaze = ConstruirEnlazePrueba(email,identidad);
                model = SetEstructuraMailTest(enlaze, email);
            }
            else if (type == EngineData.Register)
            {
                password = DecodeBase64(password);
                string enlaze = ConstruirEnlazeRegistro(email, password,identidad);
                model = SetEstructuraMailRegister(enlaze, email);
            }
            else if (type == EngineData.RegisterManager)
            {
                string enlaze = ConstruirEnlazeRegistroGerente(email,identidad);
                model = SetEstructuraMailRegisterManager(enlaze, email);
            }
            resultado = Notificacion.EnviarMailNotificacion(model);
            return resultado;
        }
    }
}