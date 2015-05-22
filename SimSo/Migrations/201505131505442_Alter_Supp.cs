namespace SimSo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Alter_Supp : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Supplier", "UserName", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AlterColumn("dbo.Supplier", "Password", c => c.String(nullable: false, maxLength: 100, unicode: false));
            AlterColumn("dbo.Supplier", "Name", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Supplier", "Name", c => c.String(maxLength: 50));
            AlterColumn("dbo.Supplier", "Password", c => c.String(maxLength: 50, unicode: false));
            AlterColumn("dbo.Supplier", "UserName", c => c.String(maxLength: 50, unicode: false));
        }
    }
}
