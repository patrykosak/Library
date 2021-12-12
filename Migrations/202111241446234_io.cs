namespace Library.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class io : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Borrows", "UserID", "dbo.AspNetUsers");
            DropIndex("dbo.Borrows", new[] { "UserID" });
            AlterColumn("dbo.Borrows", "UserID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Borrows", "UserID");
            AddForeignKey("dbo.Borrows", "UserID", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Borrows", "UserID", "dbo.AspNetUsers");
            DropIndex("dbo.Borrows", new[] { "UserID" });
            AlterColumn("dbo.Borrows", "UserID", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Borrows", "UserID");
            AddForeignKey("dbo.Borrows", "UserID", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
