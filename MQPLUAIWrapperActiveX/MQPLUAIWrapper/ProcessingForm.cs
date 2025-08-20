using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MQPLUAIWrapper
{
    public partial class ProcessingForm : Form
    {

        int flag = 1;
        public ProcessingForm()
        {
            InitializeComponent();
        }

        public void timer_Tick(object sender, EventArgs e)
        {
            if (flag == 1)
            {
                this.Info1.Visible = true;
                this.Info2.Visible = false;
                this.Info3.Visible = false;
                this.Info4.Visible = false;
                this.Info5.Visible = false;
                flag = 2;

            }
            else if (flag == 2)
            {

                this.Info1.Visible = false;
                this.Info2.Visible = true;
                this.Info3.Visible = false;
                this.Info4.Visible = false;
                this.Info5.Visible = false;
                flag = 3;
            }
            else if (flag == 3)
            {

                this.Info1.Visible = false;
                this.Info2.Visible = false;
                this.Info3.Visible = true;
                this.Info4.Visible = false;
                this.Info5.Visible = false;
                flag = 4;
            }
            else if (flag == 4)
            {

                this.Info1.Visible = false;
                this.Info2.Visible = false;
                this.Info3.Visible = false;
                this.Info4.Visible = true;
                this.Info5.Visible = false;
                flag = 5;
            }
            else if (flag == 5)
            {

                this.Info1.Visible = false;
                this.Info2.Visible = false;
                this.Info3.Visible = false;
                this.Info4.Visible = false;
                this.Info5.Visible = true;
                flag = 1;
            }

        }


        private void ProcessingForm_Load(object sender, EventArgs e)
        {
            Timer timer1 = new Timer();
            timer1.Interval = 1000;
            timer1.Tick += new EventHandler(timer_Tick);
            timer1.Enabled = true;
        }
    }
}
