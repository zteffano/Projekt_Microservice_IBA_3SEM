using Microsoft.Extensions.Primitives;

namespace Microservice_TrailerTrak.Model
{
    public class Customer
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }

        public Customer(int id, String name, string type, string phone, string address, string email)
        {
            Id = id;
            Name = name;
            Type = type;
            Phone = phone;
            Address = address;
            Email = email;
        }
    }
   
}
