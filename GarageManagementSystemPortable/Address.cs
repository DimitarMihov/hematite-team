namespace GarageManagementSystem
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    public class Address
    {
        public Address() { }

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

        public string City { get; set; }

        public string PostalCode { get; set; }

        public string District { get; set; }

        public string Street { get; set; }

        public int? Number { get; set; }

        public string Comment { get; set; }

        public static string SaveAddressInformation(Address address)
        {
            StringBuilder builder = new StringBuilder();

            var assembly = Assembly.GetExecutingAssembly();

            var ownerProperties = assembly.GetType("GarageManagementSystem.Address").GetProperties();

            foreach (var property in ownerProperties)
            {
                builder.AppendLine(property.Name);
                try
                {
                    builder.AppendLine(property.GetValue(address, null).ToString());
                }
                catch (NullReferenceException)
                {
                    builder.AppendLine("-");
                }
            }

            return builder.ToString();
        }

        public static Address LoadAddressInformation(string[] lines, ref int index)
        {
            Address address = new Address();

            var assembly = Assembly.GetExecutingAssembly();

            var userType = assembly.GetType("GarageManagementSystem.Address");

            int propertiesCount = Service.PropertiesCount(address); // Get the number of properties

            for (int i = 0; i < propertiesCount; i++, index++)
            {
                var property = userType.GetProperty(lines[index]);
                index++;

                if (lines[index] != "-")
                {
                    Type t = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                    object safeValue = (lines[index] == null) ? null : Convert.ChangeType(lines[index], t, null);
                    property.SetValue(address, safeValue, null);
                }
            }

            index--;

            return address;
        }
    }
}
