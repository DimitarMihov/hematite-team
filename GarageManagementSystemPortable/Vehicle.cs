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
        public Status Status { get; set; }
        public string Comments { get; set; }

        [DescriptionAttribute("Registration Number")]
        public string RegistrationNumber { get; set; }

        public Person Owner { get; set; }

        public List<Repair> Repairs { get; set; }

        public Vehicle(string manufacturer, string model, int year, string registrationNumber, FuelType fuelType, Gearbox gearbox, Status status)
        {
            this.Manufacturer = manufacturer;
            this.Model = model;
            this.Year = year;
            this.RegistrationNumber = registrationNumber;
            this.FuelType = fuelType;
            this.Gearbox = gearbox;
            this.Status = status;
            this.Repairs = new List<Repair>();
        }

        public Vehicle(string manufacturer, string model, Status status = Status.Informational)
        {
            this.Manufacturer = manufacturer;
            this.Model = model;
            this.Status = status;
            this.Id = Interlocked.Increment(ref nextId);
            this.Repairs = new List<Repair>();
        }

        public Vehicle(string manufacturer, string model, int? year, int? horsePower, int? mileage,
            FuelType fuelType, Gearbox gearbox, Person owner,
            string color, string comments, string registrationNumber, List<Repair> repairs, Status status = Status.New)
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

        public Vehicle() { }

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
                if (property.Name == "Owner")
                {
                    builder.AppendLine("Owner");
                    if (vehicle.Owner == null)
                    {
                        builder.AppendLine("-");
                    }
                    else
                    {
                        builder.AppendLine("1");
                        builder.Append(Person.SaveOwnerInformation(vehicle.Owner));
                    }
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
                else if (property.Name == "Tasks")
                {
                    // TODO: Implement saving tasks in Vehicle class
                }
                else
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
            }

            return builder.ToString();
        }

        public static Vehicle LoadVehicleInformation(string[] lines, ref int index)
        {
            Vehicle vehicle = new Vehicle();
            var assembly = Assembly.GetExecutingAssembly();
            var userType = assembly.GetType("GarageManagementSystem.Vehicle");
            int propertiesCount = Service.PropertiesCount(vehicle); // Get the number of properties

            for (int i = 0; i < propertiesCount; i++, index++)
            {
                var property = userType.GetProperty(lines[index]);

                if (property.Name == "Owner")
                {
                     index++;

                     if (lines[index] != "-")
                     {
                         index++;
                         Owner address = Person.LoadOwnerInformation(lines, ref index);
                         property.SetValue(vehicle, address, null);
                     }
                }
                else if (property.Name == "Status")
                {
                    index++;

                    if (lines[index] != "-")
                    {
                        var currentPropertyType = property.PropertyType;
                        Status status = (Status)Enum.Parse(typeof(Status), lines[index], false);
                        property.SetValue(vehicle, status, null);
                    }
                }
                else if (property.Name == "FuelType")
                {
                    index++;

                    if (lines[index] != "-")
                    {
                        var currentPropertyType = property.PropertyType;
                        FuelType fuilType = (FuelType)Enum.Parse(typeof(FuelType), lines[index], false);
                        property.SetValue(vehicle, fuilType, null);
                    }
                }
                else if (property.Name == "Gearbox")
                {
                    index++;

                    if (lines[index] != "-")
                    {
                        var currentPropertyType = property.PropertyType;
                        Gearbox gearBox = (Gearbox)Enum.Parse(typeof(Gearbox), lines[index], false);
                        property.SetValue(vehicle, gearBox, null);
                    }
                }
                else if (property.Name == "Repairs")
                {
                    List<Repair> repair = new List<Repair>();

                    int stopPoint = int.Parse(lines[index + 1]);
                    index += 2;

                    for (int element = 0; element < stopPoint; element++)
                    {
                        repair.Add(Repair.LoadRepairInformation(lines, ref index));
                    }

                    index--;
                    property.SetValue(vehicle, repair, null);
                }
                else if (property.Name == "Tasks")
                {
                    // TODO: Implement loading tasks in Vehicle class
                }
                else
                {
                    index++;

                    if (lines[index] != "-")
                    {
                        Type t = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                        object safeValue = (lines[index] == null) ? null : Convert.ChangeType(lines[index], t, null);
                        property.SetValue(vehicle, safeValue, null);
                    }
                }
            }

            return vehicle;
        }

        //public List<ToDo> Tasks
        //{
        //    get { return this.Tasks; }
        //}

        //public ToDo GetTaskByIndex(int toDoIndex)
        //{
        //    return this.Tasks[toDoIndex];
        //}

        //public void AddTask(ToDo task)
        //{
        //    this.Tasks.Add(task);
        //}

        //public void RemoveTask(ToDo task)
        //{
        //    this.Tasks.Remove(task);
        //}

        //public string Alarm(ToDo task)
        //{
        //    return string.Format("You need to perform the following task in relation to\n vehicle {0} {1} with registration number {2} \n {3}", this.Manufacturer, this.Model, this.RegistrationNumber, task.TaskContent.ToUpper());
        //}
    }
}