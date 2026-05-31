using Microsoft.EntityFrameworkCore;
using project01.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project01
{
    public class AppDbContext : DbContext
    {
        public DbSet<Train> Trains { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=.;Database=MetroTicketBookingDB;Trusted_Connection=True;TrustServerCertificate=True;"
            );
        }
    }
}
