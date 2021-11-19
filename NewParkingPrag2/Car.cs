using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewParkingPrag2
{
    class Car : Vehicle
    {
        public string Type { get; set; }
       
        public Car(string regNr, DateTime time) : base(regNr, time)
        {
            Size = (int)VehicleSize.Car;
            Type = "Car";

        }
      
    }


}

