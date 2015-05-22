namespace SimSo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alter_config : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Configs", newName: "Config");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Config", newName: "Configs");
        }
    }
}
