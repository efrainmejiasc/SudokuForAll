using SudokuForAll.Engine;
using SudokuForAll.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SudokuForAll.Controllers
{
    public class ManagerController : Controller
    {
        private IEngineDb Metodo;
        private IEngineProyect Funcion;
        private IEngineNotificacion Notificacion;
        private readonly SudokuContext Context;

        public ManagerController(IEngineDb _Metodo, IEngineProyect _Funcion, IEngineNotificacion _Notificacion, SudokuContext _Context)
        {
            this.Metodo = _Metodo;
            this.Context = _Context;
            this.Funcion = _Funcion;
            this.Notificacion = _Notificacion;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Update()
        {
            return View();
        }
    }
}