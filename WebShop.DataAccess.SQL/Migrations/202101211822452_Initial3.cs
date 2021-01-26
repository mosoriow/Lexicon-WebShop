namespace WebShop.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserReviews", "UserName", c => c.String());
            DropColumn("dbo.UserReviews", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserReviews", "UserId", c => c.String());
            DropColumn("dbo.UserReviews", "UserName");
        }
    }
}
