using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace NewParkingPrag2
{
    class Program
    {

        static void Main()
        {

            string menu;

            do
            {
                Console.WriteLine("\n ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("-------------------------------------------------------------");
                Console.WriteLine("--- Welcome to the parking garage at the castle in Prague.---");
                Console.WriteLine("-------------------------------------------------------------");
                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine("\n ");
                Console.WriteLine("Number of parking spaces used ({0}).", Vehicle.ParkingLotSpace());
                Console.WriteLine("\n ");
                Console.WriteLine("(1). Veiw Price. ");
                Console.WriteLine("(2). Add Car.");
                Console.WriteLine("(3). Add Mc.");
                Console.WriteLine("(4). Parking space.");
                Console.WriteLine("(5). Change parking space for Car.");
                Console.WriteLine("(6). Change parking space for Mc.");
                Console.WriteLine("(7). Remove Vehicle / Check Out. "); 
                Console.WriteLine("(8). Change settings. ");    // Ändrar Settings på ParkingJason2
                Console.WriteLine("(9). Reset Settings. ");    // Nollställer ParkingJson1
                Console.WriteLine("(0). Quit.");

                Console.Write("\n Select an option: ");

                menu = Console.ReadLine();
                try
                {

                    switch (menu)
                    {

                        case "1":
                            Utils.ViewPrice();
                            break;

                        case "2":
                            bool noCarsAllowed = Utils.NoCarsAllowed();
                            if (noCarsAllowed) break;
                            Vehicle.ParkVehicle(new Car("", DateTime.Now));
                            break;

                        case "3":
                            bool noMcsAllowed = Utils.NoMcsAllowed();
                            if (noMcsAllowed) break;
                            Vehicle.ParkVehicle(new Mc("", DateTime.Now));
                            break;

                        case "4":
                            ParkingSpot.ParkingSpaceVehicle("");
                            break;

                        case "5":
                            Vehicle.MoveVehicle(new Car("", DateTime.Now));
                            break;

                        case "6":
                            Vehicle.MoveVehicle(new Mc("", DateTime.Now));  
                            break;

                        case "7":
                            Vehicle.RemoveVehicle("");
                            break;

                        case "8":
                            Utils.ChangeSetting();
                            break;

                        case "9":
                            Utils.ResetSettings();
                            break;


                        case "0":
                            Utils.EndProgram();
                            menu = "0";
                            break;

                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Error! ---> Please make a selection between 0 - 9.");
                            break;
                    }
                    Console.ForegroundColor = ConsoleColor.White;
                   
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("... Press a key to continue ...");
                    Console.ReadKey();
                    Main();
                }

            }
            while (menu != "0");
        }
          

    }
}
