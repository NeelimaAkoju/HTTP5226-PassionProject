using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TripApplication.Models
{
    public class Trip
    {
        [Key]
        public int TripID { get; set; }
        public string TripName { get; set; }
        public string TripDescription { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        //A trip can have mutiple places
        [ForeignKey("Place")]
        public ICollection<Place> Places { get; set; }

        //A trip belongs to one user
        //user can have multiple trips
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }
}