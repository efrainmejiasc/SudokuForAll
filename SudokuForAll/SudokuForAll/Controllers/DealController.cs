using SudokuForAll.Engine;
using SudokuForAll.Models;
using SudokuForAll.Models.DbSistema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SudokuForAll.AuthData;


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

        [Auth]
        public ActionResult Index(Producto model = null)
        {
            ViewBag.Respuesta = null;
            ViewBag.Moneda = Funcion.Monedas();
            if (Request.HttpMethod == "POST")
            {
                if (model.Nombre != null && model.Nombre != string.Empty  && model.Precio > 0 && model.Impuesto > 0)
                {
                    model.FechaActualizacion = DateTime.Now;
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

        [Auth]
        public ActionResult Update (Producto modelo = null)
        {
            ViewBag.Moneda = Funcion.Monedas();
            ViewBag.CodigoProductos = Metodo.GetProductosParaVenta();
            ViewBag.Respuesta = null;
            if (Request.HttpMethod == "POST")
            {
                if (modelo.Nombre != null && modelo.Nombre != string.Empty && modelo.Codigo != string.Empty && modelo.Codigo != null && modelo.Precio > 0 && modelo.Impuesto > 0)
                {
                    modelo.FechaActualizacion = DateTime.Now;
                    bool resultado = Metodo.PutProducto(modelo);
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

            modelo = new Producto();
            modelo.FechaActualizacion = DateTime.Now;
            return View(modelo);
        }

        [HttpPost]
        public JsonResult GetProducto (string codigo)
        {
            Producto producto = new Producto();
            producto = Metodo.GetProducto(codigo);
            return Json(producto);
        }
    }
}