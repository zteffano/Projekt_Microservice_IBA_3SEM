

using Microservice_TrailerTrak.Model;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Microservice_TrailerTrak
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddAuthorization();

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseAuthorization();
			app.MapGet("/", () => "Velkommen til TrailerTrak");

			app.MapGet("/todolisten", () => new List<string>
			{
				"1. Opsætning af SQL Server i Docker",
				"2. Opsætning af API i docker",
				"3. Lav en Docker compose fil og lav opsætning, så de er på samme netværk",
				"4. Find ud af hvilke paths der skal være. F.eks. /api/v1/trailers/ ..og hvilke GET, POST, PUT / DELETE der skal være",
				"5. Prioritere via MoSCOW metoden",
				"6. Lav en class model og find ud hvilke egenskaber vi skal have",
				"7. Når vores modeller er på plads, kan vi bruge EF til at oprette Database og lave migrations",
				"8. Lav en controller"
			});

			// Trailer list eksempel til vores "/trailer" path
			var trailers = new List<Trailer>
			{
			new Trailer { ID = 1, Navn = "Trailer 1", Årgang = 2015, Mærke = "Zimula Trailerz", Vægt = 500, MaxVægt = 2000, BookedStatus = false, BookedIndtil = null, PrisDag = 150, PrisUge = 800, PrisMåned = 3000 },
			new Trailer { ID = 2, Navn = "Trailer 2", Årgang = 2017, Mærke = "Brenderup", Vægt = 450, MaxVægt = 1500, BookedStatus = true, BookedIndtil = DateTime.Now.AddDays(5), PrisDag = 200, PrisUge = 1000, PrisMåned = 3500 },	
			};
			app.MapGet("/trailers", () => trailers);


			app.Run();
		}
	}
}

