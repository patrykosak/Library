namespace Library.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addNameAndSurname : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Name", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "Surname", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Surname");
            DropColumn("dbo.AspNetUsers", "Name");
        }
    }
}
