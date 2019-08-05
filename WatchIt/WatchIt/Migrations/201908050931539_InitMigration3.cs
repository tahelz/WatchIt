namespace WatchIt.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitMigration3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Customer", "PhoneNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customer", "PhoneNumber", c => c.String());
        }
    }
}
