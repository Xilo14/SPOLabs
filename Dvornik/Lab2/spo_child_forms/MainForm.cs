using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO.Pipes;
using System.IO;

namespace spo_child_forms
{
    public partial class MainForm : Form
    {
        string server_handle_in, server_handle_out;
        int A;
        int[] x;
        public MainForm(string[] args)
        {
            InitializeComponent();
            if (args.Length < 3) MessageBox.Show("Недостаточно параметров");
            else
            {
                A = Convert.ToInt32(args[0]);
                server_handle_in = args[1];
                server_handle_out = args[2];
            }
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            using (PipeStream pipeClientIn =
            new AnonymousPipeClientStream(PipeDirection.In, server_handle_in),
            pipeClientOut =
            new AnonymousPipeClientStream(PipeDirection.Out, server_handle_out))
            {
                BinaryReader br = new BinaryReader(pipeClientIn);
                int count = br.ReadInt32();
                x = new int[count];
                for (int i = 0; i < count; i++)
                {
                    x[i] = br.ReadInt32();
                    richTextBox1.Text += x[i].ToString() + ' ';
                }
                br.Close();
                
                if (A == 1)
                    Array.Sort(x);
                else
                    Array.Sort(x, (a, b) => b.CompareTo(a));

                BinaryWriter bw = new BinaryWriter(pipeClientOut);
                bw.Write(x.Length);
                foreach (int current in x)
                    bw.Write(current);
                pipeClientOut.WaitForPipeDrain();

                bw.Close();
            }

        }

    }
}