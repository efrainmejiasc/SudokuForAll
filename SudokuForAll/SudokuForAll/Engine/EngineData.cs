using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
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

        //Parametros e-mail notificacion
        public static string UserMail = EngineProyect.DecodeBase642(WebConfigurationManager.AppSettings["UserMail"]);
        public static string IdMail = EngineProyect.DecodeBase642(WebConfigurationManager.AppSettings["IdMail"]);
        public static string Register = EngineProyect.ConvertirBase642("registro");
        public static string Test = EngineProyect.ConvertirBase642("prueba");
        public static string ResetPassword = EngineProyect.ConvertirBase642("resetPassword");

        // Url & EndPoints
        public static string EndPointValidation = WebConfigurationManager.AppSettings["EndPointValidation"];
        public static string UrlBase = WebConfigurationManager.AppSettings["UrlBase"];

        // Globalization
        public const string CulturaEspañol = "es-VE";
        private const string CulturaIngles = "en-US";
        private const string CulturaPortugues = "pt-PT";
        private static string respuesta = string.Empty;
        private static string cultura = string.Empty;

        public static string Cultura (string nombreIdioma)
        {
            switch (nombreIdioma)
            {
                case ("Español"):
                    respuesta = CulturaEspañol;
                    break;
                case ("Ingles"):
                    respuesta = CulturaIngles;
                    break;
                case ("Portugues"):
                    respuesta = CulturaPortugues;
                    break;
            }
            return respuesta;
        }

        public static string GetCultura()
        {
            if (System.Web.HttpContext.Current.Session["Cultura"] == null)
                System.Web.HttpContext.Current.Session["Cultura"] = CulturaEspañol;
                return cultura = System.Web.HttpContext.Current.Session["Cultura"].ToString();
        }

        //Mensajes Index 
        public static string EmailNoValido()
        {
            cultura = GetCultura();
            switch (cultura)
            {
                case (CulturaEspañol):
                    respuesta = " No es una direccion de e-mail valida";
                    break;
                case (CulturaIngles):
                    respuesta = " It is not a valid e-mail address";
                    break;
                case (CulturaPortugues):
                    respuesta = " Não é um endereço de e-mail válido";
                    break;
            }
            return respuesta;
        }

        public static string TiempoJuegoExpiro()
        {
            cultura = GetCultura();
            switch (cultura)
            {
                case (CulturaEspañol):
                    respuesta = " Tu Tiempo de juego expiro, debes volver a comprar";
                    break;
                case (CulturaIngles):
                    respuesta = " Your Playtime expired, you must buy again";
                    break;
                case (CulturaPortugues):
                    respuesta = " Seu Playtime expirou, você deve comprar novamente";
                    break;
            }
            return respuesta;
        }


        public static string TiempoPruebaJuegoExpiro()
        {
            cultura = GetCultura();
            switch (cultura)
            {
                case (CulturaEspañol):
                    respuesta =  " ¿Su tiempo de prueba expiro, desea comprar y registrase?";
                    break;
                case (CulturaIngles):
                    respuesta = " Does your trial time expire, do you want to buy and register?";
                    break;
                case (CulturaPortugues):
                    respuesta = " Seu tempo de avaliação expira? Você quer comprar e se registrar?";
                    break;
            }
            return respuesta;
        }

        public static string LoginFallido()
        {
            cultura = GetCultura();
            switch (cultura)
            {
                case (CulturaEspañol):
                    respuesta = " Identification failed, check your e-mail and password";
                    break;
                case (CulturaIngles):
                    respuesta = " Identification failed, check your e-mail and password";
                    break;
                case (CulturaPortugues):
                    respuesta = " Identificação falhou, verifique seu e-mail e senha";
                    break;
            }
            return respuesta;
        }

        public static string PasswordNoIdenticos()
        {
            cultura = GetCultura();
            switch (cultura)
            {
                case (CulturaEspañol):
                    respuesta = " Las contraseñas deben ser identicas";
                    break;
                case (CulturaIngles):
                    respuesta = " Passwords must be identical";
                    break;
                case (CulturaPortugues):
                    respuesta = " As senhas devem ser idênticas";
                    break;
            }
            return respuesta;
        }

        public static string ErrorRegistroCliente()
        {
            cultura = GetCultura();
            switch (cultura)
            {
                case (CulturaEspañol):
                    respuesta = " Error al registrar cliente. Verifique su e-mail";
                    break;
                case (CulturaIngles):
                    respuesta = " Error registering customer. Verify your e-mail";
                    break;
                case (CulturaPortugues):
                    respuesta = " Erro ao registrar o cliente. Confirme seu e-mail";
                    break;
            }
            return respuesta;
        }

        public static string RegistroExitoso()
        {
            cultura = GetCultura();
            switch (cultura)
            {
                case (CulturaEspañol):
                    respuesta = " Registro exitoso, Enviamos una notificacion a tu correo para que actives tu cuenta";
                    break;
                case (CulturaIngles):
                    respuesta = " Successful registration, We send a notification to your e-mail to activate your account";
                    break;
                case (CulturaPortugues):
                    respuesta = " Inscrição bem sucedida, enviamos uma notificação para o seu e-mail para ativar sua conta";
                    break;
            }
            return respuesta;
        }

        public static string ErrorEnviandoMail()
        {
            cultura = GetCultura();
            switch (cultura)
            {
                case (CulturaEspañol):
                    respuesta = " Error enviando notificacion";
                    break;
                case (CulturaIngles):
                    respuesta = " Error sending notification";
                    break;
                case (CulturaPortugues):
                    respuesta = " Erro ao enviar notificação";
                    break;
            }
            return respuesta;
        }

        public static string ErrorInternoServidor()
        {
            cultura = GetCultura();
            switch (cultura)
            {
                case (CulturaEspañol):
                    respuesta = " Error interno del servidor";
                    break;
                case (CulturaIngles):
                    respuesta = " Internal server error";
                    break;
                case (CulturaPortugues):
                    respuesta = " Erro interno do servidor";
                    break;
            }
            return respuesta;
        }

        public static string CuentaNoActivada()
        {
            cultura = GetCultura();
            switch (cultura)
            {
                case (CulturaEspañol):
                    respuesta = " Su cuenta no ha sido activada, revise su bandeja de entrada";
                    break;
                case (CulturaIngles):
                    respuesta = " Your account has not been activated, check your inbox";
                    break;
                case (CulturaPortugues):
                    respuesta = " Sua conta não foi ativada, verifique sua caixa de entrada";
                    break;
            }
            return respuesta;
        }


        public static string TiempoLinkExpiro()
        {
            cultura = GetCultura();
            switch (cultura)
            {
                case (CulturaEspañol):
                    respuesta = " El tiempo valido de el link expiro, enviamos una nueva notificacion a tu correo";
                    break;
                case (CulturaIngles):
                    respuesta = " The valid time of the link expires, we send a new notification to your e-mail";
                    break;
                case (CulturaPortugues):
                    respuesta = " O tempo válido do link expira, nós enviamos uma nova notificação para o seu e-mail";
                    break;
            }
            return respuesta;
        }


        public static string ActivacionExitosa()
        {
            cultura = GetCultura();
            switch (cultura)
            {
                case (CulturaEspañol):
                    respuesta = " Activacion exitosa, autentificate en nuestro sitio";
                    break;
                case (CulturaIngles):
                    respuesta = " Successful activation, authenticate on our site";
                    break;
                case (CulturaPortugues):
                    respuesta = " Ativação bem sucedida, autenticar no nosso site";
                    break;
            }
            return respuesta;
        }

        public static string ActivacionFallida()
        {
            cultura = GetCultura();
            switch (cultura)
            {
                case (CulturaEspañol):
                    respuesta = " Activacion fallida";
                    break;
                case (CulturaIngles):
                    respuesta = " Activation failed";
                    break;
                case (CulturaPortugues):
                    respuesta = " Ativação falhou";
                    break;
            }
            return respuesta;
        }

        public static string EmailNoRegistrado()
        {
            cultura = GetCultura();
            switch (cultura)
            {
                case (CulturaEspañol):
                    respuesta = " El e-mail no esta registrado";
                    break;
                case (CulturaIngles):
                    respuesta = " The e-mail is not registered";
                    break;
                case (CulturaPortugues):
                    respuesta = " O e-mail não está registrado";
                    break;
            }
            return respuesta;
        }

        public static string CodigoNoCoincide()
        {
            cultura = GetCultura();
            switch (cultura)
            {
                case (CulturaEspañol):
                    respuesta = " El codigo suministrado no coincide, intentelo de nuevo";
                    break;
                case (CulturaIngles):
                    respuesta = " The code provided does not match, try again";
                    break;
                case (CulturaPortugues):
                    respuesta = " O código fornecido não corresponde, tente novamente";
                    break;
            }
            return respuesta;
        }

        public static string EnvioCodigoRestablecerPassword()
        {
            cultura = GetCultura();
            switch (cultura)
            {
                case (CulturaEspañol):
                    respuesta = " Hemos enviado una notificacion via e-mail, con un codigo de verificacion pera que restablescas tu contraseña";
                    break;
                case (CulturaIngles):
                    respuesta = " We have sent a notification via e-mail, with a verification code so that you reset your password";
                    break;
                case (CulturaPortugues):
                    respuesta = " Enviamos uma notificação via e-mail, com um código de verificação para que você redefina sua senha";
                    break;
            }
            return respuesta;
        }

        public static string RestablecerContraseñaExito()
        {
            cultura = GetCultura();
            switch (cultura)
            {
                case (CulturaEspañol):
                    respuesta = " La contraseña fue modificada exitosamente, ingresa usando tu email y contraseña";
                    break;
                case (CulturaIngles):
                    respuesta = " The password was successfully modified, login using your email and password";
                    break;
                case (CulturaPortugues):
                    respuesta = " A senha foi modificada com sucesso, login usando seu email e senha";
                    break;
            }
            return respuesta;
        }

        public static string RestablecerContraseñaFallida()
        {
            cultura = GetCultura();
            switch (cultura)
            {
                case (CulturaEspañol):
                    respuesta = " No se pudo actualizar contraseña, verifique su e-mail e intente nuevamente";
                    break;
                case (CulturaIngles):
                    respuesta = " Could not update password, verify your e-mail and try again";
                    break;
                case (CulturaPortugues):
                    respuesta = " Não foi possível atualizar a senha, verificar seu e-mail e tentar novamente";
                    break;
            }
            return respuesta;
        }




        // Parametros notificacion para prueba del sitio
        public static string AsuntoTest = "Por favor verifica tu e-mail...";
        public static string ClickAqui = "Hazme Click Para Verificar Tu Identidad!";
        public static string DescripcionTest = "Por favor verifica tu direccion de correo electronico, para saber que eres realmente tu.";
        public static string ObservacionTest = "Bienvenido a nuestra aplicacion Sudoku para todos, haz click en el link y disfruta 2 dias de prueba </br>" +
            "El tiempo valido para este link es de 2 horas,</br>No tendras que aportar datos personales adicionales, tu direccion e-mail sera confidencial.";
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