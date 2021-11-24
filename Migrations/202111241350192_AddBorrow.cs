namespace Library.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBorrow : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Borrows",
                c => new
                    {
                        BorrowID = c.Int(nullable: false, identity: true),
                        BookID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        BorrowDate = c.DateTime(nullable: false),
                        RaturnDate = c.DateTime(nullable: false),
                        Book_ISBN = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.BorrowID)
                .ForeignKey("dbo.Books", t => t.Book_ISBN)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Book_ISBN)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Borrows", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Borrows", "Book_ISBN", "dbo.Books");
            DropIndex("dbo.Borrows", new[] { "User_Id" });
            DropIndex("dbo.Borrows", new[] { "Book_ISBN" });
            DropTable("dbo.Borrows");
        }
    }
}
