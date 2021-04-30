using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;


namespace CSharp_Homework_6
{
    class Program
    {
        static string globalPath = AppDomain.CurrentDomain.BaseDirectory;
        static string pathFileFindGroups = $"{globalPath}result/" + "Groups.txt";
        static string pathFileGetNumberGroup = $"{globalPath}result/" + "Groups.txt";
        static string pathFileNumber = $"{globalPath}"+"numberFile.txt";
        
        static void Main(string[] args)
        {
            ChekPath(pathFileNumber);

        }

        private static void ShowMainMenu(int number)
        {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Число:{number}");
                Console.WriteLine($"Для того чтобы посмотреть количество групп нажмите 1" +
                                $"\nДля расчета всех групп нажмите 2" +
                                $"\nВыход нажмите q");
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
            {
                Console.WriteLine("Заархивировать результат? y/n");
                ConsoleKeyInfo enter1 = Console.ReadKey(true);

                switch (enter1.Key)
                {
                    case ConsoleKey.Y:
                       Console.WriteLine("ВВедите имя архива");
                        string compressed = Console.ReadLine();

                        string startPath = $"{globalPath}/result/";
                        string zipPath =  $"{globalPath}"+@"archive\"+$"{compressed}.zip";
                        ZipFile.CreateFromDirectory(startPath, zipPath);

                        //using (FileStream ss = new FileStream(fileName, FileMode.OpenOrCreate))
                        //{
                        //    using (FileStream ts = File.Create(compressed))
                        //    {
                        //        //using (ZipArchive zip = new ZipArchive(ss, ZipArchiveMode.Update))
                        //        //{
                        //        //    ss.CopyTo(zip);
                        //        //    //}
                        //        using (GZipStream cs = new GZipStream(ss, CompressionMode.Compress))
                        //        {
                        //            ss.CopyTo(cs);
                        //        }
                        //        //}
                        //    }
                        //}
                        Console.WriteLine($"Путь к архиву:{zipPath}");
                        break;
                    case ConsoleKey.N:

                        break;
                    default:
                        Console.WriteLine("Некорректный ввод. Попробуйте ещё раз");
                        CreateArchive(fileName);
                        break;
                }
            }


        }
        private static void ChekPath(string path)
        {
            Console.WriteLine($"Текущий путь к файлу:{pathFileNumber}" +
                             "\nХотите сменить путь? y/n");
            ConsoleKeyInfo enterPath = Console.ReadKey(true);
            switch (enterPath.Key)
            { 
                case ConsoleKey.Y:
                    Console.WriteLine("Введите путь");
                    path=Console.ReadLine();
                    if(File.Exists(path)==false)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Некорректный путь к файлу. " +
                            "\nНужно вводить путь с названием и расширением" +
                            "\nНапример: File.txt");
                        Console.ResetColor();
                        ChekPath(path);
                    }
                    else
                    {
                        pathFileNumber = path;
                    }
                break;
                case ConsoleKey.N:
                    EnterNumberFromConsole();
                    break;
                default:
                    Console.WriteLine("Некорректное значение. Попробуйте ещё раз");
                    ChekPath(path);
                    break;
            } 
        }
        private static void CheckNumberText(string textNumber)
        {
            if (int.TryParse(textNumber, out int number) == true)
            {
                ShowMainMenu(number);
            }
            else
            {
                Console.WriteLine("Недопустимое значение");
                ChekPath(pathFileNumber);
            }
        }

        private static void EnterNumberFromConsole()
        {
            Console.WriteLine("Хотите ввести число из консоли? y/n");
            ConsoleKeyInfo enter = Console.ReadKey(true);
            switch(enter.Key)
            {
                case ConsoleKey.Y:
                    Console.WriteLine("ВВедите число");
                    CheckNumberText(Console.ReadLine());
                    break;
                case ConsoleKey.N:
                    string fileNumber = File.ReadAllText(pathFileNumber);
                    CheckNumberText(fileNumber);
                    break;
                default:
                    Console.WriteLine("Некорректный ввод");
                    EnterNumberFromConsole();
                    break;
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