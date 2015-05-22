namespace SimSo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Alter_Table : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NetWork", "Tags", c => c.String(maxLength: 1000, unicode: false));
            AddColumn("dbo.SimType", "Title", c => c.String(maxLength: 200));
            AddColumn("dbo.SimType", "Description", c => c.String(maxLength: 1000));
            AlterColumn("dbo.NetWork", "Title", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.NetWork", "Title", c => c.String(maxLength: 200, unicode: false));
            DropColumn("dbo.SimType", "Description");
            DropColumn("dbo.SimType", "Title");
            DropColumn("dbo.NetWork", "Tags");
        }
    }
}
