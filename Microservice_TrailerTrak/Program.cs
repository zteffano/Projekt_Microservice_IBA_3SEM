

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
				"1. Ops�tning af SQL Server i Docker",
				"2. Ops�tning af API i docker",
				"3. Lav en Docker compose fil og lav ops�tning, s� de er p� samme netv�rk",
				"4. Find ud af hvilke paths der skal v�re. F.eks. /api/v1/trailers/ ..og hvilke GET, POST, PUT / DELETE der skal v�re",
				"5. Prioritere via MoSCOW metoden",
				"6. Lav en class model og find ud hvilke egenskaber vi skal have",
				"7. N�r vores modeller er p� plads, kan vi bruge EF til at oprette Database og lave migrations",
				"8. Lav en controller"
			});

			// Trailer list eksempel til vores "/trailer" path
			var trailers = new List<Trailer>
			{
			new Trailer { ID = 1, Navn = "Trailer 1", �rgang = 2015, M�rke = "Zimula Trailerz", V�gt = 500, MaxV�gt = 2000, BookedStatus = false, BookedIndtil = null, PrisDag = 150, PrisUge = 800, PrisM�ned = 3000 },
			new Trailer { ID = 2, Navn = "Trailer 2", �rgang = 2017, M�rke = "Brenderup", V�gt = 450, MaxV�gt = 1500, BookedStatus = true, BookedIndtil = DateTime.Now.AddDays(5), PrisDag = 200, PrisUge = 1000, PrisM�ned = 3500 },	
			};
			app.MapGet("/trailers", () => trailers);


			app.Run();
		}
	}
}

