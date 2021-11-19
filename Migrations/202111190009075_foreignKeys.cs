namespace Library.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class foreignKeys : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Book", "Author_ID", "dbo.Author");
            DropForeignKey("dbo.Book", "PublishingHouse_Id", "dbo.PublishingHouse");
            DropPrimaryKey("dbo.Author");
            DropPrimaryKey("dbo.PublishingHouse");
            DropIndex("dbo.Book", new[] { "Author_ID" });
            DropIndex("dbo.Book", new[] { "PublishingHouse_Id" });
            DropColumn("dbo.Author", "ID");
            DropColumn("dbo.PublishingHouse", "Id");
            RenameColumn(table: "dbo.Book", name: "Author_ID", newName: "AuthorID");
            RenameColumn(table: "dbo.Book", name: "PublishingHouse_Id", newName: "publishingHouseID");
            AddColumn("dbo.Author", "AuthorID", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.PublishingHouse", "publishingHouseID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Book", "AuthorID", c => c.Int(nullable: false));
            AlterColumn("dbo.Book", "publishingHouseID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Author", "AuthorID");
            AddPrimaryKey("dbo.PublishingHouse", "publishingHouseID");
            CreateIndex("dbo.Book", "AuthorID");
            CreateIndex("dbo.Book", "publishingHouseID");
            AddForeignKey("dbo.Book", "AuthorID", "dbo.Author", "AuthorID", cascadeDelete: true);
            AddForeignKey("dbo.Book", "publishingHouseID", "dbo.PublishingHouse", "publishingHouseID", cascadeDelete: true);

        }
        
        public override void Down()
        {
            AddColumn("dbo.PublishingHouse", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Author", "ID", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Book", "publishingHouseID", "dbo.PublishingHouse");
            DropForeignKey("dbo.Book", "AuthorID", "dbo.Author");
            DropIndex("dbo.Book", new[] { "publishingHouseID" });
            DropIndex("dbo.Book", new[] { "AuthorID" });
            DropPrimaryKey("dbo.PublishingHouse");
            DropPrimaryKey("dbo.Author");
            AlterColumn("dbo.Book", "publishingHouseID", c => c.Int());
            AlterColumn("dbo.Book", "AuthorID", c => c.Int());
            DropColumn("dbo.PublishingHouse", "publishingHouseID");
            DropColumn("dbo.Author", "AuthorID");
            AddPrimaryKey("dbo.PublishingHouse", "Id");
            AddPrimaryKey("dbo.Author", "ID");
            RenameColumn(table: "dbo.Book", name: "publishingHouseID", newName: "PublishingHouse_Id");
            RenameColumn(table: "dbo.Book", name: "AuthorID", newName: "Author_ID");
            CreateIndex("dbo.Book", "PublishingHouse_Id");
            CreateIndex("dbo.Book", "Author_ID");
            AddForeignKey("dbo.Book", "PublishingHouse_Id", "dbo.PublishingHouse", "Id");
            AddForeignKey("dbo.Book", "Author_ID", "dbo.Author", "ID");
        }
    }
}
