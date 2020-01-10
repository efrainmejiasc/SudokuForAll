using SudokuDeTodos.Models.DbSistema;
using SudokuDeTodos.Models.Sistema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuDeTodos.Engine
{
    public interface IEngineProyect
    {
        Guid IdentificadorReg();
        string ConstruirCodigo();
        string EncodeMd5(string a);
        bool EmailEsValido(string email);
        void SetCultureInfo(string cultura);
        bool EstatusLink(DateTime fechaEnvio);
        string ConvertirBase64(string cadena);
        bool CadenaBase64Valida(string cadena);
        Cliente ConstruirCliente(string email);
        bool CompareString(string a, string b);
        string MetodoTransactionType(string type);
        SucesoLog ConstruirSucesoLog(string cadena);
        bool ValidacionTypeTransaccion(string type);
        string DecodeBase64(string base64EncodedData);
        ClientePago ConstruirClientePago(int idCliente);
        Cliente ConstruirCliente(string email, Guid identidad);
        string ConstruirEnlazePrueba(string email, Guid identidad);
        ResetPassword SetResetPassword(string email, string codigo);
        bool EstatusLink(DateTime fechaEnvio, DateTime fechaActivacion);
        EstructuraMail SetEstructuraMailTest(string enlaze, string email);
        string ConstruirEnlazeRegistroGerente(string email, Guid identidad);
        EstructuraMail SetEstructuraMailRegister(string enlaze, string email);
        bool ValidacionIdentidad(string email, string identidad, IEngineDb Metodo);
        EstructuraMail SetEstructuraMailRegisterManager(string enlaze, string email);
        string ConstruirEnlazeRegistro(string email, string password, Guid identidad);
        Respuesta ConstruirRespuesta(int id, bool status, string descripcion,string email);
        string ConstruirEnlazeRestablecerPassword(string email, string codigo, Guid identidad);
        EstructuraMail SetEstructuraMailResetPassword(string enlaze, string email, string codigo);
        Respuesta ConstruirRespuesta(int id, bool status, string descripcion, string email, string type);
        bool EnviarNuevaNotificacion(Guid identidad, string email, string type, string password, string codigo);
    }
}
