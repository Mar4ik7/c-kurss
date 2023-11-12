using System;
using System.IO;
using System.Text;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.ForegroundColor = ConsoleColor.White;
        int choice = 0;
        string[] menuOptions = new string[]
        {
            "1.Згенерувати пароль та зберегти на робочий стіл",
            "2.Згенерувати пароль вручну та зберегти на робочий стіл",
            "3.Відображення паролю в консолі",
            "4.Відображення паролю в консолі (ручний ввід)",
            "5.Вийти"
        };

        Console.BackgroundColor = ConsoleColor.DarkYellow;
        Console.Clear();
        Console.SetWindowSize(80, 25);
        do
        {
            DisplayMenu(menuOptions, choice);

            var key = Console.ReadKey().Key;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    if (choice > 0)
                        choice--;
                    break;
                case ConsoleKey.DownArrow:
                    if (choice < menuOptions.Length - 1)
                        choice++;
                    break;
                case ConsoleKey.Enter:
                    ProcessChoice(choice);
                    break;
            }

        } while (true);
    }

    static void DisplayMenu(string[] menuOptions, int selected)
    {
        Console.Clear();
        int centerX = Console.WindowWidth / 2 - 5;
        Console.SetCursorPosition(centerX, Console.CursorTop);

        Console.WriteLine("Меню:");

        for (int i = 0; i < menuOptions.Length; i++)
        {
            if (i == selected)
            {
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine(menuOptions[i]);
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.WriteLine(menuOptions[i]);
            }
        }
    }

    static void ProcessChoice(int choice)
    {
        switch (choice)
        {
            case 0:
                GenerateAndSavePasswords();
                break;
            case 1:
                GenerateAndSavePasswordsManually();
                break;
            case 2:
                DisplayPasswordsInConsole();
                break;
            case 3:
                DisplayPasswordsInConsoleManually();
                break;
            case 4:
                Console.WriteLine("Дякую за використання програми. Натисніть будь-яку клавішу для виходу.");
                Console.ReadKey();
                Environment.Exit(0);
                break;
        }

        Console.WriteLine("Натисніть будь-яку клавішу для продовження.");
        Console.ReadKey();
    }

    static void GenerateAndSavePasswords()
    {
        Console.Write("Введіть ім'я файлу для збереження: ");
        string fileName = Console.ReadLine();

        Random random = new Random();
        int count = GetIntFromUser("Введіть кількість паролів для генерації: ");

        string passwordCharacters = SelectPasswordComplexity();

        using (StreamWriter writer = new StreamWriter(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName + ".txt")))
        {
            for (int i = 1; i <= count; i++)
            {
                string password = GeneratePassword(12, passwordCharacters, random);
                writer.WriteLine($"Пароль {i}: {password}");
            }
        }

        Console.WriteLine($"Паролі збережені у файлі {fileName}.txt на робочому столі.");
    }

    static void GenerateAndSavePasswordsManually()
    {
        Console.Write("Введіть ім'я файлу для збереження: ");
        string fileName = Console.ReadLine();

        Random random = new Random();
        int count = GetIntFromUser("Введіть кількість паролів для генерації: ");

        int uppercaseCount = GetIntFromUser("Скільки великих букв: ");
        int lowercaseCount = GetIntFromUser("Скільки маленьких букв: ");
        int digitCount = GetIntFromUser("Скільки цифр: ");
        int symbolCount = GetIntFromUser("Скільки символів: ");

        string passwordCharacters = GenerateCustomCharacterSet(uppercaseCount, lowercaseCount, digitCount, symbolCount);

        using (StreamWriter writer = new StreamWriter(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName + ".txt")))
        {
            for (int i = 1; i <= count; i++)
            {
                string password = GeneratePassword(12, passwordCharacters, random);
                writer.WriteLine($"Пароль {i}: {password}");
            }
        }

        Console.WriteLine($"Паролі збережені у файлі {fileName}.txt на робочому столі.");
    }

    static void DisplayPasswordsInConsole()
    {
        int count = GetIntFromUser("Введіть кількість паролів для генерації: ");
        string passwordCharacters = SelectPasswordComplexity();

        Random random = new Random();

        for (int i = 1; i <= count; i++)
        {
            string password = GeneratePassword(12, passwordCharacters, random);
            Console.WriteLine($"Пароль {i}: {password}");
        }
    }

    static void DisplayPasswordsInConsoleManually()
    {
        int count = GetIntFromUser("Введіть кількість паролів для генерації: ");


        int uppercaseCount = GetIntFromUser("Скільки великих букв: ");
        int lowercaseCount = GetIntFromUser("Скільки маленьких букв: ");
        int digitCount = GetIntFromUser("Скільки цифр: ");
        int symbolCount = GetIntFromUser("Скільки символів: ");

        string passwordCharacters = GenerateCustomCharacterSet(uppercaseCount, lowercaseCount, digitCount, symbolCount);

        Random random = new Random();

        for (int i = 1; i <= count; i++)
        {
            string password = GeneratePassword(12, passwordCharacters, random);
            Console.WriteLine($"Пароль {i}: {password}");
        }
    }

    static int GetIntFromUser(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out int result))
            {
                return result;
            }
            Console.WriteLine("Будь ласка, введіть цифру, а не букву.");
        }
    }

    static string SelectPasswordComplexity()
    {
        Console.WriteLine("Виберіть складність паролів:");
        Console.WriteLine("1. Легкий (тільки маленькі букви і цифри)");
        Console.WriteLine("2. Середній (маленькі букви, великі букви і цифри)");
        Console.WriteLine("3. Тяжкий (маленькі букви, великі букви, цифри та символи)");

        while (true)
        {
            int complexityChoice = GetIntFromUser("Ваш вибір (1-3): ");
            switch (complexityChoice)
            {
                case 1:
                    return "abcdefghijklmnopqrstuvwxyz1234567890";
                case 2:
                    return "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
                case 3:
                    return "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()";
                default:
                    Console.WriteLine("Невірний вибір складності. Введіть 1, 2 або 3.");
                    break;
            }
        }
    }

    static string GenerateCustomCharacterSet(int uppercaseCount, int lowercaseCount, int digitCount, int symbolCount)
    {
        string uppercaseChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string lowercaseChars = "abcdefghijklmnopqrstuvwxyz";
        string digitChars = "1234567890";
        string symbolChars = "!@#$%^&*()";

        string passwordCharacters = "";

        passwordCharacters += new string(uppercaseChars.ToCharArray(), 0, uppercaseCount);
        passwordCharacters += new string(lowercaseChars.ToCharArray(), 0, lowercaseCount);
        passwordCharacters += new string(digitChars.ToCharArray(), 0, digitCount);
        passwordCharacters += new string(symbolChars.ToCharArray(), 0, symbolCount);

        return passwordCharacters;
    }

    static string GeneratePassword(int length, string characters, Random random)
    {
        char[] password = new char[length];
        for (int i = 0; i < length; i++)
        {
            password[i] = characters[random.Next(characters.Length)];
        }
        return new string(password);
    }
}