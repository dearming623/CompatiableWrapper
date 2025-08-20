using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MQOfficeToolsTester
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            ClickSend(randomPath());
        }

        public void ClickSend(string outputFileName)
        {
            tools1.SetEnableLogger(true);

            string url = tb_url.Text;
            string jsonFile = tb_json_file.Text;
            string saveFile = tb_save_path.Text;
            tools1.FileConversion(url, jsonFile, outputFileName);
        }

        public string randomPath()
        {
            string path = "C:\\Documents and Settings\\Ben_MoleQ\\Desktop\\1\\{0}.xlxs";
            return string.Format(path, randomStr()) ;
        }

        public string randomStr()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }

        private void tools1_FileCnvtDone(string result)
        {
            ClickSend(randomPath());
           // MessageBox.Show(result);
            Console.Write(" ---------------->>>>> " + result);
        }

        private void tools1_FileCnvtError(string error_msg)
        {
             MessageBox.Show(error_msg);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int res = tools1.GenerateQRCode("www.baidu.com", @"C:\Documents and Settings\Ben_MoleQ\Desktop\qrcode.bmp", 500, 500);
        }
        
    }
}
