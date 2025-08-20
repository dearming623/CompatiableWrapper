namespace MQVideoPlayer
{
    partial class DisplayControl
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DisplayControl));
            this.axVLCPlugin1 = new AxAXVLC.AxVLCPlugin();
            ((System.ComponentModel.ISupportInitialize)(this.axVLCPlugin1)).BeginInit();
            this.SuspendLayout();
            // 
            // axVLCPlugin1
            // 
            this.axVLCPlugin1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axVLCPlugin1.Enabled = true;
            this.axVLCPlugin1.Location = new System.Drawing.Point(0, 0);
            this.axVLCPlugin1.Name = "axVLCPlugin1";
            this.axVLCPlugin1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axVLCPlugin1.OcxState")));
            this.axVLCPlugin1.Size = new System.Drawing.Size(400, 300);
            this.axVLCPlugin1.TabIndex = 2;
            this.axVLCPlugin1.stopEvent += new System.EventHandler(this.axVLCPlugin1_stopEvent);
            this.axVLCPlugin1.pauseEvent += new System.EventHandler(this.axVLCPlugin1_pauseEvent);
            this.axVLCPlugin1.Enter += new System.EventHandler(this.axVLCPlugin1_Enter);
            this.axVLCPlugin1.playEvent += new System.EventHandler(this.axVLCPlugin1_playEvent);
            // 
            // DisplayControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.axVLCPlugin1);
            this.Name = "DisplayControl";
            this.Size = new System.Drawing.Size(400, 300);
            ((System.ComponentModel.ISupportInitialize)(this.axVLCPlugin1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxAXVLC.AxVLCPlugin axVLCPlugin1;


    }
}
