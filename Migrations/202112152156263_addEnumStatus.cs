namespace Library.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addEnumStatus : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "status", c => c.String());
        }
    }
}
