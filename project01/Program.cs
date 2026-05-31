
using Microsoft.EntityFrameworkCore;
using project01;
using project01.Models;

namespace MetroTicketBookingSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using AppDbContext context = new AppDbContext();

            // Seed data only if database is empty
            if (!context.Stations.Any())
            {
                Station s1 = new Station
                {
                    Name = "Ramses",
                    Location = "Cairo"
                };

                Station s2 = new Station
                {
                    Name = "Giza",
                    Location = "Giza"
                };

                Train t1 = new Train
                {
                    Number = "T1",
                    Capacity = 200
                };

                Train t2 = new Train
                {
                    Number = "T2",
                    Capacity = 150
                };

                context.Stations.AddRange(s1, s2);
                context.Trains.AddRange(t1, t2);
                context.SaveChanges();

                Ticket ticket1 = new Ticket
                {
                    PassengerName = "Ahmed",
                    Price = 25,
                    TravelDate = DateTime.Now,
                    TrainId = t1.Id,
                    StationId = s1.Id
                };

                Ticket ticket2 = new Ticket
                {
                    PassengerName = "Sara",
                    Price = 15,
                    TravelDate = DateTime.Now,
                    TrainId = t1.Id,
                    StationId = s2.Id
                };

                Ticket ticket3 = new Ticket
                {
                    PassengerName = "Omar",
                    Price = 30,
                    TravelDate = DateTime.Now,
                    TrainId = t2.Id,
                    StationId = s1.Id
                };

                Ticket ticket4 = new Ticket
                {
                    PassengerName = "Mona",
                    Price = 22,
                    TravelDate = DateTime.Now,
                    TrainId = t2.Id,
                    StationId = s2.Id
                };

                Ticket ticket5 = new Ticket
                {
                    PassengerName = "Ali",
                    Price = 18,
                    TravelDate = DateTime.Now,
                    TrainId = t1.Id,
                    StationId = s1.Id
                };

                context.Tickets.AddRange(ticket1, ticket2, ticket3, ticket4, ticket5);
                context.SaveChanges();
            }

            // 1. Get all tickets
            Console.WriteLine("All Tickets:");
            var allTickets = context.Tickets.ToList();

            foreach (var ticket in allTickets)
            {
                Console.WriteLine($"{ticket.PassengerName} - {ticket.Price}");
            }

            // 2. Get tickets with price greater than 20
            Console.WriteLine("\nTickets Price Greater Than 20:");
            var expensiveTickets = context.Tickets
                .Where(t => t.Price > 20)
                .ToList();

            foreach (var ticket in expensiveTickets)
            {
                Console.WriteLine($"{ticket.PassengerName} - {ticket.Price}");
            }

            // 3. Get the first ticket for a specific passenger
            Console.WriteLine("\nFirst Ticket for Ahmed:");
            var firstTicket = context.Tickets
                .FirstOrDefault(t => t.PassengerName == "Ahmed");

            if (firstTicket != null)
            {
                Console.WriteLine($"{firstTicket.PassengerName} - {firstTicket.Price}");
            }

            // 4. Count all tickets
            Console.WriteLine("\nTotal Tickets:");
            int ticketCount = context.Tickets.Count();
            Console.WriteLine(ticketCount);

            // 5. Order tickets by price descending
            Console.WriteLine("\nTickets Ordered By Price Descending:");
            var orderedTickets = context.Tickets
                .OrderByDescending(t => t.Price)
                .ToList();

            foreach (var ticket in orderedTickets)
            {
                Console.WriteLine($"{ticket.PassengerName} - {ticket.Price}");
            }

            // 6. Load related data using Include
            Console.WriteLine("\nTickets with Train and Station:");
            var ticketsWithDetails = context.Tickets
                .Include(t => t.Train)
                .Include(t => t.Station)
                .ToList();

            foreach (var ticket in ticketsWithDetails)
            {
                Console.WriteLine($"Passenger: {ticket.PassengerName}");
                Console.WriteLine($"Train: {ticket.Train.Number}");
                Console.WriteLine($"Station: {ticket.Station.Name}");
                Console.WriteLine($"Price: {ticket.Price}");
                Console.WriteLine("-------------------");
            }

            // Bonus 1. Number of tickets for each train
            Console.WriteLine("\nNumber of Tickets for Each Train:");
            var ticketsPerTrain = context.Trains
                .Include(t => t.Tickets)
                .Select(t => new
                {
                    TrainNumber = t.Number,
                    TicketCount = t.Tickets.Count
                })
                .ToList();

            foreach (var item in ticketsPerTrain)
            {
                Console.WriteLine($"Train: {item.TrainNumber}, Tickets: {item.TicketCount}");
            }

            // Bonus 2. Cheapest ticket
            Console.WriteLine("\nCheapest Ticket:");
            var cheapestTicket = context.Tickets
                .OrderBy(t => t.Price)
                .FirstOrDefault();

            if (cheapestTicket != null)
            {
                Console.WriteLine($"{cheapestTicket.PassengerName} - {cheapestTicket.Price}");
            }

            // Bonus 3. Most expensive ticket
            Console.WriteLine("\nMost Expensive Ticket:");
            var mostExpensiveTicket = context.Tickets
                .OrderByDescending(t => t.Price)
                .FirstOrDefault();

            if (mostExpensiveTicket != null)
            {
                Console.WriteLine($"{mostExpensiveTicket.PassengerName} - {mostExpensiveTicket.Price}");
            }
        }
    }
}