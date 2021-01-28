namespace WebShop.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
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
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        OrderDate = c.DateTime(nullable: false),
                        Username = c.String(),
                        FirstName = c.String(nullable: false, maxLength: 160),
                        LastName = c.String(nullable: false, maxLength: 160),
                        Address = c.String(nullable: false, maxLength: 70),
                        City = c.String(nullable: false, maxLength: 40),
                        PostalCode = c.String(nullable: false, maxLength: 10),
                        Phone = c.String(nullable: false, maxLength: 24),
                        Email = c.String(nullable: false),
                        BasketId = c.String(),
                        Comment = c.String(),
                        CreateAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(maxLength: 20),
                        Description = c.String(),
                        Manufacture = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Category = c.String(),
                        SubCategory = c.String(),
                        Availability = c.Int(nullable: false),
                        Size = c.String(),
                        Colour = c.String(),
                        Discount = c.Int(nullable: false),
                        CreateAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserReviews",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserName = c.String(),
                        Review = c.String(),
                        Rating = c.Int(nullable: false),
                        CreateAt = c.DateTimeOffset(nullable: false, precision: 7),
                        Product_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.UserAccounts",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(),
                        UserName = c.String(),
                        Password = c.String(),
                        Email = c.String(),
                        CreateAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WishLists",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        productId = c.String(),
                        UserName = c.String(),
                        CreateAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserReviews", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.Images", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.BasketItems", "BasketId", "dbo.Baskets");
            DropIndex("dbo.UserReviews", new[] { "Product_Id" });
            DropIndex("dbo.Images", new[] { "Product_Id" });
            DropIndex("dbo.BasketItems", new[] { "BasketId" });
            DropTable("dbo.WishLists");
            DropTable("dbo.UserAccounts");
            DropTable("dbo.UserReviews");
            DropTable("dbo.Products");
            DropTable("dbo.Orders");
            DropTable("dbo.Images");
            DropTable("dbo.Baskets");
            DropTable("dbo.BasketItems");
        }
    }
}
