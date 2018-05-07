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
        static void Main(string[] args)
        {
            Console.WriteLine("Открыть файл - 1");
            Console.WriteLine("Создать файл - 2");
            Console.WriteLine("Удалить файл - 1");
            Console.WriteLine("Введите команду:");
            string com=Console.ReadLine();
            int icom = Convert.ToInt16(com);
            string Namefile;
            switch (icom)
            {
                case 1:
                    Console.WriteLine("Введите название файла");
                    Namefile = Console.ReadLine();
                    if (File.Exists(Namefile))
                    {
                        using (FileStream fs = File.Open(Namefile, FileMode.Open))
                        {
                            byte[] b = new byte[1024];
                            UTF8Encoding temp = new UTF8Encoding(true);

                            while (fs.Read(b, 0, b.Length) > 0)
                            {
                                Console.WriteLine(temp.GetString(b));
                            }
                        }
                    }
                    break;
                case 2:
                    Console.WriteLine("Введите название файла");
                    Namefile = Console.ReadLine();
                    if (File.Exists(Namefile))
                    {
                        Console.WriteLine("Файл с таким именем уже существует");
                    }
                    else
                    {
                        File.Create(Namefile);
                    }
                    break;
                case 3:
                    Console.WriteLine("Введите название файла");
                    Namefile = Console.ReadLine();
                    if (File.Exists(Namefile))
                    {
                        File.Delete(Namefile);
                    }
                    break;
                default:
                    Console.WriteLine("Неверная команда:");
                    break;
            }
            Console.ReadKey();
        }
    }
}
