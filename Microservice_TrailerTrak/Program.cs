
using Microservice_TrailerTrak.Contexts;
using Microservice_TrailerTrak.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

			builder.Services.AddDbContext<TrailerTrakContext>(options =>
			options.UseSqlServer(builder.Configuration.GetConnectionString("UdenDockerConnection")));
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

			app.Run();
		}
	}
}

