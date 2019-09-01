using SudokuForAll.Models.DbSistema;
using SudokuForAll.Models.Sistema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuForAll.Engine
{
    public interface IEngineProyect
    {
        PagoCliente ConstruirPagoCliente(int idCliente);
        List<Roles> Roles();
        void AnularGerente();
        List<Moneda> Monedas();
        List<Roles> Gerentes();
        Guid IdentificadorReg();
        string ConstruirCodigo();
        string EncodeMd5(string a);
        bool EmailEsValido(string email);
        void SetCultureInfo(string cultura);
        string ConvertirBase64(string cadena);
        bool CompareString(string a,string b);
        bool CadenaBase64Valida(string cadena);
        SucesoLog ConstruirSucesoLog(string cadena);
        string DecodeBase64(string base64EncodedData);
        Cliente ConstruirInsertarClienteTest(string email);
        bool ConstruirSucesoLog(string cadena, IEngineDb Metodo);
        string CrearEnlazePrueba(IEngineDb Metodo, string email);
        ResetPassword SetResetPassword(string email, string codigo);
        bool EstatusLink(DateTime fechaEnvio, DateTime fechaActivacion);
        string CrearEnlazeRegistroGerente(IEngineDb Metodo, string email);
        string CrearEnlazeRegistro(IEngineDb Metodo, string email, string password);
        string CrearEnlazeRestablecerPassword(IEngineDb Metodo, string email, string codigo);
        EstructuraMail SetEstructuraMailTest(string enlaze,string email, EstructuraMail model);
        ActivarCliente ConstruirActivarCliente(IEngineDb Metodo, string email, string password);
        Cliente ConstruirActualizarClienteTest(IEngineDb Metodo, string email, string Identidad);
        EstructuraMail SetEstructuraMailRegister(string enlaze, string email, EstructuraMail model);
        EstructuraMail SetEstructuraMailRegisterManager(string enlaze, string email, EstructuraMail model);
        EstructuraMail SetEstructuraMailResetPassword(string enlaze, string email, string codigo, EstructuraMail model);
        Respuesta RespuestaProceso(string respuesta = "", string email = "", string codigo = "", string descripcion = "");
        bool EnviarNuevaNotificacion(IEngineNotificacion Notificacion, IEngineDb Metodo, string email, string type, string password = "");
    }
}
