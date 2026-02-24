using Hosptital.DAL.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptital.DAL.Entities
{
    public class Patient : BaseEntity
    {
        public DateOnly DateOfBirath { get; set; }
        public Address Address { get; set; }
        public ApplicationUser User { get; set; }
        public int UserId { get; set; }

        public Booking Booking { get; set; }
        public int BookingId { get; set; }

    }

    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
