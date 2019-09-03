using SudokuForAll.Engine;
using SudokuForAll.Models;
using System.Web.Mvc;

namespace SudokuForAll.Controllers
{
    [Authorize]
    public class GameController : Controller
    {
        private IEngineDb Metodo;
        private IEngineProyect Funcion;
        private readonly SudokuContext Context;

        public GameController(IEngineDb _Metodo, IEngineProyect _Funcion,SudokuContext _Context)
        {
            this.Metodo = _Metodo;
            this.Context = _Context;
            this.Funcion = _Funcion;
        }

        public ActionResult PlayGame()
        {
            return View();
        }
    }
}