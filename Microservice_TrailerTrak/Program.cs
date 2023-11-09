
using Microservice_TrailerTrak.Contexts;
using Microservice_TrailerTrak.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using AutoMapper;
using Microservice_TrailerTrak.DTO;
using Microservice_TrailerTrak.Endpoints;
using System.Diagnostics;

namespace Microservice_TrailerTrak
{
    public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddAuthorization();
			/*
			builder.Services.AddDbContext<TrailerTrakContext>(options =>
			options.UseSqlServer(builder.Configuration.GetConnectionString("UdenDockerConnection")));*/
			builder.Services.AddDbContext<TrailerTrakContext>(options =>
			options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

			builder.Services.AddAutoMapper(typeof(MappingProfile));
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddHttpClient();

			var app = builder.Build();
			// mapper vores Endpoints ind i vores app fra Endpoints mappen
			app.MapBookingEndpoints();
			app.MapCustomerEndpoints();
			app.MapTrailerEndpoints();

			//Brug af swagger når vi kører i dev processen 
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}
			app.UseAuthorization();

			app.MapGet("/", () => "Velkommen til TrailerTrak API version 1.0.0");

			app.MapGet("/getcar/{licenseplate}", async (string licenseplate, IHttpClientFactory clientFactory) =>
			{
			
				var client = clientFactory.CreateClient();
				var response = await client.GetAsync($"https://www.tjekbil.dk/api/v3/dmr/regnrquery/{licenseplate}");

				if (!response.IsSuccessStatusCode)
				{
					return Results.Problem("Der skete en fejl under anmodningen til tjekbil.dk´s  API.");
				}

				var content = await response.Content.ReadAsStringAsync();
				var json = JsonSerializer.Deserialize<object>(content);

				return Results.Ok(json);
			});
			// Specielle endspoints vi kun bruger i vores dev process
			if (app.Environment.IsDevelopment())
			{

			// Vores minimal test frontend til brug og der tiltænkt til test af vores API via. selenium(vores Test node)
			app.MapGet("/test_frontend", async context =>
			{
				var html = await File.ReadAllTextAsync("Testing/frontend/minimal_test_FE.html");
				context.Response.ContentType = "text/html";
				await context.Response.WriteAsync(html);
			});

				// Vores debug endpoint til at se data i databasen, - blevet brugt i start af udviklingsprocessen
				app.MapGet("/debug", async (TrailerTrakContext context, IMapper mapper) =>
			{
		
				var bookings = await context.Bookings.ToListAsync();
				var bookingDtos = mapper.Map<List<BookingDTO>>(bookings);
				var trailers = await context.Trailers.ToListAsync();
				var trailerDtos = mapper.Map<List<TrailerDTO>>(trailers);
				var customers = await context.Customers.ToListAsync();
				var customerDtos = mapper.Map<List<CustomerDTO>>(customers);


				var debugInfo = new
				{
					Bookings = bookingDtos,
					Trailers = trailerDtos,
					Customers = customerDtos
				};
				string jsonString = JsonSerializer.Serialize(debugInfo, new JsonSerializerOptions { WriteIndented = true });


				string htmlContent = $@"
				<html>
				<head>
					<title>Debug Page</title>
					<style>
						body {{
							font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
							background-color: #f4f4f4;
							margin: 0;
							padding: 20px;
						}}
						pre {{
							background-color: #fff;
							border: 1px solid #ddd;
							border-left: 3px solid #f36d33;
							color: #666;
							page-break-inside: avoid;
							font-family: monospace;
							font-size: 15px;
							line-height: 1.6;
							margin-bottom: 1.6em;
							max-width: 100%;
							overflow: auto;
							padding: 1em 1.5em;
							display: block;
							word-wrap: break-word;
						}}
					</style>
				</head>
				<body>
					<h1>Debug Information</h1>
					<pre id='json'></pre>
					<script>
						document.addEventListener('DOMContentLoaded', (event) => {{
							// Parse the JSON string
							const debugData = JSON.parse(`{jsonString}`);
							// Format the JSON and output it
							document.getElementById('json').textContent = JSON.stringify(debugData, null, 2);
						}});
					</script>
				</body>
				</html>";

				return Results.Content(htmlContent, "text/html");
			});

			}


			app.Run();
		}
	}
}

