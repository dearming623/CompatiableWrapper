using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MoleQ.Support.Wrapper.Extension;
using MoleQ.Support.Wrapper.Common;
using System.Runtime.InteropServices;
using MQPLUAIWrapper;
using System.Threading;


namespace MoleQ.Support.Wrapper.Tester
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSetCameraId_Click(object sender, EventArgs e)
        {
            try
            {
                int res = NativeApi.SetCameraId(0);
                MessageBox.Show(NativeApi.ParserResultCode(res));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                SetTextOfResponse(ex.Message);

            }
        }

        public void ThreadProc()
        {
            for (int i = 0; i < 10; i++)
            {
                SetTextOfResponse(string.Format("ThreadProc: {0}", i));

                // Yield the rest of the time slice.
                Thread.Sleep(1000);
            }
        }

        public void ThreadProc(string taskName)
        { 
          
            SetTextOfResponse(taskName + " -> start");
            Console.WriteLine(taskName + " -> start");
            Thread.Sleep(2000);
            Console.WriteLine(taskName + " -> end");
            SetTextOfResponse(taskName + " -> end");
            
        }


        private void button1_Click(object sender, EventArgs e)
        {
            //Thread t = new Thread(new ThreadStart(ThreadProc));
            //t.Start();

            var version = pluaiWrapper.Version();
            MessageBox.Show(version);

            //MessageBox.Show(pluaiWrapper.TestCallBask());



            //try
            //{
            //    pluaiWrapper.DataReceived += new MQPLUAIWrapper.Tools.DataReceivedEventHandler(pluai_DataReceived);
            //    pluaiWrapper.OnDestory();

            //}
            //catch (Exception ex)
            //{

            //    MessageBox.Show(ex.Message);
            //}

        }



        private MyBrain mBrain;
        private void Identify_Click(object sender, EventArgs e)
        {

            //if (mBrain != null)
            //{
            //    mBrain.AsynIdentify();
            //}
            //else
            //{
            //    //NotifyError("The object is not initialized.");
            //}
            pluaiWrapper.Identify();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //if (mBrain == null)
            //{
            //    mBrain = new MyBrain();
            //}

            //mBrain.DataReceived += BrainDataReceived;
            //mBrain.AsynWakeUp();

            pluaiWrapper.SetEnableLogger(true);

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //if (mBrain != null)
            //{
            //    mBrain.ShutdownEvent += AfterBrainShutdown;
            //    mBrain.AsynShutdown();
            //}



        }

        public void BrainDataReceived(string result)
        {
            //MessageBox.Show(result);
            SetTextOfResponse(result);
        }

        public void AfterBrainShutdown()
        {
            MessageBox.Show("AfterBrainShutdown");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void btn_learn_Click(object sender, EventArgs e)
        {
            string assignName = tb_assign_name.Text;
            if (string.IsNullOrEmpty(assignName))
            {
                MessageBox.Show("Please input assign name.");
                return;
            }

            pluaiWrapper.Learn(assignName);

        }

        private void btn_on_create_Click(object sender, EventArgs e)
        {
            pluaiWrapper.OnCreate();
        }

        private void btn_on_destory_Click(object sender, EventArgs e)
        {
            pluaiWrapper.OnDestory();
        }


        public delegate void UpdateLabelDelegate(string newText);
        private void SetTextResponse(string msg)
        {
            tb_response.AppendText(string.Format("{0} \r\n{1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), msg));
            tb_response.AppendText(Environment.NewLine);
            tb_response.ScrollToCaret();

        }

        public void SetTextOfResponse(string newText)
        {
            if (this.tb_response.InvokeRequired)
            {
                //while (this.tb_response.IsHandleCreated)
                //{
                //    if (this.tb_response.Disposing || tb_response.IsDisposed)
                //    {
                //        return;
                //    }
                //}
                this.tb_response.Invoke(new UpdateLabelDelegate(SetTextOfResponse), newText);
            }
            else
            {
                this.SetTextResponse(newText);
            }
        }

        private void pluaiWrapper_DataReceived(string result)
        {
            SetTextOfResponse(result);
        }

        private void btn_test_thread_pool_Click(object sender, EventArgs e)
        {
            ThreadPool.SetMinThreads(1, 1);
            ThreadPool.SetMaxThreads(1, 10);
            for (int i = 0; i < 10; i++)
            {
                string taskName = string.Format("Thread: {0}", i);

                WaitCallback cb = new WaitCallback(delegate(object x)
                {
                    string name = x as string;
                    ThreadProc(name);
                });
                ThreadPool.QueueUserWorkItem(cb, taskName);
            }

        }
    }
}
