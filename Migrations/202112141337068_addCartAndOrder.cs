namespace Library.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCartAndOrder : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        CartID = c.Int(nullable: false, identity: true),
                        userId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.CartID)
                .ForeignKey("dbo.AspNetUsers", t => t.userId)
                .Index(t => t.userId);
            
            CreateTable(
                "dbo.CartItems",
                c => new
                    {
                        ItemID = c.Int(nullable: false, identity: true),
                        CartID = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        ISBN = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ItemID)
                .ForeignKey("dbo.Books", t => t.ISBN, cascadeDelete: true)
                .ForeignKey("dbo.Carts", t => t.CartID, cascadeDelete: true)
                .Index(t => t.CartID)
                .Index(t => t.ISBN);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderID = c.Int(nullable: false, identity: true),
                        status = c.String(),
                        userId = c.String(maxLength: 128),
                        CartID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderID)
                .ForeignKey("dbo.Carts", t => t.CartID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.userId)
                .Index(t => t.userId)
                .Index(t => t.CartID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "userId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Orders", "CartID", "dbo.Carts");
            DropForeignKey("dbo.Carts", "userId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CartItems", "CartID", "dbo.Carts");
            DropForeignKey("dbo.CartItems", "ISBN", "dbo.Books");
            DropIndex("dbo.Orders", new[] { "CartID" });
            DropIndex("dbo.Orders", new[] { "userId" });
            DropIndex("dbo.CartItems", new[] { "ISBN" });
            DropIndex("dbo.CartItems", new[] { "CartID" });
            DropIndex("dbo.Carts", new[] { "userId" });
            DropTable("dbo.Orders");
            DropTable("dbo.CartItems");
            DropTable("dbo.Carts");
        }
    }
}
