using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MQWebApi;

namespace MoleQ.WebApi.Tester
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WebApiWrapper.EnableLogger(true);

            rtb_response.Text = "";

            string url = tb_url.Text;
            string body = rtb_request_body.Text;


            //WebApiWrapper.SetCharSet("ISO-8859-1");
            WebApiWrapper.OnPost(url, body);
        }

        void tools1_DataError(string errMsg)
        {
            MessageBox.Show(errMsg);
        }

        void tools1_DataReceived(string result)
        {
            Action<String> AsyncUIDelegate = delegate(string n) { rtb_response.Text = n; };//定义一个委托
            rtb_response.Invoke(AsyncUIDelegate, new object[] { result });
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show(WebApiWrapper.GetTextForPB7(""));
        }

        private void btn_OnPostSP_Click(object sender, EventArgs e)
        {
            WebApiWrapper.EnableLogger(true);

            string url = tb_url.Text;
            string body = rtb_request_body.Text;
            string stn = tb_stn.Text;

            string xmlCfg =
                        @"<WrapperConfig>
                            <CharSet>ISO-8859-1</CharSet>
                            <Dsn>market</Dsn>
                            <Uid>adm0</Uid>
                            <Pwd>systemcom</Pwd>
                            <Stn>{0}</Stn>
                        </WrapperConfig>";

            xmlCfg = string.Format(xmlCfg, stn);
            WebApiWrapper.SetConfig(xmlCfg);
            WebApiWrapper.OnPost2(url, body, 30 * 1000);
        }

        private void btn_OnPostSPWithSaveSqliteDB_Click(object sender, EventArgs e)
        {
            string dbPath = tb_db_path.Text;
            string url = tb_url.Text;
            string body = rtb_request_body.Text;
            WebApiWrapper.OnPostSPThenToSqliteDB(url, body, 30 * 1000, dbPath);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            WebApiWrapper.DataError += new MQWebApi.Tools.DataErrordEventHandler(tools1_DataError);
            WebApiWrapper.DataReceived += new MQWebApi.Tools.DataReceivedEventHandler(tools1_DataReceived);
        }

        private void btn_test_storage_Click(object sender, EventArgs e)
        {
            LocalStorage.SetItem("AuthOEM", Convert.ToBoolean(false));


            MessageBox.Show(LocalStorage.GetItem("AuthOEM").ToString());
         
           
        }

        private void btnStartMonitor_Click(object sender, EventArgs e)
        {
            var folderPath = tbListenFolder.Text.Trim();
            WebApiWrapper.OnBindMonitor(folderPath);
            WebApiWrapper.OnStartMonitor();

            btnStartMonitor.Enabled = false;
            btnStopMonitor.Enabled = true;
        }

        private void btnStopMonitor_Click(object sender, EventArgs e)
        {
            WebApiWrapper.OnStopMonitor();
            btnStartMonitor.Enabled = true;
            btnStopMonitor.Enabled = false;
        }
    }
}
