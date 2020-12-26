using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TheatreTicketBooking.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public int ShowId { get; set; }
        [ForeignKey("ShowId")]
        public Show Show { get; set; }
    
    }
}
