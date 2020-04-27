using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Threading;

namespace SemaphoreIncrementer
{
    class Program
    {
        static Semaphore sem = new Semaphore(1, 1, "CounterSem");
        static void Main(string[] args)
        {
            Console.WriteLine("Введите длину массива:");
            int ArrayLength = Int32.Parse(Console.ReadLine());
            Console.Clear();
            int[] Array = new int[ArrayLength];
            for (int i = 0; i < ArrayLength; i++)
            {
                Console.WriteLine("Введите {0}-й элемент :",i);
                Array[i] = Int32.Parse(Console.ReadLine());
                Console.Clear();
            }

            using (MemoryMappedFile mmf = MemoryMappedFile.CreateNew("mmfForSemIncrementer", 10000))
            {
                while (true)
                {
                    sem.WaitOne();
                    using (MemoryMappedViewStream stream = mmf.CreateViewStream())
                    {
                        BinaryWriter writer = new BinaryWriter(stream);
                        writer.Write(Array.Length);
                        foreach (int Item in Array)
                            writer.Write(Item);
                    }
                    sem.Release();

                    Console.Clear();
                    Console.WriteLine("Текущий массив:");
                    foreach (int Item in Array)
                        Console.Write(Item + " ");
                    Thread.Sleep(1000);

                    for (int i = 0; i < Array.Length; i++)
                        Array[i]++;

                }
            }
        }
    }
}
