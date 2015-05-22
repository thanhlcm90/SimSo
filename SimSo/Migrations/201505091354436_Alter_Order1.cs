namespace SimSo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Alter_Order1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "UserBusiness", c => c.String(maxLength: 20, unicode: false));
            DropColumn("dbo.Order", "UserBussiness");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Order", "UserBussiness", c => c.String(maxLength: 20, unicode: false));
            DropColumn("dbo.Order", "UserBusiness");
        }
    }
}
