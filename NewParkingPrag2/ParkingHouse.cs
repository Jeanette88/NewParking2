using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewParkingPrag2
{
    class ParkingHouse
    {
        public List<ParkingSpot> parkingSpots { get; set; }


        public static void InitParkingHouse()
        {
            Utils parkingConfig = Utils.ParkingConfig();

            List<dynamic> initialParkingSpots = new();

            for (int i = 0; i < parkingConfig.NrOfSpots; i++)
            {
                ParkingSpot pSpot = new();
                pSpot.Nr = i + 1;
                pSpot.Size1 = parkingConfig.SpotsSize;
                pSpot.AvailableSize = parkingConfig.SpotsSize;
                pSpot.Vehicles = new List<Vehicle>();

                initialParkingSpots.Add(pSpot);
            }


            string parkingData = JsonConvert.SerializeObject(initialParkingSpots, Formatting.Indented);
            File.WriteAllText(ParkingSpot.ParkingSpotsPath(), parkingData);

        }


        public static void SaveAndChange()
        {
            List<ParkingSpot> spots = ParkingSpot.ParkingSpotData();

            Utils parkingConfig = Utils.ParkingConfig();

            foreach (ParkingSpot spot in spots)
            {
                if (spot.Vehicles.Count == 0 && parkingConfig.SpotsSize <= spot.Size1)
                {
                    spot.Size1 = parkingConfig.SpotsSize;
                    spot.AvailableSize = parkingConfig.SpotsSize;
                }

            }

            if (parkingConfig.NrOfSpots < spots.Count)
            {
                for (int i = parkingConfig.NrOfSpots; i < spots.Count; i++)
                {

                    if (spots[i].Vehicles.Count != 0)
                    {
                        Console.WriteLine("Cannot remove spots where vehicles are parked!");
                        Console.ReadLine();
                        Console.Clear();
                        return;
                    }

                    if (spots[i].Vehicles.Count == 0)
                    {
                        spots.RemoveAt(i--);
                    }
                }
            }

            int diff = parkingConfig.CarPrice - spots.Count;

            if (parkingConfig.NrOfSpots > spots.Count)
            {
                for (int i = 0; i > diff; i++)
                {
                    spots.Add(new ParkingSpot
                    {
                        Nr = i + 1,
                        Size1 = parkingConfig.SpotsSize,
                        AvailableSize = parkingConfig.SpotsSize,
                        Vehicles = new List<Vehicle>()
                    });

                }
            }
                
               
            for (int i = 0; i < spots.Count; i++)
            {
                spots[i].Nr = i + 1;
            }
            Console.WriteLine("\nSettings are now changes.");
            Console.ReadKey();
            Console.Clear();
            Utils.SaveToFile(spots);

        }
    }
}

