using SudokuForAll.Engine;
using SudokuForAll.Models;
using SudokuForAll.Models.DbSistema;
using SudokuForAll.Models.Sistema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SudokuForAll.Controllers
{
    public class HomeController : Controller
    {
        private IEngineDb Metodo;
        private IEngineProyect Funcion;
        private IEngineNotificacion Notificacion;
        private readonly SudokuContext Context;


        public HomeController(IEngineDb _Metodo, IEngineProyect _Funcion, IEngineNotificacion _Notificacion, SudokuContext _Context)
        {
            this.Metodo = _Metodo;
            this.Context = _Context;
            this.Funcion = _Funcion;
            this.Notificacion = _Notificacion;
        }
        public ActionResult Index()
        {
            Respuesta model = new Respuesta();
            model.Descripcion = "ocultarInicio";
            return View(model);
        }

        public ActionResult Login(string email = "", string password = "")
        {
            Respuesta R = new Respuesta();
            if (email == string.Empty || password == string.Empty)
                return View(R);

            bool resultado = Funcion.EmailEsValido(email);
            string emailCode64 = Funcion.ConvertirBase64(email);
            if (!resultado)
            {
                R = Funcion.RespuestaProceso( "Login", emailCode64 ,null ,email + " No es una direccion de correo valida.");
                return RedirectToAction("State", "Home", R);
            }

            password = Funcion.ConvertirBase64(email + password);
            int result = Metodo.ResultadoLogin(password);
            if (result == 0)
            {
                R = Funcion.RespuestaProceso("Open",emailCode64, null, email + " Tu Tiempo de juego expiro,debes volver a comprar.");// Cuando RespuetaAccion = Open -> No redirecciona a ninguna pagina
                return RedirectToAction("Buy", "Home");
            }
            else if (result == 1)
            {
                return RedirectToAction("PlayGame", "Game");// Entre 1 y 5 dias para expirar
            }
            else if (result == 2)
            {
                return RedirectToAction("PlayGame", "Game");//Mas de 6 dias para expirar
            }
            else if (result == -1)
            {
                R = Funcion.RespuestaProceso("Login", emailCode64, null, email + " Identificacion fallida, compruebe su email y contraseña");
                return RedirectToAction("State", "Home", R);
            }
            return View();
        }

        public ActionResult Register(ActivarCliente model = null)
        {
            Respuesta R = new Respuesta();
            if (model == null)
                return View(R);
            if (model.Email == null || model.Password == null || model.Password2 == null || model.Nombre == null || model.Apellido == null)
                return View(R);

            bool resultado = Funcion.EmailEsValido(model.Email);
            string emailCode64 = Funcion.ConvertirBase64(model.Email);
            if (!resultado)
            {
                R = Funcion.RespuestaProceso( "Register", emailCode64 , null, model.Email + " Es una direccion de correo electronica no valida.");
                return RedirectToAction("State", "Home", R);
            }
            resultado = Funcion.CompareString(model.Password, model.Password2);
            if (!resultado)
            {
                R = Funcion.RespuestaProceso("Register", emailCode64 , null, model.Email + " Las contraseñas deben ser identicas.");
                return RedirectToAction("State", "Home", R);
            }
            model.Estatus = false;
            model.FechaRegistro = DateTime.UtcNow;
            model.Password = Funcion.ConvertirBase64(model.Email + model.Password);
            int result = Metodo.ClienteRegistro(model);
            if (result <= 0)
            {
                R = Funcion.RespuestaProceso("Register", emailCode64 , null, model.Email + " Error al registrar cliente.Puede ser que la direccion de email se diferente a la utilizada.");
                return RedirectToAction("State", "Home", R);
            }

            string enlaze = Funcion.CrearEnlazeRegistro(Metodo, model.Email, model.Password2);
            EstructuraMail estructura = new EstructuraMail();
            estructura = Funcion.SetEstructuraMailRegister(enlaze, model.Email, estructura);
            resultado = Notificacion.EnviarMailNotificacion(estructura);
            if (resultado)
            {
                R = Funcion.RespuestaProceso("Index", emailCode64 , null,"Registro exitoso " + model.Email + " Enviamos una notificacion a tu correo para activar tu cuenta.");
                return RedirectToAction("State", "Home", R);
            }
            else
            {
                R = Funcion.RespuestaProceso("Open", emailCode64 , null, model.Email + "Error enviando notificacion");
                return RedirectToAction("State", "Home", R);
            }
        }

        public ActionResult Contact(Respuesta model = null)
        {
            if (model == null || model.Email == string.Empty || model.Email == null )
                return View(model);

            bool resultado = Funcion.EmailEsValido(model.Email);
            string emailCode64 = Funcion.ConvertirBase64(model.Email);
            if (!resultado)
            {
                model = Funcion.RespuestaProceso("Contact", emailCode64, null, model.Email + " Es una direccion de correo electronica no valida.");
                return RedirectToAction("State","Home", model);
            }

            // Suceso al entrar al sitio 
            int result = Metodo.ResultadoEntradaAlSitio(model.Email);
            if (result == 1)
            {
                System.Web.HttpContext.Current.Session["Email"] = model.Email;
                return RedirectToAction("PlayGame", "Game");//TIEMPO DE PRUEBA ES VALIDO
            }
            else if (result == 2 || result == 4)
            {
                model = Funcion.RespuestaProceso( "comprarRegistrarse", emailCode64, null, "Su tiempo de prueba expiro, desea comprar y registrase?");
                return RedirectToAction("State", "Home", model);//TIEMPO DE PRUEBA EXPIRO
            }
            else if (result == 3)
            {
                System.Web.HttpContext.Current.Session["Email"] = model.Email;
                return RedirectToAction("Login", "Home"); //CUENTA ACTIVADA CLIENTE REGISTRADO
            }
            else if (result == 5 || result == 7)
            {
                model = Funcion.RespuestaProceso("Index", emailCode64, null, model.Email + " Su cuenta no ha sido activada,revise su bandeja de entrada");
                return RedirectToAction("State", "Home", model); //CUENTA NO ACTIVADA CLIENTE REGISTRADO
            }
            else if (result == 6)
            {
                model = Funcion.RespuestaProceso(Funcion.DecodeBase64(EngineData.Test), model.Email, null,null); // EMAIL NO EXISTE
                return RedirectToAction("State", "Home",model);
            }
            return View(model);
        }
    
        [HttpGet]
        public ActionResult State(string email = "", string identidad = "", string date = "", string status = "", string ide = "", string type = "", Respuesta K = null)
        {
            Respuesta R = new Respuesta();
            bool resultado = false;
            Guid guidCliente = Guid.Empty;
            string emailCode64 = string.Empty;

            //Validar email
            if (email != string.Empty && email != null )
            {
                if (Funcion.CadenaBase64Valida(email))
                    email = Funcion.DecodeBase64(email);

                resultado = Funcion.EmailEsValido(email);
                emailCode64 = Funcion.ConvertirBase64(email);
                if (!resultado)
                {
                    if (K.RespuestaAccion != string.Empty && K.RespuestaAccion != null)
                        R = Funcion.RespuestaProceso(K.RespuestaAccion, emailCode64, null, email + " Es una direccion de correo electronica no valida.");
                    else if (type != string.Empty && type != null)
                        R = Funcion.RespuestaProceso("Index", emailCode64, null, email + " Es una direccion de correo electronica no valida.");
                     return View(R);
                }  
            }

            //Validar GUID de identidad
            if (identidad != string.Empty && identidad != null && type != string.Empty && type != null)
            {
                guidCliente = Metodo.ObtenerIdentidadCliente(email);
                string identificador = Funcion.EncodeMd5(guidCliente.ToString());
                resultado = Funcion.CompareString(identidad, identificador);
                if (!resultado)
                {
                    R = Funcion.RespuestaProceso("Open", emailCode64, null, "Intento de violacion de seguridad.");
                    return View(R);
                }
            }

            //validar tiempo de expiracion del link
            if (date != string.Empty && date != null && type != string.Empty && type != null)
            {
                date = date.Replace('*', ' ');
                date = date.Replace('+', '.');
                DateTime fechaEnvio = Convert.ToDateTime(date);
                DateTime fechaActivacion = DateTime.UtcNow;
                resultado = Funcion.EstatusLink(fechaEnvio, fechaActivacion);
                if (!resultado)
                {
                    resultado = Funcion.EnviarNuevaNotificacion(Notificacion, Metodo, Funcion.ConvertirBase64(email), type, ide);
                    R = Funcion.RespuestaProceso("Index", emailCode64 , null,"El tiempo valido para el link expiro, enviamos una nueva notificacion a tu correo.");
                    return View(R);
                }
            }

            //Activacion tiempo de prueba
            if (type == EngineData.Test)
            {
                Cliente client = Funcion.ConstruirActualizarClienteTest(email, identidad);
                EngineDb Metodo = new EngineDb();
                int act = Metodo.UpdateClienteTest(client);
                if (act > 0)
                    R = Funcion.RespuestaProceso("Contact", emailCode64, null, "Activacion exitosa, ingresa con tu email.");
                else
                    R = Funcion.RespuestaProceso("Index", emailCode64, null, "Activacion fallida");
            }
            //Activacion cuanta del cliente
            else if (type == EngineData.Register)
            {
                string password = Funcion.DecodeBase64(ide);
                password = Funcion.ConvertirBase64(email + password);
                ActivarCliente model = new ActivarCliente();
                model = Funcion.ConstruirActivarCliente(email, password);
                int act = Metodo.ClienteRegistroActivacion(model);
                if (act >= 1)
                    R = Funcion.RespuestaProceso("Login", emailCode64, null, "Activacion exitosa, identificate con tu email y password");
                else
                    R = Funcion.RespuestaProceso("Login", emailCode64, null, "Activacion Fallida");
            }
            // Enviar a restablecer password
            else if (type == EngineData.ResetPassword)
            {
                if (ide == string.Empty || ide == null)
                {
                    R = Funcion.RespuestaProceso("Open", emailCode64, null, "Intento de violacion de seguridad.");
                    return View(R);
                }
                string codigo = Funcion.DecodeBase64(ide);
                string code = Metodo.ObtenerCodigoRestablecerPassword(email);
                resultado = Funcion.CompareString(codigo, code);

                R = Funcion.RespuestaProceso(null, emailCode64, "codeVerify","Ingrese codigo de verificacion");
                return RedirectToAction("EditPasswordNotify", "Home", R);
            }

            if (K.RespuestaAccion != null)
                return View(K);


            return View(R);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult NotifySend()
        {
            return View();
        }


        [HttpPost]
        public JsonResult NotificacionPrueba(string email)
        {
            Respuesta R = new Respuesta();
            string emailCode64 = Funcion.ConvertirBase64(email);
            bool resultado = Funcion.EmailEsValido(email);
            if (!resultado)
            {
                R = Funcion.RespuestaProceso(null, null, "Direccion de correo electronica no valido", email);
                return Json(R);
            }
            resultado = Metodo.InsertarClienteTest(email, Funcion);
            string enlaze = Funcion.CrearEnlazePrueba(Metodo, email);
            EstructuraMail model = new EstructuraMail();
            model = Funcion.SetEstructuraMailTest(enlaze, email, model);
            if (!resultado)
            {
                R = Funcion.RespuestaProceso(null, email,null, "Error Registrando");
                return Json(R);
            }
            resultado = Notificacion.EnviarMailNotificacion(model);
            if (resultado)
                R = Funcion.RespuestaProceso(null, email,null, "Exito");
            else
                R = Funcion.RespuestaProceso(null, email,null, "Error Enviando");

            return Json(R);
        }

        [HttpPost]
        public JsonResult EmailUser()
        {
            Respuesta R = new Respuesta();
            if (System.Web.HttpContext.Current.Session["Email"] != null)
                R.Email = System.Web.HttpContext.Current.Session["Email"].ToString();
            else
                R.Email = string.Empty;
            return Json(R);
        }


        public ActionResult EditPasswordNotify(Respuesta K = null)
        {
            if(K.CodigoResetPassword != "codeVerify")
               K.Descripcion = "Restablecer Password";
            else
                K.Descripcion = "Ingrese Codigo";
            return View(K);
        }


        [HttpPost]
        public ActionResult NotificacionRestablecerPassword(string email)
        {
            Respuesta R = new Respuesta();
            string emailCode64 = Funcion.ConvertirBase64(email);
            bool resultado = Funcion.EmailEsValido(email);
            if (!resultado)
            {
                R = Funcion.RespuestaProceso("Open", emailCode64, null, email + " No es una direccion de correo valida.");
                return RedirectToAction("State", "Home", R);
            }
            Guid identidad = Metodo.ObtenerIdentidadCliente(email);
            if (identidad == Guid.Empty)
            {
                R = Funcion.RespuestaProceso("Open", emailCode64, null, "La direccion " + email + " No esta registrada , verifiquela por favor.");
                return RedirectToAction("State", "Home", R);
            }

            string codigo = Funcion.ConstruirCodigo();
            string enlaze = Funcion.CrearEnlazeRestablecerPassword(Metodo, email, codigo);
            EstructuraMail model = new EstructuraMail();
            model = Funcion.SetEstructuraMailResetPassword(enlaze, email, codigo, model);
            ResetPassword resetPassword = new ResetPassword();
            resetPassword = Funcion.SetResetPassword(email, codigo);
            resultado = Metodo.InsertarResetPassword(resetPassword);
            if (!resultado)
            {
                R = Funcion.RespuestaProceso( "Open", emailCode64, null, email + " Error insertando codigo de restablecimiento de contraseña");
                return RedirectToAction("State", "Home", R);
            }
            resultado = Notificacion.EnviarMailNotificacion(model);
            if (resultado)
                R = Funcion.RespuestaProceso("Index", email,null, "Exito");
            else
                R = Funcion.RespuestaProceso("Index",email,null,"Error");

            return Json(R);
        }

        [HttpPost]
        public ActionResult ValidarCodigoRestablecerPassword(string email, string codigo)
        {
            Respuesta R = new Respuesta();
            string emailCode64 = Funcion.ConvertirBase64(email);
            bool resultado = Funcion.EmailEsValido(email);
            if (!resultado)
            {
                R = Funcion.RespuestaProceso("Open", emailCode64 , null, email + " No es una direccion de correo valida.");
                return RedirectToAction("State", "Home", R);
            }
            string code = Metodo.ObtenerCodigoRestablecerPassword(email).Trim();
            codigo = codigo.Trim();
            resultado = Funcion.CompareString(code, codigo.Trim());
            if (!resultado)
            {
                R = Funcion.RespuestaProceso("EditPasswordNotify", emailCode64 , Funcion.ConvertirBase64("1E-9R-2R-8O"), email + " El codigo suministrado no coincide ,intentelo de nuevo.");
                return RedirectToAction("State", "Home", R);
            }
            int act = Metodo.UpdateResetPassword(email, codigo, true);
            if (act >= 1)
            {
                R = Funcion.RespuestaProceso("Index", emailCode64, null,"Exito");
            }
            else
            {
                R = Funcion.RespuestaProceso("Index", emailCode64, null, "Error");

            }
            return Json(R);
        }


        [HttpPost]
        public JsonResult DireccionSite(string nombreControlador, string nombreAccion)
        {
            Respuesta Redireccion = new Respuesta();
            Redireccion.Descripcion = EngineData.UrlBase + nombreControlador + "/" + nombreAccion + "/";
            return Json(Redireccion);
        }
    }
}