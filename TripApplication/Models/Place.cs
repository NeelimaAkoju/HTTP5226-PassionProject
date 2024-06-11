using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TripApplication.Models
{
    public class Place
    {
        [Key]
        public int PlaceId { get; set; }
        public string PlaceName { get; set; }
        public string PlaceDescription { get; set; }
        public string PlaceCity { get; set; }

        //A place can belong to multiple trips
        public ICollection<Trip> Trips { get; set; }

    }

    public class PlaceDto
    {
        public int PlaceId { get; set; }
        public string PlaceName { get; set; }
        public string PlaceDescription { get; set; }
        public string PlaceCity { get; set; }
    }
}