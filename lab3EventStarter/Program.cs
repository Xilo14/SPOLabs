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
        static int countProducts = 0;
        static EventWaitHandle wh = new EventWaitHandle(false, EventResetMode.AutoReset, "OOO'РОГА И КОПЫТА'");
        static void Main(string[] args)
        {
            Thread thread = new Thread(Produce);
            thread.Start();
            while (true)
            {
                //#мама спаси меня

            }

        }

        static void Produce()
        {
            while(true)
            {
                Thread.Sleep(5000);
                countProducts++;               
            }
        }
    }
}
