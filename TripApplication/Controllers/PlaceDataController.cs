using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TripApplication.Migrations;
using TripApplication.Models;
using System.Web.Http.Description;
using System.Diagnostics;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Web;

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
        [Route("api/PlaceData/ListCities")]
        public IEnumerable<PlaceDto> ListCities()
        {
            List<Place> Places = db.Places.ToList();
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
        [Route("api/PlaceData/FindPlaces/{id}")]
        public IHttpActionResult FindPlaces(int id)
        {
            Place Place = db.Places.Find(id);
            Debug.WriteLine(Place);
            PlaceDto PlaceDto = new PlaceDto()
            {
                PlaceId = Place.PlaceId,
                PlaceName = Place.PlaceName,
                PlaceCity = Place.PlaceCity,
                PlaceDescription = Place.PlaceDescription
            };
            if (Place == null)
            {
                return NotFound();
            }

            return Ok(PlaceDto);
        }

        [HttpGet]
        [ResponseType(typeof(PlaceDto))]
        [Route("api/PlaceData/ListPlacesForTrip/{id}")]
        public IHttpActionResult ListPlacesForTrip(int id)
        {

            List<Place> Places = db.Places.Where(
                p => p.Trips.Any(
                    t => t.TripID == id)
                ).ToList();
            List<PlaceDto> PlaceDtos = new List<PlaceDto>();

            Places.ForEach(p => PlaceDtos.Add(new PlaceDto()
            {
                PlaceName = p.PlaceName,
                PlaceCity = p.PlaceCity,
                PlaceDescription = p.PlaceDescription
            }));

            return Ok(PlaceDtos);
        }

        [ResponseType(typeof(Place))]
        [HttpPost]
        public IHttpActionResult AddPlace(Place Place)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Places.Add(Place);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = Place.PlaceId }, Place);
        }

        [ResponseType(typeof(void))]
        [Route("api/PlaceData/UpdatePlace/{id}")]
        [HttpPost]
        public IHttpActionResult UpdatePlace(int id, Place Place)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Place.PlaceId)
            {

                return BadRequest();
            }

            db.Entry(Place).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlaceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        [ResponseType(typeof(Place))]
        [Route("api/PlaceData/DeletePlace/{id}")]
        [HttpPost]
        public IHttpActionResult DeletePlace(int id)
        {
            Place place = db.Places.Find(id);
            if (place == null)
            {
                return NotFound();
            }

            db.Places.Remove(place);
            db.SaveChanges();

            return Ok();
        }

        private bool PlaceExists(int id)
        {
            // Returns true if an artwork with the specified ID exists in the database.
            return db.Places.Count(e => e.PlaceId == id) > 0;
        }
    }
}
