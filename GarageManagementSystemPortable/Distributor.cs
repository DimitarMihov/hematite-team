namespace GarageManagementSystem
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    public class Distributor : Person
    {
        public Distributor(string name, string phone, string email) 
        {
            this.Name = name;
            this.Phone = phone;
            this.Email = email;
        }

        public Distributor(string name, Address address, string phone, string email, string comment, List<Part> parts)
            : base(name, address, phone, email, comment)
        {
            this.Parts = parts;
        }

        public Distributor() { }

        public List<Part> Parts { get; set; }

        public static string SaveDistributorInformation(Distributor distributor)
        {
            StringBuilder builder = new StringBuilder();

            var assembly = Assembly.GetExecutingAssembly();

            var distibutorProperties = assembly.GetType("GarageManagementSystem.Distributor").GetProperties();

            foreach (var property in distibutorProperties)
            {
                if (property.GetValue(distributor, null).ToString() == "GarageManagementSystem.Address")
                {
                    builder.AppendLine("Address");
                    builder.AppendLine("1");
                    builder.Append(Address.SaveAddressInformation(distributor.Address));
                }
                else if (property.Name == "Parts")
                {
                    dynamic partList = property.GetValue(distributor, null);
                    builder.AppendLine("Parts");
                    builder.AppendLine(partList.Count.ToString());
                    foreach (var part in partList)
                    {
                        builder.Append(Part.SavePartInformation(part));
                    }
                }
                else
                {
                    builder.AppendLine(property.Name);
                    builder.AppendLine(property.GetValue(distributor, null).ToString());
                }
            }

            return builder.ToString();
        }

        public static Distributor LoadDistributorInformation(string[] lines, ref int index)
        {
            Distributor distributor = new Distributor();
            var assembly = Assembly.GetExecutingAssembly();
            var userType = assembly.GetType("GarageManagementSystem.Distributor");
            int propertiesCount = Service.PropertiesCount(distributor); 

            for (int i = 0; i < propertiesCount; i++, index++)
            {
                var property = userType.GetProperty(lines[index]);

                if (property.Name == "Address")
                {
                    index += 2;

                    Address address = Address.LoadAddressInformation(lines, ref index);

                    property.SetValue(distributor, address, null);
                }
                else if (property.Name == "Parts")
                {
                    List<Part> parts = new List<Part>();

                    int stopPoint = int.Parse(lines[index + 1]);
                    index += 2;

                    for (int element = 0; element < stopPoint; element++)
                    {
                        parts.Add(Part.LoadPartInformation(lines, ref index));
                    }

                    index--;
                    property.SetValue(distributor, parts, null);
                }
                else
                {
                    index++;
                    var currentPropertyType = property.PropertyType;
                    var convertedValue = Convert.ChangeType(lines[index], currentPropertyType, null);
                    property.SetValue(distributor, convertedValue, null);
                }
            }

            return distributor;
        }
    }
}
