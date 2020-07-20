using SudokuDeTodos.Engine.Interfaces;
using SudokuDeTodos.Models;
using SudokuDeTodos.Models.DbSistema;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SudokuDeTodos.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IEngineDb Metodo;
        private readonly IEngineProyect Funcion;

        public PaymentController(IEngineDb _Metodo, IEngineProyect _Funcion)
        {
            this.Metodo = _Metodo;
            this.Funcion = _Funcion;
        }
        // GET: Payment
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost] 
        public ActionResult GetProducto()
        {
            Models.DbSistema.Producto producto = new Models.DbSistema.Producto();
            producto = Metodo.GetProducto();

            Models.Sistema.Producto productoItem = new Models.Sistema.Producto()
            {
                Codigo = producto.Codigo,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                FechaActivacion = DateTime.Now.Date.ToString().Substring(0,10),
                FechaExpiracion = DateTime.Now.Date.AddDays(30).ToString().Substring(0, 10),
                Precio = producto.Precio,
                Impuesto = producto.Impuesto,
                Total = producto.Precio +  producto.Precio * producto.Impuesto / 100 ,
                Moneda = "$ USD"
            };

            List<Models.Sistema.Producto> productoResult = new List<Models.Sistema.Producto>();
            productoResult.Add(productoItem);
            return Json(productoResult);
        }

        [AllowAnonymous]
        [HttpPost]
        public void WebHookPayment()
        {
            string cadena = string.Empty;
            if (Request.RequestType.Equals("POST"))
            {
                var stream = new StreamReader(Request.InputStream);
                stream.BaseStream.Seek(0, SeekOrigin.Begin);
                cadena = stream.ReadToEnd();
            }
            TransaccionPaypal modelo = new TransaccionPaypal();
            if (cadena != string.Empty)
                modelo.Descripcion = cadena;
            else
                modelo.Descripcion = "sin respuesta";

            modelo.Fecha = DateTime.UtcNow;
            Metodo.InsertarTransaccionPaypal(modelo);
        }
    }
}