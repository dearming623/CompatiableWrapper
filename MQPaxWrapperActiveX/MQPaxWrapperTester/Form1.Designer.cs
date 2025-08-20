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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.tv_ecr_ref_num = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.rtb_resp = new System.Windows.Forms.RichTextBox();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.btn_write = new System.Windows.Forms.Button();
            this.posLinkWrapper1 = new MQPaxWrapper.PosLinkWrapper();
            this.btn_disable = new System.Windows.Forms.Button();
            this.btn_enable = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 89);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Init";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(144, 78);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(136, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Command  -> Get Ver";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(180, 52);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 3;
            this.textBox1.Text = "MID";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(120, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "VarName";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(120, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "EDCType";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(180, 26);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 6;
            this.textBox2.Text = "1";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(96, 189);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(144, 23);
            this.button3.TabIndex = 7;
            this.button3.Text = "Read Card";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(96, 218);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(144, 23);
            this.button4.TabIndex = 8;
            this.button4.Text = "Read Input Text";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(93, 296);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(142, 23);
            this.button5.TabIndex = 10;
            this.button5.Text = "LocalDetailReport";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // tv_ecr_ref_num
            // 
            this.tv_ecr_ref_num.Location = new System.Drawing.Point(93, 270);
            this.tv_ecr_ref_num.Name = "tv_ecr_ref_num";
            this.tv_ecr_ref_num.Size = new System.Drawing.Size(100, 20);
            this.tv_ecr_ref_num.TabIndex = 11;
            this.tv_ecr_ref_num.Text = "09-0001";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 273);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "ECR Ref Num";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(327, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Response";
            // 
            // rtb_resp
            // 
            this.rtb_resp.Location = new System.Drawing.Point(330, 29);
            this.rtb_resp.Name = "rtb_resp";
            this.rtb_resp.Size = new System.Drawing.Size(288, 327);
            this.rtb_resp.TabIndex = 15;
            this.rtb_resp.Text = "";
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(96, 160);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(144, 23);
            this.button6.TabIndex = 16;
            this.button6.Text = "Do Signature";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(96, 131);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(144, 23);
            this.button7.TabIndex = 17;
            this.button7.Text = "Reset";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(16, 333);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(144, 23);
            this.button8.TabIndex = 18;
            this.button8.Text = "Cancel Trans";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // btn_write
            // 
            this.btn_write.Location = new System.Drawing.Point(29, 400);
            this.btn_write.Name = "btn_write";
            this.btn_write.Size = new System.Drawing.Size(104, 23);
            this.btn_write.TabIndex = 19;
            this.btn_write.Text = "Write to NLog";
            this.btn_write.UseVisualStyleBackColor = true;
            this.btn_write.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // posLinkWrapper1
            // 
            this.posLinkWrapper1.enableWrapperLog = false;
            this.posLinkWrapper1.errorCode = 0;
            this.posLinkWrapper1.errorMessage = null;
            this.posLinkWrapper1.Location = new System.Drawing.Point(12, 12);
            this.posLinkWrapper1.manageRequestAccountNumber = null;
            this.posLinkWrapper1.manageRequestButton1 = null;
            this.posLinkWrapper1.manageRequestButton2 = null;
            this.posLinkWrapper1.manageRequestButton3 = null;
            this.posLinkWrapper1.manageRequestButton4 = null;
            this.posLinkWrapper1.manageRequestContactlessEntryFlag = null;
            this.posLinkWrapper1.manageRequestDefaultValue = null;
            this.posLinkWrapper1.manageRequestDisplayMessage = null;
            this.posLinkWrapper1.manageRequestEDCType = null;
            this.posLinkWrapper1.manageRequestEncryptionFlag = null;
            this.posLinkWrapper1.manageRequestEncryptionType = null;
            this.posLinkWrapper1.manageRequestExpiryDatePrompt = null;
            this.posLinkWrapper1.manageRequestFileName = null;
            this.posLinkWrapper1.manageRequestHRefNum = null;
            this.posLinkWrapper1.manageRequestImageName = null;
            this.posLinkWrapper1.manageRequestImagePath = null;
            this.posLinkWrapper1.manageRequestInputType = null;
            this.posLinkWrapper1.manageRequestKeySlot = null;
            this.posLinkWrapper1.manageRequestMagneticSwipeEntryFlag = null;
            this.posLinkWrapper1.manageRequestManualEntryFlag = null;
            this.posLinkWrapper1.manageRequestMAXAccountLength = null;
            this.posLinkWrapper1.manageRequestMAXLength = null;
            this.posLinkWrapper1.manageRequestMINAccountLength = null;
            this.posLinkWrapper1.manageRequestMINLength = null;
            this.posLinkWrapper1.manageRequestNullPin = null;
            this.posLinkWrapper1.manageRequestPinAlgorithm = null;
            this.posLinkWrapper1.manageRequestPinMaxLength = null;
            this.posLinkWrapper1.manageRequestPinMinLength = null;
            this.posLinkWrapper1.manageRequestScannerEntryFlag = null;
            this.posLinkWrapper1.manageRequestSigSavePath = null;
            this.posLinkWrapper1.manageRequestThankYouMessage1 = null;
            this.posLinkWrapper1.manageRequestThankYouMessage2 = null;
            this.posLinkWrapper1.manageRequestThankYouTimeout = null;
            this.posLinkWrapper1.manageRequestThankYouTitle = null;
            this.posLinkWrapper1.manageRequestTimeout = null;
            this.posLinkWrapper1.manageRequestTitle = null;
            this.posLinkWrapper1.manageRequestTrans = null;
            this.posLinkWrapper1.manageRequestTransType = null;
            this.posLinkWrapper1.manageRequestUpload = null;
            this.posLinkWrapper1.manageRequestVarName = null;
            this.posLinkWrapper1.manageRequestVarValue = null;
            this.posLinkWrapper1.manageResponseEntryMode = null;
            this.posLinkWrapper1.manageResponseExpiryDate = null;
            this.posLinkWrapper1.manageResponseKSN = null;
            this.posLinkWrapper1.manageResponsePAN = null;
            this.posLinkWrapper1.manageResponsePinBlock = null;
            this.posLinkWrapper1.manageResponseQRCode = null;
            this.posLinkWrapper1.manageResponseResultCode = null;
            this.posLinkWrapper1.manageResponseResultTxt = null;
            this.posLinkWrapper1.manageResponseSigFileName = null;
            this.posLinkWrapper1.manageResponseSN = null;
            this.posLinkWrapper1.manageResponseText = null;
            this.posLinkWrapper1.manageResponseTrack1Data = null;
            this.posLinkWrapper1.manageResponseTrack2Data = null;
            this.posLinkWrapper1.manageResponseTrack3Data = null;
            this.posLinkWrapper1.manageResponseVarValue = null;
            this.posLinkWrapper1.Name = "posLinkWrapper1";
            this.posLinkWrapper1.paymentRequestAmount = null;
            this.posLinkWrapper1.paymentRequestAuthCode = null;
            this.posLinkWrapper1.paymentRequestCashbackAmt = null;
            this.posLinkWrapper1.paymentRequestClerkID = null;
            this.posLinkWrapper1.paymentRequestCssPath = null;
            this.posLinkWrapper1.paymentRequestCustomerName = null;
            this.posLinkWrapper1.paymentRequestCustomFields = null;
            this.posLinkWrapper1.paymentRequestECRefNum = null;
            this.posLinkWrapper1.paymentRequestExtData = null;
            this.posLinkWrapper1.paymentRequestInvNum = null;
            this.posLinkWrapper1.paymentRequestMisc1Amt = null;
            this.posLinkWrapper1.paymentRequestMisc2Amt = null;
            this.posLinkWrapper1.paymentRequestMisc3Amt = null;
            this.posLinkWrapper1.paymentRequestOrigRefNum = null;
            this.posLinkWrapper1.paymentRequestPassWord = null;
            this.posLinkWrapper1.paymentRequestPONum = null;
            this.posLinkWrapper1.paymentRequestServerID = null;
            this.posLinkWrapper1.paymentRequestStreet = null;
            this.posLinkWrapper1.paymentRequestSurchargeAmt = null;
            this.posLinkWrapper1.paymentRequestTaxAmt = null;
            this.posLinkWrapper1.paymentRequestTenderType = null;
            this.posLinkWrapper1.paymentRequestTipAmt = null;
            this.posLinkWrapper1.paymentRequestTransType = null;
            this.posLinkWrapper1.paymentRequestUserID = null;
            this.posLinkWrapper1.paymentRequestZIP = null;
            this.posLinkWrapper1.paymentResponseApprovedAmt = null;
            this.posLinkWrapper1.paymentResponseAuthCode = null;
            this.posLinkWrapper1.paymentResponseAvsResponse = null;
            this.posLinkWrapper1.paymentResponseBogusAccountNum = null;
            this.posLinkWrapper1.paymentResponseCardType = null;
            this.posLinkWrapper1.paymentResponseCvResponse = null;
            this.posLinkWrapper1.paymentResponseExtData = null;
            this.posLinkWrapper1.paymentResponseExtraBalance = null;
            this.posLinkWrapper1.paymentResponseHostCode = null;
            this.posLinkWrapper1.paymentResponseHostResponse = null;
            this.posLinkWrapper1.paymentResponseMessage = null;
            this.posLinkWrapper1.paymentResponseRawResponse = null;
            this.posLinkWrapper1.paymentResponseRefNum = null;
            this.posLinkWrapper1.paymentResponseRemainingBalance = null;
            this.posLinkWrapper1.paymentResponseRequestedAmt = null;
            this.posLinkWrapper1.paymentResponseResultCode = null;
            this.posLinkWrapper1.paymentResponseResultTxt = null;
            this.posLinkWrapper1.paymentResponseTimestamp = null;
            this.posLinkWrapper1.ResponseData = null;
            this.posLinkWrapper1.SerialNumber = null;
            this.posLinkWrapper1.Setting_BaudRate = null;
            this.posLinkWrapper1.Setting_CommType = null;
            this.posLinkWrapper1.Setting_DestIP = null;
            this.posLinkWrapper1.Setting_DestPort = null;
            this.posLinkWrapper1.Setting_SerialPort = null;
            this.posLinkWrapper1.Setting_TimeOut = null;
            this.posLinkWrapper1.Size = new System.Drawing.Size(72, 71);
            this.posLinkWrapper1.TabIndex = 9;
            this.posLinkWrapper1.transResultMsg = null;
            // 
            // btn_disable
            // 
            this.btn_disable.Location = new System.Drawing.Point(144, 400);
            this.btn_disable.Name = "btn_disable";
            this.btn_disable.Size = new System.Drawing.Size(119, 23);
            this.btn_disable.TabIndex = 20;
            this.btn_disable.Text = "Disalbe NLog";
            this.btn_disable.UseVisualStyleBackColor = true;
            this.btn_disable.Click += new System.EventHandler(this.btn_disable_Click);
            // 
            // btn_enable
            // 
            this.btn_enable.Location = new System.Drawing.Point(269, 400);
            this.btn_enable.Name = "btn_enable";
            this.btn_enable.Size = new System.Drawing.Size(119, 23);
            this.btn_enable.TabIndex = 21;
            this.btn_enable.Text = "Enable NLog";
            this.btn_enable.UseVisualStyleBackColor = true;
            this.btn_enable.Click += new System.EventHandler(this.btn_enable_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(630, 451);
            this.Controls.Add(this.btn_enable);
            this.Controls.Add(this.btn_disable);
            this.Controls.Add(this.btn_write);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.rtb_resp);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tv_ecr_ref_num);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.posLinkWrapper1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Pax Wrapper Tester";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private MQPaxWrapper.PosLinkWrapper posLinkWrapper1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox tv_ecr_ref_num;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox rtb_resp;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button btn_write;
        private System.Windows.Forms.Button btn_disable;
        private System.Windows.Forms.Button btn_enable;
    }
}

