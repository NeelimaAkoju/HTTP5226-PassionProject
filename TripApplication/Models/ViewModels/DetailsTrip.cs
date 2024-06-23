using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TripApplication.Models.ViewModels
{
    public class DetailsTrip
    {
        public TripDto Trip { get; set; }
        public IEnumerable<PlaceDto> IncludedPlaces { get; set; }
    }
}