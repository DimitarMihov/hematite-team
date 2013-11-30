namespace GarageManagementSystem
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading;

    public class Vehicle : VehicleInformation
    {
        static long nextId;

        public long Id { get; private set; }

        public string Color { get; set; }
        public int? HorsePower { get; set; }
        public int? Mileage { get; set; }
        public CarStatus Status { get; set; }
        public string Comments { get; set; }

        [DescriptionAttribute("Registration Number")]
        public string RegistrationNumber { get; set; }

        public Person Owner { get; set; }

        public List<Repair> Repairs { get; private set; }

        public Vehicle(string manufacturer, string model, int year, string registrationNumber)
        {
            this.Manufacturer = manufacturer;
            this.Model = model;
            this.Year = year;
            this.RegistrationNumber = registrationNumber;
        }

        public Vehicle(string manufacturer, string model, CarStatus status = CarStatus.Informational)
        {
            this.Manufacturer = manufacturer;
            this.Model = model;
            this.Status = status;
            this.Id = Interlocked.Increment(ref nextId);
        }

        public Vehicle(string manufacturer, string model, int? year, int? horsePower, int? mileage,
            FuelType fuelType, Gearbox gearbox, Person owner,
            string color, string comments, string registrationNumber, List<Repair> repairs, CarStatus status = CarStatus.New)
            : base(manufacturer, model, year, fuelType, gearbox)
        {
            this.HorsePower = horsePower;
            this.Mileage = mileage;
            this.Owner = owner;
            this.Color = color;
            this.Comments = comments;
            this.RegistrationNumber = registrationNumber;
            this.Repairs = repairs;
            this.Status = status;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = 17;
                result = result * 23 + ((Manufacturer != null) ? this.Manufacturer.GetHashCode() : 0);
                result = result * 23 + ((Model != null) ? this.Model.GetHashCode() : 0);
                result = result * 23 + ((Year != null) ? this.Year.GetHashCode() : 0);
                result = result * 23 + ((RegistrationNumber != null) ? this.RegistrationNumber.GetHashCode() : 0);
                result = result * 23 + ((Owner != null) ? this.Owner.GetHashCode() : 0);
                return result;
            }
        }

        public bool Equals(Vehicle value)
        {
            if (ReferenceEquals(null, value))
            {
                return false;
            }

            if (ReferenceEquals(this, value))
            {
                return true;
            }

            return Equals(this.Manufacturer, value.Manufacturer) &&
                   Equals(this.Model, value.Model) &&
                   this.Year.Equals(value.Year) &&
                   Equals(this.RegistrationNumber, value.RegistrationNumber) &&
                   Equals(this.Owner, value.Owner);
        }

        public override bool Equals(object obj)
        {
            Vehicle temp = obj as Vehicle;
            if (temp == null)
            {
                return false;
            }

            return this.Equals(temp);
        }

        public void AddRepair(Repair repair)
        {
            // TODO: check if this repair not exist yet
            this.Repairs.Add(repair);
        }

        public static string SaveVehicleInformation(Vehicle vehicle)
        {
            var assembly = Assembly.GetExecutingAssembly();

            StringBuilder builder = new StringBuilder();

            var userTypeProperties = assembly.GetType("GarageManagementSystem.Vehicle").GetProperties();


            foreach (var property in userTypeProperties)
            {
                if (property.GetValue(vehicle, null).ToString() == "GarageManagementSystem.Owner")
                {
                    builder.AppendLine("Owner");
                    builder.AppendLine("1");
                    builder.Append(Person.SaveOwnerInformation(vehicle.Owner));
                }
                else if (property.Name == "Repairs")
                {
                    dynamic repairsList = property.GetValue(vehicle, null);
                    builder.AppendLine("Repairs");
                    builder.AppendLine(repairsList.Count.ToString());

                    foreach (var repair in repairsList)
                    {
                        builder.Append(Repair.SaveRepairInformation(repair));
                    }
                }
                else
                {
                    builder.AppendLine(property.Name);
                    builder.AppendLine(property.GetValue(vehicle, null).ToString());
                }
            }

            return builder.ToString();
        }
    }
}