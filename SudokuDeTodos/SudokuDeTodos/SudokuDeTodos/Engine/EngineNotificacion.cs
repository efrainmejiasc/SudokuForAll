using SudokuDeTodos.Models.Sistema;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace SudokuDeTodos.Engine
{
    public class EngineNotificacion: IEngineNotificacion
    {
        public bool EnviarMailNotificacion(EstructuraMail model)
        {
            bool result = false;
            model = ConstruccionNotificacion(model);
            try
            {
                MailMessage mensaje = new MailMessage();
                SmtpClient servidor = new SmtpClient();
                mensaje.From = new MailAddress("SudokuParaTodos <" + EngineData.UserMail + ">");
                mensaje.Subject = model.Asunto;
                mensaje.SubjectEncoding = System.Text.Encoding.UTF8;
                mensaje.Body = model.Cuerpo;
                mensaje.BodyEncoding = System.Text.Encoding.UTF8;
                mensaje.IsBodyHtml = true;
                mensaje.To.Add(new MailAddress(model.EmailDestinatario));
                servidor.Credentials = new System.Net.NetworkCredential(EngineData.UserMail, EngineData.IdMail);
                servidor.Port = 587;
                servidor.Host = "smtp.gmail.com";
                servidor.EnableSsl = true;
                servidor.Send(mensaje);
                mensaje.Dispose();
                result = true;
            }
            catch (Exception ex)
            {
                string e = ex.ToString();
            }

            return result;
        }


        private EstructuraMail ConstruccionNotificacion(EstructuraMail model)
        {
            string body = HttpContext.Current.Server.MapPath(model.PathLecturaArchivo);
            body = File.ReadAllText(body);
            body = body.Replace("@Model.Saludo", model.Saludo);
            body = body.Replace("@Model.Fecha", model.Fecha);
            body = body.Replace("@Model.EmailDestinatario", model.EmailDestinatario);
            body = body.Replace("@Model.Observacion", model.Observacion);
            body = body.Replace("@Model.Descripcion", model.Descripcion);
            body = body.Replace("@Model.ClickAqui", model.ClickAqui);
            body = body.Replace("@Model.Link", model.Link);
            body = body.Replace("@Model.CodigoResetPassword", model.CodigoResetPassword);
            model.Cuerpo = body;
            return model;
        }
    }
}