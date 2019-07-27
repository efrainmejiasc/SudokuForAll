using SudokuForAll.Models.DbSistema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuForAll.Engine
{
    public interface IEngineProyect
    {
        string ConstruirCodigo();
        string EncodeMd5(string a);
        bool EmailEsValido(string email);
        string ConvertirBase64(string cadena);
        bool CompareString(string a, string b);
        string DecodeBase64(string base64EncodedData);
        Cliente ConstruirInsertarClienteTest(string email);
        string CrearEnlazePrueba(IEngineDb Metodo, string email);
        ResetPassword SetResetPassword(string email, string codigo);
        bool EstatusLink(DateTime fechaEnvio, DateTime fechaActivacion);
        ActivarCliente ConstruirActivarCliente(string email, string password);
        Cliente ConstruirActualizarClienteTest(string email, string Identidad);
        string CrearEnlazeRegistro(IEngineDb Metodo, string email, string password);
        Respuesta RespuestaProceso(string respuesta, string email, string codigo = "");
        string CrearEnlazeRestablecerPassword(IEngineDb Metodo, string email, string codigo);
        EstructuraMail SetEstructuraMailTest(string enlaze, string email, EstructuraMail model);
        EstructuraMail SetEstructuraMailRegister(string enlaze, string email, EstructuraMail model);
        EstructuraMail SetEstructuraMailResetPassword(string enlaze, string email, string codigo, EstructuraMail model);
        bool EnviarNuevaNotificacion(IEngineNotificacion Notificacion, IEngineDb Metodo, string email, string type, string password = "");
    }
}
