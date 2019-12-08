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

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}