using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewParkingPrag2
{
    class Mc : Vehicle
    {
        public string Type { get; set; }
        public Mc(string regNr, DateTime time) : base(regNr, time)
        {
            Size = (int)VehicleSize.Mc;
            Type = "Mc";
        }

  

    }   
}
