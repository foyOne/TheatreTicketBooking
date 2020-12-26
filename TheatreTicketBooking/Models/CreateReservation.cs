using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheatreTicketBooking.Models
{
    public class CreateReservation
    {
        [FromForm]
        public int EventId { get; set; }
        [FromForm]
        public string Email { get; set; }
        [FromForm]
        public string Name { get; set; }
        public string Level1 { get; set; }
        public string Level2 { get; set; }
        public string Level3 { get; set; }
    }
}
