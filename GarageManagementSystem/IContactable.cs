using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GarageManagementSystem
{
    public interface IContactable
    {
        int Address
        {
            get;
            set;
        }

        int MobilePhone
        {
            get;
            set;
        }

        int Email
        {
            get;
            set;
        }

        int Name
        {
            get;
            set;
        }
    }
}
