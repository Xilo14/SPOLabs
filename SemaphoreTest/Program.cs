using System;
using System.Threading;

namespace SemaphoreTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Reader reader;
            for (int i = 1; i < 6; i++)
                reader = new Reader(i);
            Console.ReadLine();
        }
    }

    class Reader
    {
        static Semaphore sem = new Semaphore(3, 3);        // создаем семафор
        Thread myThread;
        int count = 3;// счетчик чтения         
        public Reader(int i)
        {
            myThread = new Thread(Read);
            myThread.Name = "Читатель " + i.ToString();
            myThread.Start();
        }

        public void Read()
        {
            while (count > 0)
            {
                sem.WaitOne();
                Console.WriteLine("{0} в библиотеке", Thread.CurrentThread.Name);
                Thread.Sleep(1000);
                Console.WriteLine("{0} покидает библиотеку", Thread.CurrentThread.Name);
                sem.Release();
                count--;
                Thread.Sleep(1000);
            }
        }
    }
}

