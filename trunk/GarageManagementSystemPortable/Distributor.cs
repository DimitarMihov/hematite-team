namespace GarageManagementSystem
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    public class Distributor : Person
    {
        public Distributor(string name, string phone, string email)
        {
            this.Name = name;
            this.Phone = phone;
            this.Email = email;
            this.Parts = new List<Part>();
        }

        public Distributor(string name, Address address, string phone, string email, string comment, List<Part> parts)
            : base(name, address, phone, email, comment)
        {
            this.Parts = parts;
        }

        public Distributor() 
        {
        }

        public List<Part> Parts { get; set; }

        public static string SaveDistributorInformation(Distributor distributor)
        {
            StringBuilder builder = new StringBuilder();

            var assembly = Assembly.GetExecutingAssembly();

            var distibutorProperties = assembly.GetType("GarageManagementSystem.Distributor").GetProperties();

            foreach (var property in distibutorProperties)
            {
                if (property.Name == "Address")
                {
                    builder.AppendLine("Address");
                    if (distributor.Address == null)
                    {
                        builder.AppendLine("-");
                    }
                    else
                    {
                        builder.AppendLine("1");
                        builder.Append(Address.SaveAddressInformation(distributor.Address));
                    }
                }
                else if (property.Name == "Parts")
                {
                    dynamic partList = property.GetValue(distributor, null);
                    builder.AppendLine("Parts");
                    builder.AppendLine(partList.Count.ToString());
                    foreach (var part in partList)
                    {
                        builder.Append(Part.SavePartInformation(part));
                    }
                }
                else if (property.Name == "Tasks")
                {
                    // TODO: Implement saving tasks in Distributor class
                }
                else
                {
                    builder.AppendLine(property.Name);
                    try
                    {
                        builder.AppendLine(property.GetValue(distributor, null).ToString());
                    }
                    catch (NullReferenceException)
                    {
                        builder.AppendLine("-");
                    }
                }
            }

            return builder.ToString();
        }

        public static Distributor LoadDistributorInformation(string[] lines, ref int index)
        {
            Distributor distributor = new Distributor();
            var assembly = Assembly.GetExecutingAssembly();
            var userType = assembly.GetType("GarageManagementSystem.Distributor");
            int propertiesCount = Service.PropertiesCount(distributor);

            for (int i = 0; i < propertiesCount; i++, index++)
            {
                var property = userType.GetProperty(lines[index]);

                if (property.Name == "Address")
                {
                    index++;

                    if (lines[index] != "-")
                    {
                        index++;
                        Address address = Address.LoadAddressInformation(lines, ref index);
                        property.SetValue(distributor, address, null);
                    }
                }
                else if (property.Name == "Parts")
                {
                    List<Part> parts = new List<Part>();

                    int stopPoint = int.Parse(lines[index + 1]);
                    index += 2;

                    for (int element = 0; element < stopPoint; element++)
                    {
                        parts.Add(Part.LoadPartInformation(lines, ref index));
                    }

                    index--;
                    property.SetValue(distributor, parts, null);
                }
                else if (property.Name == "Tasks")
                {
                    // TODO: Implement loading tasks in Distributor class
                }
                else
                {
                    index++;

                    if (lines[index] != "-")
                    {
                        var currentPropertyType = property.PropertyType;
                        var convertedValue = Convert.ChangeType(lines[index], currentPropertyType, null);
                        property.SetValue(distributor, convertedValue, null);
                    }
                }
            }

            return distributor;
        }

        //public List<ToDo> Tasks
        //{
        //    get { return this.Tasks; }
        //}

        //public ToDo GetTaskByIndex(int toDoIndex)
        //{
        //    return this.Tasks[toDoIndex];
        //}

        //public void AddTask(ToDo task)
        //{
        //    this.Tasks.Add(task);
        //}

        //public void RemoveTask(ToDo task)
        //{
        //    this.Tasks.Remove(task);
        //}

        //public string Alarm(ToDo task)
        //{
        //    return string.Format("You need to perform the following task in relation to\n distributor {0} ({1}) \n {2}", this.Name, this.Email, task.TaskContent.ToUpper());
        //}
    }
}
