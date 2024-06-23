using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TripApplication.Models;
using TripApplication.Models.ViewModels;

namespace TripApplication.Controllers
{
    public class TripDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        [HttpGet]
        [Route("api/TripData/ListTrips")]
        public IEnumerable<TripDto> ListTrips()
        {
            List<Trip> trips = db.Trips.ToList();
            List<TripDto> tripDtos = new List<TripDto>();

            trips.ForEach(t => tripDtos.Add(new TripDto()
            {
                TripID = t.TripID,
                TripName = t.TripName,
                TripDescription = t.TripDescription,
                StartDate = t.StartDate,
                EndDate = t.EndDate,
                CustomerId = t.CustomerId,
                CustomerName = t.Customer.CustomerName
            }));

            return tripDtos;
        }

        // GET: api/TripData/FindTrip/5
        [ResponseType(typeof(TripDto))]
        [HttpGet]
        [Route("api/TripData/FindTrip/{id}")]
        public IHttpActionResult FindTrip(int id)
        {
            DetailsTrip tripDeatsils = new DetailsTrip();
            Trip trip = db.Trips.FirstOrDefault(t => t.TripID == id);
            if (trip == null)
            {
                return NotFound();
            }

            TripDto tripDto = new TripDto()
            {
                TripID = trip.TripID,
                TripName = trip.TripName,
                TripDescription = trip.TripDescription,
                StartDate = trip.StartDate,
                EndDate = trip.EndDate,
                CustomerId = trip.CustomerId,
                CustomerName = trip.Customer.CustomerName,

            };

            return Ok(tripDto);
        }


        [ResponseType(typeof(Trip))]
        [HttpPost]
        public IHttpActionResult AddTrip(Trip Trip)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Trips.Add(Trip);
            db.SaveChanges();

            return Ok();
        }

        [ResponseType(typeof(Trip))]
        [Route("api/TripData/DeleteTrip/{id}")]
        [HttpPost]
        public IHttpActionResult DeleteTrip(int id)
        {
            Trip trip = db.Trips.Find(id);
            if (trip == null)
            {
                return NotFound();
            }

            db.Trips.Remove(trip);
            db.SaveChanges();

            return Ok();
        }

        [ResponseType(typeof(void))]
        [Route("api/TripData/UpdateTrip/{id}")]
        [HttpPost]
        public IHttpActionResult UpdateTrip(int id, Trip Trip)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Trip.TripID)
            {

                return BadRequest();
            }

            db.Entry(Trip).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TripExists(id))
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

        [HttpPost]
        [Route("api/TripData/AddPlaceToTrip/{tripId}/{placeId}")]
        public IHttpActionResult AddPlaceToTrip(int tripId, int placeId)
        {
            Trip trip = db.Trips.Include(t => t.Place).FirstOrDefault(t => t.TripID == tripId);
            if (trip == null)
            {
                return NotFound();
            }

            Place place = db.Places.Find(placeId);
            if (place == null)
            {
                return NotFound();
            }

            if (!trip.Place.Any(p => p.PlaceId == placeId))
            {
                trip.Place.Add(place);
                db.SaveChanges();
            }

            return Ok();
        }

        [HttpPost]
        [Route("api/TripData/RemovePlaceFromTrip/{tripId}/{placeId}")]
        public IHttpActionResult RemovePlaceFromTrip(int tripId, int placeId)
        {
            Trip trip = db.Trips.Include(t => t.Place).FirstOrDefault(t => t.TripID == tripId);
            if (trip == null)
            {
                return NotFound();
            }

            Place place = db.Places.Find(placeId);
            if (place == null)
            {
                return NotFound();
            }

            if (trip.Place.Any(p => p.PlaceId == placeId))
            {
                trip.Place.Remove(place);
                db.SaveChanges();
            }

            return Ok();
        }

        [ResponseType(typeof(TripDto))]
        [HttpPost]
        [Route("api/TripData/TripsForCustomer/{customerId}/{customerId}")]
        public IHttpActionResult TripsForCustomer(int customerId)
        {
            List<Trip> trips = db.Trips
                 .Include(t => t.Customer)
                 .Where(t => t.CustomerId == customerId)
                 .ToList();
            List<TripDto> tripDtos = new List<TripDto>();

            trips.ForEach(t => tripDtos.Add(new TripDto()
            {
                TripID = t.TripID,
                TripName = t.TripName,
                TripDescription = t.TripDescription,
                StartDate = t.StartDate,
                EndDate = t.EndDate,
                CustomerId = t.CustomerId,
                CustomerName = t.Customer.CustomerName
            }));

            return Ok(tripDtos);
        }

        private bool TripExists(int id)
        {
            // Returns true if an artwork with the specified ID exists in the database.
            return db.Trips.Count(e => e.TripID == id) > 0;
        }
    }

   
}
