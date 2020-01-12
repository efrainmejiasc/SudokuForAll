using SudokuDeTodos.Models.DbSistema;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SudokuDeTodos.Engine
{
    public class EngineContext : DbContext
    {
        public EngineContext() : base("CnxSudoku")
        {
        }

        public DbSet<Cliente> Cliente { get; set; }

        public DbSet<ClientePago> ClientePago { get; set; }

        public DbSet<ResetPassword> ResetPassword { get; set; }

        public DbSet<SucesoLog> SucesoLog { get; set; }

        // 1. Instalar Entity Framework
        // 2. Crear clase que herede de DbContext
        // 3. Ejecutar enable-migratios
        // 4. Ejecutar update-database -force -verbose
    }
}