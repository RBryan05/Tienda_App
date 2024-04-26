namespace CapaDatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Actualizacion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DetalleVentas", "SubTotal", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DetalleVentas", "SubTotal");
        }
    }
}
