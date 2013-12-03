namespace GarageManagementSystem
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Owner : Person
    {
        public Owner(string name, Address address, string phone, string email, string comment)
            : base(name, address, phone, email, comment) { }

        public Owner() { }
        // TODO: Create properties/methods for the owner class
    }
}
