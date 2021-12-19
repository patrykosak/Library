namespace Library.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addSubcategoryToBook : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "SubcategoryID", c => c.Int(nullable: false));
            CreateIndex("dbo.Books", "SubcategoryID");
            AddForeignKey("dbo.Books", "SubcategoryID", "dbo.Subcategories", "SubcategoryID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "SubcategoryID", "dbo.Subcategories");
            DropIndex("dbo.Books", new[] { "SubcategoryID" });
            DropColumn("dbo.Books", "SubcategoryID");
        }
    }
}
