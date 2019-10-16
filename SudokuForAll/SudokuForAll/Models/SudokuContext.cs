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


        //Deal SUDOKU
        public DbSet<Producto> Producto { get; set; }

        public DbSet<Gerente> Gerente { get; set; }

        public DbSet<TransaccionPaypal> TransaccionPaypal { get; set; }

        // 1. Instalar Entity Framework
        // 2. Crear clase que herede de DbContext
        // 3. Ejecutar enable-migratios
        // 4. Ejecutar update-database -force -verbose
    }
}