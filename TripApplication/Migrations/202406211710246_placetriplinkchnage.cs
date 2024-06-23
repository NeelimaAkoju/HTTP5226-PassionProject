namespace TripApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class placetriplinkchnage : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Places", "Trip_TripID", "dbo.Trips");
            DropIndex("dbo.Places", new[] { "Trip_TripID" });
            CreateTable(
                "dbo.TripPlaces",
                c => new
                    {
                        Trip_TripID = c.Int(nullable: false),
                        Place_PlaceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Trip_TripID, t.Place_PlaceId })
                .ForeignKey("dbo.Trips", t => t.Trip_TripID, cascadeDelete: true)
                .ForeignKey("dbo.Places", t => t.Place_PlaceId, cascadeDelete: true)
                .Index(t => t.Trip_TripID)
                .Index(t => t.Place_PlaceId);
            
            DropColumn("dbo.Places", "Trip_TripID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Places", "Trip_TripID", c => c.Int());
            DropForeignKey("dbo.TripPlaces", "Place_PlaceId", "dbo.Places");
            DropForeignKey("dbo.TripPlaces", "Trip_TripID", "dbo.Trips");
            DropIndex("dbo.TripPlaces", new[] { "Place_PlaceId" });
            DropIndex("dbo.TripPlaces", new[] { "Trip_TripID" });
            DropTable("dbo.TripPlaces");
            CreateIndex("dbo.Places", "Trip_TripID");
            AddForeignKey("dbo.Places", "Trip_TripID", "dbo.Trips", "TripID");
        }
    }
}
