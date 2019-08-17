using System;
using System.Collections.Generic;
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
    }
}