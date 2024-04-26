namespace CapaDatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Correccion : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.DetalleVentas", "SubTotal");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DetalleVentas", "SubTotal", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
