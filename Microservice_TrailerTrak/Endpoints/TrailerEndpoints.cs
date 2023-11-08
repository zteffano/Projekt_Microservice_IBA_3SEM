using AutoMapper;
using Microservice_TrailerTrak.Contexts;
using Microservice_TrailerTrak.DTO;
using Microservice_TrailerTrak.Model;
using Microsoft.EntityFrameworkCore;

namespace Microservice_TrailerTrak.Endpoints
{

	//TODO: Mangler change endpoint & metode
	public static class TrailerEndpoints
	{
		public static void MapTrailerEndpoints(this WebApplication app)
		{
			app.MapGet("/trailers", GetAllTrailers);
			app.MapGet("/trailer/{id:int}", GetTrailerById);
			app.MapPost("/createtrailer", CreateTrailer);
			app.MapGet("/getprice/{id:int}/{days:int}", GetTrailerPrice);
			app.MapGet("/trailers/availables", GetAvailableTrailers);
			app.MapGet("/trailers/available/{id:int}", CheckTrailerAvailability);
			app.MapDelete("/deletetrailer/{id:int}", DeleteTrailer);
			// her kan tilføjes flere endpoints der mappes til metoder
		}

		private static async Task<IResult> GetAllTrailers(TrailerTrakContext context, IMapper mapper)
		{
			var trailers = await context.Trailers.ToListAsync();
			var trailerDtos = mapper.Map<List<TrailerDTO>>(trailers);
			return Results.Ok(trailerDtos);
		}

		private static async Task<IResult> GetTrailerById(int id, TrailerTrakContext context, IMapper mapper)
		{
			var trailer = await context.Trailers.FindAsync(id);
			if (trailer == null) return Results.NotFound("Trailer not found");
			var trailerDto = mapper.Map<TrailerDTO>(trailer);
			return Results.Ok(trailerDto);
		}

		private static async Task<IResult> CreateTrailer(TrailerDTO trailerDto, TrailerTrakContext context, IMapper mapper)
		{
			try
			{
				var trailer = mapper.Map<Trailer>(trailerDto);
				context.Trailers.Add(trailer);
				await context.SaveChangesAsync();

	
				var trailerToReturn = mapper.Map<Trailer>(trailer);

				trailerToReturn.Id = trailer.Id;

				// Returnere en 201 Created med en location header med den nye trailers data
				return Results.Created($"/trailer/{trailer.Id}", trailerToReturn);
			}
			catch (Exception ex)
			{
				return Results.Problem(ex.Message);
			}
		}


		private static async Task<IResult> GetTrailerPrice(int id, int days, TrailerTrakContext context)
		{
			var trailer = await context.Trailers.FindAsync(id);
			if (trailer == null)
			{
				return Results.NotFound($"Trailer with ID {id} not found.");
			}

			var price = trailer.DayPrice * days; // Vi henter DayPrice fra Trailer og ganger med antal dage
			return Results.Ok(new { TrailerId = id, Days = days, TotalPrice = price });
		}

		private static async Task<IResult> CheckTrailerAvailability(int id, TrailerTrakContext context)
		{
			// Find traileren baseret på ID
			var trailer = await context.Trailers.FindAsync(id);
			if (trailer == null)
			{
				return Results.NotFound($"Trailer with ID {id} not found.");
			}

			// Find ud af om traileren er booket og ikke returneret
			var bookings = await context.Bookings
										.Where(b => b.TrailerId == id &&
													!b.Returned &&
													b.BookedTo >= DateTime.UtcNow)
										.ToListAsync();
			bool isAvailable = !bookings.Any(); // Hvis der findes nogle bookinger, så ved vi traileren ikke er ledig

			return Results.Ok(new { TrailerId = id, IsAvailable = isAvailable });
		}

		private static async Task<IResult> GetAvailableTrailers(TrailerTrakContext context, IMapper mapper)
		{
			var currentDate = DateTime.UtcNow; // eller DateTime.Now, afhængigt af vores tidszone

			var availableTrailers = await context.Trailers
				.Where(trailer => !context.Bookings
					.Any(booking => booking.TrailerId == trailer.Id &&
									booking.BookedTo >= currentDate &&
									booking.Returned == false))
				.ToListAsync();

			var trailerDtos = mapper.Map<List<TrailerDTO>>(availableTrailers);
			return Results.Ok(trailerDtos);
		}

		private static async Task<IResult> DeleteTrailer(int id, TrailerTrakContext context)
		{
			var trailer = await context.Trailers.FindAsync(id);
			if (trailer == null)
			{
				return Results.NotFound("Trailer not found.");
			}

			context.Trailers.Remove(trailer);
			await context.SaveChangesAsync();

			return Results.NoContent(); // 204 No Content
		}


		// her kan tilføjes flere metoder til trailer
	}
}
