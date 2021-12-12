namespace Library.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addSubcategory2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Subcategories", "Category_CategoryID", "dbo.Categories");
            DropIndex("dbo.Subcategories", new[] { "Category_CategoryID" });
            RenameColumn(table: "dbo.Subcategories", name: "Category_CategoryID", newName: "CategoryID");
            AlterColumn("dbo.Subcategories", "CategoryID", c => c.Int(nullable: false));
            CreateIndex("dbo.Subcategories", "CategoryID");
            AddForeignKey("dbo.Subcategories", "CategoryID", "dbo.Categories", "CategoryID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Subcategories", "CategoryID", "dbo.Categories");
            DropIndex("dbo.Subcategories", new[] { "CategoryID" });
            AlterColumn("dbo.Subcategories", "CategoryID", c => c.Int());
            RenameColumn(table: "dbo.Subcategories", name: "CategoryID", newName: "Category_CategoryID");
            CreateIndex("dbo.Subcategories", "Category_CategoryID");
            AddForeignKey("dbo.Subcategories", "Category_CategoryID", "dbo.Categories", "CategoryID");
        }
    }
}
