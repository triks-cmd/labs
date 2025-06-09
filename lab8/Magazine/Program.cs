namespace MagazineApp
{
    /// <summary>
    /// Основная точка входа и логика работы консольного приложения по подпискам на журналы.
    /// </summary>
    class Program
    {
        private const string Path_Magazines   = "magazines.txt";
        private const string Path_Subscribers = "subscribers.txt";
        private const string Menu_Display     = "1";
        private const string Menu_ChangeName  = "2";
        private const string Menu_ChangePrice = "3";
        private const string Menu_Exit        = "4";

        private const string Msg_InvalidInput    = "Accsees denied";
        private const string Msg_NoData          = "No data";
        private const string Msg_SelectJournal   = "Enter number magazines: ";
        private const string Msg_EnterNewName    = "Enter new name: ";
        private const string Msg_EnterNewPrice   = "Enter new price: ";

        private readonly List<Subscriber> _subs = new();
        private readonly List<Magazine>   _mags = new();

        /// <summary>
        /// Точка входа приложения.
        /// </summary>
        static void Main()
        {
            new Program().Run();
        }

        /// <summary>
        /// Запускает загрузку данных и отображение главного меню.
        /// </summary>
        private void Run()
        {
            LoadData();
            ShowMenu();
        }

        /// <summary>
        /// Загружает журналы и подписчиков из файлов и связывает подписчиков с журналами.
        /// </summary>
        private void LoadData()
        {
            try
            {
                // Считываем список журналов из файла
                LoadMagazines();

                // Считываем список подписчиков из файла
                LoadSubscribers();

                // Регистрируем подписчиков в соответствующих журналах
                ConnectObservers();
            }
            catch (Exception ex)
            {
                // Выводим сообщение об ошибке при загрузке
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Читает файл журналов и создает объекты <see cref="Magazine"/>.
        /// </summary>
        /// <exception cref="FileNotFoundException">Если файл журналов не найден.</exception>
        private void LoadMagazines()
        {
            if (!File.Exists(Path_Magazines))
            {
                throw new FileNotFoundException("file not found");
            }

            // Для каждой строки в файле создаем новый объект Magazine
            foreach (var line in File.ReadLines(Path_Magazines))
            {
                var parts = line.Split(';');

                // Пропускаем некорректные строки
                if (parts.Length < 2)
                {
                    continue;
                }

                // Парсим цену и добавляем в коллекцию
                if (decimal.TryParse(parts[1].Trim(), out var price))
                {
                    _mags.Add(new Magazine(parts[0].Trim(), price));
                }
            }
        }

        /// <summary>
        /// Читает файл подписчиков и создает объекты <see cref="Subscriber"/>.
        /// </summary>
        /// <exception cref="FileNotFoundException">Если файл подписчиков не найден.</exception>
        private void LoadSubscribers()
        {
            if (!File.Exists(Path_Subscribers))
            {
                throw new FileNotFoundException("file not found");
            }

            // Для каждой строки в файле создаем новый объект Subscriber
            foreach (var line in File.ReadLines(Path_Subscribers))
            {
                var parts = line.Split(';');

                // Пропускаем некорректные строки
                if (parts.Length < 3)
                {
                    continue;
                }

                // Получаем список названий журналов, разделенных запятой
                string rawList = parts[2];
                string[] splitNames = rawList.Split(',');
                string[] names = splitNames
                    .Select(s => s.Trim())
                    .ToArray();

                _subs.Add(new Subscriber(
                    parts[0].Trim(),   // фамилия
                    parts[1].Trim(),   // адрес
                    names));           // журналы
            }
        }

        /// <summary>
        /// Регистрирует каждого подписчика в соответствующих журналах и устанавливает изначальные цены.
        /// </summary>
        private void ConnectObservers()
        {
            // Для каждого подписчика
            foreach (var s in _subs)
            {
                // И для каждого названия журнала в его списке
                for (int i = 0; i < s.MagazineNames.Length; i++)
                {
                    // Ищем объект Magazine по имени
                    var m = _mags.FirstOrDefault(x => x.Name == s.MagazineNames[i]);

                    if (m == null)
                    {
                        // Если журнал не найден — пропускаем
                        continue;
                    }

                    // Фиксируем его текущую цену в подписчике
                    s.SetMagazinePrice(i, m.AnnualPrice);

                    // Регистрируем подписчика на уведомления
                    m.Subscribe(s);
                }
            }
        }

        /// <summary>
        /// Отображает главное меню и обрабатывает ввод пользователя.
        /// </summary>
        private void ShowMenu()
        {
            while (true)
            {
                // Выводим пункты меню
                Console.WriteLine();
                Console.WriteLine("1. Show me subsribers");
                Console.WriteLine("2. Change magazines");
                Console.WriteLine("3. Change price");
                Console.WriteLine("4. Exit");
                Console.Write("Select: ");

                // Читаем выбор пользователя
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case Menu_Display:
                        PrintSubscribers();
                        break;

                    case Menu_ChangeName:
                        ChangeMagazineName();
                        break;

                    case Menu_ChangePrice:
                        ChangeMagazinePrice();
                        break;

                    case Menu_Exit:
                        // Завершаем программу
                        return;

                    default:
                        Console.WriteLine(Msg_InvalidInput);
                        break;
                }
            }
        }

        /// <summary>
        /// Выводит список подписчиков с фамилиями, адресами, названиями журналов и общей стоимостью подписки.
        /// </summary>
        private void PrintSubscribers()
        {
            if (!_subs.Any())
            {
                Console.WriteLine(Msg_NoData);
                return;
            }

            Console.WriteLine("№\tSurname\tAdress\tMagazine\tSum");
            int i = 1;

            // Сортируем подписчиков по убыванию общей суммы
            foreach (var s in _subs.OrderByDescending(x => x.TotalCost))
            {
                Console.WriteLine($"{i++}\t{s.LastName}\t{s.Address}\t{s.GetMagazinesString()}\t{s.TotalCost}");
            }
        }

        /// <summary>
        /// Позволяет пользователю выбрать журнал и изменить его имя.
        /// </summary>
        private void ChangeMagazineName()
        {
            if (!_mags.Any())
            {
                Console.WriteLine(Msg_NoData);
                return;
            }

            // Выводим нумерованный список журналов
            for (int i = 0; i < _mags.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_mags[i].Name} ({_mags[i].AnnualPrice} EUR)");
            }

            Console.Write(Msg_SelectJournal);

            // Считываем номер
            if (int.TryParse(Console.ReadLine(), out int idx) && idx > 0 && idx <= _mags.Count)
            {
                Console.Write(Msg_EnterNewName);
                string newName = Console.ReadLine()?.Trim() ?? string.Empty;

                // Меняем имя
                _mags[idx - 1].ChangeName(newName);
            }
            else
            {
                Console.WriteLine(Msg_InvalidInput);
            }
        }

        /// <summary>
        /// Позволяет пользователю выбрать журнал и изменить его цену.
        /// </summary>
        private void ChangeMagazinePrice()
        {
            if (!_mags.Any())
            {
                Console.WriteLine(Msg_NoData);
                return;
            }

            // Выводим нумерованный список журналов
            for (int i = 0; i < _mags.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_mags[i].Name} ({_mags[i].AnnualPrice} EUR)");
            }

            Console.Write(Msg_SelectJournal);

            // Считываем номер
            if (int.TryParse(Console.ReadLine(), out int idx) && idx > 0 && idx <= _mags.Count)
            {
                Console.Write(Msg_EnterNewPrice);

                // Считываем новую цену
                if (decimal.TryParse(Console.ReadLine(), out var p))
                {
                    _mags[idx - 1].ChangePrice(p);
                }
                else
                {
                    Console.WriteLine("Incorect");
                }
            }
            else
            {
                Console.WriteLine(Msg_InvalidInput);
            }
        }
    }
}
