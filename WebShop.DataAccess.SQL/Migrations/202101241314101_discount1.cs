namespace WebShop.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class discount1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Baskets", "DiscountItem_Id", "dbo.DiscountItems");
            DropIndex("dbo.Baskets", new[] { "DiscountItem_Id" });
            AddColumn("dbo.Baskets", "CouponName", c => c.String());
            DropColumn("dbo.Baskets", "DiscountItem_Id");
            DropTable("dbo.DiscountItems");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.DiscountItems",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CouponName = c.String(),
                        Price = c.Int(nullable: false),
                        CreateAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Baskets", "DiscountItem_Id", c => c.String(maxLength: 128));
            DropColumn("dbo.Baskets", "CouponName");
            CreateIndex("dbo.Baskets", "DiscountItem_Id");
            AddForeignKey("dbo.Baskets", "DiscountItem_Id", "dbo.DiscountItems", "Id");
        }
    }
}
