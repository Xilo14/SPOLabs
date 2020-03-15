using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO.Pipes;
using System.IO;

namespace spo2_child
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] x;
            string server_handle;
            if (args.Length < 3) Console.WriteLine("Недостаточно параметров");
            else
            {
                server_handle = args[2];
                using (PipeStream pipeClient = new AnonymousPipeClientStream(PipeDirection.In, server_handle))
                {
                    BinaryReader sr = new BinaryReader(pipeClient);
                    int count = sr.ReadInt32();
                    x = new int[count];
                    for (int i = 0; i < count; i++)
                        x[i] = sr.ReadInt32();
                    sr.Close();
                    
                }
            }

        }
    }
}