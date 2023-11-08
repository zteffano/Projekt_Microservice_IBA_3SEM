using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Cryptography.X509Certificates;

namespace Microservice_TrailerTrak.Model
{

    // Trailer Model udkast

    public class Trailer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public string Brand { get; set; }
        public int Weight { get; set; }
        public int TotalWeight { get; set; }
        public int CarryWeight { get; set; }
        public string Type { get; set; }
        public string LicensePlate { get; set; }
        public int DayPrice { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }

        // Navigation properties => For at vi også kan gå fra Trailer til Booking
		public virtual ICollection<Booking> Bookings { get; set; }

		public Trailer()
        {
			// uden parametre da vi arbejder med EF og det er "den måde at gøre det på"
		}

	}

}
