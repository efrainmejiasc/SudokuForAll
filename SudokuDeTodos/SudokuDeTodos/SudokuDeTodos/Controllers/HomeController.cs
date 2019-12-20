using SudokuDeTodos.Engine;
using SudokuDeTodos.Models.Sistema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SudokuDeTodos.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEngineDb Metodo;
        private readonly IEngineProyect Funcion;

        public HomeController(IEngineDb _Metodo, IEngineProyect _Funcion)
        {
            this.Metodo = _Metodo;
            this.Funcion = _Funcion;
        }

        #region ENTRADA_SITIO
        public ActionResult Index(int index = 0)
        {
            GetGalleta();
            Respuesta model = new Respuesta();
            if (index > 0)
            {
                System.Web.HttpContext.Current.Session["Cultura"] = EngineData.Cultura(index);
                model.Id = index;
            }
            return View(model);
        }
        private void GetGalleta()
        {
            if (Request.Cookies["GalletaSudokuForAllId"] != null)
            {
                HttpCookie MiGalletaId = Request.Cookies["GalletaSudokuForAllId"];
                HttpCookie MiGalletaExpire = Request.Cookies["GalletaSudokuForAllExpire"];
                string identificadorGalleta = MiGalletaId.Value;
                string fechaExpiracion = MiGalletaExpire.Value;
                System.Web.HttpContext.Current.Session["MiGalleta"] = true;
            }
            else
            {
                System.Web.HttpContext.Current.Session["MiGalleta"] = false;
            }
        }
        #endregion ENTRADA_SITIO

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Security()
        {
            return View();
        }

        public ActionResult ResponseMessage(string email = "")
        {
            if (email == string.Empty)
                return View();

            Respuesta respuesta = new Respuesta();
            respuesta.Status = Funcion.EmailEsValido(email);
            if (!respuesta.Status)
            {
                respuesta.Descripcion = email + EngineData.EmailNoValido();
                return Json(respuesta);
            }
            respuesta.Id = Metodo.VerificarEmail(email);
            if (respuesta.Id == 0)
            {
                respuesta = Funcion.ConstruirRespuesta(respuesta.Id, true, StringResx.Resources.MsjPruebaSitio); //Prueba sudokudetodos?
                return View("ResponseMessage", respuesta);
            }
            else if (respuesta.Id == 1)
            {
                respuesta = Funcion.ConstruirRespuesta(respuesta.Id, true, StringResx.Resources.CtaNoActivada); //Cuenta NO activada
                return View("ResponseMessage", respuesta);
            }
            else if (respuesta.Id == 2)
            {
                respuesta = Funcion.ConstruirRespuesta(respuesta.Id, true, "JUGAR PRUEBA"); // Ir a jugar prueba
                return Redirect("/Vista/GameOne.aspx");
            }
            else if (respuesta.Id == 3)
            {
                int resultado = Metodo.VerificarClientePago(email);// Verifico pago del cliente 
                if (resultado == 1)
                {
                    respuesta = Funcion.ConstruirRespuesta(10, true, "PAGO VALIDO"); // Ir Autentificacion
                    return View("About");
                }
                else if (resultado == 0)
                {
                    respuesta = Funcion.ConstruirRespuesta(4, true, EngineData.TiempoJuegoExpiro()); // Pago expirado ,comprar nuevamente
                }
                else if (resultado == -1)
                {
                    respuesta = Funcion.ConstruirRespuesta(5, true, EngineData.TiempoPruebaJuegoExpiro()); // Comprar y  fabricar contraseña
                }
                else if (resultado == -2)
                {
                    respuesta = Funcion.ConstruirRespuesta(6, true, EngineData.ErrorInternoServidor());
                }
            }

            return Json(respuesta);
        }

    }
}