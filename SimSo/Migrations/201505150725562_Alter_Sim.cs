namespace SimSo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Alter_Sim : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SIM", "TrangThaiSim", c => c.String(maxLength: 200));
            AddColumn("dbo.SIM", "CamKet", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SIM", "CamKet");
            DropColumn("dbo.SIM", "TrangThaiSim");
        }
    }
}
