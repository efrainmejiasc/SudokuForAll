using SudokuDeTodos.Models.DbSistema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SudokuDeTodos.Engine
{
    public interface  IEngineDb
    {
        int VerificarEmail(string email);
        bool InsertarCliente(Cliente model);
        int VerificarClientePago(string email);
    }
}