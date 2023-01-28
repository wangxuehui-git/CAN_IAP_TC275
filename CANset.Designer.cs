namespace CAN_IAP_ForUDS
{
    partial class CANset
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CANset));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cb_USBCANtype = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btn_OpenDev = new System.Windows.Forms.Button();
            this.comboBox_Mode = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBox_Filter = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox_BandRate = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_AccMask = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_AccCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox_DevIndex = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.combox_CanNum = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cb_USBCANtype);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.btn_OpenDev);
            this.groupBox1.Controls.Add(this.comboBox_Mode);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.comboBox_Filter);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.comboBox_BandRate);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textBox_AccMask);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBox_AccCode);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.comboBox_DevIndex);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.combox_CanNum);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(378, 241);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "InitCAN";
            // 
            // cb_USBCANtype
            // 
            this.cb_USBCANtype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_USBCANtype.FormattingEnabled = true;
            this.cb_USBCANtype.Items.AddRange(new object[] {
            "USBCAN-I",
            "USBCAN-II",
            "USBCAN-E-U",
            "USBCAN-2E-U",
            "CANalyzer"});
            this.cb_USBCANtype.Location = new System.Drawing.Point(85, 34);
            this.cb_USBCANtype.Name = "cb_USBCANtype";
            this.cb_USBCANtype.Size = new System.Drawing.Size(88, 20);
            this.cb_USBCANtype.TabIndex = 24;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(16, 37);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 23;
            this.label9.Text = "DevType:";
            // 
            // btn_OpenDev
            // 
            this.btn_OpenDev.Location = new System.Drawing.Point(129, 191);
            this.btn_OpenDev.Name = "btn_OpenDev";
            this.btn_OpenDev.Size = new System.Drawing.Size(107, 28);
            this.btn_OpenDev.TabIndex = 22;
            this.btn_OpenDev.Text = "Confirm";
            this.btn_OpenDev.UseVisualStyleBackColor = true;
            this.btn_OpenDev.Click += new System.EventHandler(this.btn_OpenDev_Click_1);
            // 
            // comboBox_Mode
            // 
            this.comboBox_Mode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Mode.FormattingEnabled = true;
            this.comboBox_Mode.Items.AddRange(new object[] {
            "Normal",
            "Listening"});
            this.comboBox_Mode.Location = new System.Drawing.Point(262, 147);
            this.comboBox_Mode.Name = "comboBox_Mode";
            this.comboBox_Mode.Size = new System.Drawing.Size(89, 20);
            this.comboBox_Mode.TabIndex = 21;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(199, 151);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 12);
            this.label7.TabIndex = 20;
            this.label7.Text = "WorkMode:";
            // 
            // comboBox_Filter
            // 
            this.comboBox_Filter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Filter.FormattingEnabled = true;
            this.comboBox_Filter.Items.AddRange(new object[] {
            "SingleFilter",
            "DoubleFilter"});
            this.comboBox_Filter.Location = new System.Drawing.Point(89, 147);
            this.comboBox_Filter.Name = "comboBox_Filter";
            this.comboBox_Filter.Size = new System.Drawing.Size(89, 20);
            this.comboBox_Filter.TabIndex = 19;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 151);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 12);
            this.label5.TabIndex = 18;
            this.label5.Text = "FilterMode:";
            // 
            // comboBox_BandRate
            // 
            this.comboBox_BandRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_BandRate.FormattingEnabled = true;
            this.comboBox_BandRate.Items.AddRange(new object[] {
            "250kbps",
            "500kbps",
            "1Mbps"});
            this.comboBox_BandRate.Location = new System.Drawing.Point(262, 109);
            this.comboBox_BandRate.Name = "comboBox_BandRate";
            this.comboBox_BandRate.Size = new System.Drawing.Size(89, 20);
            this.comboBox_BandRate.TabIndex = 17;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(204, 112);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 12);
            this.label8.TabIndex = 16;
            this.label8.Text = "Baudrate:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(207, 112);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 16;
            this.label4.Text = "波特率:";
            // 
            // textBox_AccMask
            // 
            this.textBox_AccMask.Location = new System.Drawing.Point(85, 108);
            this.textBox_AccMask.Name = "textBox_AccMask";
            this.textBox_AccMask.Size = new System.Drawing.Size(74, 21);
            this.textBox_AccMask.TabIndex = 15;
            this.textBox_AccMask.Text = "FFFFFFFF";
            this.textBox_AccMask.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 14;
            this.label3.Text = "AccMask:0x";
            // 
            // textBox_AccCode
            // 
            this.textBox_AccCode.Location = new System.Drawing.Point(85, 71);
            this.textBox_AccCode.Name = "textBox_AccCode";
            this.textBox_AccCode.Size = new System.Drawing.Size(74, 21);
            this.textBox_AccCode.TabIndex = 13;
            this.textBox_AccCode.Text = "00000000";
            this.textBox_AccCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 12;
            this.label2.Text = "AccCode:0x";
            // 
            // comboBox_DevIndex
            // 
            this.comboBox_DevIndex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_DevIndex.FormattingEnabled = true;
            this.comboBox_DevIndex.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7"});
            this.comboBox_DevIndex.Location = new System.Drawing.Point(262, 37);
            this.comboBox_DevIndex.Name = "comboBox_DevIndex";
            this.comboBox_DevIndex.Size = new System.Drawing.Size(73, 20);
            this.comboBox_DevIndex.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(205, 76);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 12);
            this.label6.TabIndex = 11;
            this.label6.Text = "CANNum:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(206, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "DevIndex:";
            // 
            // combox_CanNum
            // 
            this.combox_CanNum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combox_CanNum.FormattingEnabled = true;
            this.combox_CanNum.Items.AddRange(new object[] {
            "0",
            "1"});
            this.combox_CanNum.Location = new System.Drawing.Point(262, 73);
            this.combox_CanNum.Name = "combox_CanNum";
            this.combox_CanNum.Size = new System.Drawing.Size(73, 20);
            this.combox_CanNum.TabIndex = 10;
            // 
            // CANset
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 272);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CANset";
            this.Text = "CANset";
            this.Load += new System.EventHandler(this.CANset_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cb_USBCANtype;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btn_OpenDev;
        private System.Windows.Forms.ComboBox comboBox_Mode;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBox_Filter;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBox_BandRate;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_AccMask;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_AccCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox_DevIndex;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox combox_CanNum;
    }
}