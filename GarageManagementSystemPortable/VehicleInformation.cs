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
                builder.AppendLine(property.GetValue(vehicle, null).ToString());
            }

            return builder.ToString();
        }
    }
}
