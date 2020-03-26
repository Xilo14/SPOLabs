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
            int A, B, i, j,n, posA=-1,posB=-1;
            string S;
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
                S = br.ReadString();
                Console.WriteLine("Done.");
                string[] inarr = S.Split();
                n = inarr.Length;
                int[] array = new int[n];
                for (i = 0; i < n; i++)
                {
                    array[i] = Convert.ToInt32(inarr[i]);
                    Console.WriteLine(array[i]);
                }
                Console.WriteLine("a={0} b={1}", A, B);
                for (i = 0; i < n; i++)
                {
                    if (array[i] == A){ posA = i; break; }
                }
                for (j = n-1; j >= 0; j--)
                {
                    if (array[j] == B) { posB = j; break; }
                }
                if (posA == -1 || posB == -1 || posA>posB) { Console.WriteLine("Диапазон некорректный!"); OUT = "Диапазон некорректный!"; }
                else
                {
                    for (i = posA; i <= posB; i++)
                    {
                        sm += array[i];
                    }
                        sm /= posB-posA+1;
                    OUT = sm.ToString();
                    Console.WriteLine(sm);
                }               
                Console.WriteLine("Нажмите любую клавишу!");
                Console.ReadKey();
 
                

                bw.Write(OUT);

                Environment.Exit(0);
            }
        }
    }
}