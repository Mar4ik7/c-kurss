using System;
using System.IO;
using System.Text;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        char choice;

        do
        {
            Console.Clear();
            Console.WriteLine("Меню:");
            Console.WriteLine("1. Згенерувати пароль та зберегти на робочий стіл");
            Console.WriteLine("2. Згенерувати пароль вручну та зберегти на робочий стіл");
            Console.WriteLine("3. Відображення паролю в консолі");
            Console.WriteLine("4. Відображення паролю в консолі (ручний ввід)");
            Console.WriteLine("0. Вийти");

            choice = Console.ReadKey().KeyChar;
            Console.WriteLine();

            switch (choice)
            {
                case '1':
                    GenerateAndSavePasswords();
                    break;
                case '2':
                    GenerateAndSavePasswordsManually();
                    break;
                case '3':
                    DisplayPasswordsInConsole();
                    break;
                case '4':
                    DisplayPasswordsInConsoleManually();
                    break;
                case '0':
                    Console.WriteLine("Дякую за використання програми. Натисніть будь-яку клавішу для виходу.");
                    Console.ReadKey();
                    break;
                default:
                    Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
                    Console.ReadKey();
                    break;
            }
        } while (choice != '0');
    }

    static void GenerateAndSavePasswords()
    {
        Console.Clear();
        Console.Write("Введіть ім'я файлу для збереження: ");
        string fileName = Console.ReadLine();

        Random random = new Random();

        Console.Write("Введіть кількість паролів для генерації: ");
        int count = int.Parse(Console.ReadLine());

        Console.WriteLine("Виберіть складність паролів (легкий, середній або тяжкий):");
        Console.WriteLine("1. Легкий (тільки маленькі букви і цифри)");
        Console.WriteLine("2. Середній (маленькі букви, великі букви і цифри)");
        Console.WriteLine("3. Тяжкий (маленькі букви, великі букви, цифри та символи");

        int complexityChoice = int.Parse(Console.ReadLine());

        string passwordCharacters = "";

        switch (complexityChoice)
        {
            case 1:
                passwordCharacters = "abcdefghijklmnopqrstuvwxyz1234567890";
                break;
            case 2:
                passwordCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
                break;
            case 3:
                passwordCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()";
                break;
            default:
                Console.WriteLine("Невірний вибір складності.");
                Console.ReadKey();
                return;
        }

        using (StreamWriter writer = new StreamWriter(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName + ".txt")))
        {
            for (int i = 1; i <= count; i++)
            {
                string password = GeneratePassword(12, passwordCharacters, random);
                writer.WriteLine($"Пароль {i}: {password}");
            }
        }

        Console.Clear();
        Console.WriteLine($"Паролі збережені у файлі {fileName}.txt на робочому столі.");
        Console.WriteLine("Натисніть будь-яку клавішу для продовження.");
        Console.ReadKey();
    }

    static void GenerateAndSavePasswordsManually()
    {
        Console.Write("Введіть ім'я файлу для збереження: ");
        string fileName = Console.ReadLine();

        Random random = new Random();

        Console.Write("Введіть кількість паролів для генерації: ");
        int count = int.Parse(Console.ReadLine());

        Console.Write("Скільки великих букв: ");
        int uppercaseCount = int.Parse(Console.ReadLine());
        Console.Write("Скільки маленьких букв: ");
        int lowercaseCount = int.Parse(Console.ReadLine());
        Console.Write("Скільки цифр: ");
        int digitCount = int.Parse(Console.ReadLine());
        Console.Write("Скільки символів: ");
        int symbolCount = int.Parse(Console.ReadLine());

        string passwordCharacters = GenerateCustomCharacterSet(uppercaseCount, lowercaseCount, digitCount, symbolCount);

        using (StreamWriter writer = new StreamWriter(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName + ".txt")))
        {
            for (int i = 1; i <= count; i++)
            {
                string password = GeneratePassword(12, passwordCharacters, random);
                writer.WriteLine($"Пароль {i}: {password}");
            }
        }

        Console.Clear();
        Console.WriteLine($"Паролі збережені у файлі {fileName}.txt на робочому столі.");
        Console.WriteLine("Натисніть будь-яку клавішу для продовження.");
        Console.ReadKey();
    }

    static void DisplayPasswordsInConsole()
    {
        Console.Write("Введіть кількість паролів для генерації: ");
        int count = int.Parse(Console.ReadLine());

        Console.WriteLine("Виберіть складність паролів (легкий, середній або тяжкий):");
        Console.WriteLine("1. Легкий (тільки маленькі букви і цифри)");
        Console.WriteLine("2. Середній (маленькі букви, великі букви і цифри)");
        Console.WriteLine("3. Тяжкий (маленькі букви, великі букви, цифри та символи)");

        int complexityChoice = int.Parse(Console.ReadLine());

        string passwordCharacters = "";

        switch (complexityChoice)
        {
            case 1:
                passwordCharacters = "abcdefghijklmnopqrstuvwxyz1234567890";
                break;
            case 2:
                passwordCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
                break;
            case 3:
                passwordCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()";
                break;
            default:
                Console.WriteLine("Невірний вибір складності.");
                Console.ReadKey();
                return;
        }

        Random random = new Random();

        for (int i = 1; i <= count; i++)
        {
            string password = GeneratePassword(12, passwordCharacters, random);
            Console.WriteLine($"Пароль {i}: {password}");
        }

        Console.WriteLine("Натисніть будь-яку клавішу для продовження.");
        Console.ReadKey();
    }


    static void DisplayPasswordsInConsoleManually()
    {
        Console.Write("Введіть кількість паролів для генерації: ");
        int count = int.Parse(Console.ReadLine());

        Console.Write("Скільки великих букв: ");
        int uppercaseCount = int.Parse(Console.ReadLine());
        Console.Write("Скільки маленьких букв: ");
        int lowercaseCount = int.Parse(Console.ReadLine());
        Console.Write("Скільки цифр: ");
        int digitCount = int.Parse(Console.ReadLine());
        Console.Write("Скільки символів: ");
        int symbolCount = int.Parse(Console.ReadLine());

        string passwordCharacters = GenerateCustomCharacterSet(uppercaseCount, lowercaseCount, digitCount, symbolCount);

        Random random = new Random();

        for (int i = 1; i <= count; i++)
        {
            string password = GeneratePassword(12, passwordCharacters, random);
            Console.WriteLine($"Пароль {i}: {password}");
        }

        Console.WriteLine("Натисніть будь-яку клавішу для продовження.");
        Console.ReadKey();
    }


    static string GenerateCustomCharacterSet(int uppercaseCount, int lowercaseCount, int digitCount, int symbolCount)
    {
        string uppercaseChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string lowercaseChars = "abcdefghijklmnopqrstuvwxyz";
        string digitChars = "1234567890";
        string symbolChars = "!@#$%^&*()";

        string passwordCharacters = "";

        for (int i = 0; i < uppercaseCount; i++)
        {
            passwordCharacters += uppercaseChars;
        }

        for (int i = 0; i < lowercaseCount; i++)
        {
            passwordCharacters += lowercaseChars;
        }

        for (int i = 0; i < digitCount; i++)
        {
            passwordCharacters += digitChars;
        }

        for (int i = 0; i < symbolCount; i++)
        {
            passwordCharacters += symbolChars;
        }

        return passwordCharacters;
    }

    static string GeneratePassword(int length, string characters, Random random)
    {
        char[] password = new char[length];
        for (int i = 0; i < length; i++)
        {
            password[i] = characters[random.Next(0, characters.Length)];
        }
        return new string(password);
    }
}
