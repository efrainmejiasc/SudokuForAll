﻿using SudokuForAll.Engine;
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

        public ActionResult State(string email = "", string identidad = "", string date = "", string status = "", string ide = "", string type = "", Respuesta K = null)
        {
            Respuesta R = new Respuesta();
  
            bool resultado = false;
            Guid guidCliente = Guid.Empty;
            int id = -1;

            //Validar email
            if (email != string.Empty && email != null)
            {
                if (Funcion.CadenaBase64Valida(email))
                {
                    email = Funcion.DecodeBase64(email);
                }
                resultado = Funcion.EmailEsValido(email);
                if (!resultado)
                {
                    R = Funcion.RespuestaProceso(null,null,"Open", email ,null, "No es una direccion de correo valida.");
                    return View(R);
                }
                id = Metodo.ObtenerIdCliente(email);
                if (id <= 0)
                {
                    R = Funcion.RespuestaProceso(null, null, "Open", email , null, "Intento de violacion de seguridad.");
                    return View(R);
                }
            }

            //Validar GUID de identidad
            if (identidad != string.Empty && identidad != null)
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
            if (date != string.Empty && date != null)
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

            //Redireccionamiento entrada test
            if (System.Web.HttpContext.Current.Session["ResultadoEntrada"] != null && System.Web.HttpContext.Current.Session["Email"] != null)
            {
                string valor = System.Web.HttpContext.Current.Session["ResultadoEntrada"].ToString();
                email = System.Web.HttpContext.Current.Session["Email"].ToString();
                string[] T = valor.Split('/');
                R = Funcion.RespuestaProceso(T[1], email);
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