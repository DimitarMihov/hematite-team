namespace GarageManagementSystem
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    public class VehicleInformation
    {
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public int? Year { get; set; }
        public FuelType FuelType { get; set; }
        public Gearbox Gearbox { get; set; }

        public VehicleInformation(string manufacturer, string model, int? year, FuelType fuelType, Gearbox gearbox)
        {
            this.Manufacturer = manufacturer;
            this.Model = model;
            this.Year = year;
            this.FuelType = fuelType;
            this.Gearbox = gearbox;
        }

        public VehicleInformation() { }

        public static string SaveVehicleListInformation(VehicleInformation vehicle)
        {
            StringBuilder builder = new StringBuilder();

            var assembly = Assembly.GetExecutingAssembly();

            var ownerProperties = assembly.GetType("GarageManagementSystem.VehicleInformation").GetProperties();

            foreach (var property in ownerProperties)
            {
                builder.AppendLine(property.Name);
                try
                {
                    builder.AppendLine(property.GetValue(vehicle, null).ToString());
                }
                catch (NullReferenceException)
                {
                    builder.AppendLine("-");
                }
            }

            return builder.ToString();
        }

        public static VehicleInformation LoadVehicleListInformation(string[] lines, ref int index)
        {
            VehicleInformation vehicleInformation = new VehicleInformation();
            var assembly = Assembly.GetExecutingAssembly();
            var userType = assembly.GetType("GarageManagementSystem.VehicleInformation");
            int propertiesCount = Service.PropertiesCount(vehicleInformation);

            for (int i = 0; i < propertiesCount; i++, index++)
            {
                var property = userType.GetProperty(lines[index]);

                index++;

                if (lines[index] != "-")
                {
                    if (property.Name == "FuelType")
                    {
                        var currentPropertyType = property.PropertyType;
                        FuelType fuilType = (FuelType)Enum.Parse(typeof(FuelType), lines[index], false);
                        property.SetValue(vehicleInformation, fuilType, null);
                    }
                    else if (property.Name == "Gearbox")
                    {
                        var currentPropertyType = property.PropertyType;
                        Gearbox gearBox = (Gearbox)Enum.Parse(typeof(Gearbox), lines[index], false);
                        property.SetValue(vehicleInformation, gearBox, null);
                    }
                    else
                    {
                        Type t = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                        object safeValue = (lines[index] == null) ? null : Convert.ChangeType(lines[index], t, null);
                        property.SetValue(vehicleInformation, safeValue, null);
                    }
                }
            }

            return vehicleInformation;
        }
    }
}
