using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General
{
    public interface IContactable
    {
        void SendEmail();
        void SendSMS();
    }
}
