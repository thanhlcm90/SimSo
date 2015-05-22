namespace SimSo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Discount_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Discount",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SupID = c.Int(nullable: false),
                        PriceFrom = c.Decimal(precision: 18, scale: 2),
                        PriceTo = c.Decimal(precision: 18, scale: 2),
                        Discounts = c.Single(),
                        Description = c.String(maxLength: 500),
                        CreateBy = c.String(maxLength: 50),
                        CreateDate = c.DateTime(),
                        UpdateBy = c.String(maxLength: 50),
                        LastUpdate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Employee", "WorkingStatus", c => c.Boolean());
            AddColumn("dbo.Order", "Note", c => c.String(maxLength: 500));
            AddColumn("dbo.Order", "Status", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Order", "Status");
            DropColumn("dbo.Order", "Note");
            DropColumn("dbo.Employee", "WorkingStatus");
            DropTable("dbo.Discount");
        }
    }
}
