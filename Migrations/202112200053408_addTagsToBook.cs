namespace Library.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTagsToBook : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        TagID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        ISBN = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TagID)
                .ForeignKey("dbo.Books", t => t.ISBN, cascadeDelete: true)
                .Index(t => t.ISBN);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tags", "ISBN", "dbo.Books");
            DropIndex("dbo.Tags", new[] { "ISBN" });
            DropTable("dbo.Tags");
        }
    }
}
