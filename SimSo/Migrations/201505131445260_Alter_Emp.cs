namespace SimSo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Alter_Emp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employee", "Password", c => c.String(nullable: false, maxLength: 100, unicode: false));
            AlterColumn("dbo.Employee", "UserName", c => c.String(nullable: false, maxLength: 256));
            AlterColumn("dbo.Employee", "FullName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Employee", "Gender", c => c.String(nullable: false, maxLength: 5));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employee", "Gender", c => c.String(maxLength: 5));
            AlterColumn("dbo.Employee", "FullName", c => c.String(maxLength: 50));
            AlterColumn("dbo.Employee", "UserName", c => c.String(maxLength: 256));
            DropColumn("dbo.Employee", "Password");
        }
    }
}
