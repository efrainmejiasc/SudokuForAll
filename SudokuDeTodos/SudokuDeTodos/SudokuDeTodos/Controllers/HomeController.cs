using SudokuDeTodos.Engine;
using SudokuDeTodos.Models.DbSistema;
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
        private readonly IEngineDb Metodo;
        private readonly IEngineProyect Funcion;

        public HomeController(IEngineDb _Metodo, IEngineProyect _Funcion)
        {
            this.Metodo = _Metodo;
            this.Funcion = _Funcion;
        }

        #region ENTRADA_SITIO
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
        #endregion ENTRADA_SITIO

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult About(string email = "", string password = "")
        {
            if (email == string.Empty || password == string.Empty)
            return View();

            string key = Funcion.ConvertirBase64(email + password);
            Respuesta respuesta = new Respuesta();
            respuesta.Id = Metodo.UpdatePasswordCliente(email, key);
            if (respuesta.Id > 0)
            {
                ClientePago clientePago = Funcion.ConstruirClientePago(respuesta.Id);
                respuesta.Status = Metodo.InsertarClientePago(clientePago);
                respuesta.Id = 7;
                respuesta.Descripcion = StringResx.Resources.RegistroPago;
            }
            return View("ResponseMessage", respuesta);
        }
        
        public ActionResult Login(string password = "")
        {
            if (System.Web.HttpContext.Current.Session["EMAIL"] == null)
                return Redirect("Contact");

            Respuesta respuesta = new Respuesta();
            if (password == string.Empty)
                return View(respuesta);

            string email = System.Web.HttpContext.Current.Session["EMAIL"].ToString();
            password = Funcion.ConvertirBase64(email + password);
            respuesta.Status = Metodo.Autentificacion(password);
            respuesta.Id = -1;
            if (respuesta.Status)
                return Redirect("/Vista/GameAOne.aspx");
            else
                return View(respuesta);
        }

        public ActionResult PasswordModification(string codigo,string email,string password,string password2)
        {
            Respuesta respuesta = new Respuesta();
            respuesta.Id = -1;
            if (email == string.Empty || email == null)
                return View(respuesta);

            bool resultado = Funcion.EmailEsValido(email); //valido formato email
            if (!resultado)
            {
                respuesta = Funcion.ConstruirRespuesta(0, false, EngineData.EmailNoValido(), email);
                return View(respuesta);
            }
            resultado = Metodo.ValidarCodigoResetPassword(email, codigo);
            if (!resultado)
            {
                respuesta = Funcion.ConstruirRespuesta(1, false, EngineData.CodigoNoCoincide(), email);
                return View(respuesta);
            }
            resultado = Metodo.UpdateCodigoResetPassword(email,codigo);
            if (!resultado)
            {
                respuesta = Funcion.ConstruirRespuesta(2, false, EngineData.ErrorInternoServidor(), email);
                return View(respuesta);
            }
            password = Funcion.ConvertirBase64(email + password);
            resultado = Metodo.UpdatePassword(email, password);
            if (!resultado)
            {
                respuesta = Funcion.ConstruirRespuesta(2, false, EngineData.ErrorInternoServidor(), email);
                return View(respuesta);
            }
            Metodo.DeleteCodigoResetPassword(email);
            respuesta = Funcion.ConstruirRespuesta(3, true, EngineData.RestablecerContraseñaExito(), email);
            return View(respuesta);
        }

        public ActionResult ResponseMessage(string email = "" , int id = -2)
        {
            Respuesta respuesta = new Respuesta();

            if (id == 8)
            {
                respuesta = Funcion.ConstruirRespuesta(id, true, StringResx.Resources.MsjModificarPassword, email); //ModificarContraseña?
                return View("ResponseMessage", respuesta);
            }

            if (email == string.Empty)
            {
                respuesta.Id = -1;
                return View("ResponseMessage", respuesta);
            }
 
            System.Web.HttpContext.Current.Session["EMAIL"] = email;
            respuesta.Status = Funcion.EmailEsValido(email);
            if (!respuesta.Status)
            {
                respuesta.Descripcion = email + EngineData.EmailNoValido();
                return Json(respuesta);
            }
            respuesta.Id = Metodo.VerificarEmail(email);
            if (respuesta.Id == 0)
            {
                respuesta = Funcion.ConstruirRespuesta(respuesta.Id, true, StringResx.Resources.MsjPruebaSitio,email); //Prueba sudokudetodos?
                return View("ResponseMessage", respuesta);
            }
            else if (respuesta.Id == 1)
            {
                respuesta = Funcion.ConstruirRespuesta(respuesta.Id, true, StringResx.Resources.CtaNoActivada,email); //Cuenta NO activada de prueba
                return View("ResponseMessage", respuesta);
            }
            else if (respuesta.Id == 2)
            {
                return Redirect("/Vista/NewGame.aspx"); // Ir a jugar prueba
            }
            else if (respuesta.Id == 3)
            {
                int resultado = Metodo.VerificarClientePago(email);// Verifico pago del cliente 
                if (resultado == 1)
                {
                    return Redirect("Login");// Ir Autentificacion
                }
                else if (resultado == 0)
                {
                    respuesta = Funcion.ConstruirRespuesta(4, true, EngineData.TiempoJuegoExpiro(), email); // Pago expirado ,comprar nuevamente
                    return View("ResponseMessage", respuesta);
                }
                else if (resultado == -1)
                {
                    respuesta = Funcion.ConstruirRespuesta(5, true, EngineData.TiempoPruebaJuegoExpiro(), email); // Comprar y  fabricar contraseña
                    return View("ResponseMessage", respuesta);
                }
                else if (resultado == -2)
                {
                    respuesta = Funcion.ConstruirRespuesta(6, true, EngineData.ErrorInternoServidor(), email);// Error 
                    return View("ResponseMessage", respuesta);
                }
            }
            return View();
        }

    }
}