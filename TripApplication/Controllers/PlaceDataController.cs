using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TripApplication.Migrations;
using TripApplication.Models;
using System.Web.Http.Description;

namespace TripApplication.Controllers
{
    public class PlaceDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        /// <summary>
        /// Returns all the Places in the db
        /// </summary>
        /// <returns>
        /// A list of places
        /// </returns>
        /// <example>
        /// GET: /api/PlaceData/ListPlaces
        /// </example>
        [HttpGet]
        public List<PlaceDto> ListPlaces()
        {
            List<Place> Places  = db.Places.ToList();
            List<PlaceDto> PlaceDtos = new List<PlaceDto>();
            Places.ForEach(p => PlaceDtos.Add(new PlaceDto()
            {
                PlaceId = p.PlaceId,
                PlaceName = p.PlaceName,
                PlaceDescription = p.PlaceDescription,
                PlaceCity = p.PlaceCity
            }));

            return PlaceDtos;
        }

        [ResponseType(typeof(PlaceDto))]
        [HttpGet]
        public IHttpActionResult FindPlace(int placeId)
        {
            Place Place = db.Places.Find(placeId);
            PlaceDto PlaceDto = new PlaceDto()
            {
                PlaceId = Place.PlaceId,
                PlaceName = Place.PlaceName,
                PlaceDescription = Place.PlaceDescription,
                PlaceCity = Place.PlaceCity
            };
            if (Place == null)
            {
                return NotFound();
            }
            return Ok(PlaceDto);
        }
    }
}
