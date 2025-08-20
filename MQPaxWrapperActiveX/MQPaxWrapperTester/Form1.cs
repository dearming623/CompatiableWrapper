using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using DearMing.Common.Utils; 

namespace MQPaxWrapperTester
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // pax7 ip: 192.168.1.23
        // a35 ip:192.168.1.28
        private void button1_Click(object sender, EventArgs e)
        {
            int res = posLinkWrapper1.Init("TCP", 90, "192.168.1.23", "10009");
            if (res != 0)
            {
                MessageBox.Show("Faild to init.", "Result");
            }
            else
            {
                ShowResponse("Succ to init.");
            }
        }

        void posLinkWrapper1_onResponse(string res)
        {
            ShowResponse(res);

            string SN = string.Format("SN: {0}", posLinkWrapper1.SerialNumber);
            Console.WriteLine(SN);
            Console.WriteLine("onResponse Result -> " + res + "");
            //string SN = string.Format("SN: {0}", posLinkWrapper1.SerialNumber);
            //MessageBox.Show(SN + "\r\n\r\n" + " onResponse Result -> " + res, "Result");

            Console.WriteLine("posLinkWrapper.errorCode -> " + posLinkWrapper1.errorCode + "");
            Console.WriteLine("posLinkWrapper.errorMessage -> " + posLinkWrapper1.errorMessage + "");

            if (!string.IsNullOrEmpty(posLinkWrapper1.ResponseData))
            {
                string data = posLinkWrapper1.ResponseData;
                Console.WriteLine("ReportResponse Result -> " + data + "");
            }

            if (!string.IsNullOrEmpty(posLinkWrapper1.transResultMsg))
            {
                Console.WriteLine("transResultMsg Result -> " + posLinkWrapper1.transResultMsg + "");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int EDCType = Convert.ToInt32(textBox2.Text.ToString());
            string varName = textBox1.Text.ToString();
            posLinkWrapper1.GetVar(EDCType, varName);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            posLinkWrapper1.ReadCard(30);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new Thread(new ThreadStart(delegate()
            {
                posLinkWrapper1.RequestInputText("Enter Phone Number", 6, 30);
            })).Start();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            posLinkWrapper1.EnableLog(1);
            posLinkWrapper1.onResponse += new MQPaxWrapper.PosLinkWrapper.OnResponseEventHandler(posLinkWrapper1_onResponse);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string ECRRefNum = tv_ecr_ref_num.Text;
            new Thread(new ThreadStart(delegate()
            {
                posLinkWrapper1.QueryDetailReport(ECRRefNum);
            })).Start();
        }

        private void ShowResponse(string resp)
        {
            string str = string.Format("[{0}] Resp: {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), resp + "\r\n");
            //rtb_resp.AppendText(string.Format("[{0}] Resp: {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), resp+"\r\n"));
            //rtb_resp.ScrollToCaret();

            Console.WriteLine("PAX Wrapper -> Response: " + resp);

            //线程更新UI 示例(1)
            new UIHandler<RichTextBox, string>(rtb_resp, str)
                //.SetControl(rtb_resp)
                //.SetParams(str)
            .SetControlValueBy(new UIHandler<RichTextBox, string>.OnUpdateEventHandler(handler_OnUpdate));

            //线程更新UI 示例(2) 
            new UIManager().RunOnUiThread(rtb_resp,
             () =>
             {
                 handler_OnUpdate(rtb_resp, str);
             }); 
        }

        void handler_OnUpdate(RichTextBox control, string res)
        {
            control.AppendText(res);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            posLinkWrapper1.DoSignature(0, 30);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            posLinkWrapper1.Reset();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            posLinkWrapper1.CancelTrans();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            //NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
            //logger.Info("Hello world");   
            Log.d("WRAPPER", "This is debug message. ");
            Log.i("WRAPPER", "This is info message. ");
            Log.e("WRAPPER", "This is debug message. ");
            Log.w("WRAPPER", "This is warn message. ");
        }

        private void btn_disable_Click(object sender, EventArgs e)
        {
            Log.SetEnable(false);

        }

        private void btn_enable_Click(object sender, EventArgs e)
        {
            Log.SetEnable(true);
        }



    }
}
