using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace SudokuForAll.Engine
{
    public class EngineData
    {
        private static EngineData valor;
        public static EngineData Instance()
        {
            if ((valor == null))
            {
                valor = new EngineData();
            }
            return valor;
        }

        public static string IdSite = EngineProyect.DecodeBase642(WebConfigurationManager.AppSettings["IdSite"]);

        //Parametros email notificacion
        public static string UserMail = EngineProyect.DecodeBase642(WebConfigurationManager.AppSettings["UserMail"]);
        public static string IdMail = EngineProyect.DecodeBase642(WebConfigurationManager.AppSettings["IdMail"]);
        public static string Register = EngineProyect.ConvertirBase642("registro");
        public static string Test = EngineProyect.ConvertirBase642("prueba");
        public static string ResetPassword = EngineProyect.ConvertirBase642("resetPassword");

        // Url & EndPoints
        public static string EndPointValidation = WebConfigurationManager.AppSettings["EndPointValidation"];
        public static string UrlBase = WebConfigurationManager.AppSettings["UrlBase"];

        // Parametros notificacion para prueba del sitio
        public static string AsuntoTest = "Por favor verifica tu email...";
        public static string ClickAqui = "Hazme Click Para Verificar Tu Identidad!";
        public static string DescripcionTest = "Por favor verifica tu direccion de correo electronico, para saber que eres realmente tu.";
        public static string ObservacionTest = "Bienvenido a nuestra aplicacion Sudoku para todos, haz click en el link y disfruta 2 dias de prueba </br>" +
            "El tiempo valido para este link es de 2 horas,</br>No tendras que aportar datos personales adicionales, tu direccion email sera confidencial.";
        public static string PathLecturaArchivoTest = @"/Content/template/EmailBodyTest.cshtml";

        // Parametros notificacion para registro de clientes
        public static string AsuntoRegistro = "Por favor completa tu registro...";
        public static string ClickAqui2 = "Hazme Click Para Verificar Tu Identidad!";
        public static string DescripcionRegistro = "Por favor verifica tu direccion de correo electronico, para saber que eres realmente tu.";
        public static string ObservacionRegistro = "Bienvenido a nuestra aplicacion Sudoku para todos, haz click en el link y completa tu registro </br>" +
            "El tiempo valido para este link es de 2 horas,</br>Todos tus datos seran estrictamente cofidenciales.";
        public static string PathLecturaArchivoRegistro = @"/Content/template/EmailBodyRegister.cshtml";

        // Parametros notificacion para restablecer password
        public static string AsuntoResetPassword = "Restablecer password Sudoku para todos";
        public static string ClickAqui3 = "Hazme Click para ir a la Pagina de ingresar codigo para restablecimiento de Contraseña";
        public static string DescripcionRestablecerPassword = "Haz click en el enlaze e ingresa el codigo para restablecer tu contraseña";
        public static string ObservacionRestablecerPassword = "Este codigo tiene un tiempo de expiracion de 2 horas";
        public static string PathLecturaArchivoRestablecerPassword = @"/Content/template/EmailBodyResetPassword.cshtml";

        public static string[] AlfabetoG = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };//0-25
        public static string[] AlfabetoP = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
    }
}