using ITUniver.TeleCloud.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITUniver.TeleCloud
{
    class Program
    {
        static int option;
        static string dir = @"C:\ituniver\TeleCloud\files\";
        static Disk file;
        static string name;
        static void Main(string[] args)
        {
            try
            {
                if (args.Length == 0)
                {
                    Console.WriteLine("1 - Cписок файлов;\n2 - Вывод файла в консоль;\n3 - Cоздание файла;\n4 - Удаление файла.");
                    option = Convert.ToInt32(Console.ReadLine());
                    file = new Disk(dir);
                    switch (option)
                    {
                        case 1:
                            {
                                foreach (var item in file.FileList())
                                {
                                    Console.WriteLine(item + "\n");
                                }
                                break;
                            }
                        case 2:
                            {
                                Console.WriteLine("Введите имя файла:");
                                name = Console.ReadLine();
                                Console.WriteLine(file.OpenFile(name));
                                break;
                            }
                        case 3:
                            {
                                Console.WriteLine("Введите имя файла:");
                                name = Console.ReadLine();
                                if (file.NewFile(name))
                                {
                                    Console.WriteLine($"Файл {name} создан!");
                                }
                                else
                                {
                                    Console.WriteLine("Что-то пошло не так!");
                                }
                                break;
                            }
                        case 4:
                            {
                                Console.WriteLine("Введите имя файла:");
                                name = Console.ReadLine();
                                if (file.DeleteFile(name))
                                {
                                    Console.WriteLine($"Файл {name} удалён!");
                                }
                                else
                                {
                                    Console.WriteLine("Что-то пошло не так!");
                                }
                                break;
                            }
                        default:
                            {
                                Console.WriteLine("Введите от 1 до 4!");
                                break;
                            }
                    }
                }
            }
            catch
            {
                Console.WriteLine("Что-то пошло не так!");
            }
            finally
            {
                Main(args);
            }
        }
    }
}
