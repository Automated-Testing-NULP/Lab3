using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3_AutomatedTesting.Models.CrudBooking
{
    public class Booking
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int Totalprice { get; set; }
        public bool Depositpaid { get; set; }
        public BookingDates Bookingdates { get; set; }
        public string Additionalneeds { get; set; }
    }
}
