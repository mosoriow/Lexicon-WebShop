namespace WebShop.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Merged : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.Products", "SubCategory_Id", "dbo.SubCategories");
            DropIndex("dbo.Products", new[] { "Category_Id" });
            DropIndex("dbo.Products", new[] { "SubCategory_Id" });
            CreateTable(
                "dbo.BasketItems",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        BasketId = c.String(maxLength: 128),
                        ProductId = c.String(),
                        Quantity = c.Int(nullable: false),
                        CreateAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Baskets", t => t.BasketId)
                .Index(t => t.BasketId);
            
            CreateTable(
                "dbo.Baskets",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CouponName = c.String(),
                        Delivery = c.String(),
                        CreateAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Path = c.String(),
                        CreateAt = c.DateTimeOffset(nullable: false, precision: 7),
                        Product_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .Index(t => t.Product_Id);
            
            AddColumn("dbo.Products", "Manufacture", c => c.String());
            AddColumn("dbo.Products", "Category", c => c.String());
            AddColumn("dbo.Products", "SubCategory", c => c.String());
            AddColumn("dbo.Products", "Size", c => c.String());
            AddColumn("dbo.Products", "Colour", c => c.String());
            AddColumn("dbo.Products", "Discount", c => c.Int(nullable: false));
            AddColumn("dbo.UserReviews", "UserName", c => c.String());
            DropColumn("dbo.Products", "Category_Id");
            DropColumn("dbo.Products", "SubCategory_Id");
            DropColumn("dbo.UserReviews", "UserId");
            DropTable("dbo.Categories");
            DropTable("dbo.SubCategories");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SubCategories",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        CreateAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        CreateAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.UserReviews", "UserId", c => c.String());
            AddColumn("dbo.Products", "SubCategory_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Products", "Category_Id", c => c.String(maxLength: 128));
            DropForeignKey("dbo.Images", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.BasketItems", "BasketId", "dbo.Baskets");
            DropIndex("dbo.Images", new[] { "Product_Id" });
            DropIndex("dbo.BasketItems", new[] { "BasketId" });
            DropColumn("dbo.UserReviews", "UserName");
            DropColumn("dbo.Products", "Discount");
            DropColumn("dbo.Products", "Colour");
            DropColumn("dbo.Products", "Size");
            DropColumn("dbo.Products", "SubCategory");
            DropColumn("dbo.Products", "Category");
            DropColumn("dbo.Products", "Manufacture");
            DropTable("dbo.Images");
            DropTable("dbo.Baskets");
            DropTable("dbo.BasketItems");
            CreateIndex("dbo.Products", "SubCategory_Id");
            CreateIndex("dbo.Products", "Category_Id");
            AddForeignKey("dbo.Products", "SubCategory_Id", "dbo.SubCategories", "Id");
            AddForeignKey("dbo.Products", "Category_Id", "dbo.Categories", "Id");
        }
    }
}
