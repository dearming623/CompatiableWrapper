namespace MQPaxWrapperTester
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
            this.tbPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnPlay = new System.Windows.Forms.Button();
            this.videoPlayer = new MQVideoPlayer.DisplayControl();
            this.btnStop = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbPath
            // 
            this.tbPath.Location = new System.Drawing.Point(80, 338);
            this.tbPath.Name = "tbPath";
            this.tbPath.Size = new System.Drawing.Size(132, 22);
            this.tbPath.TabIndex = 6;
            this.tbPath.Text = "C:\\local\\sample.mp4";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(42, 341);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Path:";
            // 
            // btnPlay
            // 
            this.btnPlay.Location = new System.Drawing.Point(80, 364);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(75, 23);
            this.btnPlay.TabIndex = 10;
            this.btnPlay.Text = "Play";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // videoPlayer
            // 
            this.videoPlayer.Location = new System.Drawing.Point(12, 12);
            this.videoPlayer.Name = "videoPlayer";
            this.videoPlayer.Size = new System.Drawing.Size(400, 300);
            this.videoPlayer.TabIndex = 9;
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(161, 364);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 11;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(630, 451);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.videoPlayer);
            this.Controls.Add(this.tbPath);
            this.Controls.Add(this.label2);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Form1";
            this.Text = "Video Player Tester";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbPath;
        private MQVideoPlayer.DisplayControl videoPlayer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnStop;
    }
}

