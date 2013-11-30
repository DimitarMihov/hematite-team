using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GarageManagementSystem
{
    public class Service
    {
<<<<<<< .mine
        private int cars;
        private int distributors;
        private int employees;
        private int owners;
    
        string IContactable.Name
        {
            get
            {
                // TODO: Implement this property getter
                throw new NotImplementedException();
            }
            set
            {
                // TODO: Implement this property setter
                throw new NotImplementedException();
            }
        }

        Address IContactable.Address
        {
            get
            {
                // TODO: Implement this property getter
                throw new NotImplementedException();
            }
            set
            {
                // TODO: Implement this property setter
                throw new NotImplementedException();
            }
        }

        public string Phone
        {
            get
            {
                // TODO: Implement this property getter
                throw new NotImplementedException();
            }
            set
            {
                // TODO: Implement this property setter
                throw new NotImplementedException();
            }
        }

        string IContactable.Email
        {
            get
            {
                // TODO: Implement this property getter
                throw new NotImplementedException();
            }
            set
            {
                // TODO: Implement this property setter
                throw new NotImplementedException();
            }
        }

        public string Comment
        {
            get
            {
                // TODO: Implement this property getter
                throw new NotImplementedException();
            }
            set
            {
                // TODO: Implement this property setter
                throw new NotImplementedException();
            }
        }

        public int Address
=======
        private static Service serviceInstance;

        public static Service AutoShopInstance
>>>>>>> .r22
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
