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
            Respuesta model = new Respuesta();
            if (index > 0)
            {
                System.Web.HttpContext.Current.Session["Cultura"] = EngineData.Cultura(index);
                model.Id = index;
            }
            return View(model);
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