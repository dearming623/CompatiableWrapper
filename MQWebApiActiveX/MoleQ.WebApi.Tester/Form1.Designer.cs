namespace MoleQ.WebApi.Tester
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btn_onpost2 = new System.Windows.Forms.Button();
            this.rtb_request_body = new System.Windows.Forms.RichTextBox();
            this.rtb_response = new System.Windows.Forms.RichTextBox();
            this.tb_url = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_OnPostSP = new System.Windows.Forms.Button();
            this.tb_db_path = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_OnPostSPWithSaveSqlite = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_stn = new System.Windows.Forms.TextBox();
            this.btn_test_storage = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnStartMonitor = new System.Windows.Forms.Button();
            this.btnStopMonitor = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tbListenFolder = new System.Windows.Forms.TextBox();
            this.rtbMessageReceive = new System.Windows.Forms.RichTextBox();
            this.WebApiWrapper = new MQWebApi.Tools();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_onpost2
            // 
            this.btn_onpost2.Location = new System.Drawing.Point(298, 421);
            this.btn_onpost2.Name = "btn_onpost2";
            this.btn_onpost2.Size = new System.Drawing.Size(75, 23);
            this.btn_onpost2.TabIndex = 0;
            this.btn_onpost2.Text = "OnPost2";
            this.btn_onpost2.UseVisualStyleBackColor = true;
            this.btn_onpost2.Click += new System.EventHandler(this.button1_Click);
            // 
            // rtb_request_body
            // 
            this.rtb_request_body.Location = new System.Drawing.Point(73, 36);
            this.rtb_request_body.Name = "rtb_request_body";
            this.rtb_request_body.Size = new System.Drawing.Size(300, 379);
            this.rtb_request_body.TabIndex = 2;
            this.rtb_request_body.Text = resources.GetString("rtb_request_body.Text");
            // 
            // rtb_response
            // 
            this.rtb_response.Location = new System.Drawing.Point(379, 36);
            this.rtb_response.Name = "rtb_response";
            this.rtb_response.Size = new System.Drawing.Size(366, 379);
            this.rtb_response.TabIndex = 3;
            this.rtb_response.Text = "";
            // 
            // tb_url
            // 
            this.tb_url.Location = new System.Drawing.Point(73, 10);
            this.tb_url.Name = "tb_url";
            this.tb_url.Size = new System.Drawing.Size(300, 20);
            this.tb_url.TabIndex = 4;
            this.tb_url.Text = "http://192.168.1.32:3366/MQPay/ProcessTrans";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(639, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(106, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Get Text For PB7";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // btn_OnPostSP
            // 
            this.btn_OnPostSP.Location = new System.Drawing.Point(220, 483);
            this.btn_OnPostSP.Name = "btn_OnPostSP";
            this.btn_OnPostSP.Size = new System.Drawing.Size(75, 23);
            this.btn_OnPostSP.TabIndex = 6;
            this.btn_OnPostSP.Text = "OnPostSP";
            this.btn_OnPostSP.UseVisualStyleBackColor = true;
            this.btn_OnPostSP.Click += new System.EventHandler(this.btn_OnPostSP_Click);
            // 
            // tb_db_path
            // 
            this.tb_db_path.Location = new System.Drawing.Point(128, 455);
            this.tb_db_path.Name = "tb_db_path";
            this.tb_db_path.Size = new System.Drawing.Size(300, 20);
            this.tb_db_path.TabIndex = 7;
            this.tb_db_path.Text = "\\\\192.168.1.69\\Work\\Ming\\webapi.db";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 459);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Sqlite Database path:";
            // 
            // btn_OnPostSPWithSaveSqlite
            // 
            this.btn_OnPostSPWithSaveSqlite.Location = new System.Drawing.Point(128, 517);
            this.btn_OnPostSPWithSaveSqlite.Name = "btn_OnPostSPWithSaveSqlite";
            this.btn_OnPostSPWithSaveSqlite.Size = new System.Drawing.Size(153, 23);
            this.btn_OnPostSPWithSaveSqlite.TabIndex = 9;
            this.btn_OnPostSPWithSaveSqlite.Text = "OnPostSP With Save Sqlite";
            this.btn_OnPostSPWithSaveSqlite.UseVisualStyleBackColor = true;
            this.btn_OnPostSPWithSaveSqlite.Click += new System.EventHandler(this.btn_OnPostSPWithSaveSqliteDB_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 492);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(150, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "OnPostSP With Save Sybase:";
            // 
            // tb_stn
            // 
            this.tb_stn.Location = new System.Drawing.Point(169, 485);
            this.tb_stn.Name = "tb_stn";
            this.tb_stn.Size = new System.Drawing.Size(45, 20);
            this.tb_stn.TabIndex = 11;
            this.tb_stn.Text = "9";
            // 
            // btn_test_storage
            // 
            this.btn_test_storage.Location = new System.Drawing.Point(11, 517);
            this.btn_test_storage.Name = "btn_test_storage";
            this.btn_test_storage.Size = new System.Drawing.Size(111, 23);
            this.btn_test_storage.TabIndex = 12;
            this.btn_test_storage.Text = "Storage::SetItem";
            this.btn_test_storage.UseVisualStyleBackColor = true;
            this.btn_test_storage.Click += new System.EventHandler(this.btn_test_storage_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rtbMessageReceive);
            this.groupBox1.Controls.Add(this.tbListenFolder);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btnStopMonitor);
            this.groupBox1.Controls.Add(this.btnStartMonitor);
            this.groupBox1.Location = new System.Drawing.Point(434, 421);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(311, 137);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Message Receiver";
            // 
            // btnStartMonitor
            // 
            this.btnStartMonitor.Location = new System.Drawing.Point(133, 45);
            this.btnStartMonitor.Name = "btnStartMonitor";
            this.btnStartMonitor.Size = new System.Drawing.Size(75, 23);
            this.btnStartMonitor.TabIndex = 0;
            this.btnStartMonitor.Text = "Start Monitor";
            this.btnStartMonitor.UseVisualStyleBackColor = true;
            this.btnStartMonitor.Click += new System.EventHandler(this.btnStartMonitor_Click);
            // 
            // btnStopMonitor
            // 
            this.btnStopMonitor.Enabled = false;
            this.btnStopMonitor.Location = new System.Drawing.Point(214, 45);
            this.btnStopMonitor.Name = "btnStopMonitor";
            this.btnStopMonitor.Size = new System.Drawing.Size(75, 23);
            this.btnStopMonitor.TabIndex = 1;
            this.btnStopMonitor.Text = "Stop Monitor";
            this.btnStopMonitor.UseVisualStyleBackColor = true;
            this.btnStopMonitor.Click += new System.EventHandler(this.btnStopMonitor_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Listen Folder:";
            // 
            // tbListenFolder
            // 
            this.tbListenFolder.Location = new System.Drawing.Point(89, 19);
            this.tbListenFolder.Name = "tbListenFolder";
            this.tbListenFolder.Size = new System.Drawing.Size(200, 20);
            this.tbListenFolder.TabIndex = 3;
            this.tbListenFolder.Text = "\\\\192.168.1.69\\Work\\Ming\\tmp";
            // 
            // rtbMessageReceive
            // 
            this.rtbMessageReceive.Location = new System.Drawing.Point(16, 74);
            this.rtbMessageReceive.Name = "rtbMessageReceive";
            this.rtbMessageReceive.Size = new System.Drawing.Size(273, 57);
            this.rtbMessageReceive.TabIndex = 4;
            this.rtbMessageReceive.Text = "";
            // 
            // WebApiWrapper
            // 
            this.WebApiWrapper.Location = new System.Drawing.Point(7, 10);
            this.WebApiWrapper.Name = "WebApiWrapper";
            this.WebApiWrapper.Size = new System.Drawing.Size(60, 54);
            this.WebApiWrapper.TabIndex = 1;
            this.WebApiWrapper.wrapperConfig = null;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(761, 570);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_test_storage);
            this.Controls.Add(this.tb_stn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_OnPostSPWithSaveSqlite);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_db_path);
            this.Controls.Add(this.btn_OnPostSP);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tb_url);
            this.Controls.Add(this.rtb_response);
            this.Controls.Add(this.rtb_request_body);
            this.Controls.Add(this.WebApiWrapper);
            this.Controls.Add(this.btn_onpost2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_onpost2;
        private MQWebApi.Tools WebApiWrapper;
        private System.Windows.Forms.RichTextBox rtb_request_body;
        private System.Windows.Forms.RichTextBox rtb_response;
        private System.Windows.Forms.TextBox tb_url;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btn_OnPostSP;
        private System.Windows.Forms.TextBox tb_db_path;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_OnPostSPWithSaveSqlite;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_stn;
        private System.Windows.Forms.Button btn_test_storage;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnStartMonitor;
        private System.Windows.Forms.Button btnStopMonitor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbListenFolder;
        private System.Windows.Forms.RichTextBox rtbMessageReceive;
    }
}

