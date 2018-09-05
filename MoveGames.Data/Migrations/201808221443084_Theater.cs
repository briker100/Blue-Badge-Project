namespace MoveGames.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Theater : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Theater",
                c => new
                    {
                        TheaterId = c.Int(nullable: false, identity: true),
                        OwnerId = c.Guid(nullable: false),
                        TheaterName = c.String(nullable: false),
                        TheaterLocation = c.String(nullable: false),
                        CreatedUtc = c.DateTimeOffset(nullable: false, precision: 7),
                        ModifiedUtc = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.TheaterId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Theater");
        }
    }
}
