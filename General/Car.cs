using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace General
{
    public class Car
    {
        // TODO: Implement the STATE design patter for the cars (i.e. inRepair, Repaired etc)
        public Stakeholder ContactPerson { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public DateTime YearOfProduction { get; set; }
    }
}
