using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewParkingPrag2
{
    class Utils
    {
        public int NrOfSpots { get; set; }
        public int SpotsSize { get; set; }
        public int CarPrice { get; set; }
        public int McPrice { get; set; }


        public string[] VehicleTypes { get; set; }

        public static void SaveToFile(List<ParkingSpot> spots)
        {
            string jsonOutput = JsonConvert.SerializeObject(spots, Formatting.Indented);

            File.WriteAllText(ParkingSpot.ParkingSpotsPath(), jsonOutput);

        }

        public static Utils ParkingConfig()
        {
            string config = File.ReadAllText(@"../../../ParkingJson2.json");
            Utils parkingConfig = JsonConvert.DeserializeObject<Utils>(config);
            return parkingConfig;
        }

        public static bool NoCarsAllowed()
        {
            Utils parkingConfig = ParkingConfig();

            if (!parkingConfig.VehicleTypes.Contains("Car"))
            {
                Console.Clear();
                Console.WriteLine("Cars are not allowed!");
                Console.ReadLine();
                Console.Clear();
                return true;
            }
            return false;
        }

        public static bool NoMcsAllowed()
        {
            Utils parkingConfig = ParkingConfig();

            if (!parkingConfig.VehicleTypes.Contains("Mc"))
            {
                Console.Clear();
                Console.WriteLine("Mc are not allowed!");
                Console.ReadLine();
                Console.Clear();
                return true;
            }
            return false;
        }

        public static void ViewPrice()
        {
            Utils parkingConfig = ParkingConfig();

            Console.Clear();
            Console.WriteLine("-----------  Price ----------------");
            Console.WriteLine("\n Price for a Car: {0} CZK per hour\n", parkingConfig.CarPrice);
            Console.WriteLine("\n Price for a Mar: {0} CZK per hour\n", parkingConfig.McPrice);
            Console.WriteLine("-----------------------------------");

            Console.WriteLine("\n\n Press a key to continue!");
            Console.ReadLine();
            Console.Clear();           

        }

        public static void ChangeSetting()
        {

            Console.Clear();
            Console.WriteLine(@"Type ""yes"" to applay:");

            if (Console.ReadLine() == "yes")
            {
                ParkingHouse.SaveAndChange();
            }
           
            Console.Clear();
        }

        public static void ResetSettings()
        {
            Console.Clear();
            Console.WriteLine(@"This will delete all data. Type ""yes"" to reset: ");

            if (Console.ReadLine() == "yes")
            {
                ParkingHouse.InitParkingHouse();
                Console.WriteLine("This will settings were successfully changed!");
                Console.ReadLine();
                
                Console.Clear();
            }
          
        }

        public static bool EndProgram()
        {
            Console.Clear();

            Console.WriteLine(@"Type ""yes"" to end program: ");

            if (Console.ReadLine() == "yes")
            {
                return true;
            }

            Console.WriteLine("\n\n Press a key to continue!");
            Console.ReadLine();
            Console.Clear();
            return false;
        }




    }
}
