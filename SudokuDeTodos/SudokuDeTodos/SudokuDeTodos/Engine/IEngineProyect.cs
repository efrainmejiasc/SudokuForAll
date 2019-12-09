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
        string EncodeMd5(string a);
        bool EmailEsValido(string email);
        string ConvertirBase64(string cadena);
        bool CadenaBase64Valida(string cadena);
        bool CompareString(string a, string b);
        SucesoLog ConstruirSucesoLog(string cadena);
        string DecodeBase64(string base64EncodedData);
        bool EstatusLink(DateTime fechaEnvio, DateTime fechaActivacion);
        Respuesta ConstruirRespuesta(int id, bool status, string descripcion);
    }
}
