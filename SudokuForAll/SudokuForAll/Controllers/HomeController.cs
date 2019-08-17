using SudokuForAll.Engine;
using SudokuForAll.Models;
using SudokuForAll.Models.DbSistema;
using SudokuForAll.Models.Sistema;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
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

        public ActionResult Index(string lenguaje = "",int index = 0)
        {
            Respuesta model = new Respuesta();
            if (index > 0)
            {
                System.Web.HttpContext.Current.Session["Cultura"] = EngineData.Cultura(index);
                model.Id = index;
            }
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
                //Email no valido
                R = Funcion.RespuestaProceso( "Login", emailCode64 ,null ,email + EngineData.EmailNoValido());
                return RedirectToAction("State", "Home", R);
            }

            password = Funcion.ConvertirBase64(email + password);
            int result = Metodo.ResultadoLogin(password);
            if (result == 0)
            {
                // Cuando RespuetaAccion = Open -> No redirecciona a ninguna pagina
                R = Funcion.RespuestaProceso("Open",emailCode64, null, email + EngineData.TiempoJuegoExpiro());
                return RedirectToAction("BuyGame", "Game");
            }
            else if (result == 1)
            {
                // Entre 1 y 5 dias para expirar
                return RedirectToAction("PlayGame", "Game");
            }
            else if (result == 2)
            {
                //Mas de 6 dias para expirar
                return RedirectToAction("PlayGame", "Game");
            }
            else if (result == -1)
            {
                //Login fallido
                R = Funcion.RespuestaProceso("Login", emailCode64, null, email + EngineData.LoginFallido());
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
                //Email no valido
                R = Funcion.RespuestaProceso( "Register", emailCode64 , null, model.Email + EngineData.EmailNoValido());
                return RedirectToAction("State", "Home", R);
            }
            resultado = Funcion.CompareString(model.Password, model.Password2);
            if (!resultado)
            {
                //Las contraseñas deben ser iguales
                R = Funcion.RespuestaProceso("Register", emailCode64 , null, model.Email + EngineData.PasswordNoIdenticos());
                return RedirectToAction("State", "Home", R);
            }
            model.Estatus = false;
            model.FechaRegistro = DateTime.UtcNow;
            model.Password = Funcion.ConvertirBase64(model.Email + model.Password);
            int result = Metodo.ClienteRegistro(model);
            if (result <= 0)
            {
                //Error al registrar cliente
                R = Funcion.RespuestaProceso("Register", emailCode64 , null, model.Email + EngineData.ErrorRegistroCliente());
                return RedirectToAction("State", "Home", R);
            }

            string enlaze = Funcion.CrearEnlazeRegistro(Metodo, model.Email, model.Password2);
            EstructuraMail estructura = new EstructuraMail();
            estructura = Funcion.SetEstructuraMailRegister(enlaze, model.Email, estructura);
            resultado = Notificacion.EnviarMailNotificacion(estructura);
            if (resultado)
            {
                //Registro exitoso
                R = Funcion.RespuestaProceso("Index", emailCode64 , null, model.Email + EngineData.RegistroExitoso());
                return RedirectToAction("State", "Home", R);
            }
            else
            {
                //Error enviando notficacion - error interno
                R = Funcion.RespuestaProceso("Open", emailCode64 , null, model.Email + EngineData.ErrorEnviandoMail());
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
                model = Funcion.RespuestaProceso("Contact", emailCode64, null, model.Email + EngineData.EmailNoValido());
                return RedirectToAction("State","Home", model);
            }

            // Suceso al entrar al sitio 
            int result = Metodo.ResultadoEntradaAlSitio(model.Email);
            if (result == 1)
            {
                //TIEMPO DE PRUEBA ES VALIDO
                System.Web.HttpContext.Current.Session["Email"] = model.Email;
                return RedirectToAction("PlayGame", "Game");
            }
            else if (result == 2 || result == 4)
            {
                //TIEMPO DE PRUEBA EXPIRO
                model = Funcion.RespuestaProceso( "comprarRegistrarse", emailCode64, null, EngineData.TiempoPruebaJuegoExpiro());
                return RedirectToAction("State", "Home", model);
            }
            else if (result == 3)
            {
                //CUENTA ACTIVADA CLIENTE REGISTRADO
                System.Web.HttpContext.Current.Session["Email"] = model.Email;
                return RedirectToAction("Login", "Home");
            }
            else if (result == 5 || result == 7)
            {
                //CUENTA NO ACTIVADA CLIENTE REGISTRADO
                string enlaze = string.Empty;
                if (result == 5)
                {
                    string password = Metodo.ObtenerPasswordCliente(model.Email);
                    resultado = Funcion.EnviarNuevaNotificacion(Notificacion, Metodo, emailCode64, EngineData.Register , password);
                }
                else if (result == 7)
                {
                    resultado = Funcion.EnviarNuevaNotificacion(Notificacion, Metodo, emailCode64, EngineData.Test);
                }
                model = Funcion.RespuestaProceso("Index", emailCode64, null, model.Email + EngineData.CuentaNoActivada());
                return RedirectToAction("State", "Home", model); 
            }
            else if (result == 6)
            {
                // EMAIL NO EXISTE PUEDE PROBAR
                model = Funcion.RespuestaProceso(Funcion.DecodeBase64(EngineData.Test), model.Email, null,null);
                return RedirectToAction("State", "Home",model);
            }
            return View(model);
        }
    
       [HttpGet]
        public ActionResult State(int Id = 0 ,string email = "", string identidad = "", string date = "", string status = "", string ide = "", string type = "",  string cultureInfo = "", Respuesta K = null)
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
                        R = Funcion.RespuestaProceso(K.RespuestaAccion, emailCode64, null, email + EngineData.EmailNoValido());
                    else if (type != string.Empty && type != null)
                        R = Funcion.RespuestaProceso("Index", emailCode64, null, email + EngineData.EmailNoValido());
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
                    R = Funcion.RespuestaProceso("Open", emailCode64, null,  EngineData.ErrorInternoServidor());
                    return View(R);
                }
            }

            //validar tiempo de expiracion del link
            if (date != string.Empty && date != null && type != string.Empty && type != null)
            {
                date = date.Replace('*', ' ');
                date = date.Replace('+', ' ');
                date = date.Replace('a', ' ');
                date = date.Replace('p', ' ');
                date = date.Replace('m', ' ');
                date = date.Trim();
                Funcion.SetCultureInfo(cultureInfo);
                CultureInfo ci = new CultureInfo(cultureInfo);
                Thread.CurrentThread.CurrentUICulture = ci;
                Thread.CurrentThread.CurrentCulture = ci;
                DateTime fechaEnvio = Convert.ToDateTime(date);
                DateTime fechaActivacion = DateTime.UtcNow;
                resultado = Funcion.EstatusLink(fechaEnvio, fechaActivacion);
                if (!resultado)
                {
                    resultado = Funcion.EnviarNuevaNotificacion(Notificacion, Metodo, Funcion.ConvertirBase64(email), type, ide);
                    R = Funcion.RespuestaProceso("Index", emailCode64 , null,EngineData.TiempoLinkExpiro());
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
                    R = Funcion.RespuestaProceso("Contact", emailCode64, null, EngineData.ActivacionExitosa());
                else
                    R = Funcion.RespuestaProceso("Index", emailCode64, null, EngineData.ActivacionFallida());
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
                    R = Funcion.RespuestaProceso("Login", emailCode64, null, EngineData.ActivacionExitosa());
                else
                    R = Funcion.RespuestaProceso("Login", emailCode64, null, EngineData.ActivacionFallida());
            }
            // Enviar a restablecer password
            else if (type == EngineData.ResetPassword)
            {
                if (ide == string.Empty || ide == null)
                {
                    R = Funcion.RespuestaProceso("Open", emailCode64, null, EngineData.ErrorInternoServidor());
                    return View(R);
                }
                string codigo = Funcion.DecodeBase64(ide);
                string code = Metodo.ObtenerCodigoRestablecerPassword(email);
                resultado = Funcion.CompareString(codigo, code);
                if (!resultado)
                {
                    R = Funcion.RespuestaProceso("Open", emailCode64, null, EngineData.ErrorInternoServidor());
                    return View(R);
                }
                System.Web.HttpContext.Current.Session["Email"] = email;
                R = Funcion.RespuestaProceso(null, emailCode64, "codeVerify",EngineData.IngreseCodigoVerificacion());
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
                R = Funcion.RespuestaProceso(null, null, EngineData.EmailNoValido(), email);
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


        public ActionResult EditPasswordNotify(Respuesta K = null)
        {
            if (K.CodigoResetPassword != "codeVerify")
                K.Descripcion = EngineData.ActualizarContraseña(); 
            else
                K.Descripcion = EngineData.IngreseCodigo();
            return View(K);
        }


        public ActionResult EditPassword(ActivarCliente model)
        {
            Respuesta R = new Respuesta();
            if (model == null)
                return View(R);
            if (model.Email == null || model.Password == null || model.Password2 == null )
                return View(R);

            bool resultado = Funcion.EmailEsValido(model.Email);
            string emailCode64 = Funcion.ConvertirBase64(model.Email);
            if (!resultado)
            {
                //Email no valido
                R = Funcion.RespuestaProceso("EditPassword", emailCode64, null, model.Email + EngineData.EmailNoValido());
                return RedirectToAction("State", "Home", R);
            }
            resultado = Funcion.CompareString(model.Password, model.Password2);
            if (!resultado)
            {
                //Las contraseñas deben ser identicas
                R = Funcion.RespuestaProceso("EditPassword", emailCode64, null, model.Email + EngineData.PasswordNoIdenticos());
                return RedirectToAction("State", "Home", R);
            }
            model.Estatus = false;
            model.FechaRegistro = DateTime.UtcNow;
            model.Password = Funcion.ConvertirBase64(model.Email + model.Password);
            int result = Metodo.ClienteUpdatePassword(model);
            if (result <= 0)
            {
                //Fallo modificar contraseña
                R = Funcion.RespuestaProceso("EditPassword", emailCode64, null, model.Email + EngineData.RestablecerContraseñaFallida());
                return RedirectToAction("State", "Home", R);
            }
            System.Web.HttpContext.Current.Session["Email"] = model.Email;
            R = Funcion.RespuestaProceso("Login", emailCode64, null, model.Email + EngineData.RestablecerContraseñaExito());
            return RedirectToAction("State", "Home", R);

        }

        //*************************************************** Invoke AJAX ***********************************************************

        [HttpPost]
        public ActionResult NotificacionRestablecerPassword(string email)
        {
            Respuesta R = new Respuesta();
            string emailCode64 = Funcion.ConvertirBase64(email);
            bool resultado = Funcion.EmailEsValido(email);
            if (!resultado)
            {
                //Email no valido
                R = Funcion.RespuestaProceso("Email_No_Valido", emailCode64, null, email + EngineData.EmailNoValido());
                return Json(R);
            }
            Guid identidad = Metodo.ObtenerIdentidadCliente(email);
            if (identidad == Guid.Empty)
            {
                // Email no registrado
                R = Funcion.RespuestaProceso("Email_No_Registrado", emailCode64, null, email +  EngineData.EmailNoRegistrado());
                return Json(R);
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
                // Error insertando codigo
                R = Funcion.RespuestaProceso( "Error_Insertando_Codigo", emailCode64, null, email +  EngineData.ErrorInternoServidor());
                return Json(R);
            }
            resultado = Notificacion.EnviarMailNotificacion(model);
            if (resultado)
                R = Funcion.RespuestaProceso("Exito", email,null, email + EngineData.EnvioCodigoRestablecerPassword()); //Envio de codigo restablecer password
            else
                R = Funcion.RespuestaProceso("Error",email,null, email + EngineData.ErrorEnviandoMail()); //Error enviando notificacion

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
                //Email no valido
                R = Funcion.RespuestaProceso("Email_No_Valido", emailCode64 , null, email + EngineData.EmailNoValido());
                return Json(R);
            }
            string code = Metodo.ObtenerCodigoRestablecerPassword(email).Trim();
            codigo = codigo.Trim();
            resultado = Funcion.CompareString(code, codigo.Trim());
            if (!resultado)
            {
                //El codigo ingresado no coincide
                R = Funcion.RespuestaProceso("Codigo_No_Match", emailCode64 , Funcion.ConvertirBase64("1E-9R-2R-8O"), email + EngineData.CodigoNoCoincide());
                return Json(R);
            }
            int act = Metodo.UpdateResetPassword(email, codigo, true);
            if (act >= 1)
            {
                System.Web.HttpContext.Current.Session["Email"] = email;
                R = Funcion.RespuestaProceso("Exito", emailCode64, null, "Exito");
            }
            else
            {
                //Error al validar el codigo
                R = Funcion.RespuestaProceso("Error", emailCode64, null, email +  EngineData.ErrorInternoServidor());

            }
            return Json(R);
        }


        [HttpPost]
        public JsonResult DireccionSite(string nombreControlador, string nombreAccion)
        {
            Respuesta Redireccion = new Respuesta();
            Redireccion.Descripcion = EngineData.UrlBase + nombreControlador + "/" + nombreAccion ;
            return Json(Redireccion);
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
        public JsonResult ListaLenguaje (int index = 0)
        {
            Idiomas idiomas = new Idiomas();
            idiomas = EngineData.Idiomas(index);
            return Json(idiomas);
        }

        [HttpPost]
        public JsonResult CheckProcessEditPasswordNotify (string lblTexto, string email = "")
        {
            Respuesta model = new Respuesta();
            string texto = EngineData.ActualizarContraseña();
            int idCliente = 0;
            bool resultado = Funcion.CompareString(lblTexto, texto);
            if (resultado)
            {
                idCliente = Metodo.ObtenerIdCliente(email, true);
                if (idCliente > 0)
                    model.RespuestaAccion = "ResetPassword";
            }
            else
            {
                texto = EngineData.IngreseCodigo();
                resultado = Funcion.CompareString(lblTexto, texto);
                if(resultado)
                    model.RespuestaAccion = "EnterCode";
            }

            return Json(model);
        }

    }
}