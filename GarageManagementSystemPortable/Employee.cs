namespace GarageManagementSystem
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    public class Employee : Person
    {
        public decimal Salary { get; set; }
        public Position Position { get; set; }
        public int Rank { get; set; }

        public Employee(string phone, decimal salary, Position position, int rank)
            : base(phone)
        {
            this.Salary = salary;
            this.Position = position;
            this.Rank = rank;
        }

        public Employee(string name, decimal Salary, string phone)
        {
            this.Name = name;
            this.Salary = Salary;
            this.Phone = phone;
            this.Position = Position.JunorMechanic;
        }

        public Employee(string name, Address address, string phone, string email, string comment, decimal salary, Position position, int rank)
            : base(name,address, phone, email, comment)
        {
            this.Salary = salary;
            this.Position = position;
            this.Rank = rank;
        }

        public static string SaveEmployeeInformation(Employee employee)
        {
            StringBuilder builder = new StringBuilder();

            var assembly = Assembly.GetExecutingAssembly();

            var employeeProperties = assembly.GetType("GarageManagementSystem.Employee").GetProperties();

            foreach (var property in employeeProperties)
            {
                if (property.GetValue(employee, null).ToString() == "GarageManagementSystem.Address")
                {
                    builder.AppendLine("Address");
                    builder.AppendLine("1");
                    builder.Append(Address.SaveAddressInformation(employee.Address));
                }
                else
                {
                    builder.AppendLine(property.Name);
                    builder.AppendLine(property.GetValue(employee, null).ToString());
                }
            }

            return builder.ToString();
        }
    }
}
