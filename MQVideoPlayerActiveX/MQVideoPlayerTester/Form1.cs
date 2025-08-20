using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace MQPaxWrapperTester
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
 

        void posLinkWrapper1_onResponse(string res)
        { 
            Console.WriteLine("onResponse Result -> " + res + ""); 
        }
 
        private void Form1_Load(object sender, EventArgs e)
        {
            videoPlayer.OnResponse += new MQVideoPlayer.DisplayControl.OnResponseEventHandler(posLinkWrapper1_onResponse);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //string ECRRefNum = tv_ecr_ref_num.Text;
            //new Thread(new ThreadStart(delegate()
            //{
            //    posLinkWrapper1.QueryDetailReport(ECRRefNum);
            //})).Start();
        }

        //private void ShowResponse(string resp)
        //{
        //    string str = string.Format("[{0}] Resp: {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), resp + "\r\n");
        //    //rtb_resp.AppendText(string.Format("[{0}] Resp: {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), resp+"\r\n"));
        //    //rtb_resp.ScrollToCaret();

        //    Console.WriteLine("PAX Wrapper -> Response: " + resp);

        //    //线程更新UI 示例(1)
        //    new UIHandler<RichTextBox, string>(rtb_resp, str)
        //        //.SetControl(rtb_resp)
        //        //.SetParams(str)
        //    .SetControlValueBy(new UIHandler<RichTextBox, string>.OnUpdateEventHandler(handler_OnUpdate));

        //    //线程更新UI 示例(2) 
        //    new UIManager().RunOnUiThread(rtb_resp,
        //     () =>
        //     {
        //         handler_OnUpdate(rtb_resp, str);
        //     }); 
        //}

         
        private void btnStop_Click(object sender, EventArgs e)
        {
            videoPlayer.OnStop();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            string path = tbPath.Text.ToString();
            videoPlayer.OnPlay(path);
        }



    }
}
