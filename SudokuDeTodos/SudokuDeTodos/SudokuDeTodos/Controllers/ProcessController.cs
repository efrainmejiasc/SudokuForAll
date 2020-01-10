using SudokuDeTodos.Models.Sistema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SudokuDeTodos.Engine;
using SudokuDeTodos.Models.DbSistema;

namespace SudokuDeTodos.Controllers
{
    public class ProcessController : Controller
    {
        private readonly IEngineGameProcess Proceso;
        private readonly IEngineDb Metodo;
        private readonly IEngineProyect Funcion;
        private readonly IEngineNotificacion Notificacion;

        public ProcessController(IEngineGameProcess _Proceso, IEngineDb _Metodo, IEngineProyect _Funcion, IEngineNotificacion _Notificacion)
        {
            this.Proceso = _Proceso;
            this.Metodo = _Metodo;
            this.Funcion = _Funcion;
            this.Notificacion = _Notificacion;
        }

        [HttpPost]
        public JsonResult ListaLenguaje(int index = 0)
        {
            Idiomas idiomas = new Idiomas();
            idiomas = EngineData.Idiomas(index);
            return Json(idiomas);
        }

        [HttpPost]
        public string GuardarJuego(string valor, int i, int j)
        {
            bool resultado = false;
            try
            {
                EngineDataGame ValorGame = EngineDataGame.Instance();
                string pathArchivo = ValorGame.PathArchivo;
                ValorGame.valorIngresado[i, j] = valor;
                this.Proceso.GuardarValoresIngresados(pathArchivo, ValorGame.valorIngresado);
                this.Proceso.GuardarValoresEliminados(pathArchivo, ValorGame.valorEliminado);
                this.Proceso.GuardarValoresInicio(pathArchivo, ValorGame.valorInicio);
                this.Proceso.GuardarValoresSolucion(pathArchivo, ValorGame.valorSolucion);
                resultado = true;
            }
            catch { }
          
            Respuesta response = new Respuesta();
            response.Status = resultado;
            string respuesta = Newtonsoft.Json.JsonConvert.SerializeObject(response);
            return respuesta;
        }

        [HttpPost]
        public JsonResult ExisteGalleta()
        {
            Respuesta respuesta = new Respuesta();
            bool existe = (bool)System.Web.HttpContext.Current.Session["MiGalleta"];
            if (existe)
                respuesta.Descripcion = string.Empty;
            else
                respuesta.Descripcion = "Equipo no registrado, desea continuar y registrarlo ?";

            return Json(respuesta);
        }

        [HttpPost]
        public JsonResult ReturnVarSession(string nameVar)
        {
            Respuesta respuesta = new Respuesta();
            if (System.Web.HttpContext.Current.Session[nameVar] != null)
                respuesta.Descripcion = System.Web.HttpContext.Current.Session[nameVar].ToString();
            else
                respuesta.Descripcion = null;

            return Json(respuesta);
        }

        [HttpPost]
        public JsonResult EnviarNotificacionPrueba(string email)
        {
            Respuesta respuesta = new Respuesta();
            respuesta.Status = Funcion.EmailEsValido(email);
            if (!respuesta.Status) //email no valido
            {
                respuesta = Funcion.ConstruirRespuesta(101, false, EngineData.EmailNoValido(), email);
                return Json(respuesta);
            }
            Cliente cliente = Funcion.ConstruirCliente(email);
            respuesta.Status = Metodo.InsertarCliente(cliente);
            if (!respuesta.Status)//error interno del servidor
            {
                respuesta = Funcion.ConstruirRespuesta(102, false, EngineData.ErrorInternoServidor(), email);
                return Json(respuesta);
            }
            string enlaze = Funcion.ConstruirEnlazePrueba(email, cliente.Identidad);
            EstructuraMail estructuraMail = Funcion.SetEstructuraMailTest(enlaze, email);
            respuesta.Status = Notificacion.EnviarMailNotificacion(estructuraMail);
            if (!respuesta.Status)//No se pudo enviar notificacion
            {
                respuesta = Funcion.ConstruirRespuesta(103, false, EngineData.ErrorEnviandoMail(), email);
                return Json(respuesta);
            }
            respuesta = Funcion.ConstruirRespuesta(100, true, EngineData.RegistroExitoso(), email);
            return Json(respuesta);
        }

        [HttpPost]
        public JsonResult EnviarOtraNotificacionPrueba(string email)
        {
            Respuesta respuesta = new Respuesta();
            respuesta.Status = Funcion.EmailEsValido(email);
            if (!respuesta.Status) 
            {
                respuesta = Funcion.ConstruirRespuesta(101, false, EngineData.EmailNoValido(), email);
                return Json(respuesta);
            }
            Guid identidad = Metodo.GetIdentidadCliente(email);
            Cliente cliente = Funcion.ConstruirCliente(email,identidad);
            string enlaze = Funcion.ConstruirEnlazePrueba(email, cliente.Identidad);
            EstructuraMail estructuraMail = Funcion.SetEstructuraMailTest(enlaze, email);
            respuesta.Status = Notificacion.EnviarMailNotificacion(estructuraMail);
            if (!respuesta.Status)//No se pudo enviar notificacion
            {
                respuesta = Funcion.ConstruirRespuesta(103, false, EngineData.ErrorEnviandoMail(), email);
                return Json(respuesta);
            }
            respuesta = Funcion.ConstruirRespuesta(100, true, EngineData.TransaccionExitosa(), email);
            return Json(respuesta);
        }

        [HttpPost]
        public JsonResult EnviarCodigo(string email)
        {
            Respuesta respuesta = new Respuesta();
            string type = Funcion.DecodeBase64(EngineData.ResetPassword);
            bool resultado = Funcion.EmailEsValido(email);
            if (!resultado)
            {
                respuesta = Funcion.ConstruirRespuesta(101, false, EngineData.EmailNoValido(), email, type);
                return Json(respuesta);
            }
            Guid identidad = Metodo.GetIdentidadCliente(email);
            if (identidad == Guid.Empty)
            {
                respuesta = Funcion.ConstruirRespuesta(102, false, EngineData.ErrorInternoServidor(), email, type);
                return Json(respuesta);
            }
            string codigo = Funcion.ConstruirCodigo();
            ResetPassword resetPassword = Funcion.SetResetPassword(email, codigo);
            resultado = Metodo.InsertarCodigoResetPassword(resetPassword);
            if (!resultado)
            {
                respuesta = Funcion.ConstruirRespuesta(102, false, EngineData.ErrorInternoServidor(), email, type);
                return Json(respuesta);
            }
            resultado = Funcion.EnviarNuevaNotificacion(identidad, email, EngineData.ResetPassword, null, codigo);
            if (!resultado)
            {
                respuesta = Funcion.ConstruirRespuesta(103, false, EngineData.ErrorEnviandoMail(), email, type);
                return Json(respuesta);
            }
            respuesta = Funcion.ConstruirRespuesta(100, true, EngineData.TransaccionExitosa(), email);
            return Json(respuesta);
        }

        [HttpGet]
        public ActionResult State(int id = 0, string email = "", string identidad = "", int status = 0, string date = "", string type = "",string cultureInfo = "",string ide = "")
        {
            Respuesta respuesta = new Respuesta();
            if (email == string.Empty || email == null)
            {
                respuesta.Id = -1;
                return View(respuesta);
            }

            string codeOrPassword = string.Empty;
            if (ide!= string.Empty && ide!= null)
                codeOrPassword = Funcion.DecodeBase64(ide);
            string mail = Funcion.DecodeBase64(email);
            string tipo = Funcion.DecodeBase64(type);
            bool resultado = false;
            Guid identificadorGuid = Metodo.GetIdentidadCliente(mail);
            if (identificadorGuid == Guid.Empty)
            {
                respuesta = Funcion.ConstruirRespuesta(3, false, EngineData.ErrorInternoServidor(), mail, tipo); //valido existencia de guid cliente
                return View(respuesta);
            }
            resultado = Funcion.EmailEsValido(mail); //valido formato email
            if (!resultado)
            {
                respuesta = Funcion.ConstruirRespuesta(1, false, EngineData.EmailNoValido(), mail, tipo);
                return View(respuesta);
            }
            System.Web.HttpContext.Current.Session["EMAIL"] = mail;
            resultado = Funcion.ValidacionTypeTransaccion(type);//valido tipo de transaccion
            if (!resultado)
            {
                respuesta = Funcion.ConstruirRespuesta(2, false, EngineData.TransaccionNoValida(), mail, tipo);
                return View(respuesta);
            }
            resultado = Funcion.ValidacionIdentidad(mail, identidad, Metodo);//valido guid cliente
            if (!resultado)
            {
                respuesta = Funcion.ConstruirRespuesta(3, false, EngineData.ErrorInternoServidor(), mail, tipo);
                return View(respuesta);
            }
            Funcion.SetCultureInfo(cultureInfo);
            DateTime fechaEnvio = Convert.ToDateTime(date);
            resultado = Funcion.EstatusLink(fechaEnvio); //valido estatus del link
            if (!resultado)
            {
                if (type == EngineData.ResetPassword)
                {
                    string codigo = Funcion.ConstruirCodigo();
                    ResetPassword resetPassword = Funcion.SetResetPassword(mail, codigo);
                    resultado = Metodo.DeleteCodigoResetPassword(mail);
                    resultado = Metodo.InsertarCodigoResetPassword(resetPassword);
                    resultado = Funcion.EnviarNuevaNotificacion(identificadorGuid, email, type, codeOrPassword, codigo);
                }
                else
                {
                    resultado = Funcion.EnviarNuevaNotificacion(identificadorGuid, email, type, codeOrPassword, codeOrPassword);
                }
                if (!resultado)
                {
                    respuesta = Funcion.ConstruirRespuesta(3, false, EngineData.ErrorInternoServidor(), mail, tipo); //valido Enviar Nueva Notificacion 
                    return View(respuesta);
                }
                respuesta = Funcion.ConstruirRespuesta(0, false, EngineData.TiempoLinkExpiro(), mail, Funcion.MetodoTransactionType(type));
                return View(respuesta);
            }

            //*******************************************************************************************************************
            //*******************************************************************************************************************

            if (type == EngineData.Test) //Activacion Prueba
            {
                resultado = Metodo.UpdateClienteTest(mail, status);//actualizar el registro del cliete
                if (!resultado)
                {
                    respuesta = Funcion.ConstruirRespuesta(4, false, EngineData.ErrorActualizarCliente(), mail, tipo);
                    return View(respuesta);
                }
                respuesta = Funcion.ConstruirRespuesta(5, true, EngineData.ActivacionTestExitosa(), mail, tipo); //activacion exitosa 5
            }
            else if (type == EngineData.Register) //Activacion registro
            {
                resultado = Metodo.UpdateClienteRegister(mail, status);//actualizar registro del cliente 
                if (!resultado)
                {
                    respuesta = Funcion.ConstruirRespuesta(6, false, EngineData.ErrorActualizarCliente(), mail, tipo);
                    return View(respuesta);
                }
                respuesta = Funcion.ConstruirRespuesta(7, true, EngineData.ActivacionExitosa(), mail, tipo); //activacion exitosa 7
            }
            else if (type == EngineData.ResetPassword) //Restablecer Password
            {
                resultado = Metodo.ValidarCodigoResetPassword(mail, codeOrPassword);
                if (!resultado)
                {
                    respuesta = Funcion.ConstruirRespuesta(3, false, EngineData.ErrorInternoServidor(), mail, tipo);
                    return View(respuesta);
                }
                respuesta = Funcion.ConstruirRespuesta(8, true, EngineData.ActivacionExitosa(), mail, Funcion.MetodoTransactionType(type)); // link valido 9
            }
            else if (type == EngineData.RegisterManager)//registrar gerente
            {

            }

            return View(respuesta);
        }
    }
}