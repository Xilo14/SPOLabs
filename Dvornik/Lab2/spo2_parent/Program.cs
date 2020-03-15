using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO.Pipes;
using System.IO;

namespace spo2_parent
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] x = new int[20];

            Process child = new Process();
            using (AnonymousPipeServerStream pipeServer =
              new AnonymousPipeServerStream(PipeDirection.Out, HandleInheritability.Inheritable))
            {////////////////////////////// запуск потомка
                child.StartInfo.Arguments = "1 7 " + pipeServer.GetClientHandleAsString();
                child.StartInfo.UseShellExecute = false;
                child.StartInfo.FileName = @"d:\ProjectC#\неименованные каналы\ spo2_child.exe";
                child.Start();
                /////////////////////////////// запись в канал
                BinaryWriter sr = new BinaryWriter(pipeServer);
                sr.Write(x.Length);
                foreach (int current in x)
                    sr.Write(current);
                pipeServer.WaitForPipeDrain();

            }
        }
    }
}
