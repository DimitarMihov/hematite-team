namespace GarageManagementSystem
{
    using System;
    
    public class InvalidSalaryException : ArgumentException
    {
         public InvalidSalaryException() : base() { }
         public InvalidSalaryException(string message) : base(message) { }
    }
}
