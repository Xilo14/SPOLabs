using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace lab3EventProducer
{
    class Program
    {
        
        static EventWaitHandle wh = new EventWaitHandle(false, EventResetMode.AutoReset, "OOO'РОГА И КОПЫТА'");
        static void Main(string[] args)
        {
            while (true)
            {
                Thread.Sleep(5000);
                Console.WriteLine("Продукт произведен");
                wh.Set();
            }
        }        
    }
}
