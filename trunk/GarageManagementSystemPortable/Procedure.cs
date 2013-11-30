namespace GarageManagementSystem
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class Procedure : IPricable
    {
        public string Description { get; set; }
        public string Comment { get; set; }
        public List<Part> Part { get; set; }
        public List<Employee> EmployeeList { get; set; }
        public DateTime DateTime { get; set; } // TODO: replace with specific class for date, time, duration and etc.
        public Status Status { get; set; }
        public decimal Price { get; set; }

        public Procedure(string description, DateTime dateTime, List<Employee> employeeList, Status status, string comment, decimal price)
            : this(description, dateTime, employeeList, new List<Part>(), status, comment, price)
        {
        }

        public Procedure(string description, DateTime dateTime, List<Employee> employeeList, List<Part> part, Status status = Status.New, string comment = "", decimal price = 0m)
        {
            this.Description = description;
            this.Comment = comment;
            this.EmployeeList = employeeList;
            this.Part = part;
            this.DateTime = dateTime;
            this.Status = status;
            this.Price = price;
        }

        public decimal CalculateMargin()
        {
            // TODO: Implement this method
            throw new NotImplementedException();
        }
    }
}
