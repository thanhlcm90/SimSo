namespace SBAdmin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Table_New : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.New",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IDMenu = c.Int(nullable: false),
                        Title = c.String(maxLength: 250),
                        ShortDes = c.String(maxLength: 350),
                        Content = c.String(storeType: "ntext"),
                        Image = c.String(maxLength: 200),
                        isActive = c.Boolean(),
                        isDelete = c.Boolean(),
                        CreateBy = c.String(maxLength: 30),
                        CreateDate = c.DateTime(),
                        UpdateBy = c.String(maxLength: 30),
                        LastUpdate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Menu", "Type", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Menu", "Type");
            DropTable("dbo.New");
        }
    }
}
