using System;

namespace AeroflotFlights
{
    /// <summary>
    /// Main program class (no menu; sequential prompts).
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Main entry point: 
        /// 1) Prompt for plane type → show flights.
        /// 2) Prompt for destination → export flights.
        /// </summary>
        private static void Main()
        {
            FlightManager manager = new FlightManager();

            PlaneType[] planeTypes = (PlaneType[])Enum.GetValues(typeof(PlaneType));
            int selectedIndex = -1;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Available plane types:");
                Console.WriteLine();

                for (int i = 0; i < planeTypes.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {planeTypes[i]}");
                }

                Console.WriteLine();
                Console.Write("Enter the number of the plane type to view its flights: ");
                string input = Console.ReadLine();
                bool parsed = int.TryParse(input, out selectedIndex);

                if (!parsed || selectedIndex < 1 || selectedIndex > planeTypes.Length)
                {
                    Console.WriteLine("Invalid choice. Press any key to retry...");
                    Console.ReadKey();
                    continue;
                }

                selectedIndex--;
                break;
            }

            PlaneType chosenType = planeTypes[selectedIndex];

            Console.Clear();
            Console.WriteLine($"Flights of type {chosenType}:");
            Console.WriteLine();
            manager.PrintFlightsByType(chosenType);

            Console.WriteLine();
            Console.WriteLine("Press any key to proceed to exporting by destination...");
            Console.ReadKey();

            Console.Clear();
            Console.Write("Enter destination to export flights (e.g., Kyiv): ");
            string destinationInput = Console.ReadLine().Trim();

            if (string.IsNullOrWhiteSpace(destinationInput))
            {
                Console.WriteLine("Destination cannot be empty. Exiting.");
                return;
            }

            manager.ExportFlightsByDestination(destinationInput);

            Console.WriteLine();
            Console.WriteLine("Done. Press any key to exit.");
            Console.ReadKey();
        }
    }
}
