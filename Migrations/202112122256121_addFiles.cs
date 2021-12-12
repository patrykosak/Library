namespace Library.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addFiles : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        FileID = c.Int(nullable: false, identity: true),
                        Address = c.String(),
                        Name = c.String(),
                        ISBN = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FileID)
                .ForeignKey("dbo.Books", t => t.ISBN, cascadeDelete: true)
                .Index(t => t.ISBN);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Files", "ISBN", "dbo.Books");
            DropIndex("dbo.Files", new[] { "ISBN" });
            DropTable("dbo.Files");
        }
    }
}
