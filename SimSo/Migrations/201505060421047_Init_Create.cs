namespace SimSo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init_Create : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employee",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserName = c.String(maxLength: 256),
                        FullName = c.String(maxLength: 50),
                        Gender = c.String(maxLength: 5),
                        BirthDay = c.DateTime(),
                        Address = c.String(maxLength: 150),
                        Mobile = c.String(maxLength: 50, unicode: false),
                        Email = c.String(maxLength: 50),
                        image = c.String(maxLength: 200),
                        isActive = c.Boolean(),
                        isDeleted = c.Boolean(),
                        CreateBy = c.String(maxLength: 50, unicode: false),
                        CreateDate = c.DateTime(),
                        UpdateBy = c.String(maxLength: 50, unicode: false),
                        LastUpdate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Menu",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Stand = c.Int(),
                        isActive = c.Boolean(),
                        Type = c.Int(nullable: false),
                        isDeleted = c.Boolean(),
                        CreateBy = c.String(maxLength: 50),
                        CreateDate = c.DateTime(),
                        UpdateBy = c.String(maxLength: 50),
                        LastUpdate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.NetWork",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Number = c.String(nullable: false, maxLength: 100, unicode: false),
                        image = c.String(maxLength: 200, unicode: false),
                        isActive = c.Boolean(),
                        isDeleted = c.Boolean(),
                        CreateBy = c.String(maxLength: 50, unicode: false),
                        CreateDate = c.DateTime(),
                        UpdateBy = c.String(maxLength: 50, unicode: false),
                        LastUpdate = c.DateTime(),
                        Title = c.String(maxLength: 200, unicode: false),
                        Description = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.New",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IDMenu = c.Int(),
                        Title = c.String(maxLength: 250),
                        ShortDes = c.String(maxLength: 350),
                        Content = c.String(storeType: "ntext"),
                        image = c.String(maxLength: 200),
                        isActive = c.Boolean(),
                        isDeleted = c.Boolean(),
                        CreateBy = c.String(maxLength: 30),
                        CreateDate = c.DateTime(),
                        UpdateBy = c.String(maxLength: 30),
                        LastUpdate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SIM_ID = c.Int(),
                        FullName = c.String(maxLength: 150),
                        Address = c.String(maxLength: 250),
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
            
            CreateTable(
                "dbo.SIM",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Number = c.String(maxLength: 50, unicode: false),
                        NetWork_ID = c.Int(),
                        SimType_ID = c.Int(),
                        Supplier_ID = c.Int(),
                        Price = c.Decimal(storeType: "money"),
                        Status = c.Int(),
                        isActive = c.Boolean(),
                        isDeleted = c.Boolean(),
                        CreateBy = c.String(maxLength: 50, unicode: false),
                        CreateDate = c.DateTime(),
                        UpdateBy = c.String(maxLength: 50, unicode: false),
                        LastUpdate = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SimType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IDParent = c.Int(),
                        Name = c.String(maxLength: 150),
                        Condition = c.String(maxLength: 1000, unicode: false),
                        isActive = c.Boolean(),
                        isDeleted = c.Boolean(),
                        Title = c.String(maxLength: 200, unicode: false),
                        Description = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Supplier",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserName = c.String(maxLength: 50, unicode: false),
                        Password = c.String(maxLength: 50, unicode: false),
                        Name = c.String(maxLength: 50),
                        Mobile = c.String(maxLength: 20, unicode: false),
                        Email = c.String(maxLength: 50, unicode: false),
                        Address = c.String(maxLength: 250),
                        isActive = c.Boolean(),
                        isDeleted = c.Boolean(),
                        CreateBy = c.String(maxLength: 50, unicode: false),
                        CreateDate = c.DateTime(),
                        UpdateBy = c.String(maxLength: 50, unicode: false),
                        LastUpdate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Supplier");
            DropTable("dbo.SimType");
            DropTable("dbo.SIM");
            DropTable("dbo.Order");
            DropTable("dbo.New");
            DropTable("dbo.NetWork");
            DropTable("dbo.Menu");
            DropTable("dbo.Employee");
        }
    }
}
