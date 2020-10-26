using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Models
{
    public enum OrderStatus
    {
        New,
        Pending,
        Shipped
    }

    public class Order
    {
        [BindNever]
        public int OrderID { get; set; }
        [BindNever]
        public virtual ICollection<CartLine> Lines { get; set; }

        [BindNever]
        public OrderStatus Status { get; set; }

        [Required(ErrorMessage = "Please enter a first name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter a last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter the street name")]
        public string StreetName { get; set; }
        [Required(ErrorMessage = "Please enter the house number")]
        public string HouseNumber { get; set; }

        [Required(ErrorMessage = "Please enter a city name")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please enter a zip code")]
        public string Zip { get; set; }

        [Required(ErrorMessage = "Please enter a country")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Please enter a phone number")]
        public int Phone { get; set; }

        [Required(ErrorMessage = "Please enter a email address")]
        public string Email { get; set; }
    }
}
