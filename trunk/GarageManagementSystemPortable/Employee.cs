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

        public Employee(string name, decimal salary, string phone)
        {
            this.Name = name;
            this.Salary = salary;
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

        public Employee() { }
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

        public static Employee LoadEmployeeInformation(string[] lines, ref int index)
        {
            Employee employee = new Employee();
            var assembly = Assembly.GetExecutingAssembly();
            var userType = assembly.GetType("GarageManagementSystem.Employee");
            int propertiesCount = Service.PropertiesCount(employee); // Get the number of properties

            for (int i = 0; i < propertiesCount; i++, index++)
            {
                var property = userType.GetProperty(lines[index]);

                if (property.Name == "Address")
                {
                    index += 2;

                    Address address = Address.LoadAddressInformation(lines, ref index);

                    property.SetValue(employee, address, null);
                }
                else if (property.Name == "Position")
                {
                    index++;
                    var currentPropertyType = property.PropertyType;
                    Position position = (Position)Enum.Parse(typeof(Position), lines[index], false);
                    property.SetValue(employee, position, null);
                }
                else
                {
                    index++;
                    var currentPropertyType = property.PropertyType;
                    var convertedValue = Convert.ChangeType(lines[index], currentPropertyType, null);
                    property.SetValue(employee, convertedValue, null);
                }
            }

            return employee;
        }
    }
}
