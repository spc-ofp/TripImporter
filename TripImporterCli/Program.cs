

namespace TripImporterCli
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ImportLibrary;
    
    public class Program
    {
        static Program()
        {
            log4net.LogManager.GetLogger(typeof(Program));
        }
        
        public static void Main(string[] args)
        {
            // Initialize AutoMapper
            Console.WriteLine("Initializing AutoMapper...");
            AutoMapperConfiguration.Configure();
            Console.WriteLine("Done!");
            
            // TODO Add some argument processing for picking up a program
            string programCode = "KIOB";
            int limit = 20;
            var svc = new PendingTripService();
            Console.WriteLine("Retrieving pending trips for program '{0}'...", programCode);
            var trips = svc.GetPendingTrips(programCode);
            Console.WriteLine("There are {0} pending trip(s)", trips.Count);
            
            foreach (var trip in trips.Take(limit))
            {
                Console.WriteLine("Copying obstrip_id {0}...", trip.Id);
                var results = svc.CopyTrip(trip.Id);
                Console.WriteLine("Success? {0}\tMessage: {1}", results.Item1, results.Item2);
            }
        }
    }
}
