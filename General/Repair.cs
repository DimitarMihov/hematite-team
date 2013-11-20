using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General
{
    public class Repair : AutoService
    {
        public List<Part> RepairedParts { get; set; }
        public decimal Cost { get; set; }
        public DateTime TimeWorkedOnRepair { get; private set; }

        public void CalculatedRepairCost()
        {
            // TODO: Implement the CalculatedRepairCost() method
        }

        public void AddTime(double hoursWorked)
        {
            this.TimeWorkedOnRepair.AddHours(hoursWorked);
        }
    }
}
