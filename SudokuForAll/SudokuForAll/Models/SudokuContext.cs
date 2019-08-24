using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SudokuForAll.Models.DbSistema;

namespace SudokuForAll.Models
{
    public class SudokuContext : DbContext
    {
        public SudokuContext() : base("CnxSudoku")
        {
        }

        public DbSet<Cliente> Cliente { get; set; }

        public DbSet<PagoCliente> PagoCliente { get; set; }

        public DbSet<ResetPassword> ResetPassword { get; set; }

        public DbSet<SucesoLog> SucesoLog{ get; set; }

        public DbSet<Producto> Producto { get; set; }

        // 1. Instalar Entity Framework
        // 2. Crear clase que herede de DbContext
        // 3. Ejecutar enable-migratios
        // 4. Ejecutar update-database -force -verbose
    }
}