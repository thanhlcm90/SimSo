namespace SBAdmin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Table_Menu : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Menu",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Stand = c.Int(),
                        isActive = c.Boolean(),
                        isDeleted = c.Boolean(),
                        CreateBy = c.String(maxLength: 50),
                        CreateDate = c.DateTime(),
                        UpdateBy = c.String(maxLength: 50),
                        LastUpdate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Menu");
        }
    }
}
