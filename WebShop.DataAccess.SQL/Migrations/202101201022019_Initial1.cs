namespace WebShop.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Size", c => c.String());
            AddColumn("dbo.Products", "Colour", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Colour");
            DropColumn("dbo.Products", "Size");
        }
    }
}
