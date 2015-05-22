namespace SimSo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alter_employee_1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employee", "YahooAccount", c => c.String(maxLength: 100, unicode: false));
            AddColumn("dbo.Employee", "SkypeAccount", c => c.String(maxLength: 100, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employee", "SkypeAccount");
            DropColumn("dbo.Employee", "YahooAccount");
        }
    }
}
