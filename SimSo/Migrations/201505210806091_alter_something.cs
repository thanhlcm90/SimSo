namespace SimSo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alter_something : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "Price_Sup", c => c.Decimal(storeType: "money"));
            AddColumn("dbo.Order", "Sup_ID", c => c.Int());
            AddColumn("dbo.Order", "Network_ID", c => c.Int());
            AddColumn("dbo.Order", "SimType_ID", c => c.Int());
            AddColumn("dbo.SIM_CLIENT", "Price_Sup", c => c.Decimal(storeType: "money"));
            DropColumn("dbo.Order", "SIM_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Order", "SIM_ID", c => c.Int(nullable: false));
            DropColumn("dbo.SIM_CLIENT", "Price_Sup");
            DropColumn("dbo.Order", "SimType_ID");
            DropColumn("dbo.Order", "Network_ID");
            DropColumn("dbo.Order", "Sup_ID");
            DropColumn("dbo.Order", "Price_Sup");
        }
    }
}
