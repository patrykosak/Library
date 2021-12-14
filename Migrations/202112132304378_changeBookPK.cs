namespace Library.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeBookPK : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Files", "ISBN", "dbo.Books");
            DropIndex("dbo.Files", new[] { "ISBN" });
            DropPrimaryKey("dbo.Books");
            AddColumn("dbo.Books", "BookID", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Files", "Book_BookID", c => c.Int());
            AlterColumn("dbo.Books", "ISBN", c => c.String(nullable: false, maxLength: 13));
            AddPrimaryKey("dbo.Books", "BookID");
            CreateIndex("dbo.Files", "Book_BookID");
            AddForeignKey("dbo.Files", "Book_BookID", "dbo.Books", "BookID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Files", "Book_BookID", "dbo.Books");
            DropIndex("dbo.Files", new[] { "Book_BookID" });
            DropPrimaryKey("dbo.Books");
            DropColumn("dbo.Files", "Book_BookID");
            DropColumn("dbo.Books", "BookID");
            AlterColumn("dbo.Books", "ISBN", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Books", "ISBN");
            CreateIndex("dbo.Files", "ISBN");
            AddForeignKey("dbo.Files", "ISBN", "dbo.Books", "ISBN", cascadeDelete: true);
        }
    }
}
