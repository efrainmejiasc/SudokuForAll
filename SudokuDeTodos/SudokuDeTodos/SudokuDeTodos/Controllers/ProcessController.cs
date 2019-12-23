﻿using SudokuDeTodos.Models.Sistema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SudokuDeTodos.Engine;
using SudokuDeTodos.Models.DbSistema;

namespace SudokuDeTodos.Controllers
{
    public class ProcessController : Controller
    {
        private readonly IEngineGameProcess Proceso;
        private readonly IEngineDb Metodo;
        private readonly IEngineProyect Funcion;
        private readonly IEngineNotificacion Notificacion;

        public ProcessController(IEngineGameProcess _Proceso, IEngineDb _Metodo, IEngineProyect _Funcion, IEngineNotificacion _Notificacion)
        {
            this.Proceso = _Proceso;
            this.Metodo = _Metodo;
            this.Funcion = _Funcion;
            this.Notificacion = _Notificacion;
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
        public JsonResult EnviarNotificacionPrueba(string email)
        {
            Respuesta respuesta = new Respuesta();
            respuesta.Status = Funcion.EmailEsValido(email);
            if (!respuesta.Status) //email no valido
            {
                respuesta = Funcion.ConstruirRespuesta(101, false, EngineData.EmailNoValido(), email);
                return Json(respuesta);
            }
            Cliente cliente = Funcion.ConstruirCliente(email);
            respuesta.Status = Metodo.InsertarCliente(cliente);
            if (!respuesta.Status)//error interno del servidor
            {
                respuesta = Funcion.ConstruirRespuesta(102, false, EngineData.ErrorInternoServidor(), email);
                return Json(respuesta);
            }
            string enlaze = Funcion.ConstruirEnlazePrueba(email, cliente.Identidad);
            EstructuraMail estructuraMail = Funcion.SetEstructuraMailTest(enlaze, email);
            respuesta.Status = Notificacion.EnviarMailNotificacion(estructuraMail);
            if (!respuesta.Status)//No se pudo enviar notificacion
            {
                respuesta = Funcion.ConstruirRespuesta(103, false, EngineData.ErrorEnviandoMail(), email);
                return Json(respuesta);
            }
            respuesta = Funcion.ConstruirRespuesta(100, true, EngineData.RegistroExitoso(), email);
            return Json(respuesta);
        }
    }
}