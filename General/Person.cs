using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General
{
    public abstract class Stakeholder : IContactable
    {
        public Stakeholder(string name, string email, string phoneNumber)
        {
            // TODO: Implement validation
            this.Name = name;
            this.Email = email;
            this.PhoneNumber = phoneNumber;
        }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        // TODO: Decide if the methods should be implemented here and defined as virtual, instead of abstract
        public virtual void SendEmail(string message)
        {
            // TODO: Implement the SendEmail(string) method
        }

        public virtual void SendSMS(string message)
        {
            // TODO: Implement the SendSMS(string) method
        }
    }
}
