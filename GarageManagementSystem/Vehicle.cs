namespace GarageManagementSystem
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    public class Vehicle
    {
        static long nextId;

        public long Id { get; private set; }
        public string Vin { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public int? Year { get; set; }
        public string Color { get; set; }
        public int? HorsePower { get; set; }
        public int? Mileage { get; set; }
        public FuelType FuelType { get; set; }
        public Gearbox Gearbox { get; set; }
        public CarStatus Status { get; set; }
        public string Comments { get; set; }

        public string RegistrationNumber { get; set; }
        public Person Owner { get; set; }
        public Person ContactPerson { get; set; }

        public List<Repair> Repairs { get; private set; }

        public Vehicle(string manufacturer, string model, CarStatus status = CarStatus.Informational)
        {
            this.Manufacturer = manufacturer;
            this.Model = model;
            this.Status = status;
            this.Id = Interlocked.Increment(ref nextId);
        }

        public Vehicle(string manufacturer, string model, int? year, int? horsePower, int? mileage,
            FuelType fuelType, Gearbox gearbox, Person owner, Person contactPerson,
            string color, string comments, string registrationNumber, List<Repair> repairs, CarStatus status = CarStatus.New)
            : this(manufacturer, model, status)
        {
            this.Year = year;
            this.Color = color;
            this.HorsePower = horsePower;
            this.Mileage = mileage;
            this.FuelType = fuelType;
            this.Gearbox = gearbox;
            this.Comments = comments;
            this.RegistrationNumber = registrationNumber;
            this.Owner = owner;
            this.ContactPerson = contactPerson;
            this.Repairs = repairs;
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
    }
}