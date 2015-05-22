namespace SimSo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Alter_Order_2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Order", "FullName", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.Order", "Address", c => c.String(nullable: false, maxLength: 250));
            AlterColumn("dbo.Order", "Email", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AlterColumn("dbo.Order", "Price", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Order", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Order", "Email", c => c.String(maxLength: 50, unicode: false));
            AlterColumn("dbo.Order", "Address", c => c.String(maxLength: 250));
            AlterColumn("dbo.Order", "FullName", c => c.String(maxLength: 150));
        }
    }
}
