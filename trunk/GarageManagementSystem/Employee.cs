﻿namespace GarageManagementSystem
{
    using System;
    using System.Linq;

    public class Employee : Person
    {
        public decimal Salary { get; set; }
        public Position Position { get; set; }
        public int Rank { get; set; }

        public Employee(string phone, decimal salary, Position position, int rank)
            : base(phone)
        {
            this.Salary = salary;
            this.Position = position;
            this.Rank = rank;
        }
    }
}