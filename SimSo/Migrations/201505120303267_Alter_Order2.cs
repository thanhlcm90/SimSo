namespace SimSo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Alter_Order2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Order", "Email", c => c.String(maxLength: 50, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Order", "Email", c => c.String(nullable: false, maxLength: 50, unicode: false));
        }
    }
}
