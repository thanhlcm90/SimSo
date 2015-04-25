namespace SBAdmin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Alter_Table_New_3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.New", "image", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            DropColumn("dbo.New", "image");
        }
    }
}
