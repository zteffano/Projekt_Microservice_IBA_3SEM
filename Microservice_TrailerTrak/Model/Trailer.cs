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
        public int Weight { get; set; }  // KG
        public int TotalWeight { get; set; } // KG
        public int CarryWeight { get; set; } // KG
        public string Type { get; set; } // F.eks. hobby, bådtrailer, hestetrailer
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
