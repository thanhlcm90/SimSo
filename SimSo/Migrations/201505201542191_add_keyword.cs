namespace SimSo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_keyword : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employee", "Yahoo", c => c.String(maxLength: 100, unicode: false));
            AddColumn("dbo.Employee", "Skype", c => c.String(maxLength: 100, unicode: false));
            AddColumn("dbo.NetWork", "Keyword", c => c.String(maxLength: 1000));
            AddColumn("dbo.SimType", "Keyword", c => c.String(maxLength: 1000));
            DropColumn("dbo.Employee", "YahooAccount");
            DropColumn("dbo.Employee", "SkypeAccount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employee", "SkypeAccount", c => c.String(maxLength: 100, unicode: false));
            AddColumn("dbo.Employee", "YahooAccount", c => c.String(maxLength: 100, unicode: false));
            DropColumn("dbo.SimType", "Keyword");
            DropColumn("dbo.NetWork", "Keyword");
            DropColumn("dbo.Employee", "Skype");
            DropColumn("dbo.Employee", "Yahoo");
        }
    }
}
