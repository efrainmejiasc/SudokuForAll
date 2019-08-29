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
    public class ManagerController : Controller
    {
        private IEngineDb Metodo;
        private IEngineProyect Funcion;
        private IEngineNotificacion Notificacion;
        private readonly SudokuContext Context;

        public ManagerController(IEngineDb _Metodo, IEngineProyect _Funcion, IEngineNotificacion _Notificacion, SudokuContext _Context)
        {
            this.Metodo = _Metodo;
            this.Context = _Context;
            this.Funcion = _Funcion;
            this.Notificacion = _Notificacion;
        }

        public ActionResult Login(string email = "" , string password = "")
        {
            ViewBag.Respuesta = null;
            if (Request.HttpMethod == "POST")
            {
                if (email == string.Empty || password == string.Empty)
                {
                    ViewBag.Respuesta = "Usuario y email son requeridos";
                    return View();
                }

                password = Funcion.ConvertirBase64(email + password);
                Gerente gerente = Metodo.GetLoginGerente(password);
                if(gerente.NombreUsuario != null && gerente.NombreUsuario != string.Empty)
                {
                    System.Web.HttpContext.Current.Session["Usuario"] = gerente.NombreUsuario;
                    System.Web.HttpContext.Current.Session["Rol"] = gerente.Rol;
                    return RedirectToAction("MainManager", "Manager");
                }
            }
            return View();
        }

        public ActionResult MainManager()
        {
            return View();
        }

        public ActionResult Index(Gerente modelo = null)
        {
            ViewBag.Respuesta = null;
            ViewBag.Roles = Funcion.Roles();
            modelo.FechaRegistro = DateTime.Now;
            modelo.FechaActualizacion = DateTime.Now;

            if (Request.HttpMethod == "POST")
            {
                if (modelo.Nombre == string.Empty || modelo.NombreUsuario == string.Empty || modelo.Email == string.Empty || modelo.Rol == string.Empty || modelo.Nombre == null || modelo.NombreUsuario == null || modelo.Email == null || modelo.Rol == null)
                {
                    ViewBag.Respuesta = "Todos los campos son requeridos, completelos por favor";
                    return View(modelo);
                }
                bool resultado = false;
                resultado = Funcion.EmailEsValido(modelo.Email);
                if (!resultado)
                {
                    ViewBag.Respuesta = "La direccion de correo no es valida";
                    return View(modelo);
                }
                modelo.Identidad = Funcion.IdentificadorReg();
                resultado = Metodo.InsertarNuevoGerente(modelo);
                if (!resultado)
                {
                    ViewBag.Respuesta = "Error al crear administrador";
                    return View(modelo);
                }
                string enlaze = Funcion.CrearEnlazeRegistroGerente(Metodo, modelo.Email);
                EstructuraMail estructura = new EstructuraMail();
                estructura = Funcion.SetEstructuraMailRegisterManager(enlaze, modelo.Email, estructura);
                resultado = Notificacion.EnviarMailNotificacion(estructura);
                ViewBag.Respuesta = "Administrador creado exitosamente,debe revisar bandeja de entrada";
                modelo = new Gerente();
                modelo.FechaActualizacion = DateTime.Now;
                return View(modelo);

            }



            return View(modelo);
        }

        public ActionResult Update(Gerente modelo = null,string ConfirmarPassword = "",string subEjecutada = "")
        {
            ViewBag.Respuesta = null;
            ViewBag.Gerentes = Funcion.Gerentes();
            ViewBag.Type = null;
            ViewBag.Roles = Funcion.Roles();
            modelo.FechaActualizacion = DateTime.Now;
            bool resultado = false;

            if (Request.HttpMethod == "GET")
            {
                if (modelo.Id == EngineData.IdActivacion)
                {
                    ViewBag.Type = Funcion.DecodeBase64(EngineData.RegisterManager);
                    Gerente gerente = new Gerente();
                    gerente = Metodo.GetGerente(modelo.Email);
                    gerente.FechaActualizacion = DateTime.Now;
                    System.Web.HttpContext.Current.Session["SubEjecutada"] = "Bajo";
                    return View(gerente);
                }
                else
                {
                    ViewBag.Type = "Alto";
                    ViewBag.Gerentes = Metodo.GetAllGerentes();
                    return View(modelo);
                }
            }
          
            if(Request.HttpMethod == "POST")
            {
                if(subEjecutada == "Alto")
                {
                    if (modelo.Nombre == string.Empty || modelo.NombreUsuario == string.Empty || modelo.Email == string.Empty || modelo.Rol == string.Empty
                        || modelo.Nombre == null || modelo.NombreUsuario == null || modelo.Email == null || modelo.Rol == null)
                    {
                        ViewBag.Type = "Alto";
                        ViewBag.Respuesta = "Todos los campos son requeridos, completelos por favor";
                        return View(modelo);
                    }

                }
                else
                {
                    if (modelo.Nombre == string.Empty || modelo.NombreUsuario == string.Empty || modelo.Email == string.Empty || modelo.Rol == string.Empty ||modelo.Password == string.Empty 
                    || ConfirmarPassword == string.Empty || modelo.Nombre == null || modelo.NombreUsuario == null || modelo.Email == null || modelo.Rol == null || modelo.Password == null || ConfirmarPassword == null)
                    {
                        ViewBag.Respuesta = "Todos los campos son requeridos, completelos por favor 2";
                        return View(modelo);
                    }
                    resultado = Funcion.CompareString(modelo.Password, ConfirmarPassword);
                    if (!resultado)
                    {
                        ViewBag.Respuesta = "Las contraseñas deben ser identicas";
                        return View(modelo);
                    }
                    modelo.Password = Funcion.ConvertirBase64(modelo.Email + modelo.Password);
                }
            }

            resultado = Metodo.PutGerente(modelo, subEjecutada);
            if (!resultado)
                ViewBag.Respuesta = "Actualizacion fallida";
            else
                ViewBag.Respuesta = "Actualizacion exitosa";

            return View(modelo);
        }

        [HttpPost]
        public JsonResult GetGerente(string nombre)
        {
            Gerente Gerente = new Gerente();
            Gerente = Metodo.GetGerenteName(nombre);
            return Json(Gerente);
        }
    }
}