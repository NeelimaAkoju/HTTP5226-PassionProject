using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TripApplication.Models;

namespace TripApplication.Controllers
{
    public class PlaceController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();
            
        // GET: Place
        static PlaceController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44344/api/");
        }

        //GET: Place/List
        public ActionResult List()
        {
            string url = "placedata/ListCities";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<PlaceDto> places = response.Content.ReadAsAsync<IEnumerable<PlaceDto>>().Result;
            return View(places);
        }

        //GET: Place/Show/2
        public ActionResult Show(int id)
        {
            string url = "placedata/findPlaces/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            
           
                PlaceDto placeDtos = response.Content.ReadAsAsync<PlaceDto>().Result;
                return View(placeDtos);
           
        }

        public ActionResult New() { 
            return View();
        }


        // POST: Place/Create
        [HttpPost]

        public ActionResult Create(Place Place)
        {
            Debug.WriteLine("the json payload is :");

            string url = "placedata/addPlace";


            string jsonpayload = jss.Serialize(Place);

            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);


            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;

            Debug.WriteLine(response);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        //GET: Place/Edit/2
        public ActionResult Edit(int id)
        {
            string url = "placedata/findPlaces/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            PlaceDto placeDtos = response.Content.ReadAsAsync<PlaceDto>().Result;
            return View(placeDtos);
        }

        // POST: Place/Update/5
        [HttpPost]

        public ActionResult Update(int id, Place Place)
        {
            string url = "placedata/updatePlace/" +id;


            string jsonpayload = jss.Serialize(Place);

            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);


            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;

            Debug.WriteLine(response);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
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


        public ActionResult DeleteConfirm(int id)
        {
            string url = "placedata/findPlaces/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            PlaceDto selectedPlace = response.Content.ReadAsAsync<PlaceDto>().Result;
            return View(selectedPlace);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {

            string url = "placedata/deletePlace/" + id;
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
    }
}