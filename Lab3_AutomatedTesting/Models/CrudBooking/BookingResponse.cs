using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3_AutomatedTesting.Models.CrudBooking
{
    public class BookingResponse
    {
        public int Bookingid { get; set; }
        public Booking Booking { get; set; }
    }
}
