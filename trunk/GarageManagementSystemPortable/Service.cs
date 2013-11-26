using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GarageManagementSystem
{
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
