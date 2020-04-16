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

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult MainManager()
        {
            //CreateGalleta();
            return View();
        }

        public void CreateGalleta()
        {
            if (Request.Cookies["GalletaSudokuForAllId"] == null)
            {
                HttpCookie MiGalletaId = new HttpCookie("GalletaSudokuForAllId");
                MiGalletaId.Value = System.Web.HttpContext.Current.Session["Gerente"].ToString();
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
    }
}