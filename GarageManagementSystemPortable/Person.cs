namespace GarageManagementSystem
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    public abstract class Person : IContactable
    {
        public Person() { }

        public string Name { get; set; }
        public Address Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Comment { get; set; }

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


        public void SendSms()
        {
            // TODO: Implement the SendSms() method
        }

        public void SendEmail()
        {
            // TODO: Implement the SendEmail() method
        }

        public static string SaveOwnerInformation(Person owner)
        {
            StringBuilder builder = new StringBuilder();

            var assembly = Assembly.GetExecutingAssembly();

            var ownerProperties = assembly.GetType("GarageManagementSystem.Owner").GetProperties();

            foreach (var property in ownerProperties)
            {
                if (property.GetValue(owner, null).ToString() == "GarageManagementSystem.Address")
                {
                    builder.AppendLine("Address");
                    builder.AppendLine("1");
                    builder.Append(Address.SaveAddressInformation(owner.Address));
                }
                else
                {
                    builder.AppendLine(property.Name);
                    builder.AppendLine(property.GetValue(owner, null).ToString());
                }
            }

            return builder.ToString();
        }

    }
}
