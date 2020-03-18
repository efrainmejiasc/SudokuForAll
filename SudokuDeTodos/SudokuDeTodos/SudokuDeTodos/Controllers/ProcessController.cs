using SudokuDeTodos.Models.Sistema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SudokuDeTodos.Engine;
using SudokuDeTodos.Models.DbSistema;
using SudokuDeTodos.Models.Game;
using System.Collections;
using System.Data;

namespace SudokuDeTodos.Controllers
{
    public class ProcessController : Controller
    {
        private readonly IEngineGameProcess Proceso;
        private readonly IEngineDb Metodo;
        private readonly IEngineProyect Funcion;
        private readonly IEngineNotificacion Notificacion;
        private readonly IEngineGameChild FuncionGame;

        public ProcessController(IEngineGameProcess _Proceso, IEngineDb _Metodo, IEngineProyect _Funcion, IEngineNotificacion _Notificacion, IEngineGameChild _FuncionGame)
        {
            this.Proceso = _Proceso;
            this.Metodo = _Metodo;
            this.Funcion = _Funcion;
            this.Notificacion = _Notificacion;
            this.FuncionGame = _FuncionGame;
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
            EngineDataGame ValorGame = EngineDataGame.Instance();
            string pathArchivo = ValorGame.PathArchivo;
            ValorGame.valorIngresado[i, j] = valor;
            this.Proceso.GuardarValoresIngresados(pathArchivo, ValorGame.valorIngresado);
            this.Proceso.GuardarValoresEliminados(pathArchivo, ValorGame.valorEliminado);
            this.Proceso.GuardarValoresInicio(pathArchivo, ValorGame.valorInicio);
            this.Proceso.GuardarValoresSolucion(pathArchivo, ValorGame.valorSolucion);
            ValorGame.valorCandidatoSinEliminados = FuncionGame.CandidatosSinEliminados(ValorGame.valorIngresado, ValorGame.valorCandidato, ValorGame.valorEliminado);
            resultado = true;
            Respuesta response = new Respuesta();
            response.Status = resultado;
            string respuesta = Newtonsoft.Json.JsonConvert.SerializeObject(response);
            return respuesta;
        }

        [HttpPost]
        public JsonResult GetLetrasJuego(bool contadorActivado,int numGrilla)
        {
            LetrasJuego I = new LetrasJuego();
            string pathArchivo = string.Empty;
            if (System.Web.HttpContext.Current.Session["PathArchivo"] == null)
                return Json(I);
            else
                pathArchivo = System.Web.HttpContext.Current.Session["PathArchivo"].ToString();

            I = FuncionGame._ContadorIngresado(contadorActivado,numGrilla);
            return Json(I);
        }

        [HttpPost]
        public  JsonResult ProcesosContables()
        {
            List<TableTest> test = new List<TableTest>();
            string pathArchivo = string.Empty;
            if (System.Web.HttpContext.Current.Session["PathArchivo"] == null)
                return Json(test);
            else
                pathArchivo = System.Web.HttpContext.Current.Session["PathArchivo"].ToString();

            test = FuncionGame._ProcesosContables();
            return Json(test);
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
        public JsonResult ReturnVarSession(string nameVar)
        {
            Respuesta respuesta = new Respuesta();
            if (System.Web.HttpContext.Current.Session[nameVar] != null)
                respuesta.Descripcion = System.Web.HttpContext.Current.Session[nameVar].ToString();
            else
                respuesta.Descripcion = null;

            return Json(respuesta);
        }

        [HttpPost]
        public JsonResult AddNuevoJuego(int numero)
        {
            NuevoJuego nuevoJuego = new NuevoJuego();

            return Json(nuevoJuego);
        }

        [HttpPost]
        public JsonResult ReturnValorPlantilla (string tipo , string id , string valor)
        {
            int lnt = id.Length;
            int row = Convert.ToInt32(id.Substring(lnt - 2, 1));
            int col = Convert.ToInt32(id.Substring(lnt - 1, 1));
            ValorPosicion V = new ValorPosicion();
            FuncionGame.ReadValuesFile();
            if (tipo == "vEliminado")
            {
                V.Id = "#" + id;
                V.Valor = FuncionGame.GetValorPosicion(tipo, row, col);
                if (V.Valor.Contains(valor))
                    V.Valor = valor;
            }
            return Json(V);
        }


        [HttpPost]
        public JsonResult FilaRecuadroColumna (string tipo)
        {
            if (string.IsNullOrEmpty(tipo))
                return Json(tipo);

            System.Web.HttpContext.Current.Session["Circuito"] = tipo;
            return Json(tipo);
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

        [HttpPost]
        public JsonResult EnviarOtraNotificacionPrueba(string email)
        {
            Respuesta respuesta = new Respuesta();
            respuesta.Status = Funcion.EmailEsValido(email);
            if (!respuesta.Status) 
            {
                respuesta = Funcion.ConstruirRespuesta(101, false, EngineData.EmailNoValido(), email);
                return Json(respuesta);
            }
            Guid identidad = Metodo.GetIdentidadCliente(email);
            Cliente cliente = Funcion.ConstruirCliente(email,identidad);
            string enlaze = Funcion.ConstruirEnlazePrueba(email, cliente.Identidad);
            EstructuraMail estructuraMail = Funcion.SetEstructuraMailTest(enlaze, email);
            respuesta.Status = Notificacion.EnviarMailNotificacion(estructuraMail);
            if (!respuesta.Status)//No se pudo enviar notificacion
            {
                respuesta = Funcion.ConstruirRespuesta(103, false, EngineData.ErrorEnviandoMail(), email);
                return Json(respuesta);
            }
            respuesta = Funcion.ConstruirRespuesta(100, true, EngineData.EnvioNuevoEmail(), email);
            return Json(respuesta);
        }

        [HttpPost]
        public JsonResult EnviarCodigo(string email)
        {
            Respuesta respuesta = new Respuesta();
            string type = Funcion.DecodeBase64(EngineData.ResetPassword);
            bool resultado = Funcion.EmailEsValido(email);
            if (!resultado)
            {
                respuesta = Funcion.ConstruirRespuesta(101, false, EngineData.EmailNoValido(), email, type);
                return Json(respuesta);
            }
            Guid identidad = Metodo.GetIdentidadCliente(email);
            if (identidad == Guid.Empty)
            {
                respuesta = Funcion.ConstruirRespuesta(102, false, EngineData.ErrorInternoServidor(), email, type);
                return Json(respuesta);
            }
            string codigo = Funcion.ConstruirCodigo();
            ResetPassword resetPassword = Funcion.SetResetPassword(email, codigo);
            resultado = Metodo.InsertarCodigoResetPassword(resetPassword);
            if (!resultado)
            {
                respuesta = Funcion.ConstruirRespuesta(102, false, EngineData.ErrorInternoServidor(), email, type);
                return Json(respuesta);
            }
            resultado = Funcion.EnviarNuevaNotificacion(identidad, email, EngineData.ResetPassword, null, codigo);
            if (!resultado)
            {
                respuesta = Funcion.ConstruirRespuesta(103, false, EngineData.ErrorEnviandoMail(), email, type);
                return Json(respuesta);
            }
            respuesta = Funcion.ConstruirRespuesta(100, true, EngineData.EnvioCodigoRestablecerPassword(), email);
            return Json(respuesta);
        }

        [HttpGet]
        public ActionResult State(int id = 0, string email = "", string identidad = "", int status = 0, string date = "", string type = "",string cultureInfo = "",string ide = "")
        {
            Respuesta respuesta = new Respuesta();
            if (email == string.Empty || email == null)
            {
                respuesta.Id = -1;
                return View(respuesta);
            }

            string codeOrPassword = string.Empty;
            if (ide!= string.Empty && ide!= null)
                codeOrPassword = Funcion.DecodeBase64(ide);
            string mail = Funcion.DecodeBase64(email);
            string tipo = Funcion.DecodeBase64(type);
            bool resultado = false;
            Guid identificadorGuid = Metodo.GetIdentidadCliente(mail);
            if (identificadorGuid == Guid.Empty)
            {
                respuesta = Funcion.ConstruirRespuesta(3, false, EngineData.ErrorInternoServidor(), mail, tipo); //valido existencia de guid cliente
                return View(respuesta);
            }
            resultado = Funcion.EmailEsValido(mail); //valido formato email
            if (!resultado)
            {
                respuesta = Funcion.ConstruirRespuesta(1, false, EngineData.EmailNoValido(), mail, tipo);
                return View(respuesta);
            }
            System.Web.HttpContext.Current.Session["EMAIL"] = mail;
            resultado = Funcion.ValidacionTypeTransaccion(type);//valido tipo de transaccion
            if (!resultado)
            {
                respuesta = Funcion.ConstruirRespuesta(2, false, EngineData.TransaccionNoValida(), mail, tipo);
                return View(respuesta);
            }
            resultado = Funcion.ValidacionIdentidad(mail, identidad, Metodo);//valido guid cliente
            if (!resultado)
            {
                respuesta = Funcion.ConstruirRespuesta(3, false, EngineData.ErrorInternoServidor(), mail, tipo);
                return View(respuesta);
            }
            Funcion.SetCultureInfo(cultureInfo);
            DateTime fechaEnvio = Convert.ToDateTime(date);
            resultado = Funcion.EstatusLink(fechaEnvio); //valido estatus del link
            if (!resultado)
            {
                if (type == EngineData.ResetPassword)
                {
                    string codigo = Funcion.ConstruirCodigo();
                    ResetPassword resetPassword = Funcion.SetResetPassword(mail, codigo);
                    resultado = Metodo.DeleteCodigoResetPassword(mail);
                    resultado = Metodo.InsertarCodigoResetPassword(resetPassword);
                    resultado = Funcion.EnviarNuevaNotificacion(identificadorGuid, email, type, codeOrPassword, codigo);
                }
                else
                {
                    resultado = Funcion.EnviarNuevaNotificacion(identificadorGuid, email, type, codeOrPassword, codeOrPassword);
                }
                if (!resultado)
                {
                    respuesta = Funcion.ConstruirRespuesta(3, false, EngineData.ErrorInternoServidor(), mail, tipo); //valido Enviar Nueva Notificacion 
                    return View(respuesta);
                }
                respuesta = Funcion.ConstruirRespuesta(100, false, EngineData.TiempoLinkExpiro(), mail, Funcion.MetodoTransactionType(type));
                return View(respuesta);
            }

            //*******************************************************************************************************************
            //*******************************************************************************************************************

            if (type == EngineData.Test) //Activacion Prueba
            {
                resultado = Metodo.UpdateClienteTest(mail, status);//actualizar el registro del cliete
                if (!resultado)
                {
                    respuesta = Funcion.ConstruirRespuesta(4, false, EngineData.ErrorActualizarCliente(), mail, tipo);
                    return View(respuesta);
                }
                respuesta = Funcion.ConstruirRespuesta(5, true, EngineData.ActivacionTestExitosa(), mail, tipo); //activacion exitosa 5
            }
            else if (type == EngineData.Register) //Activacion registro
            {
                resultado = Metodo.UpdateClienteRegister(mail, status);//actualizar registro del cliente 
                if (!resultado)
                {
                    respuesta = Funcion.ConstruirRespuesta(6, false, EngineData.ErrorActualizarCliente(), mail, tipo);
                    return View(respuesta);
                }
                respuesta = Funcion.ConstruirRespuesta(7, true, EngineData.ActivacionExitosa(), mail, tipo); //activacion exitosa 7
            }
            else if (type == EngineData.ResetPassword) //Restablecer Password
            {
                resultado = Metodo.ValidarCodigoResetPassword(mail, codeOrPassword);
                if (!resultado)
                {
                    respuesta = Funcion.ConstruirRespuesta(3, false, EngineData.ErrorInternoServidor(), mail, tipo);
                    return View(respuesta);
                }
                respuesta = Funcion.ConstruirRespuesta(8, true, EngineData.ActivacionExitosa(), mail, Funcion.MetodoTransactionType(type)); // link valido 9
            }
            else if (type == EngineData.RegisterManager)//registrar gerente
            {

            }

            return View(respuesta);
        }


        [HttpPost]

        public ActionResult NumeroIngresado()
        {
            string[,] valorInicio = new string[9, 9];
            string[,] valorIngresado = new string[9, 9];
            string[,] valorCandidato = new string[9, 9];
            string[,] valorEliminado = new string[9, 9];
            string[,] valorSolucion = new string[9, 9];
            string[,] v = new string[9, 9];
            string[,] c = new string[9, 9];
            EngineDataGame ValorGame = EngineDataGame.Instance();
            ArrayList arrText = FuncionGame.AbrirValoresArchivo(ValorGame.PathArchivo);
            ValorGame.valorIngresado = FuncionGame.SetValorIngresado(arrText,valorIngresado);
            ValorGame.valorEliminado = FuncionGame.SetValorEliminado(arrText, valorEliminado);
            ValorGame.valorInicio = FuncionGame.SetValorInicio(arrText, valorInicio);
            ValorGame.valorSolucion = FuncionGame.SetValorSolucion(arrText, valorSolucion);
            ValorGame.valorCandidato = FuncionGame.ElejiblesInstantaneos(ValorGame.valorIngresado, valorCandidato);
            c = FuncionGame.CandidatosSinEliminados(ValorGame.valorIngresado, valorCandidato, ValorGame.valorEliminado);
            v = ValorGame.valorIngresado;

            TextBoxOne t = new TextBoxOne();

            // FILA 0
            if (!string.IsNullOrEmpty(v[0, 0]))
                t.txt00 = v[0, 0]; 
            else
              t.txt00 = c[0, 0];

            if (!string.IsNullOrEmpty(v[0, 1]))
                t.txt01 = v[0, 1];
            else
                t.txt01 = c[0, 1];

            if (!string.IsNullOrEmpty(v[0, 2]))
                t.txt02 = v[0, 2];
            else
                t.txt02 = c[0, 2];

            if (!string.IsNullOrEmpty(v[0, 3]))
                t.txt03 = v[0, 3];
            else
                t.txt03 = c[0, 3];

            if (!string.IsNullOrEmpty(v[0, 4]))
                t.txt04 = v[0, 4];
            else
                t.txt04 = c[0, 4];

            if (!string.IsNullOrEmpty(v[0, 5]))
                t.txt05 = v[0, 5];
            else
                t.txt05 = c[0, 5];

            if (!string.IsNullOrEmpty(v[0, 6]))
                t.txt06 = v[0, 6];
            else
                t.txt06 = c[0, 6];

            if (!string.IsNullOrEmpty(v[0, 7]))
                t.txt07 = v[0, 7];
            else
                t.txt07 = c[0, 7];

            if (!string.IsNullOrEmpty(v[0, 8]))
                t.txt08 = v[0, 8];
            else
                t.txt08 = c[0, 8];

            //FILA 1

            if (!string.IsNullOrEmpty(v[1, 0]))
                t.txt10 = v[1, 0];
            else
                t.txt10 = c[1, 0];

            if (!string.IsNullOrEmpty(v[1, 1]))
                t.txt11 = v[1, 1];
            else
                t.txt11 = c[1, 1];

            if (!string.IsNullOrEmpty(v[1, 2]))
                t.txt12 = v[1, 2];
            else
                t.txt12 = c[1, 2];

            if (!string.IsNullOrEmpty(v[1, 3]))
                t.txt13 = v[1, 3];
            else
                t.txt13 = c[1, 3];

            if (!string.IsNullOrEmpty(v[1, 4]))
                t.txt14 = v[1, 4];
            else
                t.txt14 = c[1, 4];

            if (!string.IsNullOrEmpty(v[1, 5]))
                t.txt15 = v[1, 5];
            else
                t.txt15 = c[1, 5];

            if (!string.IsNullOrEmpty(v[1, 6]))
                t.txt16 = v[1, 6];
            else
                t.txt16 = c[1, 6];

            if (!string.IsNullOrEmpty(v[1, 7]))
                t.txt17 = v[1, 7];
            else
                t.txt17 = c[1, 7];

            if (!string.IsNullOrEmpty(v[1, 8]))
                t.txt18 = v[1, 8];
            else
                t.txt18 = c[1, 8];

            //FILA 2
            if (!string.IsNullOrEmpty(v[2, 0]))
                t.txt20 = v[2, 0];
            else
                t.txt20 = c[2, 0];

            if (!string.IsNullOrEmpty(v[2, 1]))
                t.txt21 = v[2, 1];
            else
                t.txt21 = c[2, 1];

            if (!string.IsNullOrEmpty(v[2, 2]))
                t.txt22 = v[2, 2];
            else
                t.txt22 = c[2, 2];

            if (!string.IsNullOrEmpty(v[2, 3]))
                t.txt23 = v[2, 3];
            else
                t.txt23 = c[2, 3];

            if (!string.IsNullOrEmpty(v[2, 4]))
                t.txt24 = v[2, 4];
            else
                t.txt24 = c[2, 4];

            if (!string.IsNullOrEmpty(v[2, 5]))
                t.txt25 = v[2, 5];
            else
                t.txt25 = c[2, 5];

            if (!string.IsNullOrEmpty(v[2, 6]))
                t.txt26 = v[2, 6];
            else
                t.txt26 = c[2, 6];

            if (!string.IsNullOrEmpty(v[2, 7]))
                t.txt27 = v[2, 7];
            else
                t.txt27 = c[2, 7];

            if (!string.IsNullOrEmpty(v[2, 8]))
                t.txt28 = v[2, 8];
            else
                t.txt28 = c[2, 8];

            // FILA 3

            if (!string.IsNullOrEmpty(v[3, 0]))
                t.txt30 = v[3, 0];
            else
                t.txt30 = c[3, 0];

            if (!string.IsNullOrEmpty(v[3, 1]))
                t.txt31 = v[3, 1];
            else
                t.txt31 = c[3, 1];

            if (!string.IsNullOrEmpty(v[3, 2]))
                t.txt32 = v[3, 2];
            else
                t.txt32 = c[3, 2];

            if (!string.IsNullOrEmpty(v[3, 3]))
                t.txt33 = v[3, 3];
            else
                t.txt33 = c[3, 3];

            if (!string.IsNullOrEmpty(v[3, 4]))
                t.txt34 = v[3, 4];
            else
                t.txt34 = c[3, 4];

            if (!string.IsNullOrEmpty(v[3, 5]))
                t.txt35 = v[3, 5];
            else
                t.txt35 = c[3, 5];

            if (!string.IsNullOrEmpty(v[3, 6]))
                t.txt36 = v[3, 6];
            else
                t.txt36 = c[3, 6];

            if (!string.IsNullOrEmpty(v[3, 7]))
                t.txt37 = v[3, 7];
            else
                t.txt37 = c[3, 7];

            if (!string.IsNullOrEmpty(v[3, 8]))
                t.txt38 = v[3, 8];
            else
                t.txt38 = c[3, 8];

            // FILA 4

            if (!string.IsNullOrEmpty(v[4, 0]))
                t.txt40 = v[4, 0];
            else
                t.txt40 = c[4, 0];

            if (!string.IsNullOrEmpty(v[4, 1]))
                t.txt41 = v[4, 1];
            else
                t.txt41 = c[4, 1];

            if (!string.IsNullOrEmpty(v[4, 2]))
                t.txt42 = v[4, 2];
            else
                t.txt42 = c[4, 2];

            if (!string.IsNullOrEmpty(v[4, 3]))
                t.txt43 = v[4, 3];
            else
                t.txt43 = c[4, 3];

            if (!string.IsNullOrEmpty(v[4, 4]))
                t.txt44 = v[4, 4];
            else
                t.txt44 = c[4, 4];

            if (!string.IsNullOrEmpty(v[4, 5]))
                t.txt45 = v[4, 5];
            else
                t.txt45 = c[4, 5];

            if (!string.IsNullOrEmpty(v[4, 6]))
                t.txt46 = v[4, 6];
            else
                t.txt46 = c[4, 6];

            if (!string.IsNullOrEmpty(v[4, 7]))
                t.txt47 = v[4, 7];
            else
                t.txt47 = c[4, 7];

            if (!string.IsNullOrEmpty(v[4, 8]))
                t.txt48 = v[4, 8];
            else
                t.txt48 = c[4, 8];

            // FILA 5
            if (!string.IsNullOrEmpty(v[5, 0]))
                t.txt50 = v[5, 0];
            else
                t.txt50 = c[5, 0];

            if (!string.IsNullOrEmpty(v[5, 1]))
                t.txt51 = v[5, 1];
            else
                t.txt51 = c[5, 1];

            if (!string.IsNullOrEmpty(v[5, 2]))
                t.txt52 = v[5, 2];
            else
                t.txt52 = c[5, 2];

            if (!string.IsNullOrEmpty(v[5, 3]))
                t.txt53 = v[5, 3];
            else
                t.txt53 = c[5, 3];

            if (!string.IsNullOrEmpty(v[5, 4]))
                t.txt54 = v[5, 4];
            else
                t.txt54 = c[5, 4];

            if (!string.IsNullOrEmpty(v[5, 5]))
                t.txt55 = v[5, 5];
            else
                t.txt55 = c[5, 5];

            if (!string.IsNullOrEmpty(v[5, 6]))
                t.txt56 = v[5, 6];
            else
                t.txt56 = c[5, 6];

            if (!string.IsNullOrEmpty(v[5, 7]))
                t.txt57 = v[5, 7];
            else
                t.txt57 = c[5, 7];

            if (!string.IsNullOrEmpty(v[5, 8]))
                t.txt58 = v[5, 8];
            else
                t.txt58 = c[5, 8];

            // FILA 6
            if (!string.IsNullOrEmpty(v[6, 0]))
                t.txt60 = v[6, 0];
            else
                t.txt60 = c[6, 0];

            if (!string.IsNullOrEmpty(v[6, 1]))
                t.txt61 = v[6, 1];
            else
                t.txt61 = c[6, 1];

            if (!string.IsNullOrEmpty(v[6, 2]))
                t.txt62 = v[6, 2];
            else
                t.txt62 = c[6, 2];

            if (!string.IsNullOrEmpty(v[6, 3]))
                t.txt63 = v[6, 3];
            else
                t.txt63 = c[6, 3];

            if (!string.IsNullOrEmpty(v[6, 4]))
                t.txt64 = v[6, 4];
            else
                t.txt64 = c[6, 4];

            if (!string.IsNullOrEmpty(v[6, 5]))
                t.txt65 = v[6, 5];
            else
                t.txt65 = c[6, 5];

            if (!string.IsNullOrEmpty(v[6, 6]))
                t.txt66 = v[6, 6];
            else
                t.txt66 = c[6, 6];

            if (!string.IsNullOrEmpty(v[6, 7]))
                t.txt67 = v[6, 7];
            else
                t.txt67 = c[6, 7];

            if (!string.IsNullOrEmpty(v[6, 8]))
                t.txt68 = v[6, 8];
            else
                t.txt68 = c[6, 8];

            //FILA 7

            if (!string.IsNullOrEmpty(v[7, 0]))
                t.txt70 = v[7, 0];
            else
                t.txt70 = c[7, 0];

            if (!string.IsNullOrEmpty(v[7, 1]))
                t.txt71 = v[7, 1];
            else
                t.txt71 = c[7, 1];

            if (!string.IsNullOrEmpty(v[7, 2]))
                t.txt72 = v[7, 2];
            else
                t.txt72 = c[7, 2];

            if (!string.IsNullOrEmpty(v[7, 3]))
                t.txt73 = v[7, 3];
            else
                t.txt73 = c[7, 3];

            if (!string.IsNullOrEmpty(v[7, 4]))
                t.txt74 = v[7, 4];
            else
                t.txt74 = c[7, 4];

            if (!string.IsNullOrEmpty(v[7, 5]))
                t.txt75 = v[7, 5];
            else
                t.txt75 = c[7, 5];

            if (!string.IsNullOrEmpty(v[7, 6]))
                t.txt76 = v[7, 6];
            else
                t.txt76 = c[7, 6];

            if (!string.IsNullOrEmpty(v[7, 7]))
                t.txt77 = v[7, 7];
            else
                t.txt77 = c[7, 7];

            if (!string.IsNullOrEmpty(v[7, 8]))
                t.txt78 = v[7, 8];
            else
                t.txt78 = c[7, 8];


            //FILA 8

            if (!string.IsNullOrEmpty(v[8, 0]))
                t.txt80 = v[8, 0];
            else
                t.txt80 = c[8, 0];

            if (!string.IsNullOrEmpty(v[8, 1]))
                t.txt81 = v[8, 1];
            else
                t.txt81 = c[8, 1];

            if (!string.IsNullOrEmpty(v[8, 2]))
                t.txt82 = v[8, 2];
            else
                t.txt82 = c[8, 2];

            if (!string.IsNullOrEmpty(v[8, 3]))
                t.txt83 = v[8, 3];
            else
                t.txt83 = c[8, 3];

            if (!string.IsNullOrEmpty(v[8, 4]))
                t.txt84 = v[8, 4];
            else
                t.txt84 = c[8, 4];

            if (!string.IsNullOrEmpty(v[8, 5]))
                t.txt85 = v[8, 5];
            else
                t.txt85 = c[8, 5];

            if (!string.IsNullOrEmpty(v[8, 6]))
                t.txt86 = v[8, 6];
            else
                t.txt86 = c[8, 6];

            if (!string.IsNullOrEmpty(v[8, 7]))
                t.txt87 = v[8, 7];
            else
                t.txt87 = c[8, 7];

            if (!string.IsNullOrEmpty(v[8, 8]))
                t.txt88 = v[8, 8];
            else
                t.txt88 = c[8, 8];


            return Json(t);
        }
    }
}