namespace GarageManagementSystem
{
    using System;
    using System.Linq;

    public abstract class Person : IContactable
    {
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
    }
}
