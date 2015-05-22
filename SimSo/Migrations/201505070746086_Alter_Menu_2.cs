namespace SimSo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Alter_Menu_2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Menu", "Title");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Menu", "Title", c => c.String(maxLength: 100));
        }
    }
}
