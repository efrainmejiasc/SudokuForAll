using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SudokuForAll.Controllers
{
    public class GameController : Controller
    {

        public ActionResult PlayGame()
        {
            return View();
        }


        public ActionResult BuyGame()
        {
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