namespace Library.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPicturesToBook : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Pictures",
                c => new
                    {
                        PictureID = c.Int(nullable: false, identity: true),
                        Link = c.String(),
                        ISBN = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PictureID)
                .ForeignKey("dbo.Books", t => t.ISBN, cascadeDelete: true)
                .Index(t => t.ISBN);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pictures", "ISBN", "dbo.Books");
            DropIndex("dbo.Pictures", new[] { "ISBN" });
            DropTable("dbo.Pictures");
        }
    }
}
