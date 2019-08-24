using SudokuForAll.Engine;
using SudokuForAll.Models;
using SudokuForAll.Models.DbSistema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SudokuForAll.Controllers
{
    public class DealController : Controller
    {
        private IEngineDb Metodo;
        private IEngineProyect Funcion;
        private IEnginePaypal Paypal;
        private readonly SudokuContext Context;

        public DealController(IEngineDb _Metodo, IEngineProyect _Funcion, IEnginePaypal _Paypal, SudokuContext _Context)
        {
            this.Metodo = _Metodo;
            this.Context = _Context;
            this.Funcion = _Funcion;
            this.Paypal = _Paypal;
        }

        public ActionResult Index(Producto model = null)
        {
            ViewBag.Respuesta = null;
            ViewBag.Moneda = Funcion.Monedas();
            if (Request.HttpMethod == "POST")
            {
                if (model.Nombre != null && model.Nombre != string.Empty  && model.Precio > 0 && model.Impuesto > 0)
                {

                    bool resultado = Metodo.InsertarProductoParaVenta(model);
                    if (resultado)
                        ViewBag.Respuesta = "Exito";
                    else
                        ViewBag.Respuesta = "Fallo";
                }
                else
                {
                    ViewBag.Respuesta = "Complete todos los campos";
                }

            }

            model = new Producto();
            model.Codigo = Funcion.ConstruirCodigo();
            model.Fecha = DateTime.Now;
            return View(model);
        }

        public ActionResult Update (Producto model = null)
        {
            return View(model);
        }
    }
}