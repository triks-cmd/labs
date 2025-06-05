namespace AeroflotFlights
{
    /// <summary>
    /// Manager class for loading, filtering, and exporting flights.
    /// </summary>
    public class FlightManager
    {
        // Constants for file paths
        private const string InputFilePath = "flights.txt";
        private const string OutputFileNameFormat = "output_{0}.txt";

        private readonly List<Flight> _flights;

        /// <summary>
        /// Constructor: loads flights from the input file into the list.
        /// </summary>
        public FlightManager()
        {
            _flights = new List<Flight>();
            LoadFlightsFromFile();
        }

        /// <summary>
        /// Reads flight data from the input file and populates the _flights list.
        /// </summary>
        private void LoadFlightsFromFile()
        {
            if (!File.Exists(InputFilePath))
            {
                Console.WriteLine("Input file not found: " + InputFilePath);
                return;
            }

            using (StreamReader reader = new StreamReader(InputFilePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }

                    string[] parts = line.Split(';');
                    if (parts.Length != 5)
                    {
                        Console.WriteLine("Skipping invalid line: " + line);
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
                        _flights.Add(flight);
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine("Error parsing line, skipping: " + ex.Message);
                    }
                }
            }
        }

        /// <summary>
        /// Prints all flights of a given plane type, sorted by flight number.
        /// </summary>
        /// <param name="type">The PlaneType to filter by.</param>
        public void PrintFlightsByType(PlaneType type)
        {
            // Filter flights by chosen type
            List<Flight> filteredFlights = new List<Flight>();
            foreach (Flight flight in _flights)
            {
                if (flight.Type == type)
                {
                    filteredFlights.Add(flight);
                }
            }

            // Sort by FlightNumber
            filteredFlights.Sort(
                (a, b) => string.Compare(
                    a.FlightNumber,
                    b.FlightNumber,
                    StringComparison.Ordinal));

            if (filteredFlights.Count == 0)
            {
                Console.WriteLine("No flights found for this type.");
                return;
            }

            Console.WriteLine($"{"No.",-5} {"Time",-5} {"Date",-10} {"Days"}");
            Console.WriteLine("---------------------------------");
            foreach (Flight flight in filteredFlights)
            {
                Console.WriteLine(flight.GetInfoString());
            }
        }

        /// <summary>
        /// Exports flights to the specified destination into a text file.
        /// </summary>
        /// <param name="destinationInput">Destination to filter by (case‐insensitive).</param>
        public void ExportFlightsByDestination(string destinationInput)
        {
            // Filter flights by destination (case‐insensitive)
            List<Flight> destinationFlights = new List<Flight>();
            foreach (Flight flight in _flights)
            {
                if (string.Equals(
                        flight.Destination,
                        destinationInput,
                        StringComparison.OrdinalIgnoreCase))
                {
                    destinationFlights.Add(flight);
                }
            }

            if (destinationFlights.Count == 0)
            {
                Console.WriteLine("No flights found for the specified destination.");
                return;
            }

            // Group flights by PlaneType
            Dictionary<PlaneType, List<Flight>> groupedByType =
                new Dictionary<PlaneType, List<Flight>>();

            foreach (Flight flight in destinationFlights)
            {
                PlaneType typeKey = flight.Type;
                if (!groupedByType.ContainsKey(typeKey))
                {
                    groupedByType[typeKey] = new List<Flight>();
                }
                groupedByType[typeKey].Add(flight);
            }

            // Get sorted list of plane types
            List<PlaneType> planeTypeKeys = new List<PlaneType>(groupedByType.Keys);
            planeTypeKeys.Sort();

            // Create output file name
            string safeDestination = destinationInput.Replace(" ", "_").ToLower();
            string outputFileName = string.Format(OutputFileNameFormat, safeDestination);

            using (StreamWriter writer = new StreamWriter(outputFileName))
            {
                writer.WriteLine("Destination: " + destinationInput);
                writer.WriteLine();

                foreach (PlaneType typeKey in planeTypeKeys)
                {
                    writer.WriteLine(typeKey.ToString());
                    writer.WriteLine();
                    writer.WriteLine("No.   Time   Date       Days Until Departure");
                    writer.WriteLine("------------------------------------------");

                    List<Flight> listForType = groupedByType[typeKey];

                    // Sort this list by FlightNumber
                    listForType.Sort(
                        (a, b) => string.Compare(
                            a.FlightNumber,
                            b.FlightNumber,
                            StringComparison.Ordinal));

                    foreach (Flight flight in listForType)
                    {
                        writer.WriteLine(flight.GetInfoString());
                    }

                    writer.WriteLine();
                }
            }

            Console.WriteLine($"Export completed. File saved as: {outputFileName}");
        }
    }
}
