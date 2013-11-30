namespace GarageManagementSystem
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Text;

    public class Part : IPricable
    {
        public long Id { get; set; }

        public string Name { get; set; }

        // This parts is approved for this List of Vehicle
        public List<VehicleInformation> VehicleList { get; private set; }

        public decimal Price { get; set; }

        public Part(long id, string name, decimal price)
        {
            this.Id = id;
            this.Name = name;
            this.VehicleList = new List<VehicleInformation>();
            this.Price = price;
        }

        public Part(long id, string name, decimal price, List<VehicleInformation> vehicleList)
            : this(id, name, price)
        {
            this.VehicleList = vehicleList;
        }

        public decimal CalculateMargin()
        {
            return this.Price;
        }

        public static string SavePartInformation(Part part)
        {
            StringBuilder builder = new StringBuilder();

            var assembly = Assembly.GetExecutingAssembly();

            var partProperties = assembly.GetType("GarageManagementSystem.Part").GetProperties();

            foreach (var property in partProperties)
            {
                if (property.Name == "VehicleList")
                {
                    dynamic vehicleList = property.GetValue(part, null);
                    builder.AppendLine("VehicleList");
                    builder.AppendLine(vehicleList.Count.ToString());
                    foreach (var vehicle in vehicleList)
                    {
                        builder.Append(VehicleInformation.SaveVehicleListInformation(vehicle));
                    }
                }
                else
                {
                    builder.AppendLine(property.Name);
                    builder.AppendLine(property.GetValue(part, null).ToString());
                }
            }

            return builder.ToString();
        }
    }
}
