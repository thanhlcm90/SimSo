namespace SBAdmin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitCreate : DbMigration
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
                        images = c.Binary(storeType: "image"),
                        isActive = c.Boolean(),
                        isDeleted = c.Boolean(),
                        CreateBy = c.String(maxLength: 50, unicode: false),
                        CreateDate = c.DateTime(),
                        UpdateBy = c.String(maxLength: 50, unicode: false),
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
                        Status = c.Boolean(),
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
            DropTable("dbo.NetWork");
            DropTable("dbo.Employee");
        }
    }
}
