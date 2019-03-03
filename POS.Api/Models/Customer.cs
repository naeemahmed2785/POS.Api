using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS.Api.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Town { get; set; }
        public string Postcode { get; set; }
    }
}
