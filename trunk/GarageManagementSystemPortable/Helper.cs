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
                from employee in Service.AutoShopInstance.GetEmployeesList()
                where string.Format("{0} {1} {2}", employee.Name, employee.Salary, employee.Email).ToLower().Contains(keyword.ToLower())
                select employee;

            List<Employee> result = new List<Employee>();

            foreach (var employee in filteredEmployees)
            {
                result.Add(employee as Employee);
            }

            return result;
        }

        public static List<Repair> SearchForRepairs(string keyword, Vehicle vehicle)
        {
            var repairs =
                from repair in vehicle.Repairs
                where string.Format("{0} {1}", repair.Caption, repair.Guarantee).ToLower().Contains(keyword.ToLower())
                select repair;

            List<Repair> result = new List<Repair>();

            foreach (var repair in repairs)
            {
                result.Add(repair as Repair);
            }

            return result;
        }
    }
}
