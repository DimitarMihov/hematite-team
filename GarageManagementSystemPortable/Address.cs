namespace GarageManagementSystem
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    public class Address
    {
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string District { get; set; }
        public string Street { get; set; }
        public int? Number { get; set; }
        public string Comment { get; set; }

        public Address(string city, string street, int? number, string comment)
        {
            this.City = city;
            this.Street = street;
            this.Number = number;
            this.Comment = comment;
        }

        public Address(string city, string postalCode, string district, string street, int? number, string comment)
            : this(city, street, number, comment)
        {
            this.PostalCode = postalCode;
            this.District = district;
        }
        public static string SaveAddressInformation(Address address)
        {
            StringBuilder builder = new StringBuilder();

            var assembly = Assembly.GetExecutingAssembly();

            var ownerProperties = assembly.GetType("GarageManagementSystem.Address").GetProperties();

            foreach (var property in ownerProperties)
            {
                builder.AppendLine(property.Name);
                builder.AppendLine(property.GetValue(address, null).ToString());
            }

            return builder.ToString();
        }

    }
}
