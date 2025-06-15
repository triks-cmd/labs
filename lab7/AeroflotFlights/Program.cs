using System;

namespace AeroflotFlights
{
    /// <summary>
    /// Основной класс запуска приложения.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// Точка входа в программу.
        /// </summary>
        private static void Main()
        {
            FlightManager manager = new FlightManager();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Aeroflot Flight Manager ===");
                Console.WriteLine();

                PlaneType[] types = Enum.GetValues<PlaneType>();

                for (int i = 0; i < types.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {types[i]}");
                }

                Console.WriteLine("0. Exit");
                Console.WriteLine();
                Console.Write("Select plane type number: ");

                string input = Console.ReadLine();
                bool isParsed = int.TryParse(input, out int choice);

                if (!isParsed)
                {
                    Console.WriteLine("Invalid input. Press any key to retry...");
                    Console.ReadKey();
                    continue;
                }

                if (choice == 0)
                {
                    break;
                }

                if (choice < 1 || choice > types.Length)
                {
                    Console.WriteLine("Invalid choice. Press any key to retry...");
                    Console.ReadKey();
                    continue;
                }

                PlaneType selectedType = types[choice - 1];

                Console.Clear();
                Console.WriteLine($"Flights of type {selectedType}:");
                Console.WriteLine();
                manager.PrintFlightsByType(selectedType);

                Console.WriteLine();
                Console.WriteLine("Press any key to export by destination or return...");
                Console.ReadKey();

                Console.Clear();
                Console.Write("Enter destination to export flights (leave empty to skip): ");
                string destinationInput = Console.ReadLine()?.Trim();

                if (!string.IsNullOrEmpty(destinationInput))
                {
                    manager.ExportFlightsByDestination(destinationInput);
                }

                Console.WriteLine();
                Console.WriteLine("Press any key to return to main menu...");
                Console.ReadKey();
            }

            Console.WriteLine("Exiting. Goodbye!");
        }
    }
}