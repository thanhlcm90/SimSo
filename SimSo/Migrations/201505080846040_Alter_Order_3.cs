namespace SimSo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Alter_Order_3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Order", "Mobile", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Order", "Mobile", c => c.String(maxLength: 50));
        }
    }
}
