using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GarageManagementSystem
{
    // TODO: Fix the properties in the repair class. Implement the IPricable interface
    public class Repair : IPricable
    {
        private int caption;
        private int guarantee;
        private int employee;
        private int service;
        private int exchangedParts;
        private int date;
    
        public int Price
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        decimal IPricable.Price
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }


        public decimal CalculateMargin()
        {
            throw new NotImplementedException();
        }
    }
}
