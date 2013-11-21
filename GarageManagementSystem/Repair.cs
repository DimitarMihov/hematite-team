using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GarageManagementSystem
{
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
    }
}
