using Resources.Abstract;
using Resources.Concrete;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace SudokuForAll.StringResx
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

    }
}