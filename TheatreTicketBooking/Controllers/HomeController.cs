using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TheatreTicketBooking.Models;
using TheatreTicketBooking.DAL;

namespace TheatreTicketBooking.Controllers
{
    public class HomeController : Controller
    {
        TTBContext db;

        public HomeController(TTBContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            var shows = db.Shows.Select(s => s).ToList();
            ViewBag.shows = shows;
            return View("Index");
        }

        public IActionResult ShowDates(int id)
        {
            var show = db.Shows.FirstOrDefault(f => f.Id == id);
            var dates = db.Events.Where(w => w.ShowId == id).Select(s => s.StartTime.ToString("dd/MM/yyyy HH:mm")).ToList();
            ViewBag.dates = dates;
            ViewBag.show = show;
            return View("Show");
        }

        public IActionResult ShowSeats(string Start)
        {
            var date = Convert.ToDateTime(Start);
            var e = db.Events.FirstOrDefault(f => f.StartTime == date);
            var seats = db.Seats.Where(s => s.EventId == e.Id).ToList();
            ViewBag.eventId = e.Id;
            ViewBag.seats = seats;
            return View("Seats");
        }

        [HttpPost]
        public IActionResult Reservation(CreateReservation model)
        {
            Reservation res = new Reservation();
            res.Email = model.Email;
            res.Name = model.Name;
            res.EventId = model.EventId;
            res.Event = db.Events.FirstOrDefault(f => f.Id == model.EventId);

            db.Reservations.Add(res);
            db.SaveChanges();
            res = db.Reservations.FirstOrDefault(f => f.Email == model.Email && f.Name == model.Name);

            var seatsOnEvent = db.Seats.Where(s => s.EventId == model.EventId).ToList();
            var dict = new Dictionary<int, List<int>>();
            dict.Add(1, model.Level1 != null ? model.Level1.Split(",").Select(s => Convert.ToInt32(s)).ToList() : new List<int>());
            dict.Add(2, model.Level2 != null ? model.Level2.Split(",").Select(s => Convert.ToInt32(s)).ToList() : new List<int>());
            dict.Add(3, model.Level3 != null ? model.Level3.Split(",").Select(s => Convert.ToInt32(s)).ToList() : new List<int>());

            seatsOnEvent.ForEach(e =>
            {
                if (dict[e.Level].Contains(e.SeatNumber))
                {
                    e.Reservation = res;
                    e.ReservationId = res.Id;
                    e.Status = false;
                }
            });

            db.Seats.UpdateRange(seatsOnEvent);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
