namespace Library.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Author",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Surname = c.String(nullable: false, maxLength: 100),
                        BirthDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Book",
                c => new
                    {
                        ISBN = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 150),
                        PublicYear = c.Int(nullable: false),
                        Amount = c.Int(nullable: false),
                        Author_ID = c.Int(),
                        PublishingHouse_Id = c.Int(),
                    })
                .PrimaryKey(t => t.ISBN)
                .ForeignKey("dbo.Author", t => t.Author_ID)
                .ForeignKey("dbo.PublishingHouse", t => t.PublishingHouse_Id)
                .Index(t => t.Author_ID)
                .Index(t => t.PublishingHouse_Id);
            
            CreateTable(
                "dbo.PublishingHouse",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 60),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Book", "PublishingHouse_Id", "dbo.PublishingHouse");
            DropForeignKey("dbo.Book", "Author_ID", "dbo.Author");
            DropIndex("dbo.Book", new[] { "PublishingHouse_Id" });
            DropIndex("dbo.Book", new[] { "Author_ID" });
            DropTable("dbo.PublishingHouse");
            DropTable("dbo.Book");
            DropTable("dbo.Author");
        }
    }
}
