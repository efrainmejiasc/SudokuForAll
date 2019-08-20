using SudokuForAll.Engine;
using SudokuForAll.Models;
using SudokuForAll.Models.Sistema;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SudokuForAll.Controllers
{
    public class GameController : Controller
    {
        private IEngineDb Metodo;
        private IEngineProyect Funcion;
        private IEnginePaypal Paypal;
        private readonly SudokuContext Context;

        public GameController(IEngineDb _Metodo, IEngineProyect _Funcion, IEnginePaypal _Paypal, SudokuContext _Context)
        {
            this.Metodo = _Metodo;
            this.Context = _Context;
            this.Funcion = _Funcion;
            this.Paypal = _Paypal;
        }

        public ActionResult PlayGame()
        {
            return View();
        }


        public async Task<ActionResult> BuyGame()
        {
            RespuestaPaypalToken Respuesta = new RespuestaPaypalToken();
            Respuesta = await Task.Run(() => Paypal.GetTokenPaypal());
            return View();
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