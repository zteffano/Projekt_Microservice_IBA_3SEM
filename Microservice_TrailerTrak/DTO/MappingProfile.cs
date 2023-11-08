using Microservice_TrailerTrak.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;
using AutoMapper;

namespace Microservice_TrailerTrak.DTO
{
    public class MappingProfile : Profile
    {
        // Vi laver mapping med DTO´s (Data Transfer Objects) for at kunne administrer
        // hvilke data klienten kan se og ikke se
        public MappingProfile()
        {
            // Mapping fra DTO til vores model
            CreateMap<CustomerDTO, Customer>();
            CreateMap<TrailerDTO, Trailer>();

            // Mapping fra vores model til DTO 
            CreateMap<Customer, CustomerDTO>();
            CreateMap<Trailer, TrailerDTO>();

            CreateMap<BookingDTO, Booking>();
            CreateMap<Booking, BookingDTO>();

        }
    }
}

