namespace SimSo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Alter_Order : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Order", "SIM_ID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Order", "SIM_ID", c => c.Int());
        }
    }
}
