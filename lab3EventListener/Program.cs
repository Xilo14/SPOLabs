using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace lab3EventConsumer
{
    class Program
    {
        static int countProducts = 0;
        static EventWaitHandle wh = new EventWaitHandle(false, EventResetMode.AutoReset, "OOO'РОГА И КОПЫТА'");
        static void Main(string[] args)
        {
            Thread thread1 = new Thread(Consume);
            thread1.Start();
            Thread thread2 = new Thread(Shop);
            thread2.Name = "Shop";
            thread2.Start();
        }
        static void Shop()
        {
            while (true)
            {
                wh.WaitOne();
                Console.WriteLine(Thread.CurrentThread.Name + " получил товар");
                countProducts++;
            }
        }
        static void Consume()
        {
            while (true)
            {
                Console.WriteLine("Ожидание продукта");
                while (true)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    if (keyInfo.Key == ConsoleKey.Enter)
                    {
                        if (countProducts >= 1)
                        {
                            countProducts--;
                            Console.WriteLine("Продукт получен");
                        }
                        else
                        {
                            Console.WriteLine("Нет доступных продуктов, ожидайте");
                        }
                    }
                }
            }
        }
    }

}
