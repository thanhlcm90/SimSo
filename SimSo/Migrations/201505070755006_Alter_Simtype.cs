namespace SimSo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Alter_Simtype : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.SimType", "Title");
            DropColumn("dbo.SimType", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SimType", "Description", c => c.String(maxLength: 1000));
            AddColumn("dbo.SimType", "Title", c => c.String(maxLength: 200, unicode: false));
        }
    }
}
