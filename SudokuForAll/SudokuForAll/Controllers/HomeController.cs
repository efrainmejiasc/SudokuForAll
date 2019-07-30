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
            if (!resultado)
            {
                R = Funcion.RespuestaProceso("State", "Home", "Login", email,null ,email + " No es una direccion de correo valida.");
                return RedirectToAction(R.NombreAccion,R.NombreControlador, R);
            }

            password = Funcion.ConvertirBase64(email + password);
            int result = Metodo.ResultadoLogin(password);
            if (result == 0)
            {
                R = Funcion.RespuestaProceso("State", "Home", "Open", email,null, email + " Tu Tiempo de juego expiro,debes volver a comprar.");// Cuando RespuetaAccion = Open -> No redirecciona a ninguna pagina
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
                R = Funcion.RespuestaProceso("State", "Home", "Login",email,null, email + " Identificacion fallida, compruebe su email y contraseña");
             return RedirectToAction(R.NombreAccion,R.NombreControlador, R);
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
            if (!resultado)
            {
                R = Funcion.RespuestaProceso("State", "Home", "Register", model.Email, null, model.Email + " Es una direccion de correo electronica no valida.");
                return RedirectToAction(R.NombreAccion,R.NombreControlador, R);
            }
            resultado = Funcion.CompareString(model.Password, model.Password2);
            if (!resultado)
            {
                R = Funcion.RespuestaProceso("State","Home","Register",model.Email,null, model.Email + " Las contraseñas deben ser identicas.");
                return RedirectToAction(R.NombreAccion, R.NombreControlador, R);
            }
            model.Estatus = false;
            model.FechaRegistro = DateTime.UtcNow;
            model.Password = Funcion.ConvertirBase64(model.Email + model.Password);
            int result = Metodo.ClienteRegistro(model);
            if (result <= 0)
            {
                R = Funcion.RespuestaProceso("State","Home","Register",model.Email,null, model.Email + " Error al registrar cliente.Puede ser que la direccion de email se diferente a la utilizada.");
                return RedirectToAction(R.NombreAccion, R.NombreControlador, R);
            }

            string enlaze = Funcion.CrearEnlazeRegistro(Metodo, model.Email, model.Password2);
            EstructuraMail estructura = new EstructuraMail();
            estructura = Funcion.SetEstructuraMailRegister(enlaze, model.Email, estructura);
            resultado = Notificacion.EnviarMailNotificacion(estructura);
            if (resultado)
            {
                R = Funcion.RespuestaProceso("State", "Home", "Index", model.Email,null,"Registro exitoso " + model.Email + " Enviamos una notificacion a tu correo para activar tu cuenta.");
                return RedirectToAction(R.NombreAccion, R.NombreControlador, R);
            }
            else
            {
                R = Funcion.RespuestaProceso("State", "Home", "Open",model.Email, null, model.Email + "Error enviando notificacion");
                return RedirectToAction(R.NombreAccion, R.NombreControlador, R);
            }
        }

        public ActionResult Contact(Respuesta model = null)
        {
            if (model == null || model.Email == string.Empty || model.Email == null )
                return View(model);

            bool resultado = Funcion.EmailEsValido(model.Email);
            if (!resultado)
            {
                model = Funcion.RespuestaProceso("State", "Home","Contact", model.Email, null, model.Email + " Es una direccion de correo electronica no valida.");
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
                model = Funcion.RespuestaProceso("State", "Home", "comprarRegistrarse", model.Email, null, "Su tiempo de prueba expiro, desea comprar y registrase?");
                return RedirectToAction(model.NombreAccion, model.NombreControlador,model);//TIEMPO DE PRUEBA EXPIRO
            }
            else if (result == 3)
            {
                System.Web.HttpContext.Current.Session["Email"] = model.Email;
                return RedirectToAction("Login", "Home"); //CUENTA ACTIVADA CLIENTE REGISTRADO
            }
            else if (result == 5 || result == 7)
            {
                model = Funcion.RespuestaProceso("State", "Home", "Index", model.Email, null, model.Email + " Su cuenta no ha sido activada,revise su bandeja de entrada");
                return RedirectToAction(model.NombreAccion, model.NombreControlador,model); //CUENTA NO ACTIVADA CLIENTE REGISTRADO
            }
            else if (result == 6)
            {
                model = Funcion.RespuestaProceso("State","Home", Funcion.DecodeBase64(EngineData.Test), model.Email,null,null); // EMAIL NO EXISTE
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
            int id = -1;

            //Validar email
            if (email != string.Empty && email != null )
            {
                if (Funcion.CadenaBase64Valida(email))
                    email = Funcion.DecodeBase64(email);
              
                resultado = Funcion.EmailEsValido(email);
                if (!resultado)
                {
                    if(K.RespuestaAccion != string.Empty && K.RespuestaAccion != null)
                        R = Funcion.RespuestaProceso(null, null, K.RespuestaAccion, email, null, email + " Es una direccion de correo electronica no valida.");
                    else if (type != string.Empty && type != null)
                        R = Funcion.RespuestaProceso(null, null, "Open", email, null, email + " Es una direccion de correo electronica no valida.");
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
                    R = Funcion.RespuestaProceso(null, null,"Open", email, null, "Intento de violacion de seguridad.");
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
                    R = Funcion.RespuestaProceso(null,null,"Open", email,null,"El tiempo valido para el link expiro, enviamos una nueva notificacion a tu correo.");
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
                    R = Funcion.RespuestaProceso(null,null,"Contact", email ,null, "Activacion exitosa, ingresa con tu email.");
                else
                    R = Funcion.RespuestaProceso(null,null,"Open", email ,null, "Activacion fallida");
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
                    R = Funcion.RespuestaProceso(null,null,"Login", email, null, "Activacion exitosa, identificate con tu email y password");
                else
                    R = Funcion.RespuestaProceso(null,null,"Login",email,null, "Activacion Fallida");
            }
            // Enviar a restablecer password
            else if (type == EngineData.ResetPassword)
            {
                if (ide == string.Empty || ide == null)
                {
                    R = Funcion.RespuestaProceso(null,null,"Open", email ,null, "Intento de violacion de seguridad.");
                    return View(R);
                }
                string codigo = Funcion.DecodeBase64(ide);
                string code = Metodo.ObtenerCodigoRestablecerPassword(email);
                resultado = Funcion.CompareString(codigo, code);

                R = Funcion.RespuestaProceso("Verificacion de codigo de seguridad", email, ide);
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

        public ActionResult EditPasswordNotify()
        {
            return View();
        }

        [HttpPost]
        public JsonResult NotificacionPrueba(string email)
        {
            Respuesta R = new Respuesta();
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
                R = Funcion.RespuestaProceso(null,null,"Error Registrando", email);
                return Json(R);
            }
            resultado = Notificacion.EnviarMailNotificacion(model);
            if (resultado)
                R = Funcion.RespuestaProceso(null,null,"Exito", email);
            else
                R = Funcion.RespuestaProceso(null,null,"Error Enviando", email);

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

        [HttpPost]
        public ActionResult NotificacionRestablecerPassword(string email)
        {
            Respuesta R = new Respuesta();
            bool resultado = Funcion.EmailEsValido(email);
            if (!resultado)
            {
                R = Funcion.RespuestaProceso("State","Home","Open",email,null, email + " No es una direccion de correo valida.");
                return RedirectToAction("State", "Home", R);
            }
            Guid identidad = Metodo.ObtenerIdentidadCliente(email);
            if (identidad == Guid.Empty)
            {
                R = Funcion.RespuestaProceso("State", "Home","Open",email,null, "La direccion " + email + " No esta registrada , verifiquela por favor.");
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
                R = Funcion.RespuestaProceso("State", "Home", "Open", email, null, email + " Error insertando codigo de restablecimiento de contraseña");
                return RedirectToAction("State", "Home", R);
            }
            resultado = Notificacion.EnviarMailNotificacion(model);
            if (resultado)
                R = Funcion.RespuestaProceso(null,null,"Index", email,null, "Exito");
            else
                R = Funcion.RespuestaProceso(null,null,"Index",email,null,"Error Enviando");

            return Json(R);
        }

        [HttpPost]
        public ActionResult ValidarCodigoRestablecerPassword(string email, string codigo)
        {
            Respuesta R = new Respuesta();
            bool resultado = Funcion.EmailEsValido(email);
            if (!resultado)
            {
                R = Funcion.RespuestaProceso("State", "Home", "Open", email, null, email + " No es una direccion de correo valida.");
                return RedirectToAction("State", "Home", R);
            }
            string code = Metodo.ObtenerCodigoRestablecerPassword(email).Trim();
            codigo = codigo.Trim();
            resultado = Funcion.CompareString(code, codigo.Trim());
            if (!resultado)
            {
                R = Funcion.RespuestaProceso("State","Home","EditPasswordNotify",email, Funcion.ConvertirBase64("-1E-9R-2R-8O"), email + " El codigo suministrado no coincide ,intentelo de nuevo.");
                return RedirectToAction("State", "Home", R);
            }
            int act = Metodo.UpdateResetPassword(email, codigo, true);
            if (act >= 1)
            {
                R = Funcion.RespuestaProceso(null , null , "Index",email, null,"Exito, " +  email + " Codigo de verificacion validado exitosamente");
            }
            else
            {
                R = Funcion.RespuestaProceso(null, null, "Index", email, null, "Error, " + email + " Restriccion para restablecer password");

            }
            return Json(R);
        }


    }
}