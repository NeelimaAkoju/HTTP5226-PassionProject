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
    public class CustomerController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        // GET: Place
        static CustomerController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44344/api/");
        }

        //GET: Customer/List
        public ActionResult List()
        {
            string url = "CustomerData/ListCustomers";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<Customer> customers = response.Content.ReadAsAsync<IEnumerable<Customer>>().Result;
            return View(customers);
        }

        //GET: Customer/Show/2
        public ActionResult Show(int id)
        {
            string url = "CustomerData/FindCustomer/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            Customer customer = response.Content.ReadAsAsync<Customer>().Result;
            return View(customer);
        }

    }
}