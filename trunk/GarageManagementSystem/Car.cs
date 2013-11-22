namespace GarageManagementSystem
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    public class Car // or Vehicle
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


        public Car(string manufacturer, string model)
        {
            this.Manufacturer = manufacturer;
            this.Model = model;
            this.Status = CarStatus.New;
            this.Id = Interlocked.Increment(ref nextId);
        }

        public Car(string manufacturer, string model, int year, string color = String.Empty, int horsePower, int mileage,
            FuelType fuelType, Gearbox gearbox, string comments = String.Empty, string registrationNumber = String.Empty,
            CarStatus status = CarStatus.New, Person owner, Person contactPerson, List<Repair> repairs = new List<Repair>())
            : this(manufacturer, model)
        {
            this.Year = year;
            this.Color = color;
            this.HorsePower = horsePower;
            this.Mileage = mileage;
            this.FuelType = fuelType;
            this.Gearbox = gearbox;
            this.Status = status;
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

        public bool Equals(Car value)
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
            Car temp = obj as Car;
            if (temp == null)
            {
                return false;
            }

            return this.Equals(temp);
        }

        public void AddRepair(Repair repair)
        {
        }
    }
}