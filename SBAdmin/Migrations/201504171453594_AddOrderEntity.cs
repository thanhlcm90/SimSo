namespace SBAdmin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrderEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SIM_ID = c.Int(),
                        FullName = c.String(maxLength: 150),
                        Address = c.String(maxLength: 250),
                        Province = c.Int(),
                        Mobile = c.String(maxLength: 50),
                        Email = c.String(maxLength: 50, unicode: false),
                        Description = c.String(maxLength: 850),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UserBussiness = c.String(maxLength: 20, unicode: false),
                        CreateBy = c.String(maxLength: 50),
                        CreateDate = c.DateTime(),
                        UpdateBy = c.String(maxLength: 50),
                        LastUpdate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Order");
        }
    }
}
