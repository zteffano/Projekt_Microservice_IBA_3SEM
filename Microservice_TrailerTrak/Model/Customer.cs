using Microsoft.EntityFrameworkCore;

namespace Microservice_TrailerTrak.Model
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }

        // Navigation properties => For at vi også kan gå fra Customer til Booking
		public virtual ICollection<Booking> Bookings { get; set; }

		public Customer()
		{
			// uden parametre da vi arbejder med EF og det er "den måde at gøre det på"
		}
	}



}
