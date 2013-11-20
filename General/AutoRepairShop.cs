using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General
{
    public class AutoRepairShop
    {
        private AutoRepairShop() { }

        // TODO: Think of more properties to add to the Repair shop
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Url { get; set; }

        public List<Distributor> Distributors { get; set; }
        public List<Car> ServicedCars { get; set; }
        public List<Employee> Employees { get; set; }

        private static AutoRepairShop autoShopInstance;
        
        public static AutoRepairShop AutoShopInstance
        {
            get
            {
                if (autoShopInstance == null)
                {
                    autoShopInstance = new AutoRepairShop();

                    // TODO: Create dialogs to get the information about the shop (name, address etc) 
                }

                return autoShopInstance;
            }
        }

        public void AddEmployee(Employee employee)
        {
            this.Employees.Add(employee)
        }
        public void RemoveEmployee(Employee employee)
        {
            this.Employees.Remove(employee)
        }

        public void AddCar(Car car)
        {
            this.ServicedCars.Add(car);
        }

        public void RemoveCar(Car car)
        {
            this.ServicedCars.Remove(car);
        }

        public void AddDistributor(Distributor distributor)
        {
            this.Distributors.Add(Distributors);
        }

        public void RemoveDistributor(Distributor distributor)
        {
            this.Distributors.Remove(distributor);
        }
    }
}
