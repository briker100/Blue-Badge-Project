namespace MoveGames.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class work : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comment", "MovieName", c => c.String(nullable: false));
            DropColumn("dbo.Comment", "MovieId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comment", "MovieId", c => c.Int(nullable: false));
            DropColumn("dbo.Comment", "MovieName");
        }
    }
}
