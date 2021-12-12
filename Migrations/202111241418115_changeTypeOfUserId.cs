namespace Library.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeTypeOfUserId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Borrows", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Borrows", new[] { "User_Id" });
            DropColumn("dbo.Borrows", "UserID");
            RenameColumn(table: "dbo.Borrows", name: "User_Id", newName: "UserID");
            AlterColumn("dbo.Borrows", "UserID", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Borrows", "UserID", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Borrows", "UserID");
            AddForeignKey("dbo.Borrows", "UserID", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Borrows", "UserID", "dbo.AspNetUsers");
            DropIndex("dbo.Borrows", new[] { "UserID" });
            AlterColumn("dbo.Borrows", "UserID", c => c.String(maxLength: 128));
            AlterColumn("dbo.Borrows", "UserID", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Borrows", name: "UserID", newName: "User_Id");
            AddColumn("dbo.Borrows", "UserID", c => c.Int(nullable: false));
            CreateIndex("dbo.Borrows", "User_Id");
            AddForeignKey("dbo.Borrows", "User_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
