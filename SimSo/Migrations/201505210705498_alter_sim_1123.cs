namespace SimSo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alter_sim_1123 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SIM", "Price_Sup", c => c.Decimal(storeType: "money"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SIM", "Price_Sup");
        }
    }
}
