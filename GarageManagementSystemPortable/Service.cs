﻿namespace GarageManagementSystem
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    public class Service
    {
        // TODO: Add at least one more interface and implement it
        // TODO: "StyleCop" the final code
        // TODO: Create at least one exception class and implement it somewhere in the code
        // TODO: Create and use at least one structure in the code
        // TODO: Update the class diagram before submission
        // TODO: Create a breaf documentation of the project

        // TODO: UI: Optimize the code in the UI
        // TODO: UI: Create a Settings page
        // TODO: UI: Create Repairs page
        // TODO: UI: Implement edit popups for the Cars/Distibutors/Employees etc views
        // TODO: UI: Enhance the Add functions (i.e. with dropdowns for enums, etc)
        // TODO: UI: Implement email validation for the forms in the Helper class

        private static Service serviceInstance;

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

        private List<Vehicle> vehicles = new List<Vehicle>();
        private List<Distributor> distributors = new List<Distributor>();
        private List<Employee> employees = new List<Employee>();


        public List<Vehicle> Vehicles
        {
            get { return vehicles; }
            private set { vehicles = value; }
        }
        public List<Employee> Employees
        {
            get { return employees; }
            private set { employees = value; }
        }

        public List<Distributor> Distributors
        {
            get { return distributors; }
            private set { distributors = value; }
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

        // Implement multiple

       
    

        public static string SaveServiceInformation()
        {
            var assembly = Assembly.GetExecutingAssembly();

            StringBuilder builder = new StringBuilder();

            var serviceTypeProperties = assembly.GetType("GarageManagementSystem.Service").GetProperties();

            foreach (var property in serviceTypeProperties)
            {
                if (property.Name == "Vehicles")
                {
                    dynamic vehicleList = property.GetValue(serviceInstance, null);
                    builder.AppendLine("Vehicles");
                    builder.AppendLine(vehicleList.Count.ToString());

                    foreach (var vehicle in vehicleList)
                    {
                        builder.Append(Vehicle.SaveVehicleInformation(vehicle));
                    }
                }
                else if (property.Name == "Distributors")
                {
                    dynamic distributorList = property.GetValue(serviceInstance, null);
                    builder.AppendLine("Distributors");
                    builder.AppendLine(distributorList.Count.ToString());

                    foreach (var distributor in distributorList)
                    {
                        builder.Append(Distributor.SaveDistributorInformation(distributor));
                    }
                }
                else if (property.Name == "Employees")
                {
                    dynamic employeeList = property.GetValue(serviceInstance, null);
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
    }
}
