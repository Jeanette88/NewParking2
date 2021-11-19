using System;
using System.Collections.Generic;
using System.Linq;

namespace NewParkingPrag2
{
    public enum VehicleSize
    {
        Car = 4,
        Mc = 2,
        Bus = 16,
        Bike = 1,
    }

    class Vehicle
    {
        public string Type { get; set; }
        public string RegNr { get; set; }
        public int Size { get; set; }
        public DateTime Time { get; set; }

        public Vehicle(string regNr, DateTime time)
        {
            RegNr = regNr;
            Time = time;
        }

        public static int ParkingLotSpace()
        {
            List<ParkingSpot> spots = ParkingSpot.ParkingSpotData();

            int input = 0;
            for (int i = 0; i < spots.Count; i++)
            {
                if (spots[i].AvailableSize != 4)
                    input++;
            }
            Utils.SaveToFile(spots);
            return input;
        }

        public static void ParkVehicle(Vehicle vehicle)
        {
            List<ParkingSpot> spots = ParkingSpot.ParkingSpotData();
            Console.WriteLine("Enter reg number:");
            string regNr = Console.ReadLine().ToUpper();

            vehicle.RegNr = regNr;

            int foundSpot = 0;

            if (vehicle.Size == (int)VehicleSize.Car)
            {
                foundSpot = FindCarSpot();
            }
            if (vehicle.Size == (int)VehicleSize.Mc)
            {
                foundSpot = FindMcSpot();
            }

            bool isAlreadyParked = ParkingSpot.CheckIfIsAlreadyParked(regNr);

            if (isAlreadyParked)
            {
                Console.WriteLine(" Reg number already exists! ");
            }

            InsertVehicle(foundSpot, vehicle);
        }

        public static bool CheckIfMc(string regNr)
        {
            List<ParkingSpot> spots = ParkingSpot.ParkingSpotData();

            foreach (ParkingSpot spot in spots)
            {
                Vehicle vehicle = spot.Vehicles.Find(x => x.RegNr == regNr);

                if (vehicle is not null)
                {
                    if (vehicle.Size != (int)VehicleSize.Mc)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool CheckIfCar(string regNr)
        {
            List<ParkingSpot> spots = ParkingSpot.ParkingSpotData();

            foreach (ParkingSpot spot in spots)
            {
                Vehicle vehicle = spot.Vehicles.Find(x => x.RegNr == regNr);

                if (vehicle is not null)
                {
                    if (vehicle.Size != (int)VehicleSize.Car)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static int FindCarSpot()
        {
            List<ParkingSpot> spots = ParkingSpot.ParkingSpotData();

            ParkingSpot spot = spots.First(x => x.AvailableSize >= 4);
            int foundSpot = spot.Nr;

            return foundSpot;
        }

        public static int FindMcSpot()
        {
            List<ParkingSpot> spots = ParkingSpot.ParkingSpotData();

            ParkingSpot spot = spots.First(x => x.AvailableSize >= 2);
            int foundSpot = spot.Nr;

            return foundSpot;
        }

        public static void InsertVehicle(int foundSpot, Vehicle vehicle)
        {
            List<ParkingSpot> spots = ParkingSpot.ParkingSpotData();

            foreach (ParkingSpot spot in spots)
            {
                if (spot.Nr == foundSpot)
                {
                    spot.AvailableSize -= vehicle.Size;
                    spot.Vehicles.Add(vehicle);
                    Console.WriteLine("Addad");
                    Utils.SaveToFile(spots);

                }

            }

            ParkingSpot.ParkingTicket(vehicle, foundSpot);

        }

        public static DateTime SaveTime(string regNr)
        {
            List<ParkingSpot> spots = ParkingSpot.ParkingSpotData();
            DateTime saveTime = new();

            foreach (ParkingSpot spot in spots)
            {
                Vehicle vehicle = spot.Vehicles.Find(vehicle => vehicle.RegNr == regNr);

                if (vehicle != null)
                {
                    saveTime = vehicle.Time;
                }
            }
            return saveTime;
        }

        public static void MoveVehicle(Vehicle vehicle)
        {
            List<ParkingSpot> spots = ParkingSpot.ParkingSpotData();
            Console.WriteLine("Enter reg number:");
            string regNr = Console.ReadLine().ToUpper();

            bool isParked = ParkingSpot.CheckParking(regNr);

            if (!isParked)
            {
                Console.WriteLine("The Vechile was not Found");
                return;
            }

            bool isNotCar = CheckIfCar(regNr);
            bool isNotMc = CheckIfMc(regNr);

            if (vehicle.Size == (int)VehicleSize.Car && isNotCar)
            {
                Console.WriteLine("Choose a Car!");
                Console.WriteLine("\n\n Press a key to continue!");
                Console.ReadKey();
                Console.Clear();
                return;
            }
            if (vehicle.Size == (int)VehicleSize.Mc && isNotMc)
            {
                Console.WriteLine("Choose a Mc!");
                Console.WriteLine("\n\n Press a key to continue!");
                Console.ReadKey();
                Console.Clear();
                return;
            }

            DateTime saveTime = SaveTime(regNr);
            Console.WriteLine(saveTime);

            RemoveVehicle(regNr);

            foreach (ParkingSpot spot in spots)
            {
                vehicle.RegNr = regNr;
                vehicle.Time = saveTime;

                Console.WriteLine("Choose a spot:");
                int chosenSpot = int.Parse(Console.ReadLine());

                bool noSpace = ParkingSpot.CheckSpotSpace(chosenSpot, vehicle.Size);

                if (noSpace)
                {
                    Console.WriteLine("There is not enough space left in this Parkinglot!");
                }
                if (!noSpace)
                {
                    InsertVehicle(chosenSpot, vehicle);
                    return;
                }

            }
        }

        public static void RemoveVehicle(string regNr)
        {
            List<ParkingSpot> spots = ParkingSpot.ParkingSpotData();

            Console.WriteLine("Enter Regnr;");
            regNr = Console.ReadLine().ToUpper();

            bool isParked = ParkingSpot.CheckIfIsAlreadyParked(regNr);

            if (!isParked)
            {
                Console.WriteLine(" The Vehicle was not found.");
                return;
            }

            foreach (ParkingSpot spot in spots)
            {
                Vehicle vehicle = spot.Vehicles.Find(vehicle => vehicle.RegNr == regNr);
            
                if (vehicle is not null)
                {
                    
                    spot.AvailableSize += vehicle.Size;
                    PriceTime(regNr);
                    spot.Vehicles.Remove(vehicle);                   
                    Utils.SaveToFile(spots);
                    Console.WriteLine(" Vehicle removed.");

                    return;
                }
            }
        }

        public static void PriceTime(string regNr)
        {
            List<ParkingSpot> spots = ParkingSpot.ParkingSpotData();
            Utils parkingConfig = Utils.ParkingConfig();

            foreach (ParkingSpot spot in spots)
            {
                Vehicle vehicle = spot.Vehicles.Find(vehicle => vehicle.RegNr == regNr || vehicle.RegNr == regNr + "/");

                if (vehicle is not null)
                {
                    TimeSpan timeSpan = DateTime.Now.Subtract(vehicle.Time);
                    string formatted = timeSpan.ToString(@"dd\.hh\:mm\:ss");
                    double minutes = (DateTime.Now - vehicle.Time).TotalMinutes;

                    double span = Math.Ceiling((DateTime.Now - vehicle.Time).TotalHours);
                    int vehiclePrice = vehicle.Size == (int)VehicleSize.Mc ? parkingConfig.McPrice : parkingConfig.CarPrice;
                    double price = vehiclePrice * span;

                    ParkingSpot.CheckOut(price, minutes, timeSpan);

                }
            }
        }

    }

}


