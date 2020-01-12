using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SudokuDeTodos.Controllers
{
    public class ManagerController : Controller
    {
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
            CreateGalleta();
            return View();
        }

        public void CreateGalleta()
        {
            if (Request.Cookies["GalletaSudokuForAllId"] == null)
            {
                HttpCookie MiGalletaId = new HttpCookie("GalletaSudokuForAllId");
                MiGalletaId.Value = System.Web.HttpContext.Current.Session["Gerente"].ToString();
                MiGalletaId.Expires = DateTime.UtcNow.AddDays(1);
                HttpCookie MiGalletaExpire = new HttpCookie("GalletaSudokuForAllExpire");
                MiGalletaExpire.Value = DateTime.UtcNow.AddDays(1).ToString();
                MiGalletaExpire.Expires = DateTime.UtcNow.AddDays(1);
                Response.Cookies.Add(MiGalletaId);
                Response.Cookies.Add(MiGalletaExpire);
            }
            else
            {
                HttpCookie MiGalleta = Request.Cookies["GalletaSudokuForAllExpire"];
                DateTime expire = Convert.ToDateTime(MiGalleta.Value);
                if (DateTime.UtcNow > expire)
                {
                    HttpCookie MiGalletaId = new HttpCookie("GalletaSudokuForAllId");
                    HttpCookie MiGalletaExpire = new HttpCookie("GalletaSudokuForAllExpire");
                    MiGalletaId.Value = System.Web.HttpContext.Current.Session["Gerente"].ToString();
                    MiGalletaId.Expires = DateTime.UtcNow.AddDays(1);
                    Response.SetCookie(MiGalletaId);
                    Response.Flush();
                    MiGalletaExpire.Value = DateTime.UtcNow.AddHours(1).ToString();
                    MiGalletaExpire.Expires = DateTime.UtcNow.AddDays(1);
                    Response.SetCookie(MiGalletaExpire);
                    Response.Flush();
                }
            }
        }
    }
}