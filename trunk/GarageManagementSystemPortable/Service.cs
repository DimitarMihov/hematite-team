namespace GarageManagementSystem
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    public class Service
    {
        // TODO: DONE MANDATORY Add at least one more interface and implement it
        // TODO: DONE MANDATORY Create and use at least one structure in the code
        // TODO: MANDATORY Update the class diagram before submission
        // TODO: MANDATORY Create a brief documentation of the project

        // TODO: UI: Fix the search methods (Now it takes the name of the searched item and the properties of the first element in the list)
        // TODO: UI: Optimize the code in the UI
        // TODO: UI: Create a Settings page
        // TODO: UI: Create Repairs page
        // TODO: UI: DONE Enhance the Add functions (i.e. with dropdowns for enums, etc)
        // TODO: UI: Implement email validation for the forms in the Helper class

        private static Service serviceInstance;

        private List<Vehicle> vehicles = new List<Vehicle>();
        private List<Distributor> distributors = new List<Distributor>();
        private List<Employee> employees = new List<Employee>();

        public static Service AutoShopInstance
        {
            get
            {
                if (serviceInstance == null)
                {
                    serviceInstance = new Service();
                }

                return serviceInstance;
            }
        }

        public List<Vehicle> Vehicles
        {
            get { return this.vehicles; }
            private set { this.vehicles = value; }
        }

        public List<Employee> Employees
        {
            get { return this.employees; }
            private set { this.employees = value; }
        }

        public List<Distributor> Distributors
        {
            get { return this.distributors; }
            private set { this.distributors = value; }
        }

        public static int PropertiesCount(object entity)
        {
            return entity.GetType()
                         .GetProperties()
                         .Select(x => x.GetValue(entity, null))
                         .Count(v => v != null || v == null);
        }

        public static string SaveServiceInformation()
        {
            var assembly = Assembly.GetExecutingAssembly();

            StringBuilder builder = new StringBuilder();

            var serviceTypeProperties = assembly.GetType("GarageManagementSystem.Service").GetProperties();

            foreach (var property in serviceTypeProperties)
            {
                if (property.Name == "Vehicles")
                {
                    dynamic vehicleList = property.GetValue(AutoShopInstance, null);
                    builder.AppendLine("Vehicles");
                    builder.AppendLine(vehicleList.Count.ToString());

                    foreach (var vehicle in vehicleList)
                    {
                        builder.Append(Vehicle.SaveVehicleInformation(vehicle));
                    }
                }
                else if (property.Name == "Distributors")
                {
                    dynamic distributorList = property.GetValue(AutoShopInstance, null);
                    builder.AppendLine("Distributors");
                    builder.AppendLine(distributorList.Count.ToString());

                    foreach (var distributor in distributorList)
                    {
                        builder.Append(Distributor.SaveDistributorInformation(distributor));
                    }
                }
                else if (property.Name == "Employees")
                {
                    dynamic employeeList = property.GetValue(AutoShopInstance, null);
                    builder.AppendLine("Employees");
                    builder.AppendLine(employeeList.Count.ToString());

                    foreach (var employee in employeeList)
                    {
                        builder.Append(Employee.SaveEmployeeInformation(employee));
                    }
                }
            }

            return builder.ToString();
        }

        public static void LoadServiceInformation(string[] lines)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var userType = assembly.GetType("GarageManagementSystem.Service");

            for (int index = 0; index < lines.Length - 1; index++)
            {
                var property = userType.GetProperty(lines[index]);

                if (property.Name == "Vehicles")
                {
                    List<Vehicle> vehicles = new List<Vehicle>();
                    int stopPoint = int.Parse(lines[index + 1]);
                    index += 2;

                    for (int element = 0; element < stopPoint; element++)
                    {
                        vehicles.Add(Vehicle.LoadVehicleInformation(lines, ref index));
                    }

                    index--;

                    property.SetValue(AutoShopInstance, vehicles, null);
                }
                else if (property.Name == "Distributors")
                {
                    List<Distributor> distributors = new List<Distributor>();
                    int stopPoint = int.Parse(lines[index + 1]);
                    index += 2;

                    for (int element = 0; element < stopPoint; element++)
                    {
                        distributors.Add(Distributor.LoadDistributorInformation(lines, ref index));
                    }

                    index--;

                    property.SetValue(AutoShopInstance, distributors, null);
                }
                else if (property.Name == "Employees")
                {
                    List<Employee> employees = new List<Employee>();
                    int stopPoint = int.Parse(lines[index + 1]);
                    index += 2;

                    for (int element = 0; element < stopPoint; element++)
                    {
                        employees.Add(Employee.LoadEmployeeInformation(lines, ref index));
                    }

                    index--;
                    property.SetValue(AutoShopInstance, employees, null);
                }
            }
        }

        public void AddVehicle(Vehicle vehicle)
        {
            this.vehicles.Add(vehicle);
        }

        public List<Vehicle> GetVehiclesList()
        {
            return this.vehicles;
        }

        public Vehicle GetVehicleByIndex(int index)
        {
            return this.vehicles[index];
        }

        public void RemoveVehicle(Vehicle vehicle)
        {
            this.vehicles.Remove(vehicle);
        }

        public void AddDistributor(Distributor distributor)
        {
            this.distributors.Add(distributor);
        }

        public List<Distributor> GetDistributorsList()
        {
            return this.distributors;
        }

        public Distributor GetDistributorByIndex(int index)
        {
            return this.distributors[index];
        }

        public void RemoveDistributor(Distributor distributor)
        {
            this.distributors.Remove(distributor);
        }

        public void AddEmployee(Employee employee)
        {
            this.employees.Add(employee);
        }

        public List<Employee> GetEmployeesList()
        {
            return this.employees;
        }

        public Employee GetEmployeeByIndex(int index)
        {
            return this.employees[index];
        }

        public void RemoveEmployee(Employee employee)
        {
            this.employees.Remove(employee);
        }
    }
}
