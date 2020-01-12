using Resources.Abstract;
using Resources.Concrete;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace SudokuDeTodos.StringResx
{
    public class Resources
    {
        private static System.IO.FileInfo pathResourceFile = new System.IO.FileInfo(System.Web.HttpContext.Current.Server.MapPath("~/StringResx/Resources.xml"));
        private static IResourceProvider resourceProvider = new XmlResourceProvider(pathResourceFile.ToString());


        public static string IngreseEmail
        {
            get { return resourceProvider.GetResource("IngreseEmail", CultureInfo.CurrentUICulture.Name) as String; }
        }

        public static string ModificarContraseña
        {
            get { return resourceProvider.GetResource("ModificarContraseña", CultureInfo.CurrentUICulture.Name) as String; }
        }

        public static string IngreseContraseña
        {
            get { return resourceProvider.GetResource("IngreseContraseña", CultureInfo.CurrentUICulture.Name) as String; }
        }

        public static string ConfirmeContraseña
        {
            get { return resourceProvider.GetResource("ConfirmeContraseña", CultureInfo.CurrentUICulture.Name) as String; }
        }

        public static string NoSoyUnRobot
        {
            get { return resourceProvider.GetResource("NoSoyUnRobot", CultureInfo.CurrentUICulture.Name) as String; }
        }

        public static string Enviar
        {
            get { return resourceProvider.GetResource("Enviar", CultureInfo.CurrentUICulture.Name) as String; }
        }

        public static string Aceptar
        {
            get { return resourceProvider.GetResource("Aceptar", CultureInfo.CurrentUICulture.Name) as String; }
        }

        public static string Cancelar
        {
            get { return resourceProvider.GetResource("Cancelar", CultureInfo.CurrentUICulture.Name) as String; }
        }

        public static string IngreseCodigoDeVerificacion
        {
            get { return resourceProvider.GetResource("IngreseCodigoDeVerificacion", CultureInfo.CurrentUICulture.Name) as String; }
        }

        public static string IngreseNombre
        {
            get { return resourceProvider.GetResource("IngreseNombre", CultureInfo.CurrentUICulture.Name) as String; }
        }

        public static string IngreseApellido
        {
            get { return resourceProvider.GetResource("IngreseApellido", CultureInfo.CurrentUICulture.Name) as String; }
        }

        public static string MsjPruebaSitio
        {
            get { return resourceProvider.GetResource("MsjPruebaSitio", CultureInfo.CurrentUICulture.Name) as String; }
        }

        public static string CtaNoActivada
        {
            get { return resourceProvider.GetResource("CtaNoActivada", CultureInfo.CurrentUICulture.Name) as String; }
        }


        public static string Entrar
        {
            get { return resourceProvider.GetResource("Entrar", CultureInfo.CurrentUICulture.Name) as String; }
        }

        public static string Inicio
        {
            get { return resourceProvider.GetResource("Inicio", CultureInfo.CurrentUICulture.Name) as String; }
        }

        public static string OlvidoSuContraseña
        {
            get { return resourceProvider.GetResource("OlvidoSuContraseña", CultureInfo.CurrentUICulture.Name) as String; }
        }

        public static string Terminos
        {
            get { return resourceProvider.GetResource("Terminos", CultureInfo.CurrentUICulture.Name) as String; }
        }

        public static string Autentificate
        {
            get { return resourceProvider.GetResource("Autentificate", CultureInfo.CurrentUICulture.Name) as String; }
        }

        public static string Registrate
        {
            get { return resourceProvider.GetResource("Registrate", CultureInfo.CurrentUICulture.Name) as String; }
        }

        public static string Equipo
        {
            get { return resourceProvider.GetResource("Equipo", CultureInfo.CurrentUICulture.Name) as String; }
        }

        public static string IniciarSesion
        {
            get { return resourceProvider.GetResource("IniciarSesion", CultureInfo.CurrentUICulture.Name) as String; }
        }

        public static string AceptarCondicion
        {
            get { return resourceProvider.GetResource("AceptarCondicion", CultureInfo.CurrentUICulture.Name) as String; }
        }

        public static string Requerido
        {
            get { return resourceProvider.GetResource("Requerido", CultureInfo.CurrentUICulture.Name) as String; }
        }

        public static string EmailRequerido
        {
            get { return resourceProvider.GetResource("EmailRequerido", CultureInfo.CurrentUICulture.Name) as String; }
        }

        public static string PasswordRequerido
        {
            get { return resourceProvider.GetResource("PasswordRequerido", CultureInfo.CurrentUICulture.Name) as String; }
        }
        public static string ChxRequerido
        {
            get { return resourceProvider.GetResource("ChxRequerido", CultureInfo.CurrentUICulture.Name) as String; }
        }
        public static string EnviarOtroEmail
        {
            get { return resourceProvider.GetResource("EnviarOtroEmail", CultureInfo.CurrentUICulture.Name) as String; }
        }
        public static string Si
        {
            get { return resourceProvider.GetResource("Si", CultureInfo.CurrentUICulture.Name) as String; }
        }
        public static string No
        {
            get { return resourceProvider.GetResource("No", CultureInfo.CurrentUICulture.Name) as String; }
        }
        public static string TiempoPruebaExpiro
        {
            get { return resourceProvider.GetResource("TiempoPruebaExpiro", CultureInfo.CurrentUICulture.Name) as String; }
        }

        public static string PagoExpiro
        {
            get { return resourceProvider.GetResource("PagoExpiro", CultureInfo.CurrentUICulture.Name) as String; }
        }

        public static string ErrorInterno
        {
            get { return resourceProvider.GetResource("ErrorInterno", CultureInfo.CurrentUICulture.Name) as String; }
        }

        public static string OtroEmailEnviado
        {
            get { return resourceProvider.GetResource("OtroEmailEnviado", CultureInfo.CurrentUICulture.Name) as String; }
        }

        public static string ContraseñaRequerida
        {
            get { return resourceProvider.GetResource("ContraseñaRequerida", CultureInfo.CurrentUICulture.Name) as String; }
        }

        public static string ContraseñaDebeIdentica
        {
            get { return resourceProvider.GetResource("ContraseñaDebeIdentica", CultureInfo.CurrentUICulture.Name) as String; }
        }

        public static string FormatContraseña
        {
            get { return resourceProvider.GetResource("FormatContraseña", CultureInfo.CurrentUICulture.Name) as String; }
        }

        public static string ContraseñaDebeLetra
        {
            get { return resourceProvider.GetResource("ContraseñaDebeLetra", CultureInfo.CurrentUICulture.Name) as String; }
        }

        public static string ContraseñaDebeNumero
        {
            get { return resourceProvider.GetResource("ContraseñaDebeNumero", CultureInfo.CurrentUICulture.Name) as String; }
        }

        public static string ContraseñaDebeEspacioBlanco
        {
            get { return resourceProvider.GetResource("ContraseñaDebeEspacioBlanco", CultureInfo.CurrentUICulture.Name) as String; }
        }

        public static string UsarMismoEmail
        {
            get { return resourceProvider.GetResource("UsarMismoEmail", CultureInfo.CurrentUICulture.Name) as String; }
        }

        public static string RegistroPago
        {
            get { return resourceProvider.GetResource("RegistroPago", CultureInfo.CurrentUICulture.Name) as String; }
        }

        public static string Auth
        {
            get { return resourceProvider.GetResource("Auth", CultureInfo.CurrentUICulture.Name) as String; }
        }
        public static string PasswordInvalido
        {
            get { return resourceProvider.GetResource("PasswordInvalido", CultureInfo.CurrentUICulture.Name) as String; }
        }

        public static string MsjModificarPassword
        {
            get { return resourceProvider.GetResource("MsjModificarPassword", CultureInfo.CurrentUICulture.Name) as String; }
        }
    }
}
