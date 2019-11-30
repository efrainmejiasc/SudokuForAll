using SudokuDeTodos.Models.Sistema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SudokuDeTodos.Engine;

namespace SudokuDeTodos.Controllers
{
    public class ProcessController : Controller
    {
        private IEngineGameProcess Proceso;

        public ProcessController(IEngineGameProcess _Proceso)
        {
            this.Proceso = _Proceso;
        }

        [HttpPost]
        public string GuardarJuego(int valor, int i, int j)
        {
            Response response = new Response();
            string respuesta = Newtonsoft.Json.JsonConvert.SerializeObject(response);
            return respuesta;
        }
    }
}