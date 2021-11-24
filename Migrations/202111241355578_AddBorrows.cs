namespace Library.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBorrows : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Borrows", "Book_ISBN", "dbo.Books");
            DropIndex("dbo.Borrows", new[] { "Book_ISBN" });
            RenameColumn(table: "dbo.Borrows", name: "Book_ISBN", newName: "ISBN");
            AlterColumn("dbo.Borrows", "ISBN", c => c.Int(nullable: false));
            CreateIndex("dbo.Borrows", "ISBN");
            AddForeignKey("dbo.Borrows", "ISBN", "dbo.Books", "ISBN", cascadeDelete: true);
            DropColumn("dbo.Borrows", "BookID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Borrows", "BookID", c => c.Int(nullable: false));
            DropForeignKey("dbo.Borrows", "ISBN", "dbo.Books");
            DropIndex("dbo.Borrows", new[] { "ISBN" });
            AlterColumn("dbo.Borrows", "ISBN", c => c.Int());
            RenameColumn(table: "dbo.Borrows", name: "ISBN", newName: "Book_ISBN");
            CreateIndex("dbo.Borrows", "Book_ISBN");
            AddForeignKey("dbo.Borrows", "Book_ISBN", "dbo.Books", "ISBN");
        }
    }
}
