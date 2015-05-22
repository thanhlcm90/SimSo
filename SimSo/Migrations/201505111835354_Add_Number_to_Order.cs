namespace SimSo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Number_to_Order : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "Number", c => c.String(maxLength: 50, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Order", "Number");
        }
    }
}
