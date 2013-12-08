namespace GarageManagementSystem
{
    using System;
    using System.Linq;

    public class Owner : Person
    {
        public Owner()
        {
        }

        public Owner(string name, Address address, string phone, string email, string comment)
            : base(name, address, phone, email, comment)
        {
        }

        // TODO: Create properties/methods for the owner class
    }
}
