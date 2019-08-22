using SudokuForAll.Models.DbSistema;
using SudokuForAll.Models.Sistema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuForAll.Engine
{
    public interface IEngineDb
    {
        int ObtenerNumeroDePago();
        int ObtenerIdCliente(string email);
        int ResultadoLogin(string password);
        int UpdateClienteTest(Cliente model);
        bool InsertarSucesoLog(SucesoLog model);
        int ResultadoEntradaAlSitio(string email);
        int ClienteRegistro(ActivarCliente model);
        Guid ObtenerIdentidadCliente(string email);
        string ObtenerPasswordCliente(string email);
        bool InsertarResetPassword(ResetPassword model);
        int ClienteUpdatePassword(ActivarCliente model);
        int ObtenerIdCliente(string email, bool estatus);
        int ClienteRegistroActivacion(ActivarCliente model);
        string ObtenerCodigoRestablecerPassword(string email);
        bool InsertarClienteTest(IEngineProyect Funcion,string email);
        int UpdateResetPassword(string email, string codigo, bool estatus);
    }
}
