using Microservice_TrailerTrak.Model;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Microservice_TrailerTrak.Model
{
    // Booking Model 
    public class Booking
    {
        public int Id { get; set; }
        public DateTime BookedFrom { get; set; }
        public DateTime BookedTo { get; set; }
        public bool Returned { get; set; }
        public bool Paid { get; set; }
		// Foreign keys
		public int CustomerId { get; set; }
        public int TrailerId { get; set; }

		// Navigation properties
		public virtual Customer CustomerData { get; set; }
		public virtual Trailer TrailerData { get; set; }

        public Booking()
		{
            // uden parametre da vi arbejder med EF og det er "den måde at gøre det på"
		}

	}

}

