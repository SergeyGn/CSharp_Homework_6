using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace CSharp_Homework_6
{
    class Program
    {
        static string pathFileFindGroups = "Groups.txt";
        static string pathFileGetNumberGroup = "Groups1.txt";
        static string pathFileNumber = "numberFile.txt";
        static void Main(string[] args)
        {
            string fileNumber = File.ReadAllText(pathFileNumber);
            if (int.TryParse(fileNumber, out int number) == true)
            {
                ShowMainMenu(number);
            }
            else
            {
                Console.WriteLine("Недопустимое значение в файле");
            }

        }

        private static void ShowMainMenu(int number)
        {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Число в фaйле:{number}");
                Console.WriteLine($"Для того чтобы посмотреть количество групп нажмите 1" +
                                $"\nДля расчета всех групп нажмите 2" +
                                $"\nq.Выход");
                Console.ResetColor();
                ConsoleKeyInfo enter = Console.ReadKey(true);

                switch (enter.Key)
                {
                    case ConsoleKey.D2:
                        FindGroups(number);
                        break;
                    case ConsoleKey.D1:
                        GetNumberGroup(number);
                        break;
                    case ConsoleKey.Q:
                        break;
                    default:
                        Console.WriteLine("Некорректный ввод. Попробуйте ещё раз");
                        ShowMainMenu(number);
                        break;
                
            }
        }
        private static void FindGroups(int number)
        {
            using (StreamWriter streamWriter = new StreamWriter(pathFileFindGroups))
            {
                int numberGroup = 0;
                while (number >= 1)
                {
                    int result = number / 2 ;
                    int writeResult = result;
                    streamWriter.Write($"Группа №{++numberGroup}:");
                    while (true)
                    {

                        if (writeResult < number - 1)
                        {
                            writeResult++;
                            streamWriter.Write($"{writeResult},");
                        }
                        else if (writeResult < number)
                        {
                            writeResult++;
                            streamWriter.Write($"{writeResult}.");

                        }
                        else if (writeResult == number)
                        {
                            number = result;
                            result = number / 2;
                            streamWriter.WriteLine();
                            break;
                        }
                    }
                }
            }
            CreateArchive(pathFileFindGroups);

        }
        private static void CreateArchive(string fileName)
        {
            bool _isExit = false;
            while (_isExit == false)
            {
                Console.WriteLine("Заархивировать результат? y/n");
                ConsoleKeyInfo enter1 = Console.ReadKey(true);

                switch (enter1.Key)
                {
                    case ConsoleKey.Y:
                        string compresed = "Result.zip";
                        using (FileStream ss = new FileStream(fileName, FileMode.OpenOrCreate))
                        {
                            using (FileStream ts = File.Create(compresed))
                            {
                                using (GZipStream cs = new GZipStream(ts, CompressionMode.Compress))
                                {
                                    ss.CopyTo(cs);
                                }
                            }

                        }
                        _isExit = true;
                        break;
                    case ConsoleKey.N:
                        _isExit = true;
                        break;
                    default:
                        Console.WriteLine("Некорректный ввод. Попробуйте ещё раз");
                        break;
                }
            }


        }
        private static void GetNumberGroup(int number)
        {
            using (StreamWriter streamWriter = new StreamWriter(pathFileGetNumberGroup))
            {
                int numberGroup = 1;
                for (int i = 1; Math.Pow(2, i) <= number; i++)
                {
                    numberGroup++;
                }
                streamWriter.Write($"Количество групп:{numberGroup}");
            }
            CreateArchive(pathFileGetNumberGroup);
        }
    }
}