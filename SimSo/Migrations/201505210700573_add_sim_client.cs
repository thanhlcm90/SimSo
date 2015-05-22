namespace SimSo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_sim_client : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SIM_CLIENT",
                c => new
                    {
                        Number = c.String(nullable: false, maxLength: 20),
                        NetWork_ID = c.Int(),
                        SimType_ID = c.Int(),
                        Supplier_ID = c.Int(),
                        Price = c.Decimal(storeType: "money"),
                    })
                .PrimaryKey(t => t.Number);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SIM_CLIENT");
        }
    }
}
