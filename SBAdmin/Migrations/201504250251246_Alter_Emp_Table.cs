namespace SBAdmin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Alter_Emp_Table : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employee", "image", c => c.String(maxLength: 200));
            DropColumn("dbo.Employee", "images");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employee", "images", c => c.String(maxLength: 200));
            DropColumn("dbo.Employee", "image");
        }
    }
}
