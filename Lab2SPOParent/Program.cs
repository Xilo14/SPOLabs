using System;
using System.IO.Pipes;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Lab2SPO
{
    class PipeParent
    {
        static string NamePipe = "PleaseFindMe";
        static string s,a,b;

        static NamedPipeServerStream pipeParent;
        static void Main()
        {
            Input();

            Process proc = new Process();
            proc.StartInfo.Arguments = a.ToString() + ' ' + b.ToString();
            proc.StartInfo.FileName = @"D:\Учёба\CiSharp2019Studio\spo 2020\Lab2SPOChild\bin\Release\Lab2SPOChild.exe";
            proc.Start();
            Thread.Sleep(1000);
            Thread thr = new Thread(new ThreadStart(ServerThread));
            thr.Start();

            Console.ReadLine();
        }

        private static void Input()
        {

            Console.WriteLine("Input Array: ");
            s = Console.ReadLine();

            Console.WriteLine("Input a: ");
            a = Console.ReadLine();
            Console.WriteLine("Input b: ");
            b = Console.ReadLine();
        }
        private static void ServerThread()
        {
            try
            {
                using (pipeParent = new NamedPipeServerStream("PleaseFindMe", PipeDirection.InOut))
                using (BinaryWriter bw = new BinaryWriter(pipeParent))
                using (BinaryReader br = new BinaryReader(pipeParent))
                {
                    Console.Write("Waiting For Connection........");

                    pipeParent.WaitForConnection();
                    if (pipeParent.IsConnected == true)
                    {
                        Console.WriteLine("Connected.");
                        bw.Write(s);
                    }
                    pipeParent.WaitForPipeDrain();

                    Console.WriteLine("Result: {0} ", br.ReadString());

                    pipeParent.Disconnect();
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("ERROR: {0}", e.Message);
                Console.ReadKey();
            }

        }
    }
}