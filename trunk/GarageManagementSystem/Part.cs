﻿namespace GarageManagementSystem
{
    using System;
    using System.Collections.Generic;

    public class Part : IPricable
    {
        public long Id { get; set; }

        public string Name { get; set; }

        // This parts is approved for this List of Vehicle
        public List<Vehicle> VehicleList { get; private set; }

        public decimal Price { get; set; } // Appreciation exist ?

        // Distributor or Owner if the part is purchased from owner
        public Person Provider { get; set; }

        public Part(long id, string name, List<Vehicle> vehicleList = new List<Vehicle>(), decimal price, Person provider)
        {
            this.Id = id;
            this.Name = name;
            this.VehicleList = vehicleList;
            this.Price = price;
            this.Provider = provider;
        }
    }
}
