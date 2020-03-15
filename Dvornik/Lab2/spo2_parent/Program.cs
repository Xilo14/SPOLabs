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
            int[] SourceArray;
            int A;
            try
            {
                Console.WriteLine("Введите массив:");
                string[] inputs = Console.ReadLine().Split(new char[] { ' ', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                SourceArray = new int[inputs.Length];

            
                foreach (var input in inputs.Select((v, i) => new { Value = v, Index = i }))
                    SourceArray[input.Index] = Convert.ToInt32(input.Value);

                Console.WriteLine("Введите число А(1/0):");
                A = Convert.ToInt32(Console.ReadLine());
                if (A != 0 && A != 1)
                {
                    Console.WriteLine("Неверное число А!");
                    Console.ReadLine();
                    return;
                }


            }
            catch
            {
                Console.WriteLine("Неверынй формат ввода!");
                Console.ReadLine();
                return;
            }
            using (
                AnonymousPipeServerStream pipeServerOut =
                new AnonymousPipeServerStream(PipeDirection.Out, HandleInheritability.Inheritable),
                pipeServerIn =
                new AnonymousPipeServerStream(PipeDirection.In, HandleInheritability.Inheritable)
                )
            {
                Process child = new Process();
                child.StartInfo.Arguments =  A.ToString() + ' ' + pipeServerOut.GetClientHandleAsString()
                    + ' ' + pipeServerIn.GetClientHandleAsString();
                child.StartInfo.UseShellExecute = false;
                //child.StartInfo.FileName =
                //    @"F:\Repos\Visual stdio\SPOLabs\Dvornik\Lab2\spo2_child\bin\Debug\spo2_child.exe";
                child.StartInfo.FileName =
                    @"F:\Repos\Visual stdio\SPOLabs\Dvornik\Lab2\spo_child_forms\bin\Debug\spo_child_forms.exe";
                child.Start();

                
                BinaryWriter bw = new BinaryWriter(pipeServerOut);
                bw.Write(SourceArray.Length);
                foreach (int current in SourceArray)
                    bw.Write(current);
                pipeServerOut.WaitForPipeDrain();

                string result = "";
                BinaryReader br = new BinaryReader(pipeServerIn);
                int count = br.ReadInt32();
                SourceArray = new int[count];
                for (int i = 0; i < count; i++)
                {
                    SourceArray[i] = br.ReadInt32();
                    result += SourceArray[i].ToString() + ' ';
                }
                br.Close();

                Console.WriteLine("Результат:");
                Console.WriteLine(result);

                Console.ReadLine();

            }
        }
    }
}
