namespace SBAdmin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Change_Order_and_SIM_table : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SIM", "Status", c => c.Int());
            DropColumn("dbo.Order", "Province");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Order", "Province", c => c.Int());
            AlterColumn("dbo.SIM", "Status", c => c.Boolean());
        }
    }
}
