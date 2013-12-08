namespace GarageManagementSystem
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Helper
    {
        // TODO: Implement data validations and other auxiliary methods in the Helper class
        public static List<Vehicle> SearchForVehicles(string keyword)
        {
            var filteredVehicles =
                from vehicle in Service.AutoShopInstance.GetVehiclesList()
                where string.Format("{0} {1} {2} {3}", vehicle.Manufacturer, vehicle.Model, vehicle.Year, vehicle.RegistrationNumber).ToLower().Contains(keyword.ToLower())
                select vehicle;

            List<Vehicle> result = new List<Vehicle>();

            foreach (var vehicle in filteredVehicles)
            {
                result.Add(vehicle as Vehicle);
            }

            return result;
        }

        public static List<Distributor> SearchForDistributors(string keyword)
        {
            var filteredDistributors =
                from distributor in Service.AutoShopInstance.GetDistributorsList()
                where string.Format("{0} {1} {2}", distributor.Name, distributor.Phone, distributor.Email).ToLower().Contains(keyword.ToLower())
                select distributor;

            List<Distributor> result = new List<Distributor>();

            foreach (var distributor in filteredDistributors)
            {
                result.Add(distributor as Distributor);
            }

            return result;
        }

        public static List<Employee> SearchForEmployees(string keyword)
        {
            var filteredEmployees =
                from Employee in Service.AutoShopInstance.GetEmployeesList()
                where string.Format("{0} {1} {2}", Employee.Name, Employee.Salary, Employee.Email).ToLower().Contains(keyword.ToLower())
                select Employee;

            List<Employee> result = new List<Employee>();

            foreach (var employee in filteredEmployees)
            {
                result.Add(employee as Employee);
            }

            return result;
        }
    }
}
