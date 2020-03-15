using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab1liteversion
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            RegistryKey currentUserKey = Registry.LocalMachine;
            RegistryKey Key = currentUserKey.OpenSubKey("HARDWARE\\DESCRIPTION\\System\\CentralProcessor\\0");
            textBox1.Text = Key.GetValue("ProcessorNameString").ToString();
            textBox2.Text = Key.GetValue("~MHz").ToString();

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            RegistryKey currentUserKey = Registry.CurrentUser;
            RegistryKey Key = currentUserKey.OpenSubKey("SOFTWARE", true);
            RegistryKey yurokkeKey = Key.CreateSubKey("YurokkeKey");
            yurokkeKey.SetValue("datetoday", DateTime.Now);
            yurokkeKey.Close();
            Key.Close();
            MessageBox.Show("Done");
        }
        private void Button2_Click(object sender, EventArgs e)
        {
            RegistryKey currentUserKey = Registry.CurrentUser;
            RegistryKey key = currentUserKey.OpenSubKey("SOFTWARE\\YurokkeKey", true); 
            key.DeleteValue("datetoday");// удаляем значение из ключа
            key = currentUserKey.OpenSubKey("SOFTWARE", true);
            key.DeleteSubKey("YurokkeKey");// удаляем вложенный ключ
            key.Close();
            MessageBox.Show("Done");
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            RegistryKey currentUserKey = Registry.CurrentUser;
            RegistryKey Key = currentUserKey.OpenSubKey("Control Panel\\Desktop", true);
            Key.SetValue("MenuShowDelay", "12345678");
            Key.Close();
            MessageBox.Show("Done");
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            RegistryKey currentUserKey = Registry.CurrentUser;
            RegistryKey Key = currentUserKey.OpenSubKey("Control Panel\\Desktop", true);
            Key.SetValue("MenuShowDelay", "400");
            Key.Close();
            MessageBox.Show("Done");
        }
    }
}
