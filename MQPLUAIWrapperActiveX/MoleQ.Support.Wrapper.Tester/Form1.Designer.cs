namespace MoleQ.Support.Wrapper.Tester
{
    partial class Form1
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
            this.btnSetCameraId = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.tb_response = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_learn = new System.Windows.Forms.Button();
            this.tb_assign_name = new System.Windows.Forms.TextBox();
            this.btn_on_create = new System.Windows.Forms.Button();
            this.btn_on_destory = new System.Windows.Forms.Button();
            this.pluaiWrapper = new MQPLUAIWrapper.Tools();
            this.btn_test_thread_pool = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSetCameraId
            // 
            this.btnSetCameraId.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetCameraId.Location = new System.Drawing.Point(12, 144);
            this.btnSetCameraId.Name = "btnSetCameraId";
            this.btnSetCameraId.Size = new System.Drawing.Size(83, 24);
            this.btnSetCameraId.TabIndex = 0;
            this.btnSetCameraId.Text = "SetCameraId";
            this.btnSetCameraId.UseVisualStyleBackColor = true;
            this.btnSetCameraId.Click += new System.EventHandler(this.btnSetCameraId_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(12, 103);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(83, 24);
            this.button1.TabIndex = 2;
            this.button1.Text = "Version";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(134, 103);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(83, 24);
            this.button2.TabIndex = 3;
            this.button2.Text = "Identify";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Identify_Click);
            // 
            // tb_response
            // 
            this.tb_response.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_response.Location = new System.Drawing.Point(272, 33);
            this.tb_response.Multiline = true;
            this.tb_response.Name = "tb_response";
            this.tb_response.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_response.Size = new System.Drawing.Size(372, 394);
            this.tb_response.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(269, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Response";
            // 
            // btn_learn
            // 
            this.btn_learn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_learn.Location = new System.Drawing.Point(134, 209);
            this.btn_learn.Name = "btn_learn";
            this.btn_learn.Size = new System.Drawing.Size(83, 24);
            this.btn_learn.TabIndex = 6;
            this.btn_learn.Text = "Learn";
            this.btn_learn.UseVisualStyleBackColor = true;
            this.btn_learn.Click += new System.EventHandler(this.btn_learn_Click);
            // 
            // tb_assign_name
            // 
            this.tb_assign_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_assign_name.Location = new System.Drawing.Point(134, 183);
            this.tb_assign_name.Name = "tb_assign_name";
            this.tb_assign_name.Size = new System.Drawing.Size(100, 20);
            this.tb_assign_name.TabIndex = 7;
            // 
            // btn_on_create
            // 
            this.btn_on_create.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_on_create.Location = new System.Drawing.Point(134, 33);
            this.btn_on_create.Name = "btn_on_create";
            this.btn_on_create.Size = new System.Drawing.Size(83, 24);
            this.btn_on_create.TabIndex = 8;
            this.btn_on_create.Text = "onCreate";
            this.btn_on_create.UseVisualStyleBackColor = true;
            this.btn_on_create.Click += new System.EventHandler(this.btn_on_create_Click);
            // 
            // btn_on_destory
            // 
            this.btn_on_destory.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_on_destory.Location = new System.Drawing.Point(134, 403);
            this.btn_on_destory.Name = "btn_on_destory";
            this.btn_on_destory.Size = new System.Drawing.Size(83, 24);
            this.btn_on_destory.TabIndex = 9;
            this.btn_on_destory.Text = "onDestory";
            this.btn_on_destory.UseVisualStyleBackColor = true;
            this.btn_on_destory.Click += new System.EventHandler(this.btn_on_destory_Click);
            // 
            // pluaiWrapper
            // 
            this.pluaiWrapper.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pluaiWrapper.Location = new System.Drawing.Point(12, 12);
            this.pluaiWrapper.Name = "pluaiWrapper";
            this.pluaiWrapper.Size = new System.Drawing.Size(83, 85);
            this.pluaiWrapper.TabIndex = 1;
            this.pluaiWrapper.VerInfo = null;
            this.pluaiWrapper.VerNo = ((long)(0));
            this.pluaiWrapper.DataReceived += new MQPLUAIWrapper.Tools.DataReceivedEventHandler(this.pluaiWrapper_DataReceived);
            // 
            // btn_test_thread_pool
            // 
            this.btn_test_thread_pool.Location = new System.Drawing.Point(12, 238);
            this.btn_test_thread_pool.Name = "btn_test_thread_pool";
            this.btn_test_thread_pool.Size = new System.Drawing.Size(109, 24);
            this.btn_test_thread_pool.TabIndex = 10;
            this.btn_test_thread_pool.Text = "Test Thread Pool";
            this.btn_test_thread_pool.UseVisualStyleBackColor = true;
            this.btn_test_thread_pool.Click += new System.EventHandler(this.btn_test_thread_pool_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 438);
            this.Controls.Add(this.btn_test_thread_pool);
            this.Controls.Add(this.btn_on_destory);
            this.Controls.Add(this.btn_on_create);
            this.Controls.Add(this.tb_assign_name);
            this.Controls.Add(this.btn_learn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_response);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pluaiWrapper);
            this.Controls.Add(this.btnSetCameraId);
            this.Name = "Form1";
            this.Text = "MQWrapper Tester";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSetCameraId;
        private MQPLUAIWrapper.Tools pluaiWrapper;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox tb_response;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_learn;
        private System.Windows.Forms.TextBox tb_assign_name;
        private System.Windows.Forms.Button btn_on_create;
        private System.Windows.Forms.Button btn_on_destory;
        private System.Windows.Forms.Button btn_test_thread_pool;
    }
}

