using AutoMapper;
using Microservice_TrailerTrak.Contexts;
using Microservice_TrailerTrak.DTO;
using Microservice_TrailerTrak.Model;
using Microsoft.EntityFrameworkCore;

namespace Microservice_TrailerTrak.Endpoints
{
	//TODO: Mangler change endpoint & metode
	public static class BookingEndpoints
	{
		public static void MapBookingEndpoints(this WebApplication app)
		{
			app.MapPost("/createbooking", CreateBooking);
			app.MapGet("/booking/{id}", GetBookingById);
			app.MapGet("/bookings", GetAllBookings);
			//delete booking
			app.MapDelete("/deletebooking/{id:int}", DeleteBooking);
			
		
			// her kan tilføjes flere endpoints der mappes til metoder
		}

		private static async Task<IResult> CreateBooking(BookingDTO bookingDto, TrailerTrakContext context, IMapper mapper)
		{
			
			var booking = mapper.Map<Booking>(bookingDto);

			// Validering af data
			var customerExists = await context.Customers.AnyAsync(c => c.Id == bookingDto.CustomerId);
			if (!customerExists) return Results.NotFound("Customer not found");

			var trailerExists = await context.Trailers.AnyAsync(t => t.Id == bookingDto.TrailerId);
			if (!trailerExists) return Results.NotFound("Trailer not found");

			
			context.Bookings.Add(booking);
			await context.SaveChangesAsync();
			// En manuel måde at få id med retur, - kan også lave en ny DTO 
			var bookingToReturn = mapper.Map<Booking>(booking);
			bookingToReturn.Id = booking.Id;
			return Results.Created($"/bookings/{booking.Id}", bookingToReturn);
		}

		private static async Task<IResult> GetBookingById(int id, TrailerTrakContext context, IMapper mapper)
		{
			var booking = await context.Bookings
				.Include(b => b.CustomerData) 
				.Include(b => b.TrailerData) 
				.FirstOrDefaultAsync(b => b.Id == id);

			if (booking == null)
			{
				return Results.NotFound(new { Message = $"Booking with ID {id} not found." });
			}

			var bookingDto = mapper.Map<BookingDTO>(booking);
			return Results.Ok(bookingDto);
		}

		private static async Task<IResult> DeleteBooking(int id, TrailerTrakContext context)
		{
			var booking = await context.Bookings.FindAsync(id);
			if (booking == null)
			{
				return Results.NotFound("Booking not found.");
			}

			context.Bookings.Remove(booking);
			await context.SaveChangesAsync();

			return Results.NoContent(); // 204 No Content
		}

		private static async Task<IResult> GetAllBookings(TrailerTrakContext context, IMapper mapper)
		{
			var bookings = await context.Bookings.ToListAsync();
			var bookingDtos = mapper.Map<List<BookingDTO>>(bookings);
			return Results.Ok(bookingDtos);
		}

		// her kan tilføjes flere metoder til trailer
	}

}
