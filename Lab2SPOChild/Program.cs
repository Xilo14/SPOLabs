using System;
using System.IO;
using System.IO.Pipes;

using System.Threading;

namespace Lab2SPO_child
{
    class PipeChild
    {
        static void Main()
        {
            string OUT = "";
            int A, B, i, j = 0;
            float sm = 0;
            using (NamedPipeClientStream pipeClient = new NamedPipeClientStream(".", "PleaseFindMe", PipeDirection.InOut))
            using (BinaryWriter bw = new BinaryWriter(pipeClient))
            using (BinaryReader br = new BinaryReader(pipeClient))
            {
                Console.Write("Attempting to connect to pipe......");
                pipeClient.Connect();
                Console.WriteLine("Connected.");
                Console.WriteLine("There are currently {0} pipe server instances open.", pipeClient.NumberOfServerInstances);

                Console.Write("Receiving data.......");
                A = br.ReadInt32();
                B = br.ReadInt32();
                Console.WriteLine("Done.");
                Console.WriteLine("a={0} b={1}", A, B);
                i = A;
                while (i <= B)
                {
                    sm += i;
                    j++;
                    i++;
                    Console.WriteLine(sm);
                } 
                sm /= j;
                Console.WriteLine(sm);
                Console.WriteLine("Нажмите любую клавишу!");
                Console.ReadKey();
                OUT = sm.ToString();

                bw.Write(OUT);

                Environment.Exit(0);
            }
        }
    }
}