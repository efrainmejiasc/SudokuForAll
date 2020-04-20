using SudokuDeTodos.Models.DbSistema;
using SudokuDeTodos.Models.Sistema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SudokuDeTodos.Engine.Interfaces
{
    public interface  IEngineDb
    {
        int VerificarEmail(string email);
        bool InsertarCliente(Cliente model);
        bool Autentificacion(string password);
        string GetAdministrador(string email);
        Guid GetIdentidadCliente(string email);
        int VerificarClientePago(string email);
        bool ValidarAdministrador(string email);
        bool InsertarClientePago(ClientePago model);
        bool DeleteCodigoResetPassword(string email);
        bool CreateAdministrador(Administrador modelo);
        bool UpdateClienteTest(string email, int status);
        bool UpdatePassword(string email, string password);
        bool UpdateClienteRegister(string email, int status);
        bool InsertarCodigoResetPassword(ResetPassword model);
        int UpdatePasswordCliente(string email, string password);
        bool UpdateCodigoResetPassword(string email, string codigo);
        bool ValidarCodigoResetPassword(string email, string codigo);
        bool InsertarCliente(Cliente cliente, ClientePago clientePago);
        bool EditarClientePagoFechaVencimiento(int id, DateTime fechaVencimiento);
        List<ConsultaReporte> ConsultaReporte(DateTime fechaInicial, DateTime fechaFinal);
    }
}