namespace WebShop.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FilePathAdded1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Products", "filePath2");
            DropColumn("dbo.Products", "filePath3");
            DropColumn("dbo.Products", "filePath4");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "filePath4", c => c.String());
            AddColumn("dbo.Products", "filePath3", c => c.String());
            AddColumn("dbo.Products", "filePath2", c => c.String());
        }
    }
}
