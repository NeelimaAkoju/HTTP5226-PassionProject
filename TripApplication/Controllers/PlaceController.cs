using System;
using System.Collections.Generic;
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
            string url = "placedata/listplaces";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<PlaceDto> PlaceDtos = response.Content.ReadAsAsync<IEnumerable<PlaceDto>>().Result;
            return View(PlaceDtos);
        }

        //GET: Place/Show/2
        public ActionResult Show(int id)
        {
            string url = "findPlace/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            PlaceDto PlaceDto = response.Content.ReadAsAsync<PlaceDto>().Result;
            return View(PlaceDto);
        }
    }
}