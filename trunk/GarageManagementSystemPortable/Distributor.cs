namespace GarageManagementSystem
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Distributor : Person
    {
        public Distributor(string name, string phone, string email) 
        {
            this.Name = name;
            this.Phone = phone;
            this.Email = email;
        }

        public List<Part> Part { get; set; }
    }
}
