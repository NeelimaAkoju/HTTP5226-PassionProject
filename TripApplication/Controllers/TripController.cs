using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TripApplication.Models;
using TripApplication.Models.ViewModels;

namespace TripApplication.Controllers
{
    public class TripController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        // GET: Place
        static TripController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44344/api/");
        }

        //GET: Place/List
        public ActionResult List()
        {
            string url = "tripdata/ListTrips";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<TripDto> trips = response.Content.ReadAsAsync<IEnumerable<TripDto>>().Result;
            return View(trips);
        }

        // GET: Trip/Show/5
        public ActionResult Show(int id)
        {
            string url = "tripdata/FindTrip/" + id;
            DetailsTrip tripDeatails = new DetailsTrip();

            HttpResponseMessage response = client.GetAsync(url).Result;

            TripDto trip = response.Content.ReadAsAsync<TripDto>().Result;

            tripDeatails.Trip = trip;

            url = "PlaceData/ListPlacesForTrip/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<PlaceDto> placesInTrip = response.Content.ReadAsAsync<IEnumerable<PlaceDto>>().Result;
            tripDeatails.IncludedPlaces = placesInTrip;
            return View(tripDeatails);
        }

        // GET: Trip/New
        public ActionResult New()
        {
            string url = "PlaceData/ListCities";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<PlaceDto> places = response.Content.ReadAsAsync<IEnumerable<PlaceDto>>().Result;
            return View(places);
        }

       
        // POST: Trip/Create
        [HttpPost]
        public ActionResult Create(Trip trip, int[] selectedPlaces)
        {
            List<Place> place = new List<Place>();
            foreach (var placeId in selectedPlaces)
            {
                string url1 = "PlaceData/FindPlaces/" + placeId;
                HttpResponseMessage response1 = client.GetAsync(url1).Result;
                Place placeDto = response1.Content.ReadAsAsync<Place>().Result;
                place.Add(placeDto);
            }
            
            trip.Place = place;

            string url = "tripdata/AddTrip";
            string jsonPayload = jss.Serialize(trip);
            HttpContent content = new StringContent(jsonPayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }


        // GET: Trip/Edit/5
        public ActionResult Edit(int id)
        {
            string url = "tripdata/FindTrip/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            TripDto trip = response.Content.ReadAsAsync<TripDto>().Result;
            return View(trip);
        }

        // POST: Trip/Update/5
        [HttpPost]
        public ActionResult Update(int id, Trip trip)
        {
            string url = "tripdata/UpdateTrip/" + id;
            string jsonPayload = jss.Serialize(trip);
            HttpContent content = new StringContent(jsonPayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Trip/DeleteConfirm/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "tripdata/FindTrip/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            TripDto trip = response.Content.ReadAsAsync<TripDto>().Result;
            return View(trip);
        }

        // POST: Trip/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "TripData/DeleteTrip/" + id;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Trip/ListTripsForCustomer/5
        public ActionResult ListTripsForCustomer(int customerId)
        {
            string url = "tripdata/ListTripsForCustomer/" + customerId;
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<TripDto> trips = response.Content.ReadAsAsync<IEnumerable<TripDto>>().Result;
            return View(trips);
        }


        // POST: Trip/EditPlaceFromTrip
        [HttpPost]
        public ActionResult AddPlaceFromTrip(int tripId, int placeId)
        {
            string url = "tripdata/RemovePlaceFromTrip/" + tripId + "/" + placeId;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Show", new { id = tripId });
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        [HttpGet]
        public ActionResult EditPlaceFromTrip(int id)
        {
            string url = "tripdata/FindTrip/" + id;
            EditPlaceInTrip editPlaceInTrip = new EditPlaceInTrip();

            HttpResponseMessage response = client.GetAsync(url).Result;

            TripDto trip = response.Content.ReadAsAsync<TripDto>().Result;

            editPlaceInTrip.Trip = trip;

            url = "PlaceData/ListPlacesForTrip/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<PlaceDto> placesInTrip = response.Content.ReadAsAsync<IEnumerable<PlaceDto>>().Result;
            editPlaceInTrip.IncludedPlaces = placesInTrip;

            url = "PlaceData/ListCities";
            response = client.GetAsync(url).Result;
            IEnumerable<PlaceDto> places = response.Content.ReadAsAsync<IEnumerable<PlaceDto>>().Result;
            editPlaceInTrip.AllPlace = places;

            if (response.IsSuccessStatusCode)
            {
                return View(editPlaceInTrip);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

            public ActionResult Error()
        {
            return View();
        }
    }
}
