using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheatreTicketBooking.Models;

namespace TheatreTicketBooking.DAL
{
    public class TTBInitializer
    {
        public static void Initialize(TTBContext context)
        {
            if (!context.Shows.Any())
            {
                var shows = new List<Show>
                {
                    new Show { Title = "Hello wordl", Genre = "Drama", Description = "Empty", Price = 500, Time = 2 },
                    new Show { Title = "Andan", Genre = "Horror", Description = "Empty", Price = 1500, Time = 4 },
                    new Show { Title = "Who am I?", Genre = "Comedy", Description = "Empty", Price = 500, Time = 3 },
                    new Show { Title = "Genshin", Genre = "Comedy", Description = "Empty", Price = 300, Time = 2 },
                };
                context.Shows.AddRange(shows);
                context.SaveChanges();
            }

            if (!context.Events.Any())
            {

                var shows = context.Shows.Select(s => s).ToList();
                int min = shows.Select(s => s.Id).Min();
                int max = shows.Select(s => s.Id).Max();
                var events = new List<Event>();
                var random = new Random();
                int day = 15;
                for (int i = 0; i < 10; ++i)
                {
                    int id = random.Next(min, max + 1);
                    events.Add(new Event { StartTime = new DateTime(2020, 12, day++, 18, 30, 0), Show = shows.FirstOrDefault(f => f.Id == id), ShowId = id });
                }
                context.Events.AddRange(events);

                context.SaveChanges();
            }


            if (!context.Seats.Any())
            {
                var eventList = context.Events.Select(s => s).ToList();

                eventList.ForEach(e =>
                {
                    var seats = new List<Seat>();
                    for (int i = 0; i < 3; ++i)
                    {
                        for (int j = 0; j < 10; ++j)
                            seats.Add(new Seat { SeatNumber = j + 1, Level = i + 1, Status = true, Price = 100, EventId = e.Id, Event = e, ReservationId = null, Reservation = null });
                    }
                    context.Seats.AddRange(seats);
                });

                context.SaveChanges();
            }
        }
    }
}
