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
            this.Parts = new List<Part>();
        }

        public Distributor(string name, Address address, string phone, string email, string comment)
            : base(name,address, phone, email, comment)
        {
            this.Parts = new List<Part>();
        }

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
    }
}
