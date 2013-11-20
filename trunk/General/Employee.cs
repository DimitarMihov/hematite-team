using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace General
{
    public class Employee : Stakeholder
    {
        // TODO: Add more properties to the Employee class
        public string SocialSecurityNumber { get; set; }
        public decimal Salary { get; set; }
        public DateTime StartDate { get; set; }
    }
}
