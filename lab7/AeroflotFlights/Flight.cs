using System;
using System.Globalization;

namespace AeroflotFlights
{
    /// <summary>
    /// Класс, представляющий один рейс.
    /// </summary>
    public class Flight
    {
        /// <summary>Формат даты для разбора и вывода.</summary>
        private const string DateFormat = "dd.MM.yyyy";

        /// <summary>Формат времени для разбора и вывода.</summary>
        private const string TimeFormat = "HH:mm";

        /// <summary>Номер рейса (например, "182").</summary>
        public string FlightNumber { get; }

        /// <summary>Тип самолета (из перечисления PlaneType).</summary>
        public PlaneType Type { get; }

        /// <summary>Пункт назначения рейса (город или аэропорт).</summary>
        public string Destination { get; }

        /// <summary>Дата и время отправления рейса.</summary>
        public DateTime DepartureDateTime { get; }

        /// <summary>
        /// Количество дней до отправления (или 0, если дата прошла).
        /// </summary>
        public int DaysUntilDeparture
        {
            get
            {
                DateTime today = DateTime.Now.Date;
                TimeSpan difference = DepartureDateTime.Date - today;

                if (difference.Days > 0)
                {
                    return difference.Days;
                }

                return 0;
            }
        }

        /// <summary>
        /// Инициализирует новый экземпляр Flight, парся входные строки.
        /// </summary>
        /// <param name="flightNumber">Номер рейса.</param>
        /// <param name="planeTypeString">Строковое представление типа самолета.</param>
        /// <param name="destination">Пункт назначения.</param>
        /// <param name="dateString">Дата отправления (dd.MM.yyyy).</param>
        /// <param name="timeString">Время отправления (HH:mm).</param>
        /// <exception cref="ArgumentNullException">Если обязательный параметр пустой.</exception>
        /// <exception cref="ArgumentException">Если неверный формат типа, даты или времени.</exception>
        public Flight(
            string flightNumber,
            string planeTypeString,
            string destination,
            string dateString,
            string timeString)
        {
            if (string.IsNullOrWhiteSpace(flightNumber))
            {
                throw new ArgumentNullException(nameof(flightNumber));
            }

            FlightNumber = flightNumber.Trim();

            bool parsedType = Enum.TryParse(
                planeTypeString?.Trim(),
                ignoreCase: true,
                result: out PlaneType parsed);

            if (!parsedType)
            {
                throw new ArgumentException(
                    $"Invalid plane type: '{planeTypeString}'",
                    nameof(planeTypeString));
            }

            Type = parsed;

            if (string.IsNullOrWhiteSpace(destination))
            {
                throw new ArgumentNullException(nameof(destination));
            }

            Destination = destination.Trim();

            bool parsedDate = DateTime.TryParseExact(
                dateString?.Trim(),
                DateFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out DateTime datePart);

            bool parsedTime = DateTime.TryParseExact(
                timeString?.Trim(),
                TimeFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out DateTime timePart);

            if (!parsedDate || !parsedTime)
            {
                throw new ArgumentException(
                    $"Invalid date or time format: '{dateString} {timeString}'");
            }

            DepartureDateTime = new DateTime(
                datePart.Year,
                datePart.Month,
                datePart.Day,
                timePart.Hour,
                timePart.Minute,
                0);
        }

        /// <summary>
        /// Возвращает строку с информацией о рейсе для вывода.
        /// </summary>
        /// <returns>Строка: номер, время, дата, дни до вылета.</returns>
        public string GetInfoString()
        {
            string time = DepartureDateTime.ToString(TimeFormat);
            string date = DepartureDateTime.ToString(DateFormat);

            return string.Format(
                "{0,-5} {1,-5} {2,-10} {3}",
                FlightNumber,
                time,
                date,
                DaysUntilDeparture);
        }
    }
}
