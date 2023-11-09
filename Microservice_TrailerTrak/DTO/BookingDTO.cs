namespace Microservice_TrailerTrak.DTO
{
	public class BookingDTO
	{
		//public int Id { get; set; }
		public DateTime BookedFrom { get; set; }
		public DateTime BookedTo { get; set; }
		public bool Returned { get; set; }
		public bool Paid { get; set; }
		// Include navigation properties if needed
		public int CustomerId { get; set; }
		public int TrailerId { get; set; }
	}
}
