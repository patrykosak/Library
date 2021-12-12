namespace Library.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addSubcategory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Subcategories",
                c => new
                    {
                        SubcategoryID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 500),
                        Category_CategoryID = c.Int(),
                    })
                .PrimaryKey(t => t.SubcategoryID)
                .ForeignKey("dbo.Categories", t => t.Category_CategoryID)
                .Index(t => t.Category_CategoryID);
            
            AlterColumn("dbo.Categories", "Name", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Subcategories", "Category_CategoryID", "dbo.Categories");
            DropIndex("dbo.Subcategories", new[] { "Category_CategoryID" });
            AlterColumn("dbo.Categories", "Name", c => c.String(nullable: false, maxLength: 500));
            DropTable("dbo.Subcategories");
        }
    }
}
