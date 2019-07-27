using SudokuForAll.Engine;
using SudokuForAll.Models;
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

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }
        public ActionResult Contact(Respuesta model = null)
        {
            if (model == null || model.Email == string.Empty || model.Email == null )
                return View(model);

            bool resultado = Funcion.EmailEsValido(model.Email);
            if (!resultado)
            {
                model = Funcion.RespuestaProceso("State", "Home", "Contact",model.Email, null, model.Email + " Es una direccion de correo electronica no valida.");
                return RedirectToAction(model.NombreAccion, model.NombreControlador, model);
            }

            // Suceso al entrar al sitio 
            int result = Metodo.ResultadoEntradaAlSitio(model.Email);
            if (result == 1)
            {
                System.Web.HttpContext.Current.Session["Email"] = model.Email;
                return RedirectToAction("PlayGame", "Game");//TIEMPO DE PRUEBA ES VALIDO
            }
            else if (result == 2)
            {
                return RedirectToAction("NotifySend", "Home",Funcion.DecodeBase64(EngineData.Test)); //TIEMPO PRUEBA VALIDO CUENTA NO ACTIVADA
            }
            else if (result == 3)
            {
                return RedirectToAction("Login", "Home"); //CUENTA ACTIVADA CLIENTE REGISTRADO
            }
            else if (result == 4)
            {
                return RedirectToAction("Register", "Home");//TIEMPO DE PRUEBA EXPIRO
            }
            else if (result == 5)
            {
                return RedirectToAction("NotifySend", "Home", Funcion.DecodeBase64(EngineData.Register)); //CUENTA NO ACTIVADA CLIENTE REGISTRADO
            }
            else if (result == 6)
            {
                model = Funcion.RespuestaProceso(null,null, Funcion.DecodeBase64(EngineData.Test), model.Email,null,null); //EMAIL NO EXISTE
                return RedirectToAction("State", "Home",model);
            }
            return View(model);
        }
    
        private Respuesta SucesoMensaje (int result , Respuesta model)
        {
            string retorno = string.Empty;
            switch (result)
            {
                case (1):
                    model = Funcion.RespuestaProceso("PlayGame","Game"); //Tiempo de prueba valido
                    break;

            }
            return model;
        }

        public ActionResult State(Respuesta model = null)
        {
            return View(model);
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

    }
}