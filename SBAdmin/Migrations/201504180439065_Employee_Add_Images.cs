namespace SBAdmin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Employee_Add_Images : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employee", "images", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employee", "images");
        }
    }
}
