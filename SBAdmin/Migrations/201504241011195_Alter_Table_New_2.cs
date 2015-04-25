namespace SBAdmin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Alter_Table_New_2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.New", "isDeleted", c => c.Boolean());
            DropColumn("dbo.New", "Image");
            DropColumn("dbo.New", "isDelete");
        }
        
        public override void Down()
        {
            AddColumn("dbo.New", "isDelete", c => c.Boolean());
            AddColumn("dbo.New", "Image", c => c.String(maxLength: 200));
            DropColumn("dbo.New", "isDeleted");
        }
    }
}
