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
        int ObtenerIdCliente(string email);
        int ResultadoLogin(string password);
        int UpdateClienteTest(Cliente model);
        int ResultadoEntradaAlSitio(string email);
        int ClienteRegistro(ActivarCliente model);
        Guid ObtenerIdentidadCliente(string email);
        bool InsertarResetPassword(ResetPassword model);
        int ClienteRegistroActivacion(ActivarCliente model);
        string ObtenerCodigoRestablecerPassword(string email);
        bool InsertarClienteTest(string email, IEngineProyect Funcion);
        int UpdateResetPassword(string email, string codigo, bool estatus);
    }
}
