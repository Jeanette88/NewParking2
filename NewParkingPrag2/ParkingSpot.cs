using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewParkingPrag2
{
    class ParkingSpot
    {
        public int Nr { get; set; }
        public int Size1 { get; set; }
        public int AvailableSize { get; set; }
        public List<Vehicle> Vehicles { get; set; }

        public static string ParkingSpotsPath()
        {
            return @"../../../ParkingJson1.json";
        }

        public static List<ParkingSpot> ParkingSpotData()
        {
            string file = File.ReadAllText(ParkingSpotsPath());
            List<ParkingSpot> spots = JsonConvert.DeserializeObject<List<ParkingSpot>>(file);
            return spots;
        }

        public static bool CheckSpotSpace(int spot, int size)
        {
            List<ParkingSpot> spots = ParkingSpotData();
            if (spots[spot - 1].AvailableSize < size)
            {
                return true;
            }

            return false;
        }

        public static bool CheckParking(string regNr)
        {
            List<ParkingSpot> spots = ParkingSpotData();

            foreach (ParkingSpot spot in spots)
            {
                Vehicle vehicle = spot.Vehicles.Find(x => x.RegNr == regNr);

                if (vehicle != null)
                {
                    return true;
                }
            }
            return false;

        }

        public static bool CheckIfIsAlreadyParked(string regNr)
        {
            List<ParkingSpot> spots = ParkingSpot.ParkingSpotData();

            foreach (ParkingSpot spot in spots)
            {
                Vehicle vehicle = spot.Vehicles.Find(x => x.RegNr == regNr);

                if (vehicle != null)
                {
                    return true;
                }
            }
            return false;
        }

        public static string EmptyParkingLot(List<ParkingSpot> spots, int index)
        {
            string empty = "";

            string prefix = index + 1 < 10 ? "000" : index + 1 < 100 ? "00" : index + 1 < 1000 ? "0" : "";

            if (spots[index].Vehicles.Count == 0)
            {
                empty = $"[{prefix}{index + 1}) .........  Emtpy  .......]";

                if ((index + 1) % 3 == 0)
                {
                    empty = empty + "\n";
                }
            }
            return empty;

        }

        public static int DiffCounter(List<ParkingSpot> spots, int index)
        {
            int diff = 0;
            
            if (spots[index].Vehicles.Count == 2)
            {
                diff = 20 - (spots[index].Vehicles[0].RegNr.Length + spots[index].Vehicles[1].RegNr.Length); 
            }
            else
            {
                diff = 21 - spots[index].Vehicles[0].RegNr.Length;
            }
            return diff;
        }

        public static string TypeParkingLot(List<ParkingSpot> spots, int index, Vehicle vehicle)
        {
            string print = "";
            string prefix = index + 1 < 10 ? "000" : index + 1 < 100 ? "00" : index + 1 < 1000 ? "0" : "";
            

            if (spots[index].Vehicles.Count == 2)
            {
                print = $"[{prefix}{index + 1}) {vehicle.Type}: {spots[index].Vehicles[0].RegNr}/{spots[index].Vehicles[1].RegNr}";
               
            }
            else
            {
                print = $"[{prefix}{index + 1}) {vehicle.Type}:{spots[index].Vehicles[0].RegNr}";
            }

            return print;
        }

        public static void ParkingSpaceVehicle(string regNr)

        {
            Console.Clear();
            List<ParkingSpot> spots = ParkingSpot.ParkingSpotData();


            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" CAR");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" MC");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" EMPTY");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("");

            Console.WriteLine("----------------------------------- PARKING SPACE --------------------------------------------------");
            Console.WriteLine("");
            Console.WriteLine("");

            for (int i = 0; i < spots.Count; i++)
            {
                string print = "";

                foreach (Vehicle vehicle in spots[i].Vehicles)
                {
                    int diff = DiffCounter(spots, i);
                    print = TypeParkingLot(spots, i, vehicle);

                    string space = ".";

                    for (int j = 0; j < diff; j++)
                    {
                        print = print + space;
                    }

                    print = print + "]";

                    if ((i + 1) % 3 == 0)
                    {
                        print = print + "\n";
                    }

                }
                string empty = EmptyParkingLot(spots, i);

                ParkingColor(empty, spots[i].AvailableSize, i);
                ParkingColor(print, spots[i].AvailableSize, i);
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("");
            Console.WriteLine("-------------------------------------------------------------------------------------------------------");
            Console.WriteLine("");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n\n Press a key to continue!");
            Console.ReadKey();
            Console.Clear();
            Utils.SaveToFile(spots);

        }

        public static void ParkingTicket(Vehicle vehicle, int spot)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("-----------------------------");
            Console.WriteLine("Vehicle type: {0}\n", vehicle.Type);
            Console.WriteLine("Your reg number: {0}\n", vehicle.RegNr);
            Console.WriteLine("Spot: {0}\n", spot);
            Console.WriteLine("Time: {0}\n", vehicle.Time);
            Console.WriteLine("-----------------------------");
            Console.ReadLine();
            Console.Clear();
        }
       
        public static void CheckOut(double price, double minutes, TimeSpan time)
        {
   

            string parkingText = minutes <= 10 ? "Free Parking." : $"Price: {price} CZK.";

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\t---------------------------------------------");
            Console.WriteLine("\t------------------ TICKET -------------------");
            Console.WriteLine("\t---------------------------------------------");
            Console.WriteLine("\t  You have now gone out of the parking lot.  ");
            Console.WriteLine();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\t        Parking Duation: {0}   ", time);
            Console.WriteLine();
            Console.WriteLine("\t\t" + parkingText);
            Console.WriteLine();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\tThank you for choosing Prague Castle Parking.");
            Console.WriteLine("\t---------------------------------------------");
            Console.WriteLine("\t--------------- Welcome again!---------------");
            Console.WriteLine("\t---------------------------------------------");
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n\n Press a key to continue!");
            Console.ReadKey();
            Console.Clear();

        }

        public static void ParkingColor(string color, int size, int index)
        {
            List<ParkingSpot> spots = ParkingSpotData();

            if (size == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(color);
                Console.ResetColor();
                return;
            }
            if (size == spots[index].Size1)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(color);
                Console.ResetColor();
                return;
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(color);
            Console.ResetColor();
        }


    }

}


