using System;
using System.Collections.Generic;
using System.IO;

namespace AeroflotFlights
{
    /// <summary>
    /// Управление загрузкой, фильтрацией и экспортом рейсов.
    /// </summary>
    public class FlightManager
    {
        private const string InputFilePath = "flights.txt";
        private const string OutputFilePattern = "output_{0}.txt";
        private const char Delimiter = ';';
        private const int ExpectedFieldCount = 5;
        private const int TableSeparatorLength = 35;
        private const int ExportSeparatorLength = 42;

        private readonly List<Flight> flights;

        /// <summary>
        /// Конструктор: загружает рейсы из файла.
        /// </summary>
        public FlightManager()
        {
            flights = new List<Flight>();
            LoadFlightsFromFile();
        }

        /// <summary>
        /// Читает входной файл и создает список рейсов.
        /// </summary>
        private void LoadFlightsFromFile()
        {
            if (!File.Exists(InputFilePath))
            {
                Console.WriteLine($"Input file not found: {InputFilePath}");
                return;
            }

            foreach (string line in File.ReadLines(InputFilePath))
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                string[] parts = line.Split(Delimiter);

                if (parts.Length != ExpectedFieldCount)
                {
                    Console.WriteLine($"Skipping invalid line: {line}");
                    continue;
                }

                try
                {
                    Flight flight = new Flight(
                        parts[0],
                        parts[1],
                        parts[2],
                        parts[3],
                        parts[4]);

                    flights.Add(flight);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Error parsing line: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Выводит рейсы заданного типа, отсортированные по номеру.
        /// </summary>
        public void PrintFlightsByType(PlaneType type)
        {
            List<Flight> filtered = flights.FindAll(
                f => f.Type == type);

            filtered.Sort((a, b) => string.Compare(
                a.FlightNumber,
                b.FlightNumber,
                StringComparison.Ordinal));

            if (filtered.Count == 0)
            {
                Console.WriteLine("No flights found.");
                return;
            }

            Console.WriteLine("No.   Time   Date       Days");
            Console.WriteLine(new string('-', TableSeparatorLength));

            foreach (Flight flight in filtered)
            {
                Console.WriteLine(flight.GetInfoString());
            }
        }

        /// <summary>
        /// Экспортирует рейсы по пункту назначения.
        /// </summary>
        public void ExportFlightsByDestination(string destination)
        {
            List<Flight> destinationFlights = flights.FindAll(
                f => f.Destination.Equals(
                    destination,
                    StringComparison.OrdinalIgnoreCase));

            if (destinationFlights.Count == 0)
            {
                Console.WriteLine("No flights found for the specified destination.");
                return;
            }

            SortedDictionary<PlaneType, List<Flight>> grouped = new SortedDictionary<PlaneType, List<Flight>>();

            foreach (Flight flight in destinationFlights)
            {
                if (!grouped.ContainsKey(flight.Type))
                {
                    grouped[flight.Type] = new List<Flight>();
                }

                grouped[flight.Type].Add(flight);
            }

            string safeDest = destination.Replace(' ', '_').ToLowerInvariant();
            string outputFileName = string.Format(
                OutputFilePattern,
                safeDest);

            using StreamWriter writer = new StreamWriter(outputFileName);
            writer.WriteLine($"Destination: {destination}");
            writer.WriteLine();

            foreach (KeyValuePair<PlaneType, List<Flight>> kvp in grouped)
            {
                writer.WriteLine(kvp.Key);
                writer.WriteLine();
                writer.WriteLine("No.   Time   Date       Days Until Departure");
                writer.WriteLine(new string('-', ExportSeparatorLength));

                kvp.Value.Sort((a, b) => string.Compare(
                    a.FlightNumber,
                    b.FlightNumber,
                    StringComparison.Ordinal));

                foreach (Flight flight in kvp.Value)
                {
                    writer.WriteLine(flight.GetInfoString());
                }

                writer.WriteLine();
            }

            Console.WriteLine($"Export completed. File saved as: {outputFileName}");
        }
    }
}