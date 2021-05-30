namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Last : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BuyerName = c.String(nullable: false, maxLength: 25),
                        Address = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        TimeOfOrder = c.DateTime(nullable: false),
                        GameId = c.Int(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        CreatedOn = c.DateTime(),
                        UpdatedBy = c.Int(nullable: false),
                        UpdatedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.GameId, cascadeDelete: true)
                .Index(t => t.GameId);
            
            AlterColumn("dbo.Categories", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.Games", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "GameId", "dbo.Games");
            DropIndex("dbo.Orders", new[] { "GameId" });
            AlterColumn("dbo.Games", "Name", c => c.String());
            AlterColumn("dbo.Categories", "Title", c => c.String());
            DropTable("dbo.Orders");
        }
    }
}
