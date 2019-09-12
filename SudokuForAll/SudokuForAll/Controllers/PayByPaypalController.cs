using SudokuForAll.Engine;
using SudokuForAll.Models;
using SudokuForAll.Models.DbSistema;
using SudokuForAll.Models.Sistema;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SudokuForAll.AuthData;

namespace SudokuForAll.Controllers
{
    public class PayByPaypalController : Controller
    {

        private IEngineDb Metodo;
        private IEngineProyect Funcion;
        private IEnginePaypal Paypal;
        private readonly SudokuContext Context;

        public PayByPaypalController(IEngineDb _Metodo, IEngineProyect _Funcion, IEnginePaypal _Paypal, SudokuContext _Context)
        {
            this.Metodo = _Metodo;
            this.Context = _Context;
            this.Funcion = _Funcion;
            this.Paypal = _Paypal;
        }

        public ActionResult BusinessGame()
        {
            //RespuestaPaypalToken Respuesta = new RespuestaPaypalToken();
            //Respuesta = await Task.Run(() => Paypal.GetTokenPaypal());
            //PayPal.Api.APIContext apiContext = Paypal.GetApiContext(Respuesta.access_token);
            //int n = Metodo.ObtenerNumeroDePago();

            List<Producto> model = new List<Producto>();
            model = Metodo.ProductosParaVenta();
            return View(model);
        }

        [HttpPost]
        public void WebHookPay()
        {
            string cadena = string.Empty;
            if (Request.RequestType.Equals("POST"))
            {
                var stream = new StreamReader(Request.InputStream);
                stream.BaseStream.Seek(0, SeekOrigin.Begin);
                cadena = stream.ReadToEnd();
            }
        }
    }
}