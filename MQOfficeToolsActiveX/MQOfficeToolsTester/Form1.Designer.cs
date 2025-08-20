namespace MQOfficeToolsTester
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
            this.tools1 = new MQOfficeTools.Tools();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_url = new System.Windows.Forms.TextBox();
            this.tb_json_file = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_save_path = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tools1
            // 
            this.tools1.ErrorMsg = null;
            this.tools1.ErrorNum = ((long)(0));
            this.tools1.Location = new System.Drawing.Point(23, 12);
            this.tools1.Name = "tools1";
            this.tools1.Size = new System.Drawing.Size(83, 84);
            this.tools1.TabIndex = 0;
            this.tools1.FileCnvtDone += new MQOfficeTools.Tools.FileCnvtDoneEventHandler(this.tools1_FileCnvtDone);
            this.tools1.FileCnvtError += new MQOfficeTools.Tools.FileCnvtErrorEventHandler(this.tools1_FileCnvtError);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(115, 209);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Send";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(77, 134);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "URL:";
            // 
            // tb_url
            // 
            this.tb_url.Location = new System.Drawing.Point(115, 131);
            this.tb_url.Name = "tb_url";
            this.tb_url.Size = new System.Drawing.Size(388, 20);
            this.tb_url.TabIndex = 3;
            this.tb_url.Text = "http://121.33.8.32:19802/api/FileManager/JsonToExcel";
            // 
            // tb_json_file
            // 
            this.tb_json_file.Location = new System.Drawing.Point(115, 157);
            this.tb_json_file.Name = "tb_json_file";
            this.tb_json_file.Size = new System.Drawing.Size(388, 20);
            this.tb_json_file.TabIndex = 5;
            this.tb_json_file.Text = "C:\\Documents and Settings\\Ben_MoleQ\\Desktop\\1\\1.json";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 160);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "From Json File Path:";
            // 
            // tb_save_path
            // 
            this.tb_save_path.Location = new System.Drawing.Point(115, 183);
            this.tb_save_path.Name = "tb_save_path";
            this.tb_save_path.Size = new System.Drawing.Size(388, 20);
            this.tb_save_path.TabIndex = 7;
            this.tb_save_path.Text = "C:\\Documents and Settings\\Ben_MoleQ\\Desktop\\1\\output.xlxs";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 186);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Output File Path:";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(115, 239);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(136, 23);
            this.button2.TabIndex = 8;
            this.button2.Text = "Generate QRCode";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(515, 275);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.tb_save_path);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tb_json_file);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tb_url);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tools1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MQOfficeTools.Tools tools1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_url;
        private System.Windows.Forms.TextBox tb_json_file;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_save_path;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button2;
    }
}

