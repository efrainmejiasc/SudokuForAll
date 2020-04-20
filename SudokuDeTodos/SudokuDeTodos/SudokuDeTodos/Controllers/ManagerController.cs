using SudokuDeTodos.Engine.Interfaces;
using SudokuDeTodos.Models.DbSistema;
using SudokuDeTodos.Models.Sistema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SudokuDeTodos.Controllers
{
    public class ManagerController : Controller
    {
        private readonly IEngineDb Metodo;
        private readonly IEngineProyect Funcion;

        public ManagerController(IEngineDb _Metodo, IEngineProyect _Funcion)
        {
            this.Metodo = _Metodo;
            this.Funcion = _Funcion;
        }

        public ActionResult Index(string email = "") //AUTENTIFICACION
        {
            ViewBag.Respuesta = null;
            if (Request.HttpMethod == "GET" || email == string.Empty)
            return View();

            string Adm = Metodo.GetAdministrador(email);
            if (!string.IsNullOrEmpty(Adm))
            {
                System.Web.HttpContext.Current.Session["GERENTE"] = email;
                return RedirectToAction("MenuManager", "Manager");
            }
            else
            {
                ViewBag.Respuesta = "Autentificacion Fallida";
                return View();
            }
              
        }

        public ActionResult MenuManager() //MENU
        {
            CreateGalleta();
            return View();
        }

        public ActionResult MainManager() //REPORTES
        {
            if (System.Web.HttpContext.Current.Session["GERENTE"] == null)
                return RedirectToAction("Index", "Manager");

            return View();
        }

        public ActionResult InvitedManager(string email = "",string tiempo = "" ,string cultura = "")  //REGISTRAR INVITADO
        {
            if (System.Web.HttpContext.Current.Session["GERENTE"] == null)
                return RedirectToAction("Index", "Manager");

            ViewBag.Respuesta = null;
            if (Request.HttpMethod == "GET" || email == string.Empty || tiempo == string.Empty || cultura == string.Empty)
                return View();

            Guid ide = Metodo.GetIdentidadCliente(email);
            if (ide != Guid.Empty)
            {
                ViewBag.Respuesta = "El email ya se encuentra registrado";
                return View();
            }

            int dias = 30;
            if (tiempo == "2")
                dias = 60;
            else if (tiempo == "3")
               dias = 90;

            Cliente cliente = Funcion.ConstruirCliente(email, cultura);
            ClientePago clientePago = Funcion.ConstruirClientePago(0, dias);
            bool resultado = Metodo.InsertarCliente(cliente, clientePago);
            if (resultado)
                ViewBag.Respuesta = "Transaccion Exitosa";
           else
                ViewBag.Respuesta = "Transaccion Fallida";

            return View();
        }


        public ActionResult CreateManager (string email = "" , string emailNuevo = "") //CREAR ADMINISTRADOR
        {
            if (System.Web.HttpContext.Current.Session["GERENTE"] == null)
                return RedirectToAction("Index", "Manager");

            ViewBag.Respuesta = null;
            if (Request.HttpMethod == "GET" || email == string.Empty || emailNuevo == string.Empty)
                return View();

            bool resultado = Funcion.EmailEsValido(emailNuevo);
            if (!resultado)
            {
                ViewBag.Respuesta = "Email No Valido";
                return View();
            }
            resultado = Metodo.ValidarAdministrador(email);
            if (!resultado)
            {
                ViewBag.Respuesta = "Email Administrador No Valido";
                return View();
            }

            Administrador Adm = Funcion.BuilAdministrador(email, emailNuevo);
            resultado = Metodo.CreateAdministrador(Adm);
            if (!resultado)
                ViewBag.Respuesta = "Transaccion Fallida";
            else
                ViewBag.Respuesta = "Administrador Creado Satisfactoriamente";

            return View();

        }

        public void CreateGalleta()
        {
            if (Request.Cookies["GalletaSudokuForAllId"] == null)
            {
                HttpCookie MiGalletaId = new HttpCookie("GalletaSudokuForAllId");
                MiGalletaId.Value = System.Web.HttpContext.Current.Session["GERENTE"].ToString();
                MiGalletaId.Expires = DateTime.UtcNow.AddDays(1);
                Response.Cookies.Add(MiGalletaId);
            }
            else
            {
                HttpCookie MiGalleta = Request.Cookies["GalletaSudokuForAllExpire"];
                HttpCookie MiGalletaId = new HttpCookie("Entrepidus");
                MiGalletaId.Expires = DateTime.UtcNow.AddDays(1);
                Response.SetCookie(MiGalletaId);
                Response.Flush();
            }
        }


        [HttpPost]
        public  ActionResult ReportePago(DateTime fechaInicial, DateTime fechaFinal)
        {
            List<ConsultaReporte> consulta = Metodo.ConsultaReporte(fechaInicial, fechaFinal);
            return Json(consulta);
        }

        [HttpPost]
        public ActionResult EditarClientePagoFechaVencimiento(int idClientePago , DateTime fechaVencimiento)
        {
            Respuesta R = new Respuesta();
            bool resultado = Metodo.EditarClientePagoFechaVencimiento(idClientePago, fechaVencimiento);
            if (resultado)
                R.Descripcion = "Transaccion Exitosa";
            else
                R.Descripcion = "Transaccion Fallida";

            return Json(R);
        }

        [HttpPost]
        public JsonResult ReturnVarGerente()
        {
            Respuesta respuesta = new Respuesta();
            if (System.Web.HttpContext.Current.Session["GERENTE"] != null)
                respuesta.Descripcion = "ACTIVO";
            else
                respuesta.Descripcion = "INACTIVO";

            return Json(respuesta);
        }
    }
}