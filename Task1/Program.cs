using System;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        int passwordLength;
        do
        {
            Console.Write("Enter password length: ");
            string inputLength = Console.ReadLine();

            if (!int.TryParse(inputLength, out passwordLength))
            {
                Console.WriteLine("You need to enter a number");
            }
        } while (passwordLength <= 0);

        Console.Write("Use capital letters? (yes/no): ");
        bool useUpperCase = Console.ReadLine().ToLower() == "yes";

        Console.Write("Use numbers? (yes/no): ");
        bool useDigits = Console.ReadLine().ToLower() == "yes";

        Console.Write("Use special symbols? (yes/no): ");
        bool useSpecialChars = Console.ReadLine().ToLower() == "yes";

        string password = GeneratePassword(passwordLength, useUpperCase, useDigits, useSpecialChars);

        Console.WriteLine("Your password: " + password);

       
        SavePasswordToFile(password);
    }

    static string GeneratePassword(int length, bool useUpperCase, bool useDigits, bool useSpecialChars)
    {
        const string lowerCaseChars = "abcdefghijklmnopqrstuvwxyz";
        const string upperCaseChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string digits = "0123456789";
        const string specialChars = "!@#$%^&*()_+-=[]{}|;:,.<>?";

        string chars = lowerCaseChars;
        if (useUpperCase) chars += upperCaseChars;
        if (useDigits) chars += digits;
        if (useSpecialChars) chars += specialChars;

        Random random = new Random();
        return new string(Enumerable.Repeat(chars, length)
          .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    static void SavePasswordToFile(string password)
    {
        string filePath = "generated_password.txt";
        try
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(password);
                Console.WriteLine("Password saved in file: " + filePath);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error when saving password to file: " + ex.Message);
        }
    }
}