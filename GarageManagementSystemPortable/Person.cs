namespace GarageManagementSystem
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    public abstract class Person : IContactable, iTask
    {
        public Person()
        {
        }

        protected Person(string phone)
        {
            this.Phone = phone;
        }

        protected Person(string phone, string comment)
            : this(phone)
        {
            this.Comment = comment;
        }

        protected Person(string name, Address address, string phone, string email, string comment)
            : this(phone, comment)
        {
            this.Name = name;
            this.Address = address;
            this.Email = email;
        }

        public string Name { get; set; }

        public Address Address { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Comment { get; set; }

        public static string SaveOwnerInformation(Person owner)
        {
            StringBuilder builder = new StringBuilder();

            var assembly = Assembly.GetExecutingAssembly();

            var ownerProperties = assembly.GetType("GarageManagementSystem.Owner").GetProperties();

            foreach (var property in ownerProperties)
            {
                if (property.Name == "Address")
                {
                    builder.AppendLine("Address");
                    if (owner.Address == null)
                    {
                        builder.AppendLine("-");
                    }
                    else
                    {
                        builder.AppendLine("1");
                        builder.Append(Address.SaveAddressInformation(owner.Address));
                    }
                }
                else
                {
                    builder.AppendLine(property.Name);
                    try
                    {
                        builder.AppendLine(property.GetValue(owner, null).ToString());
                    }
                    catch (NullReferenceException)
                    {
                        builder.AppendLine("-");
                    }
                }
            }

            return builder.ToString();
        }

        public static Owner LoadOwnerInformation(string[] lines, ref int index)
        {
            Owner owner = new Owner();
            var assembly = Assembly.GetExecutingAssembly();
            var userType = assembly.GetType("GarageManagementSystem.Owner");
            int propertiesCount = Service.PropertiesCount(owner); // Get the number of properties

            for (int i = 0; i < propertiesCount; i++, index++)
            {
                var property = userType.GetProperty(lines[index]);
                index++;

                if (property.Name == "Address")
                {
                    if (lines[index] != "-")
                    {
                        index++;
                        Address address = Address.LoadAddressInformation(lines, ref index);
                        property.SetValue(owner, address, null);
                    }
                }
                else
                {
                    if (lines[index] != "-")
                    {
                        var currentPropertyType = property.PropertyType;
                        var convertedValue = Convert.ChangeType(lines[index], currentPropertyType, null);
                        property.SetValue(owner, convertedValue, null);
                    }
                }
            }

            index--;

            return owner;
        }

        public void SendSms()
        {
            // TODO: Implement the SendSms() method
        }

        public void SendEmail()
        {
            // TODO: Implement the SendEmail() method
        }

        public List<ToDo> Tasks
        {
            get { return this.Tasks; }
        }

        public ToDo GetTaskByIndex(int toDoIndex)
        {
            return this.Tasks[toDoIndex];
        }

        public void AddTask(ToDo task)
        {
            this.Tasks.Add(task);
        }

        public void RemoveTask(ToDo task)
        {
            this.Tasks.Remove(task);
        }

        public string Alarm(ToDo task)
        {
            return string.Format("You need to perform the following task in relation to\n {0} {1} \n {2}", this.GetType().Name, this.Name, task.TaskContent.ToUpper());
        }
    }
}
