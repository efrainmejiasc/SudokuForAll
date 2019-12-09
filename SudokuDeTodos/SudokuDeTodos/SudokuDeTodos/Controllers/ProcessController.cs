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
        private readonly IEngineGameProcess Proceso;
        private readonly IEngineDb Metodo;
        private readonly IEngineProyect Funcion;

        public ProcessController(IEngineGameProcess _Proceso, IEngineDb _Metodo, IEngineProyect _Funcion)
        {
            this.Proceso = _Proceso;
            this.Metodo = _Metodo;
            this.Funcion = _Funcion;
        }

        [HttpPost]
        public JsonResult ListaLenguaje(int index = 0)
        {
            Idiomas idiomas = new Idiomas();
            idiomas = EngineData.Idiomas(index);
            return Json(idiomas);
        }

        [HttpPost]
        public string GuardarJuego(string valor, int i, int j)
        {
            bool resultado = false;
            try
            {
                EngineDataGame ValorGame = EngineDataGame.Instance();
                string pathArchivo = ValorGame.PathArchivo;
                ValorGame.valorIngresado[i, j] = valor;
                this.Proceso.GuardarValoresIngresados(pathArchivo, ValorGame.valorIngresado);
                this.Proceso.GuardarValoresEliminados(pathArchivo, ValorGame.valorEliminado);
                this.Proceso.GuardarValoresInicio(pathArchivo, ValorGame.valorInicio);
                this.Proceso.GuardarValoresSolucion(pathArchivo, ValorGame.valorSolucion);
                resultado = true;
            }
            catch { }
          
            Respuesta response = new Respuesta();
            response.Status = resultado;
            string respuesta = Newtonsoft.Json.JsonConvert.SerializeObject(response);
            return respuesta;
        }

        [HttpPost]
        public JsonResult ExisteGalleta()
        {
            Respuesta respuesta = new Respuesta();
            bool existe = (bool)System.Web.HttpContext.Current.Session["MiGalleta"];
            if (existe)
                respuesta.Descripcion = string.Empty;
            else
                respuesta.Descripcion = "Equipo no registrado, desea continuar y registrarlo ?";

            return Json(respuesta);
        }

        [HttpPost]
        public JsonResult VerificarEmail (string email)
        {
            Respuesta respuesta = new Respuesta();
            respuesta.Status = Funcion.EmailEsValido(email);
            if (!respuesta.Status)
            {
                respuesta.Descripcion = email + EngineData.EmailNoValido();
                return Json(respuesta);
            }
            respuesta.Id = Metodo.ObtenerIdCliente(email);
            return Json(respuesta);
        }

    }
}