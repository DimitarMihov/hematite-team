namespace GarageManagementSystem
{
    using System;
    using System.Linq;

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
    }
}
