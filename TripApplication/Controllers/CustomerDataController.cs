using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TripApplication.Models;

namespace TripApplication.Controllers
{
    public class CustomerDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        [HttpGet]
        [Route("api/CustomerData/ListCustomers")]
        public IEnumerable<Customer> ListCustomers()
        {
            List<Customer> customers = db.Customers.ToList();
            return customers;
        }

        [ResponseType(typeof(Customer))]
        [HttpGet]
        [Route("api/CustomerData/FindCustomer/{id}")]
        public IHttpActionResult FindCustomer(int id)
        {
            Customer customer = db.Customers.Find(id);
            Debug.WriteLine(customer);
            return Ok(customer);
        }

    }
}
