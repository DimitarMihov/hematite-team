using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General
{
    public class CarInsurance : AutoService
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public void RemindForExpiration() 
        {
            // TODO: Implement the RemindForExpiration() method
        }
    }
}
