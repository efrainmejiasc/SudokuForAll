namespace SudokuForAll.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PassExtend : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Cliente", "Password", c => c.String(maxLength: 100, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Cliente", "Password", c => c.String(maxLength: 50, unicode: false));
        }
    }
}
