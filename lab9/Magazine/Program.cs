namespace MagazineApp
{
    internal class Program
    {
        private const string PathMags = "magazines.txt";
        private const string PathSubs = "subscribers.txt";

        public delegate void DataChangedHandler();

        public static event DataChangedHandler? DataChanged = delegate { };
        public static event DataChangedHandler? SubscriberAdded = delegate { };
        public static event DataChangedHandler? SubscriberRemoved = delegate { };
        public static event DataChangedHandler? MagazineAdded = delegate { };
        public static event DataChangedHandler? MagazineRemoved = delegate { };

        private List<Magazine> _mags = new();
        private List<Subscriber> _subs = new();

        private static void Main()
        {
            Program program = new();
            DataChanged += program.ShowLinqInfo;
            SubscriberAdded += program.ShowLinqInfo;
            SubscriberRemoved += program.ShowLinqInfo;
            MagazineAdded += program.ShowLinqInfo;
            MagazineRemoved += program.ShowLinqInfo;
            program.Run();
        }

        private void Run()
        {
            LoadData();
            DataChanged.Invoke();
            ShowMenu();
        }

        private void LoadData()
        {
            if (!File.Exists(PathMags))
            {
                throw new FileNotFoundException();
            }

            if (!File.Exists(PathSubs))
            {
                throw new FileNotFoundException();
            }

            _mags = File
                .ReadLines(PathMags)
                .Select(line => line.Split(';'))
                .Where(parts => parts.Length >= 2 && decimal.TryParse(parts[1], out _))
                .Select(parts => new Magazine(parts[0].Trim(), decimal.Parse(parts[1].Trim())))
                .ToList();

            _subs = File
                .ReadLines(PathSubs)
                .Select(line => line.Split(';'))
                .Where(parts => parts.Length >= 3)
                .Select(parts =>
                {
                    string[] names = parts[2]
                        .Split(',')
                        .Select(n => n.Trim())
                        .ToArray();

                    return new Subscriber(parts[0].Trim(), parts[1].Trim(), names);
                })
                .ToList();

            foreach (Subscriber sub in _subs)
            {
                foreach (string magName in sub.MagazineNames)
                {
                    Magazine? magazine = _mags.FirstOrDefault(m => m.Name == magName);
                    if (magazine is not null)
                    {
                        magazine.Subscribe(sub);
                    }
                }
            }
        }

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
                Console.WriteLine("6.Add subscriber");
                Console.WriteLine("7.Remove subscriber");
                Console.WriteLine("8.Add magazine");
                Console.WriteLine("9.Remove magazine");
                Console.WriteLine("10.Exit");
                Console.Write("Select: ");

                string? choice = Console.ReadLine();

                switch (choice)
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
                        AddSubscriber();
                        break;
                    case "7":
                        RemoveSubscriber();
                        break;
                    case "8":
                        AddMagazine();
                        break;
                    case "9":
                        RemoveMagazine();
                        break;
                    case "10":
                        return;
                    default:
                        Console.WriteLine("Invalid");
                        break;
                }
            }
        }

        private void PrintSubscribers()
        {
            if (!_subs.Any())
            {
                Console.WriteLine("No data");
                return;
            }

            Console.WriteLine("№\tLastName\tAddress\tMagazines\tTotal");

            int index = 1;

            foreach (Subscriber subscriber in _subs.OrderByDescending(s => s.TotalCost))
            {
                Console.WriteLine($"{index++}\t{subscriber}");
            }
        }

        private void ChangeName()
        {
            if (!_mags.Any())
            {
                Console.WriteLine("No data");
                return;
            }

            for (int i = 0; i < _mags.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_mags[i].Name} ({_mags[i].AnnualPrice})");
            }

            Console.Write("#: ");

            if (!int.TryParse(Console.ReadLine(), out int idx) || idx < 1 || idx > _mags.Count)
            {
                Console.WriteLine("Invalid");
                return;
            }

            Console.Write("New name: ");

            string newName = Console.ReadLine()?.Trim() ?? string.Empty;

            _mags[idx - 1].ChangeName(newName);

            DataChanged.Invoke();
        }

        private void ChangePrice()
        {
            if (!_mags.Any())
            {
                Console.WriteLine("No data");
                return;
            }

            for (int i = 0; i < _mags.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_mags[i].Name} ({_mags[i].AnnualPrice})");
            }

            Console.Write("#: ");

            if (!int.TryParse(Console.ReadLine(), out int idx) || idx < 1 || idx > _mags.Count)
            {
                Console.WriteLine("Invalid");
                return;
            }

            Console.Write("New price: ");

            if (!decimal.TryParse(Console.ReadLine(), out decimal price))
            {
                Console.WriteLine("Invalid");
                return;
            }

            _mags[idx - 1].ChangePrice(price);

            DataChanged.Invoke();
        }

        private void AddSubscriber()
        {
            Console.Write("LastName: ");
            string lastName = Console.ReadLine()?.Trim() ?? string.Empty;

            Console.Write("Address: ");
            string address = Console.ReadLine()?.Trim() ?? string.Empty;

            Console.Write("Magazines (comma-separated): ");
            string[] names = Console.ReadLine()?.Split(',').Select(n => n.Trim()).ToArray() ?? Array.Empty<string>();

            Subscriber sub = new(lastName, address, names);

            for (int i = 0; i < names.Length; i++)
            {
                Magazine? magazine = _mags.FirstOrDefault(m => m.Name == names[i]);
                if (magazine is not null)
                {
                    sub.SetMagazinePrice(i, magazine.AnnualPrice);
                    magazine.Subscribe(sub);
                }
            }

            _subs.Add(sub);

            SubscriberAdded.Invoke();
        }

        private void RemoveSubscriber()
        {
            PrintSubscribers();

            Console.Write("№ to remove: ");

            if (!int.TryParse(Console.ReadLine(), out int idx) || idx < 1 || idx > _subs.Count)
            {
                Console.WriteLine("Invalid");
                return;
            }

            _subs.RemoveAt(idx - 1);

            SubscriberRemoved.Invoke();
        }

        private void AddMagazine()
        {
            Console.Write("Name: ");
            string name = Console.ReadLine()?.Trim() ?? string.Empty;

            Console.Write("Annual price: ");

            if (!decimal.TryParse(Console.ReadLine(), out decimal price))
            {
                Console.WriteLine("Invalid");
                return;
            }

            Magazine mag = new(name, price);

            _mags.Add(mag);

            MagazineAdded.Invoke();
        }

        private void RemoveMagazine()
        {
            for (int i = 0; i < _mags.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_mags[i].Name}");
            }

            Console.Write("№ to remove: ");

            if (!int.TryParse(Console.ReadLine(), out int idx) || idx < 1 || idx > _mags.Count)
            {
                Console.WriteLine("Invalid");
                return;
            }

            _mags.RemoveAt(idx - 1);

            MagazineRemoved.Invoke();
        }

        private void ShowLinqInfo()
        {
            Console.WriteLine($"Total magazines: {_mags.Count}");
            Console.WriteLine($"Total subscribers: {_subs.Count}");
            Console.WriteLine($"List of mags: {string.Join(", ", _mags.OrderBy(m => m.Name).Select(m => m.Name))}");
            Console.WriteLine($"Avg. subscription cost: {_subs.Average(s => s.TotalCost):F2}");
        }

        private void FilterMenu()
        {
            Console.WriteLine("1.By magazine  2.By street  3.By cost");

            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Magazine name: ");
                    string magName = Console.ReadLine()?.Trim() ?? string.Empty;
                    foreach (Subscriber s in _subs.Where(s => s.MagazineNames.Contains(magName)))
                    {
                        Console.WriteLine(s);
                    }

                    break;
                case "2":
                    Console.Write("Street contains: ");
                    string street = Console.ReadLine()?.Trim() ?? string.Empty;
                    foreach (Subscriber s in _subs.Where(s => s.Address.Contains(street)))
                    {
                        Console.WriteLine(s);
                    }

                    break;
                case "3":
                    Console.Write("Min total cost: ");
                    if (!decimal.TryParse(Console.ReadLine(), out decimal min))
                    {
                        Console.WriteLine("Invalid");
                        return;
                    }

                    foreach (Subscriber s in _subs.Where(s => s.TotalCost > min))
                    {
                        Console.WriteLine(s);
                    }

                    break;
                default:
                    Console.WriteLine("Invalid");
                    break;
            }
        }
    }
}
