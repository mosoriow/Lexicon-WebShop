namespace WebShop.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FilePathAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "filePath1", c => c.String());
            AddColumn("dbo.Products", "filePath2", c => c.String());
            AddColumn("dbo.Products", "filePath3", c => c.String());
            AddColumn("dbo.Products", "filePath4", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "filePath4");
            DropColumn("dbo.Products", "filePath3");
            DropColumn("dbo.Products", "filePath2");
            DropColumn("dbo.Products", "filePath1");
        }
    }
}
