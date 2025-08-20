namespace MQPLUAIWrapper
{
    partial class ProcessingForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Info0 = new System.Windows.Forms.Label();
            this.Info1 = new System.Windows.Forms.Label();
            this.Info2 = new System.Windows.Forms.Label();
            this.Info3 = new System.Windows.Forms.Label();
            this.Info4 = new System.Windows.Forms.Label();
            this.Info5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Info0
            // 
            this.Info0.AllowDrop = true;
            this.Info0.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Info0.Location = new System.Drawing.Point(56, 24);
            this.Info0.Name = "Info0";
            this.Info0.Size = new System.Drawing.Size(263, 30);
            this.Info0.TabIndex = 1;
            this.Info0.Text = "Please Wait...";
            this.Info0.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Info1
            // 
            this.Info1.AllowDrop = true;
            this.Info1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Info1.Location = new System.Drawing.Point(50, 48);
            this.Info1.Name = "Info1";
            this.Info1.Size = new System.Drawing.Size(34, 33);
            this.Info1.TabIndex = 0;
            this.Info1.Text = "..";
            this.Info1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Info2
            // 
            this.Info2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Info2.Location = new System.Drawing.Point(50, 48);
            this.Info2.Name = "Info2";
            this.Info2.Size = new System.Drawing.Size(50, 33);
            this.Info2.TabIndex = 2;
            this.Info2.Text = "....";
            // 
            // Info3
            // 
            this.Info3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Info3.Location = new System.Drawing.Point(50, 48);
            this.Info3.Name = "Info3";
            this.Info3.Size = new System.Drawing.Size(78, 33);
            this.Info3.TabIndex = 3;
            this.Info3.Text = ".......";
            // 
            // Info4
            // 
            this.Info4.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Info4.Location = new System.Drawing.Point(50, 48);
            this.Info4.Name = "Info4";
            this.Info4.Size = new System.Drawing.Size(142, 33);
            this.Info4.TabIndex = 4;
            this.Info4.Text = "..............";
            // 
            // Info5
            // 
            this.Info5.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Info5.Location = new System.Drawing.Point(50, 48);
            this.Info5.Name = "Info5";
            this.Info5.Size = new System.Drawing.Size(204, 33);
            this.Info5.TabIndex = 5;
            this.Info5.Text = ".....................";
            // 
            // ProcessingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 103);
            this.ControlBox = false;
            this.Controls.Add(this.Info5);
            this.Controls.Add(this.Info4);
            this.Controls.Add(this.Info3);
            this.Controls.Add(this.Info2);
            this.Controls.Add(this.Info1);
            this.Controls.Add(this.Info0);
            this.MaximumSize = new System.Drawing.Size(377, 119);
            this.MinimumSize = new System.Drawing.Size(377, 119);
            this.Name = "ProcessingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.ProcessingForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label Info0;
        private System.Windows.Forms.Label Info1;
        private System.Windows.Forms.Label Info2;
        private System.Windows.Forms.Label Info3;
        private System.Windows.Forms.Label Info4;
        private System.Windows.Forms.Label Info5;
    }
}