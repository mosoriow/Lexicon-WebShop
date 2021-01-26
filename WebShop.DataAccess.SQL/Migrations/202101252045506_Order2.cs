namespace WebShop.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Order2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "Basket_Id", "dbo.Baskets");
            DropIndex("dbo.Orders", new[] { "Basket_Id" });
            AddColumn("dbo.Orders", "BasketId", c => c.String());
            DropColumn("dbo.Orders", "Basket_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "Basket_Id", c => c.String(maxLength: 128));
            DropColumn("dbo.Orders", "BasketId");
            CreateIndex("dbo.Orders", "Basket_Id");
            AddForeignKey("dbo.Orders", "Basket_Id", "dbo.Baskets", "Id");
        }
    }
}
