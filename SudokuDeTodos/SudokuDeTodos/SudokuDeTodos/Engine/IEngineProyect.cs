﻿using System;
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
        string DecodeBase64(string base64EncodedData);
        bool EstatusLink(DateTime fechaEnvio, DateTime fechaActivacion);
    }
}
