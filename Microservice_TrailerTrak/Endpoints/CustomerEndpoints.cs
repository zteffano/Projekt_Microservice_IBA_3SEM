using AutoMapper;
using Microservice_TrailerTrak.Contexts;
using Microservice_TrailerTrak.DTO;
using Microservice_TrailerTrak.Model;
using Microsoft.EntityFrameworkCore;

namespace Microservice_TrailerTrak.Endpoints
{
	//TODO: Mangler change endpoints & metode
	public static class CustomerEndpoints
	{
		//Customer endpoints "controller" , der mapper til metoder
		//Bemærk vi bruger async og await for at undgå at blokere tråden ved flere requests
		public static void MapCustomerEndpoints(this WebApplication app)
		{
			app.MapGet("/customers", GetAllCustomers);
			app.MapGet("/customer/{id:int}", GetCustomerById);
			app.MapPost("/createcustomer", CreateCustomer);
			app.MapDelete("/deletecustomer/{id:int}", DeleteCustomer);
			
		}
		// Returnerer alle kunder fra databasen
		private static async Task<IResult> GetAllCustomers(TrailerTrakContext context, IMapper mapper)
		{
			var customers = await context.Customers.ToListAsync();
			var customerDtos = mapper.Map<List<CustomerDTO>>(customers);
			return Results.Ok(customerDtos);
		}
		// Returnerer en kunde med et specifikt id fra databasen
		private static async Task<IResult> GetCustomerById(int id, TrailerTrakContext context, IMapper mapper)
		{
			var customer = await context.Customers.FindAsync(id);
			if (customer == null) return Results.NotFound("Customer not found");
			var customerDto = mapper.Map<CustomerDTO>(customer);
			return Results.Ok(customerDto);
		}
		// Opretter en ny kunde i databasen
		private static async Task<IResult> CreateCustomer(CustomerDTO customerDto, TrailerTrakContext context, IMapper mapper)
		{
			var customer = mapper.Map<Customer>(customerDto);
			await context.Customers.AddAsync(customer);
			await context.SaveChangesAsync();

			var customerToReturn = mapper.Map<Customer>(customer);
			customerToReturn.Id = customer.Id;
			return Results.Created($"/customer/{customer.Id}",customerToReturn);
		}

		private static async Task<IResult> DeleteCustomer(int id, TrailerTrakContext context)
		{
			var customer = await context.Customers.FindAsync(id);
			if (customer == null)
			{
				return Results.NotFound("Customer not found.");
			}

			context.Customers.Remove(customer);
			await context.SaveChangesAsync();

			return Results.NoContent(); // 204 No Content
		}


		
	}
}
