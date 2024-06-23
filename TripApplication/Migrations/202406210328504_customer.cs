namespace TripApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class customer : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.TripPlaces", newName: "PlaceTrips");
            DropPrimaryKey("dbo.PlaceTrips");
            AddPrimaryKey("dbo.PlaceTrips", new[] { "Place_PlaceId", "Trip_TripID" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.PlaceTrips");
            AddPrimaryKey("dbo.PlaceTrips", new[] { "Trip_TripID", "Place_PlaceId" });
            RenameTable(name: "dbo.PlaceTrips", newName: "TripPlaces");
        }
    }
}
