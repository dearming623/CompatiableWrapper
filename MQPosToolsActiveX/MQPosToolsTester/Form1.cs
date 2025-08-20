using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace MQPosToolsTester
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string barcode = tb_barcode.Text.Trim();
            string fileName = tb_file_name.Text.Trim();

            string appPath = System.AppDomain.CurrentDomain.BaseDirectory;
            string path = Path.Combine(appPath, fileName);
            int res = MQPosToolsWrapper.GenerateQRCode(barcode, path, 500, 500);

            if (res == 0)
            {
                picBoxRes.Load(path);
            }
            else
            {
                MessageBox.Show("Error Result Code: " + res);
            }
  

        }


    }
}
