using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MagazineApp
{
    /// <summary>
    /// Главный класс приложения для управления подписками на журналы.
    /// </summary>
    class Program
    {
        private const string PathMags = "magazines.txt";
        private const string PathSubs = "subscribers.txt";

        /// <summary>
        /// Событие для оповещения об изменении данных.
        /// </summary>
        public static event Action DataChanged;

        private List<Magazine> _mags;
        private List<Subscriber> _subs;

        /// <summary>
        /// Точка входа в приложение.
        /// </summary>
        static void Main()
        {
            var program = new Program();
            program.Run();
        }

        /// <summary>
        /// Основной метод выполнения программы.
        /// </summary>
        private void Run()
        {
            LoadData();
            DataChanged += ShowLinqInfo;
            ShowMenu();
        }

        /// <summary>
        /// Загружает данные из файлов.
        /// </summary>
        /// <exception cref="FileNotFoundException">Выбрасывается, если файлы не найдены.</exception>
        private void LoadData()
        {
            bool magsExists = File.Exists(PathMags);
            if (!magsExists)
            {
                throw new FileNotFoundException();
            }

            bool subsExists = File.Exists(PathSubs);
            if (!subsExists)
            {
                throw new FileNotFoundException();
            }

            var magazineLines = File.ReadLines(PathMags);
            _mags = magazineLines
                .Select(line => line.Split(';'))
                .Where(parts => parts.Length >= 2)
                .Where(parts => decimal.TryParse(parts[1], out _))
                .Select(parts => new Magazine(
                    parts[0].Trim(),
                    decimal.Parse(parts[1].Trim())))
                .ToList();

            var subscriberLines = File.ReadLines(PathSubs);
            _subs = subscriberLines
                .Select(line => line.Split(';'))
                .Where(parts => parts.Length >= 3)
                .Select(parts =>
                {
                    var rawNames = parts[2].Split(',');
                    var names = rawNames
                        .Select(name => name.Trim())
                        .ToArray();

                    return new Subscriber(
                        parts[0].Trim(),
                        parts[1].Trim(),
                        names);
                })
                .ToList();

            foreach (var subscriber in _subs)
            {
                for (int i = 0; i < subscriber.MagazineNames.Length; i++)
                {
                    string magName = subscriber.MagazineNames[i];
                    var magazine = _mags.FirstOrDefault(m => m.Name == magName);
                    if (magazine == null)
                    {
                        continue;
                    }

                    subscriber.SetMagazinePrice(i, magazine.AnnualPrice);
                    magazine.Subscribe(subscriber);
                }
            }

            DataChanged?.Invoke();
        }

        /// <summary>
        /// Отображает главное меню приложения.
        /// </summary>
        private void ShowMenu()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("1.Show subscribers");
                Console.WriteLine("2.Change name");
                Console.WriteLine("3.Change price");
                Console.WriteLine("4.LINQ Info");
                Console.WriteLine("5.Filter subs");
                Console.WriteLine("6.Exit");
                Console.Write("Select: ");

                var input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        PrintSubscribers();
                        break;
                    case "2":
                        ChangeName();
                        break;
                    case "3":
                        ChangePrice();
                        break;
                    case "4":
                        ShowLinqInfo();
                        break;
                    case "5":
                        FilterMenu();
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Invalid");
                        break;
                }
            }
        }

        /// <summary>
        /// Выводит список подписчиков.
        /// </summary>
        private void PrintSubscribers()
        {
            bool hasSubscribers = _subs.Any();
            if (!hasSubscribers)
            {
                Console.WriteLine("No data");
                return;
            }

            Console.WriteLine("№\tLastName\tAddress\tMagazines\tTotal");
            int index = 1;

            var orderedSubs = _subs.OrderByDescending(s => s.TotalCost);
            foreach (var subscriber in orderedSubs)
            {
                Console.WriteLine($"{index}\t{subscriber}");
                index++;
            }
        }

        /// <summary>
        /// Изменяет название журнала.
        /// </summary>
        private void ChangeName()
        {
            bool hasMags = _mags.Any();
            if (!hasMags)
            {
                Console.WriteLine("No data");
                return;
            }

            for (int i = 0; i < _mags.Count; i++)
            {
                var mag = _mags[i];
                Console.WriteLine($"{i + 1}. {mag.Name} ({mag.AnnualPrice})");
            }

            Console.Write("#: ");
            var input = Console.ReadLine();
            bool parsedIndex = int.TryParse(input, out int selected);
            if (!parsedIndex)
            {
                Console.WriteLine("Invalid");
                return;
            }

            bool validIndex = selected > 0 && selected <= _mags.Count;
            if (!validIndex)
            {
                Console.WriteLine("Invalid");
                return;
            }

            Console.Write("New name: ");
            var newName = Console.ReadLine().Trim();
            _mags[selected - 1].ChangeName(newName);
            DataChanged?.Invoke();
        }

        /// <summary>
        /// Изменяет цену подписки на журнал.
        /// </summary>
        private void ChangePrice()
        {
            bool hasMags = _mags.Any();
            if (!hasMags)
            {
                Console.WriteLine("No data");
                return;
            }

            for (int i = 0; i < _mags.Count; i++)
            {
                var mag = _mags[i];
                Console.WriteLine($"{i + 1}. {mag.Name} ({mag.AnnualPrice})");
            }

            Console.Write("#: ");
            var input = Console.ReadLine();
            bool parsedIndex = int.TryParse(input, out int selected);
            if (!parsedIndex)
            {
                Console.WriteLine("Invalid");
                return;
            }

            bool validIndex = selected > 0 && selected <= _mags.Count;
            if (!validIndex)
            {
                Console.WriteLine("Invalid");
                return;
            }

            Console.Write("New price: ");
            var priceInput = Console.ReadLine();
            bool parsedPrice = decimal.TryParse(priceInput, out var newPrice);
            if (!parsedPrice)
            {
                Console.WriteLine("Invalid");
                return;
            }

            _mags[selected - 1].ChangePrice(newPrice);
            DataChanged?.Invoke();
        }

        /// <summary>
        /// Выводит LINQ-статистику.
        /// </summary>
        private void ShowLinqInfo()
        {
            Console.WriteLine($"Total magazines: {_mags.Count}");
            Console.WriteLine($"Total subscribers: {_subs.Count}");

            var sortedNames = _mags
                .OrderBy(m => m.Name)
                .Select(m => m.Name);
            var joinedNames = string.Join(", ", sortedNames);
            Console.WriteLine($"Sorted by name: {joinedNames}");

            var averageCost = _subs.Average(s => s.TotalCost);
            Console.WriteLine($"Average subscription cost: {averageCost:F2}");
        }

        /// <summary>
        /// Отображает меню фильтрации подписчиков.
        /// </summary>
        private void FilterMenu()
        {
            Console.WriteLine("1.By magazine");
            Console.WriteLine("2.By street");
            Console.WriteLine("3.By cost");
            Console.Write("#: ");

            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.Write("Magazine name: ");
                    var magazineName = Console.ReadLine().Trim();
                    var byMagazine = _subs.Where(s => s.MagazineNames.Contains(magazineName));
                    foreach (var subscriber in byMagazine)
                    {
                        Console.WriteLine(subscriber);
                    }
                    break;
                case "2":
                    Console.Write("Street contains: ");
                    var street = Console.ReadLine().Trim();
                    var byStreet = _subs.Where(s => s.Address.Contains(street));
                    foreach (var subscriber in byStreet)
                    {
                        Console.WriteLine(subscriber);
                    }
                    break;
                case "3":
                    Console.Write("Min total cost: ");
                    var costInput = Console.ReadLine();
                    bool parsedMin = decimal.TryParse(costInput, out var minCost);
                    if (!parsedMin)
                    {
                        Console.WriteLine("Invalid");
                        return;
                    }

                    var byCost = _subs.Where(s => s.TotalCost > minCost);
                    foreach (var subscriber in byCost)
                    {
                        Console.WriteLine(subscriber);
                    }
                    break;
                default:
                    Console.WriteLine("Invalid");
                    break;
            }
        }
    }
}
