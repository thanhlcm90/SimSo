namespace SimSo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Alter_Menu1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Menu", "isShow", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Menu", "isShow");
        }
    }
}
