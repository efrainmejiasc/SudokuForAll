using SudokuForAll.Models.Sistema;
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
        public static string EndPointResetPassword = WebConfigurationManager.AppSettings["EndPointResetPassword"];
        public static string EndPointTerminos= WebConfigurationManager.AppSettings["EndPointTerminos"];

        //Construccion de codigos
        public static string[] AlfabetoG = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };//0-25
        public static string[] AlfabetoP = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };

        //Paypal 
        public static string EndPointTokenPaypal = WebConfigurationManager.AppSettings["EndPointTokenPaypal"];
        public static string ClientId  = WebConfigurationManager.AppSettings["ClientId"];
        public static string KeySecret = WebConfigurationManager.AppSettings["KeySecret"];
        public static string Client_Credentials = WebConfigurationManager.AppSettings["Client_Credentials"];
        public static string Grant_Type = "grant_type";

        // Globalization
        public const string CulturaEspañol = "es-ES";
        private const string CulturaIngles = "en-US";
        private const string CulturaPortugues = "pt-PT";
        private static string respuesta = string.Empty;
        private static string cultura = string.Empty;

        private static Idiomas I = new Idiomas();

        public static Idiomas Idiomas(int index)
        {
            string cultura = GetCultura();
            switch (cultura)
            {
                case (CulturaEspañol):
                    I.Español = "Español";
                    I.Ingles = "Ingles";
                    I.Portugues = "Portugues";
                    I.Terminos = "Terminos y condiciones de uso";
                    I.ResetPassword = "Olvido su contraseña";
                    I.Entrar = "Entrar";
                    I.Inicio = "Inicio";
                    I.Id = 1;
                    break;
                case (CulturaIngles):
                    I.Español = "Spanish";
                    I.Ingles = "English";
                    I.Portugues = "Portuguese";
                    I.Terminos = "Terms and conditions of use";
                    I.ResetPassword = "Forgot your password";
                    I.Entrar = "Get in";
                    I.Inicio = "Home";
                    I.Id = 2;
                    break;
                case (CulturaPortugues):
                    I.Español = "Espanhol";
                    I.Ingles = "Inglês";
                    I.Portugues = "Português";
                    I.Terminos = "Termos e condições de uso";
                    I.ResetPassword = "Esqueceu sua senha";
                    I.Entrar = "Para entrar";
                    I.Inicio = "Home";
                    I.Id = 3;
                    break;
            }
            if (index > 0)
                I.Id = index;

            return I;
        }

        public static string Cultura (int index)
        {
            switch (index)
            {
                case (1):
                    respuesta = CulturaEspañol;
                    break;
                case (2):
                    respuesta = CulturaIngles;
                    break;
                case (3):
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
                    respuesta = " Registro exitoso, enviamos una notificacion a tu correo para que actives tu cuenta";
                    break;
                case (CulturaIngles):
                    respuesta = " Successful registration, we send a notification to your e-mail to activate your account";
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
                    respuesta = " Su cuenta no ha sido activada, enviamos una notificacion a tu correo para que actives tu cuenta";
                    break;
                case (CulturaIngles):
                    respuesta = " Your account has not been activated, we send a notification to your e-mail to activate your account";
                    break;
                case (CulturaPortugues):
                    respuesta = " Sua conta não foi ativada, enviamos uma notificação para o seu e-mail para ativar sua conta";
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


        public static string IngreseCodigoVerificacion()
        {
            cultura = GetCultura();
            switch (cultura)
            {
                case (CulturaEspañol):
                    respuesta = "Ingrese codigo de verificacion";
                    break;
                case (CulturaIngles):
                    respuesta = "Enter verification code";
                    break;
                case (CulturaPortugues):
                    respuesta = "Insira o código de verificação";
                    break;
            }
            return respuesta;
        }


        public static string ActualizarContraseña()
        {
            cultura = GetCultura();
            switch (cultura)
            {
                case (CulturaEspañol):
                    respuesta = "Actualizar contraseña";
                    break;
                case (CulturaIngles):
                    respuesta = "Update password";
                    break;
                case (CulturaPortugues):
                    respuesta = "Atualizar senha";
                    break;
            }
            return respuesta;
        }

        public static string IngreseCodigo()
        {
            cultura = GetCultura();
            switch (cultura)
            {
                case (CulturaEspañol):
                    respuesta = "Ingrese codigo";
                    break;
                case (CulturaIngles):
                    respuesta = "Enter code";
                    break;
                case (CulturaPortugues):
                    respuesta = "Digite o código";
                    break;
            }
            return respuesta;
        }

        // Parametros notificacion para prueba del sitio

        public static string Saludo()
        {
            cultura = GetCultura();
            switch (cultura)
            {
                case (CulturaEspañol):
                    respuesta = "Saludos estimado(a) : ";
                    break;
                case (CulturaIngles):
                    respuesta = "Regards dear : ";
                    break;
                case (CulturaPortugues):
                    respuesta = "Saudações querida : ";
                    break;
            }
            return respuesta;
        }

        public static string PathLecturaArchivoTest = @"/Content/template/EmailBodyTest.cshtml";

        public static string AsuntoTest()
        {
            cultura = GetCultura();
            switch (cultura)
            {
                case (CulturaEspañol):
                    respuesta = "Por favor verifica tu e-mail...";
                    break;
                case (CulturaIngles):
                    respuesta = "Please verify your e-mail ...";
                    break;
                case (CulturaPortugues):
                    respuesta = "Por favor, verifique seu e-mail ...";
                    break;
            }
            return respuesta;
        }

        public static string ClickAqui()
        {
            cultura = GetCultura();
            switch (cultura)
            {
                case (CulturaEspañol):
                    respuesta = "Hazme Click Para Confirmar Tu Identidad!";
                    break;
                case (CulturaIngles):
                    respuesta = "Click Me To Confirm Your Identity!";
                    break;
                case (CulturaPortugues):
                    respuesta = "Clique Em Mim Para Confirmar Sua Identidade!";
                    break;
            }
            return respuesta;
        }

        public static string DescripcionTest()
        {
            cultura = GetCultura();
            switch (cultura)
            {
                case (CulturaEspañol):
                    respuesta = "Por favor confirma la direccion de correo electronico, para saber que eres realmente tu.";
                    break;
                case (CulturaIngles):
                    respuesta = "Please confirm the email address, to know that it is really you.";
                    break;
                case (CulturaPortugues):
                    respuesta = "Por favor confirme o endereço de e-mail, para saber que é realmente você.";
                    break;
            }
            return respuesta;
        }

        public static string ObservacionTest()
        {
            cultura = GetCultura();
            switch (cultura)
            {
                case (CulturaEspañol):
                    respuesta = "Bienvenido a nuestra aplicacion SudokuParaTodos, haz click en el link y disfruta 2 dias de prueba gratis </br>" + 
                                "El tiempo valido para este link es de 2 horas,</br>No tendras que aportar datos personales adicionales, tu direccion e-mail y toda tu informacion sera confidencial."; 
                    break;
                case (CulturaIngles):
                    respuesta = "Welcome to our SudokuParaTodos app, click on the link and enjoy 2 days of free trial </br> " + 
                                "The valid time for this link is 2 hours, </br> You will not have to provide additional personal data, Your e-mail address and all your information will be confidential.";
                    break;
                case (CulturaPortugues):
                    respuesta = "Bem-vindo ao nosso aplicativo SudokuParaTodos, clique no link e aproveite 2 dias de teste gratuito</ br > " + 
                                "O horário válido para este link é 2 horas, Você não terá que fornecer dados pessoais adicionais, seu endereço de e-mail e todas as suas informações serão confidenciais.";
                    break;
            }
            return respuesta;
        }


        // Parametros notificacion para registro de clientes
        public static string PathLecturaArchivoRegistro = @"/Content/template/EmailBodyRegister.cshtml";

        public static string AsuntoRegistro()
        {
            cultura = GetCultura();
            switch (cultura)
            {
                case (CulturaEspañol):
                    respuesta = "Por favor completa tu registro...";
                    break;
                case (CulturaIngles):
                    respuesta = "Please complete your registration...";
                    break;
                case (CulturaPortugues):
                    respuesta = "Por favor complete seu cadastro...";
                    break;
            }
            return respuesta;
        }


        public static string ClickAqui2()
        {
            cultura = GetCultura();
            switch (cultura)
            {
                case (CulturaEspañol):
                    respuesta = "Hazme Click Para Confirmar Tu Identidad!";
                    break;
                case (CulturaIngles):
                    respuesta = "Click Me To Confirm Your Identity!";
                    break;
                case (CulturaPortugues):
                    respuesta = "Clique Em Mim Para Confirmar Sua Identidade!";
                    break;
            }
            return respuesta;
        }


        public static string DescripcionRegistro()
        {
            cultura = GetCultura();
            switch (cultura)
            {
                case (CulturaEspañol):
                    respuesta = "Por favor confirma la direccion de correo electronico, para saber que eres realmente tu.";
                    break;
                case (CulturaIngles):
                    respuesta = "Please confirm the email address, to know that it is really you.";
                    break;
                case (CulturaPortugues):
                    respuesta = "Por favor confirme o endereço de e-mail, para saber que é realmente você.";
                    break;
            }
            return respuesta;
        }

        public static string ObservacionRegistro()
        {
            cultura = GetCultura();
            switch (cultura)
            {
                case (CulturaEspañol):
                    respuesta = "Bienvenido a nuestra aplicacion SudokuParaTodos, haz click en el link y disfruta 2 dias de prueba gratis </br>" +
                                "El tiempo valido para este link es de 2 horas,</br>No tendras que aportar datos personales adicionales, tu direccion e-mail y toda tu informacion sera confidencial.";
                    break;
                case (CulturaIngles):
                    respuesta = "Welcome to our SudokuParaTodos, click on the link and complete your registration </br>" +
                                "The valid time for this link is 2 hours, </br> All your information will be strictly confidential.";
                    break;
                case (CulturaPortugues):
                    respuesta = "Bem-vindo ao nosso aplicativo SudokuParaTodos, clique no link e complete seu registro</ br > " +
                                 "O tempo válido para este link é de 2 horas, </br> Todas as suas informações serão estritamente confidenciais.";
                    break;
            }
            return respuesta;
        }


        // Parametros notificacion para restablecer password
        public static string PathLecturaArchivoRestablecerPassword = @"/Content/template/EmailBodyResetPassword.cshtml";

        public static string AsuntoResetPassword()
        {
            cultura = GetCultura();
            switch (cultura)
            {
                case (CulturaEspañol):
                    respuesta = "Restablecer password SudokuParaTodos";
                    break;
                case (CulturaIngles):
                    respuesta = "Reset password SudokuParaTodos";
                    break;
                case (CulturaPortugues):
                    respuesta = "Redefinir senha SudokuParaTodos";
                    break;
            }
            return respuesta;
        }


        public static string ClickAqui3()
        {
            cultura = GetCultura();
            switch (cultura)
            {
                case (CulturaEspañol):
                    respuesta = "Hazme click para ir a la pagina de ingresar codigo para el restablecimiento de contraseña";
                    break;
                case (CulturaIngles):
                    respuesta = "Click me to go to the code entry page for password reset";
                    break;
                case (CulturaPortugues):
                    respuesta = "Clique em mim para ir para a página de entrada de código para redefinição de senha";
                    break;
            }
            return respuesta;
        }

        public static string DescripcionRestablecerPassword()
        {
            cultura = GetCultura();
            switch (cultura)
            {
                case (CulturaEspañol):
                    respuesta = "Haz click en el enlaze e ingresa el codigo para restablecer tu contraseña.";
                    break;
                case (CulturaIngles):
                    respuesta = "Click on the link and enter the code to reset your password.";
                    break;
                case (CulturaPortugues):
                    respuesta = "Clique no link e insira o código para redefinir sua senha.";
                    break;
            }
            return respuesta;
        }

        
        public static string ObservacionRestablecerPassword()
        {
            cultura = GetCultura();
            switch (cultura)
            {
                case (CulturaEspañol):
                    respuesta = "Este codigo es valido durante 2 horas.";
                    break;
                case (CulturaIngles):
                    respuesta = "This code is valid for 2 hours.";
                    break;
                case (CulturaPortugues):
                    respuesta = "Este código é válido por 2 horas.";
                    break;
            }
            return respuesta;
        }

    }
}