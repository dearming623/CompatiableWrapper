namespace MQPosToolsTester
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
            this.button1 = new System.Windows.Forms.Button();
            this.MQPosToolsWrapper = new MQPosTools.Tools();
            this.lbl_barcode = new System.Windows.Forms.Label();
            this.tb_barcode = new System.Windows.Forms.TextBox();
            this.picBoxRes = new System.Windows.Forms.PictureBox();
            this.tb_file_name = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxRes)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(63, 73);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(123, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Generate QRCode";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MQPosToolsWrapper
            // 
            this.MQPosToolsWrapper.Location = new System.Drawing.Point(322, 12);
            this.MQPosToolsWrapper.Name = "MQPosToolsWrapper";
            this.MQPosToolsWrapper.Size = new System.Drawing.Size(72, 71);
            this.MQPosToolsWrapper.TabIndex = 1;
            // 
            // lbl_barcode
            // 
            this.lbl_barcode.AutoSize = true;
            this.lbl_barcode.Location = new System.Drawing.Point(12, 22);
            this.lbl_barcode.Name = "lbl_barcode";
            this.lbl_barcode.Size = new System.Drawing.Size(51, 13);
            this.lbl_barcode.TabIndex = 2;
            this.lbl_barcode.Text = "BarCode:";
            // 
            // tb_barcode
            // 
            this.tb_barcode.Location = new System.Drawing.Point(63, 19);
            this.tb_barcode.Name = "tb_barcode";
            this.tb_barcode.Size = new System.Drawing.Size(219, 20);
            this.tb_barcode.TabIndex = 3;
            this.tb_barcode.Text = "https://www.moleq.com/BarcodeGenerator";
            // 
            // picBoxRes
            // 
            this.picBoxRes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBoxRes.Location = new System.Drawing.Point(15, 110);
            this.picBoxRes.Name = "picBoxRes";
            this.picBoxRes.Size = new System.Drawing.Size(258, 200);
            this.picBoxRes.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBoxRes.TabIndex = 4;
            this.picBoxRes.TabStop = false;
            // 
            // tb_file_name
            // 
            this.tb_file_name.Location = new System.Drawing.Point(63, 47);
            this.tb_file_name.Name = "tb_file_name";
            this.tb_file_name.Size = new System.Drawing.Size(100, 20);
            this.tb_file_name.TabIndex = 5;
            this.tb_file_name.Text = "res_qrcode.bmp";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "File Name:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 322);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_file_name);
            this.Controls.Add(this.picBoxRes);
            this.Controls.Add(this.tb_barcode);
            this.Controls.Add(this.lbl_barcode);
            this.Controls.Add(this.MQPosToolsWrapper);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "MQPosTools Tester";
            ((System.ComponentModel.ISupportInitialize)(this.picBoxRes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private MQPosTools.Tools MQPosToolsWrapper;
        private System.Windows.Forms.Label lbl_barcode;
        private System.Windows.Forms.TextBox tb_barcode;
        private System.Windows.Forms.PictureBox picBoxRes;
        private System.Windows.Forms.TextBox tb_file_name;
        private System.Windows.Forms.Label label1;
    }
}

