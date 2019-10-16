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
        bool PutProducto(Producto m);
        List<Roles> GetAllGerentes();
        Gerente GetGerente(string email);
        int ObtenerIdCliente(string email);   
        int ResultadoLogin(string password);
        Producto GetProducto(string codigo);
        List<Producto> ProductosParaVenta();
        int UpdateClienteTest(Cliente model);
        Gerente GetGerenteName(string nombre);
        bool SetPagoCliente(PagoCliente model);
        List<Producto> GetProductosParaVenta();
        bool InsertarSucesoLog(SucesoLog model);
        Gerente GetLoginGerente(string password);
        bool InsertarNuevoGerente(Gerente model);
        int ResultadoEntradaAlSitio(string email);
        int ClienteRegistro(ActivarCliente model);
        Guid ObtenerIdentidadCliente(string email);
        Guid ObtenerIdentidadGerente(string email);
        string ObtenerPasswordCliente(string email);
        void EstablecerCulturaCliente(string email);
        bool InsertarProductoParaVenta(Producto model);
        bool PutGerente(Gerente m, string subEjecutada);
        bool InsertarResetPassword(ResetPassword model);
        int ClienteUpdatePassword(ActivarCliente model);
        int ObtenerIdCliente(string email, bool estatus);
        Gerente GetGerenteUserName(string nombreUsuario);
        int ClienteRegistroActivacion(ActivarCliente model);
        string ObtenerCodigoRestablecerPassword(string email);
        bool InsertarTransaccionPaypal(TransaccionPaypal model);
        bool InsertarClienteTest(IEngineProyect Funcion,string email);
        int UpdateResetPassword(string email, string codigo, bool estatus);
    }
}
