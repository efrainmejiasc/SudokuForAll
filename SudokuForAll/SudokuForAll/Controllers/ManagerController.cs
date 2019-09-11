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
    [Authorize]
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

        [AllowAnonymous]
        public ActionResult Login(string nombreUsuario = "" , string password = "")
        {
            ViewBag.Respuesta = null;
            if (Request.HttpMethod == "POST")
            {
                if (nombreUsuario == string.Empty || password == string.Empty)
                {
                    ViewBag.Respuesta = "Usuario y password son requeridos";
                    return View();
                }

                password = Funcion.ConvertirBase64(nombreUsuario + password);
                Gerente gerente = Metodo.GetLoginGerente(password);
                if(gerente.NombreUsuario != null && gerente.NombreUsuario != string.Empty)
                {
                    System.Web.HttpContext.Current.Session["Gerente"] = gerente.NombreUsuario;
                    System.Web.HttpContext.Current.Session["Rol"] = gerente.Rol;
                    return RedirectToAction("MainManager", "Manager");
                }
            }
            return View();
        }

       
        public ActionResult MainManager()
        {
            CreateGalleta();
            return View();
        }

        public void  CreateGalleta()
        {
            if (Request.Cookies["GalletaSudokuForAllId"] == null)
            {
                HttpCookie MiGalletaId = new HttpCookie("GalletaSudokuForAllId");
                MiGalletaId.Value = System.Web.HttpContext.Current.Session["Gerente"].ToString();
                MiGalletaId.Expires = DateTime.UtcNow.AddDays(1);
                HttpCookie MiGalletaExpire = new HttpCookie("GalletaSudokuForAllExpire");
                MiGalletaExpire.Value = DateTime.UtcNow.AddDays(1).ToString();
                MiGalletaExpire.Expires = DateTime.UtcNow.AddDays(1);
                Response.Cookies.Add(MiGalletaId);
                Response.Cookies.Add(MiGalletaExpire);
            }
            else
            {
                HttpCookie MiGalleta = Request.Cookies["GalletaSudokuForAllExpire"];
                DateTime expire = Convert.ToDateTime(MiGalleta.Value);
                if (DateTime.UtcNow > expire)
                {
                    HttpCookie MiGalletaId = new HttpCookie("GalletaSudokuForAllId");
                    HttpCookie MiGalletaExpire = new HttpCookie("GalletaSudokuForAllExpire");
                    MiGalletaId.Value = System.Web.HttpContext.Current.Session["Gerente"].ToString();
                    MiGalletaId.Expires = DateTime.UtcNow.AddDays(1);
                    Response.SetCookie(MiGalletaId);
                    Response.Flush();
                    MiGalletaExpire.Value = DateTime.UtcNow.AddHours(1).ToString();
                    MiGalletaExpire.Expires = DateTime.UtcNow.AddDays(1);
                    Response.SetCookie(MiGalletaExpire);
                    Response.Flush();
                }
            }
        }

     
        public ActionResult Index(Gerente modelo = null)
        {
            CreateGalleta();
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

        public ActionResult Update(Gerente modelo = null,string CPassword = "")
        {
            ViewBag.Respuesta = null;
            ViewBag.Gerentes = Funcion.Gerentes();
            ViewBag.Roles = Funcion.Roles();
            modelo.FechaActualizacion = DateTime.Now;
            bool resultado = false;

            if (System.Web.HttpContext.Current.Session["Rol"] != null)
                ViewBag.Type = System.Web.HttpContext.Current.Session["Rol"].ToString();
            else
                System.Web.HttpContext.Current.Session["Rol"] = "Alto";


           string subEjecutada = System.Web.HttpContext.Current.Session["Rol"].ToString();

            if (Request.HttpMethod == "GET")
            {
                Gerente gerente = new Gerente();
                if (modelo.Id == EngineData.IdActivacion)
                {
                    ViewBag.Type = Funcion.DecodeBase64(EngineData.RegisterManager);
                    gerente = Metodo.GetGerente(modelo.Email);
                    gerente.FechaActualizacion = DateTime.Now;
                    ViewBag.Type = "Bajo";
                    System.Web.HttpContext.Current.Session["Rol"] = "Bajo";
                    return View(gerente);
                }
                else
                {
                    if (subEjecutada == "Alto")
                    {
                        ViewBag.Gerentes = Metodo.GetAllGerentes();
                        ViewBag.Type = "Alto";
                    }
                    else
                    {
                        if (System.Web.HttpContext.Current.Session["Gerente"] != null)
                        {
                            string nombreUsuario = System.Web.HttpContext.Current.Session["Gerente"].ToString();
                            gerente = Metodo.GetGerenteUserName(nombreUsuario);
                            ViewBag.Type = gerente.Rol;
                        }
                    }
                    return View(gerente);
                }
            }

           
            if (Request.HttpMethod == "POST")
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
                    if (modelo.Nombre == string.Empty || modelo.NombreUsuario == string.Empty || modelo.Email == string.Empty || modelo.Password == string.Empty 
                    || CPassword == string.Empty || modelo.Nombre == null || modelo.NombreUsuario == null || modelo.Email == null ||  modelo.Password == null || CPassword == null)
                    {
                        ViewBag.Respuesta = "Todos los campos son requeridos, completelos por favor 2";
                        return View(modelo);
                    }
                    resultado = Funcion.CompareString(modelo.Password, CPassword);
                    if (!resultado)
                    {
                        ViewBag.Respuesta = "Las contraseñas deben ser identicas";
                        return View(modelo);
                    }
                    modelo.Password = Funcion.ConvertirBase64(modelo.NombreUsuario + modelo.Password);
                }
            }

            resultado = Metodo.PutGerente(modelo, subEjecutada);
            if (!resultado)
                ViewBag.Respuesta = "Actualizacion fallida";
            else
                ViewBag.Respuesta = "Actualizacion exitosa";

            return View(modelo);
        }

        [AllowAnonymous]
        [HttpPost]
        public JsonResult GetGerente(string nombre)
        {
            Gerente Gerente = new Gerente();
            Gerente = Metodo.GetGerenteName(nombre);
            return Json(Gerente);
        }
    }
}