namespace SBAdmin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmployeeDeleteImagesField : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Employee", "images");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employee", "images", c => c.Binary(storeType: "image"));
        }
    }
}
