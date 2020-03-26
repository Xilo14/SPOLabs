using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace spo1
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            RegistryKey currentUserKey = Registry.LocalMachine;
            RegistryKey Key = currentUserKey.OpenSubKey("HARDWARE\\DESCRIPTION\\System\\BIOS");
            textBox1.Text = Key.GetValue("SystemManufacturer").ToString();
            

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            RegistryKey currentUserKey = Registry.CurrentUser;
            RegistryKey Key = currentUserKey.OpenSubKey("SOFTWARE", true);
            RegistryKey DvornikKey = Key.CreateSubKey("Dvornik");
            DvornikKey.SetValue("DATE", DateTime.Now);
            DvornikKey.Close();
            Key.Close();
            MessageBox.Show("Gotovo", "Дан", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void Button2_Click(object sender, EventArgs e)
        {
            RegistryKey currentUserKey = Registry.CurrentUser;
            RegistryKey key = currentUserKey.OpenSubKey("SOFTWARE\\Dvornik", true);
            key.DeleteValue("DATE");// удаляем значение из ключа
            key = currentUserKey.OpenSubKey("SOFTWARE", true);
            key.DeleteSubKey("Dvornik");// удаляем вложенный ключ
            key.Close();
            MessageBox.Show("Gotovo", "Дан", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            RegistryKey currentUserKey = Registry.CurrentUser;
            RegistryKey Key = currentUserKey.OpenSubKey("Control Panel\\Desktop", true);
            Key.SetValue("ScreenSaveIsSecure", "0");
            Key.Close();
            MessageBox.Show("Gotovo", "Дан", MessageBoxButtons.OK,MessageBoxIcon.Information);
        }
    }

}
