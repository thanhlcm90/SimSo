namespace SimSo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Alter_Menu : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Menu", "Title", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Menu", "Title");
        }
    }
}
