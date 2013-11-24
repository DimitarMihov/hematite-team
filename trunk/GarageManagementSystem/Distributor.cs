namespace GarageManagementSystem
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Distributor : Person
    {
        public List<Part> Part { get; set; }
    }
}
