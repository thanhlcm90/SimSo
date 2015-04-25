namespace SBAdmin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Alter_Table_New : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.New", "IDMenu", c => c.Int());
            AlterColumn("dbo.New", "LastUpdate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.New", "LastUpdate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.New", "IDMenu", c => c.Int(nullable: false));
        }
    }
}
