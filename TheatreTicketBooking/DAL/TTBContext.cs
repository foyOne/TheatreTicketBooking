using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheatreTicketBooking.Models;

namespace TheatreTicketBooking.DAL
{
    public class TTBContext : DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Show> Shows { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Seat> Seats { get; set; }

        public TTBContext(DbContextOptions<TTBContext> options)
        : base(options)
        {

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if(optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseNpgsql(...)
        //    }
        //}

        //public TTBContext(DbContextOptions<TTBContext> options)
        //: base(options)
        //{
        //    //Database.EnsureDeleted();
        //    Database.EnsureCreated();
        //}
    }
}
