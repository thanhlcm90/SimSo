namespace SimSo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alter_employee : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employee", "STT", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employee", "STT");
        }
    }
}
