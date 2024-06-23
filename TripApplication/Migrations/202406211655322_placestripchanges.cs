namespace TripApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class placestripchanges : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PlaceTrips", "Place_PlaceId", "dbo.Places");
            DropForeignKey("dbo.PlaceTrips", "Trip_TripID", "dbo.Trips");
            DropIndex("dbo.PlaceTrips", new[] { "Place_PlaceId" });
            DropIndex("dbo.PlaceTrips", new[] { "Trip_TripID" });
            AddColumn("dbo.Places", "Trip_TripID", c => c.Int());
            CreateIndex("dbo.Places", "Trip_TripID");
            AddForeignKey("dbo.Places", "Trip_TripID", "dbo.Trips", "TripID");
            DropTable("dbo.PlaceTrips");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PlaceTrips",
                c => new
                    {
                        Place_PlaceId = c.Int(nullable: false),
                        Trip_TripID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Place_PlaceId, t.Trip_TripID });
            
            DropForeignKey("dbo.Places", "Trip_TripID", "dbo.Trips");
            DropIndex("dbo.Places", new[] { "Trip_TripID" });
            DropColumn("dbo.Places", "Trip_TripID");
            CreateIndex("dbo.PlaceTrips", "Trip_TripID");
            CreateIndex("dbo.PlaceTrips", "Place_PlaceId");
            AddForeignKey("dbo.PlaceTrips", "Trip_TripID", "dbo.Trips", "TripID", cascadeDelete: true);
            AddForeignKey("dbo.PlaceTrips", "Place_PlaceId", "dbo.Places", "PlaceId", cascadeDelete: true);
        }
    }
}
