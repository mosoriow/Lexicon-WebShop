﻿namespace WebShop.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedMembershipTypeDatabase : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO [dbo].[MembershipTypes]([Name]) VALUES ('Member')");
            Sql("INSERT INTO [dbo].[MembershipTypes]([Name]) VALUES ('SuperAdmin')");
        }
        
        public override void Down()
        {
        }
    }
}
