namespace GarageManagementSystem
{
    using System;
    using System.Linq;

    public interface IContactable
    {
        string Name { get; set; }

        Address Address { get; set; }

        string Phone { get; set; }

        string Email { get; set; }

        string Comment { get; set; }

        void SendSms();

        void SendEmail();
    }
}
