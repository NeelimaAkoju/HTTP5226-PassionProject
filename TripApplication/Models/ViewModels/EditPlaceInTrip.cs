using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TripApplication.Models.ViewModels
{
    public class EditPlaceInTrip
    {
        public TripDto Trip { get; set; }
        public IEnumerable<PlaceDto> IncludedPlaces { get; set; }
        public IEnumerable<PlaceDto> AllPlace { get; set; }
    }
}