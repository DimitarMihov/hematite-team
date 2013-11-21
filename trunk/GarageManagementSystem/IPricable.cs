using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GarageManagementSystem
{
    public interface IPricable
    {
        int Price
        {
            get;
            set;
        }
    }
}
