namespace Library.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        AuthorID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Surname = c.String(nullable: false, maxLength: 100),
                        BirthDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.AuthorID);
            
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        ISBN = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 150),
                        PublicYear = c.Int(nullable: false),
                        Amount = c.Int(nullable: false),
                        AuthorID = c.Int(nullable: false),
                        publishingHouseID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ISBN)
                .ForeignKey("dbo.Authors", t => t.AuthorID, cascadeDelete: true)
                .ForeignKey("dbo.PublishingHouses", t => t.publishingHouseID, cascadeDelete: true)
                .Index(t => t.AuthorID)
                .Index(t => t.publishingHouseID);
            
            CreateTable(
                "dbo.PublishingHouses",
                c => new
                    {
                        publishingHouseID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 60),
                    })
                .PrimaryKey(t => t.publishingHouseID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "publishingHouseID", "dbo.PublishingHouses");
            DropForeignKey("dbo.Books", "AuthorID", "dbo.Authors");
            DropIndex("dbo.Books", new[] { "publishingHouseID" });
            DropIndex("dbo.Books", new[] { "AuthorID" });
            DropTable("dbo.PublishingHouses");
            DropTable("dbo.Books");
            DropTable("dbo.Authors");
        }
    }
}
