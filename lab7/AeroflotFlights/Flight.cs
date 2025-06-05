using System;
using System.Globalization;

namespace AeroflotFlights
{
    /// <summary>
    /// Class representing a single flight.
    /// </summary>
    public class Flight
    {
        // Constants for parsing date and time
        private const string DateFormat = "dd.MM.yyyy";
        private const string TimeFormat = "HH:mm";

        /// <summary>
        /// Flight number.
        /// </summary>
        public string FlightNumber { get; }

        /// <summary>
        /// Type of airplane.
        /// </summary>
        public PlaneType Type { get; }

        /// <summary>
        /// Destination city or airport.
        /// </summary>
        public string Destination { get; }

        /// <summary>
        /// Date and time of departure.
        /// </summary>
        public DateTime DepartureDateTime { get; }

        /// <summary>
        /// Days remaining until departure (0 if departure date is in the past).
        /// </summary>
        public int DaysUntilDeparture
        {
            get
            {
                DateTime todayDate = DateTime.Now.Date;
                TimeSpan difference = DepartureDateTime.Date - todayDate;
                if (difference.Days >= 0)
                {
                    return difference.Days;
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// Constructor for Flight. Parses strings into appropriate types.
        /// </summary>
        /// <param name="flightNumber">Flight number string.</param>
        /// <param name="planeTypeString">Plane type string that must match PlaneType enum.</param>
        /// <param name="destination">Destination string.</param>
        /// <param name="dateString">Departure date string in dd.MM.yyyy format.</param>
        /// <param name="timeString">Departure time string in HH:mm format.</param>
        public Flight(
            string flightNumber,
            string planeTypeString,
            string destination,
            string dateString,
            string timeString)
        {
            FlightNumber = flightNumber.Trim();

            // Parse planeTypeString into PlaneType enum
            PlaneType parsedType;
            bool parsedEnum = Enum.TryParse(planeTypeString.Trim(), true, out parsedType);
            if (!parsedEnum)
            {
                throw new ArgumentException("Invalid plane type: " + planeTypeString);
            }
            Type = parsedType;

            Destination = destination.Trim();

            // Parse date part
            DateTime datePart;
            bool parsedDate = DateTime.TryParseExact(
                dateString.Trim(),
                DateFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out datePart);

            // Parse time part
            DateTime timePart;
            bool parsedTime = DateTime.TryParseExact(
                timeString.Trim(),
                TimeFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out timePart);

            if (!parsedDate || !parsedTime)
            {
                throw new ArgumentException(
                    "Invalid date or time format: " + dateString + " " + timeString);
            }

            // Combine date and time into a single DateTime
            DepartureDateTime = new DateTime(
                datePart.Year,
                datePart.Month,
                datePart.Day,
                timePart.Hour,
                timePart.Minute,
                0);
        }

        /// <summary>
        /// Returns a formatted string with flight information.
        /// </summary>
        public string GetInfoString()
        {
            string timeString = DepartureDateTime.ToString(TimeFormat);
            string dateString = DepartureDateTime.ToString(DateFormat);
            return $"{FlightNumber,-5} {timeString,-5} {dateString,-10} {DaysUntilDeparture}";
        }
    }
}
