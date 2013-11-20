using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General
{
    public class Client : Stakeholder
    {
        public List<Car> CarsOwned { get; set; }

        public void AddCar(Car car)
        {
            this.CarsOwned.Add(car);
        }

        public void RemoveCar(Car car)
        {
            this.CarsOwned.Remove(car);
        }
    }
}
