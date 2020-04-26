using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Threading;

namespace SemaphoreReader
{
    class Program
    {
        static Semaphore sem = new Semaphore(1, 1,"CounterSem");
        static void Main(string[] args)
        {
            while (true)
            {
                
                try
                {
                    using (MemoryMappedFile mmf = MemoryMappedFile.OpenExisting("mmfForSemIncrementer"))
                    {
                        Console.WriteLine("Прочитанный массив:");
                        while (true)
                        {
                            sem.WaitOne();
                            using (MemoryMappedViewStream stream = mmf.CreateViewStream())
                            {
                                BinaryReader reader = new BinaryReader(stream);
                                int ArrayLength = reader.ReadInt32();
                                int[] Array = new int[ArrayLength];
                                Console.CursorLeft = 0;
                                for (int i = 0; i < ArrayLength; i++)
                                {
                                    Array[i] = reader.ReadInt32();
                                    Console.Write("{0} ", Array[i]);
                                }
                            }
                            sem.Release();
                        }
                    }
                }
                catch (FileNotFoundException)
                {
                    Console.Clear();
                    Console.WriteLine("Memory-mapped file does not exist.");
                    Thread.Sleep(1000);
                }
                
            }
        }
    }
}
