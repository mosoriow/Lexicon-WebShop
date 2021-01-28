namespace WebShop.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class wishlist1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WishLists", "productId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.WishLists", "productId");
        }
    }
}
