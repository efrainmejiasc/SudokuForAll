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
        bool Autentificacion(string password);
        Guid GetIdentidadCliente(string email);
        int VerificarClientePago(string email);
        bool InsertarClientePago(ClientePago model);
        bool UpdateClienteTest(string email, int status);
        bool UpdateClienteRegister(string email, int status);
        int UpdatePasswordCliente(string email, string password);
    }
}