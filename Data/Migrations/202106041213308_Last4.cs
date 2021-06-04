namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Last4 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Orders", "TimeOfOrder");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "TimeOfOrder", c => c.DateTime(nullable: false));
        }
    }
}
