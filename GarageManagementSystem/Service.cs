using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GarageManagementSystem
{
    public class Service
    {
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

    }
}
