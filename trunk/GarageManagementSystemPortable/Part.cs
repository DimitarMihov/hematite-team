namespace GarageManagementSystem
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Text;

    public class Part : IPricable
    {
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

        public Part()
        {
        }

        public long Id { get; set; }

        public string Name { get; set; }

        // This parts is approved for this List of Vehicle
        public List<VehicleInformation> VehicleList { get; private set; }

        public decimal Price { get; set; }

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
                    try
                    {
                        builder.AppendLine(property.GetValue(part, null).ToString());
                    }
                    catch (NullReferenceException)
                    {
                        builder.AppendLine("-");
                    }
                }
            }

            return builder.ToString();
        }

        public static Part LoadPartInformation(string[] lines, ref int index)
        {
            Part part = new Part();
            var assembly = Assembly.GetExecutingAssembly();
            var userType = assembly.GetType("GarageManagementSystem.Part");
            int propertiesCount = Service.PropertiesCount(part);

            for (int i = 0; i < propertiesCount; i++, index++)
            {
                var property = userType.GetProperty(lines[index]);

                if (property.Name == "VehicleList")
                {
                    List<VehicleInformation> vehicleInformation = new List<VehicleInformation>();

                    int stopPoint = int.Parse(lines[index + 1]);
                    index += 2;

                    for (int element = 0; element < stopPoint; element++)
                    {
                        vehicleInformation.Add(VehicleInformation.LoadVehicleListInformation(lines, ref index));
                    }

                    index--;
                    property.SetValue(part, vehicleInformation, null);
                }
                else
                {
                    index++;

                    if (lines[index] != "-")
                    {
                        var currentPropertyType = property.PropertyType;
                        var convertedValue = Convert.ChangeType(lines[index], currentPropertyType, null);
                        property.SetValue(part, convertedValue, null);
                    }
                }
            }

            return part;
        }

        public decimal CalculateMargin()
        {
            return this.Price;
        }
    }
}
