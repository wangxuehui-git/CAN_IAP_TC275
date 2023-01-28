namespace CAN_IAP_ForUDS
{
    partial class Main
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.timer_rec = new System.Windows.Forms.Timer(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.CanSetButton = new System.Windows.Forms.ToolStripButton();
            this.CanStarButton = new System.Windows.Forms.ToolStripButton();
            this.CanCloseButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.CleanDataButton = new System.Windows.Forms.ToolStripButton();
            this.StopShowButton = new System.Windows.Forms.ToolStripButton();
            this.OpenFileButton = new System.Windows.Forms.ToolStripButton();
            this.SaveMessButton = new System.Windows.Forms.ToolStripButton();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.IAP_Down = new System.Windows.Forms.TabPage();
            this.HEX_gBox = new System.Windows.Forms.GroupBox();
            this.btnStartIAP = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label28 = new System.Windows.Forms.Label();
            this.lb_curPage = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label_totalPage = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.fileNameText = new System.Windows.Forms.TextBox();
            this.label_PrjVer = new System.Windows.Forms.Label();
            this.label_Node = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label_Version = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label_Line = new System.Windows.Forms.Label();
            this.label_Len = new System.Windows.Forms.Label();
            this.IAP_SelfTest = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lb_IAPinfo = new System.Windows.Forms.ListBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.listBox_Info = new System.Windows.Forms.ListBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.toolStrip1.SuspendLayout();
            this.IAP_Down.SuspendLayout();
            this.HEX_gBox.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer_rec
            // 
            this.timer_rec.Interval = 10;
            this.timer_rec.Tick += new System.EventHandler(this.timer_rec_Tick);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.Tan;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CanSetButton,
            this.CanStarButton,
            this.CanCloseButton,
            this.toolStripSeparator1,
            this.CleanDataButton,
            this.StopShowButton,
            this.OpenFileButton,
            this.SaveMessButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(711, 39);
            this.toolStrip1.TabIndex = 8;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // CanSetButton
            // 
            this.CanSetButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.CanSetButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CanSetButton.Image = global::CAN_IAP_ForUDS.Properties.Resources.config_set_128px_548300_easyicon_net;
            this.CanSetButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CanSetButton.Name = "CanSetButton";
            this.CanSetButton.Size = new System.Drawing.Size(36, 36);
            this.CanSetButton.Text = "CAN设置";
            this.CanSetButton.ToolTipText = "CANSetting";
            this.CanSetButton.Click += new System.EventHandler(this.CanSetButton_Click);
            // 
            // CanStarButton
            // 
            this.CanStarButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CanStarButton.Image = global::CAN_IAP_ForUDS.Properties.Resources.background_data_on_100px_1067671_easyicon_net;
            this.CanStarButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CanStarButton.Name = "CanStarButton";
            this.CanStarButton.Size = new System.Drawing.Size(36, 36);
            this.CanStarButton.Text = "OpenCAN";
            this.CanStarButton.Click += new System.EventHandler(this.CanStarButton_Click);
            // 
            // CanCloseButton
            // 
            this.CanCloseButton.BackColor = System.Drawing.Color.Transparent;
            this.CanCloseButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CanCloseButton.Image = global::CAN_IAP_ForUDS.Properties.Resources.close_128px_1142186_easyicon_net;
            this.CanCloseButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CanCloseButton.Name = "CanCloseButton";
            this.CanCloseButton.Size = new System.Drawing.Size(36, 36);
            this.CanCloseButton.Text = "toolStripButton2";
            this.CanCloseButton.ToolTipText = "CloseCAN";
            this.CanCloseButton.Click += new System.EventHandler(this.CanCloseButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // CleanDataButton
            // 
            this.CleanDataButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CleanDataButton.Image = ((System.Drawing.Image)(resources.GetObject("CleanDataButton.Image")));
            this.CleanDataButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CleanDataButton.Name = "CleanDataButton";
            this.CleanDataButton.Size = new System.Drawing.Size(36, 36);
            this.CleanDataButton.Text = "ClearDisplay";
            this.CleanDataButton.Click += new System.EventHandler(this.CleanDataButton_Click);
            // 
            // StopShowButton
            // 
            this.StopShowButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.StopShowButton.Image = global::CAN_IAP_ForUDS.Properties.Resources.ooopic_1440654146;
            this.StopShowButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.StopShowButton.Name = "StopShowButton";
            this.StopShowButton.Size = new System.Drawing.Size(36, 36);
            this.StopShowButton.Text = "OpenDisplay";
            this.StopShowButton.Click += new System.EventHandler(this.StopShowButton_Click);
            // 
            // OpenFileButton
            // 
            this.OpenFileButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.OpenFileButton.Image = global::CAN_IAP_ForUDS.Properties.Resources.File_explorer_128px_1186172_easyicon_net;
            this.OpenFileButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.OpenFileButton.Name = "OpenFileButton";
            this.OpenFileButton.Size = new System.Drawing.Size(36, 36);
            this.OpenFileButton.Text = "OpenFile";
            this.OpenFileButton.Click += new System.EventHandler(this.OpenFileButton_Click);
            // 
            // SaveMessButton
            // 
            this.SaveMessButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SaveMessButton.Image = global::CAN_IAP_ForUDS.Properties.Resources.Hard_Disk_128px_1060325_easyicon_net;
            this.SaveMessButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SaveMessButton.Name = "SaveMessButton";
            this.SaveMessButton.Size = new System.Drawing.Size(36, 36);
            this.SaveMessButton.Text = "SaveLog";
            this.SaveMessButton.Click += new System.EventHandler(this.SaveMessButton_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "所有文件(*.*)|*.*";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 486);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(711, 22);
            this.statusStrip1.TabIndex = 10;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(711, 24);
            this.menuStrip1.TabIndex = 12;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.Visible = false;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "play_player_right_start_128px_1692_easyicon.net.ico");
            this.imageList1.Images.SetKeyName(1, "ooopic_1440654087.ico");
            // 
            // IAP_Down
            // 
            this.IAP_Down.Controls.Add(this.HEX_gBox);
            this.IAP_Down.Controls.Add(this.groupBox5);
            this.IAP_Down.Location = new System.Drawing.Point(4, 22);
            this.IAP_Down.Name = "IAP_Down";
            this.IAP_Down.Padding = new System.Windows.Forms.Padding(3);
            this.IAP_Down.Size = new System.Drawing.Size(677, 415);
            this.IAP_Down.TabIndex = 0;
            this.IAP_Down.Text = "IAP_Download";
            this.IAP_Down.UseVisualStyleBackColor = true;
            // 
            // HEX_gBox
            // 
            this.HEX_gBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.HEX_gBox.BackColor = System.Drawing.Color.White;
            this.HEX_gBox.Controls.Add(this.btnStartIAP);
            this.HEX_gBox.Controls.Add(this.groupBox7);
            this.HEX_gBox.Controls.Add(this.groupBox1);
            this.HEX_gBox.Location = new System.Drawing.Point(9, 3);
            this.HEX_gBox.Name = "HEX_gBox";
            this.HEX_gBox.Size = new System.Drawing.Size(383, 406);
            this.HEX_gBox.TabIndex = 7;
            this.HEX_gBox.TabStop = false;
            this.HEX_gBox.Text = "IAPControl";
            // 
            // btnStartIAP
            // 
            this.btnStartIAP.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnStartIAP.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStartIAP.ImageIndex = 0;
            this.btnStartIAP.ImageList = this.imageList1;
            this.btnStartIAP.Location = new System.Drawing.Point(250, 327);
            this.btnStartIAP.Name = "btnStartIAP";
            this.btnStartIAP.Size = new System.Drawing.Size(126, 65);
            this.btnStartIAP.TabIndex = 7;
            this.btnStartIAP.Text = "StartIAPDownload";
            this.btnStartIAP.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnStartIAP.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnStartIAP.UseVisualStyleBackColor = true;
            this.btnStartIAP.Click += new System.EventHandler(this.btnStartIAP_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBox7.Controls.Add(this.label28);
            this.groupBox7.Controls.Add(this.lb_curPage);
            this.groupBox7.Controls.Add(this.label30);
            this.groupBox7.Controls.Add(this.label27);
            this.groupBox7.Controls.Add(this.label_totalPage);
            this.groupBox7.Controls.Add(this.label25);
            this.groupBox7.Location = new System.Drawing.Point(9, 215);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(368, 106);
            this.groupBox7.TabIndex = 38;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "IAPData";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("华文细黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label28.Location = new System.Drawing.Point(293, 66);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(53, 17);
            this.label28.TabIndex = 43;
            this.label28.Text = "Page";
            // 
            // lb_curPage
            // 
            this.lb_curPage.AutoSize = true;
            this.lb_curPage.BackColor = System.Drawing.Color.White;
            this.lb_curPage.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_curPage.ForeColor = System.Drawing.Color.ForestGreen;
            this.lb_curPage.Location = new System.Drawing.Point(193, 62);
            this.lb_curPage.Name = "lb_curPage";
            this.lb_curPage.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lb_curPage.Size = new System.Drawing.Size(66, 27);
            this.lb_curPage.TabIndex = 42;
            this.lb_curPage.Text = "No.000";
            this.lb_curPage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lb_curPage.UseCompatibleTextRendering = true;
            this.lb_curPage.UseMnemonic = false;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("华文细黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label30.Location = new System.Drawing.Point(95, 66);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(73, 17);
            this.label30.TabIndex = 41;
            this.label30.Text = "Current";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("华文细黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label27.Location = new System.Drawing.Point(138, 34);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(77, 17);
            this.label27.TabIndex = 40;
            this.label27.Text = "Pages |";
            // 
            // label_totalPage
            // 
            this.label_totalPage.AutoSize = true;
            this.label_totalPage.BackColor = System.Drawing.Color.White;
            this.label_totalPage.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_totalPage.ForeColor = System.Drawing.Color.ForestGreen;
            this.label_totalPage.Location = new System.Drawing.Point(77, 31);
            this.label_totalPage.Name = "label_totalPage";
            this.label_totalPage.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label_totalPage.Size = new System.Drawing.Size(36, 27);
            this.label_totalPage.TabIndex = 39;
            this.label_totalPage.Text = "000";
            this.label_totalPage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label_totalPage.UseCompatibleTextRendering = true;
            this.label_totalPage.UseMnemonic = false;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("华文细黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label25.Location = new System.Drawing.Point(9, 34);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(49, 17);
            this.label25.TabIndex = 0;
            this.label25.Text = "Total";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.fileNameText);
            this.groupBox1.Controls.Add(this.label_PrjVer);
            this.groupBox1.Controls.Add(this.label_Node);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label_Version);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.label_Line);
            this.groupBox1.Controls.Add(this.label_Len);
            this.groupBox1.Controls.Add(this.IAP_SelfTest);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(6, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(376, 190);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "FileInfo";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(2, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 55;
            this.label8.Text = "HexPath:";
            // 
            // fileNameText
            // 
            this.fileNameText.AcceptsReturn = true;
            this.fileNameText.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.fileNameText.Location = new System.Drawing.Point(58, 15);
            this.fileNameText.Name = "fileNameText";
            this.fileNameText.Size = new System.Drawing.Size(318, 21);
            this.fileNameText.TabIndex = 54;
            this.fileNameText.Click += new System.EventHandler(this.fileNameText_Click);
            this.fileNameText.MouseHover += new System.EventHandler(this.fileNameText_MouseHover);
            // 
            // label_PrjVer
            // 
            this.label_PrjVer.AutoSize = true;
            this.label_PrjVer.BackColor = System.Drawing.Color.White;
            this.label_PrjVer.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_PrjVer.ForeColor = System.Drawing.Color.Black;
            this.label_PrjVer.Location = new System.Drawing.Point(223, 150);
            this.label_PrjVer.Name = "label_PrjVer";
            this.label_PrjVer.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label_PrjVer.Size = new System.Drawing.Size(27, 27);
            this.label_PrjVer.TabIndex = 12;
            this.label_PrjVer.Text = "---";
            this.label_PrjVer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label_PrjVer.UseCompatibleTextRendering = true;
            this.label_PrjVer.UseMnemonic = false;
            // 
            // label_Node
            // 
            this.label_Node.AutoSize = true;
            this.label_Node.BackColor = System.Drawing.Color.White;
            this.label_Node.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_Node.ForeColor = System.Drawing.Color.Black;
            this.label_Node.Location = new System.Drawing.Point(73, 148);
            this.label_Node.Name = "label_Node";
            this.label_Node.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label_Node.Size = new System.Drawing.Size(27, 27);
            this.label_Node.TabIndex = 12;
            this.label_Node.Text = "---";
            this.label_Node.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label_Node.UseCompatibleTextRendering = true;
            this.label_Node.UseMnemonic = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(141, 158);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 11;
            this.label2.Text = "ProjectVer:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 155);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 11;
            this.label1.Text = "NodeType:";
            // 
            // label_Version
            // 
            this.label_Version.AutoSize = true;
            this.label_Version.BackColor = System.Drawing.Color.White;
            this.label_Version.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_Version.ForeColor = System.Drawing.Color.Black;
            this.label_Version.Location = new System.Drawing.Point(107, 123);
            this.label_Version.Name = "label_Version";
            this.label_Version.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label_Version.Size = new System.Drawing.Size(24, 24);
            this.label_Version.TabIndex = 10;
            this.label_Version.Text = "---";
            this.label_Version.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label_Version.UseCompatibleTextRendering = true;
            this.label_Version.UseMnemonic = false;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(9, 129);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(83, 12);
            this.label15.TabIndex = 9;
            this.label15.Text = "Software Ver:";
            // 
            // label_Line
            // 
            this.label_Line.BackColor = System.Drawing.Color.White;
            this.label_Line.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_Line.ForeColor = System.Drawing.Color.Black;
            this.label_Line.Location = new System.Drawing.Point(84, 89);
            this.label_Line.Name = "label_Line";
            this.label_Line.Size = new System.Drawing.Size(92, 22);
            this.label_Line.TabIndex = 8;
            this.label_Line.Text = "0000";
            this.label_Line.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label_Line.UseCompatibleTextRendering = true;
            this.label_Line.UseMnemonic = false;
            // 
            // label_Len
            // 
            this.label_Len.BackColor = System.Drawing.Color.White;
            this.label_Len.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Len.ForeColor = System.Drawing.Color.Black;
            this.label_Len.Location = new System.Drawing.Point(87, 51);
            this.label_Len.Name = "label_Len";
            this.label_Len.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label_Len.Size = new System.Drawing.Size(85, 22);
            this.label_Len.TabIndex = 7;
            this.label_Len.Text = "00000";
            this.label_Len.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label_Len.UseCompatibleTextRendering = true;
            this.label_Len.UseMnemonic = false;
            // 
            // IAP_SelfTest
            // 
            this.IAP_SelfTest.AutoSize = true;
            this.IAP_SelfTest.Cursor = System.Windows.Forms.Cursors.Hand;
            this.IAP_SelfTest.Image = global::CAN_IAP_ForUDS.Properties.Resources.check_zoom_40px_575970_easyicon_net;
            this.IAP_SelfTest.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.IAP_SelfTest.Location = new System.Drawing.Point(260, 55);
            this.IAP_SelfTest.Name = "IAP_SelfTest";
            this.IAP_SelfTest.Size = new System.Drawing.Size(111, 67);
            this.IAP_SelfTest.TabIndex = 1;
            this.IAP_SelfTest.Text = "HEXFileSelfCheck";
            this.IAP_SelfTest.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.IAP_SelfTest.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.IAP_SelfTest.UseVisualStyleBackColor = true;
            this.IAP_SelfTest.Click += new System.EventHandler(this.IAP_SelfTest_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "Count(Line):";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 55);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 12);
            this.label5.TabIndex = 5;
            this.label5.Text = "Length(Byte):";
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox5.BackColor = System.Drawing.Color.White;
            this.groupBox5.Controls.Add(this.lb_IAPinfo);
            this.groupBox5.Location = new System.Drawing.Point(392, 3);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(279, 406);
            this.groupBox5.TabIndex = 8;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "IAPInforRecords";
            // 
            // lb_IAPinfo
            // 
            this.lb_IAPinfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_IAPinfo.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lb_IAPinfo.FormattingEnabled = true;
            this.lb_IAPinfo.HorizontalScrollbar = true;
            this.lb_IAPinfo.ItemHeight = 12;
            this.lb_IAPinfo.Location = new System.Drawing.Point(6, 16);
            this.lb_IAPinfo.Name = "lb_IAPinfo";
            this.lb_IAPinfo.Size = new System.Drawing.Size(267, 376);
            this.lb_IAPinfo.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.IAP_Down);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(12, 42);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(685, 441);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.listBox_Info);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(677, 415);
            this.tabPage1.TabIndex = 1;
            this.tabPage1.Text = "CAN_Data";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // listBox_Info
            // 
            this.listBox_Info.FormattingEnabled = true;
            this.listBox_Info.ItemHeight = 12;
            this.listBox_Info.Location = new System.Drawing.Point(12, 6);
            this.listBox_Info.Name = "listBox_Info";
            this.listBox_Info.Size = new System.Drawing.Size(651, 400);
            this.listBox_Info.TabIndex = 0;
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 30000;
            this.toolTip1.InitialDelay = 500;
            this.toolTip1.ReshowDelay = 100;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(711, 508);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Main";
            this.Text = "CAN_IAP_V10";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainClosed);
            this.Load += new System.EventHandler(this.Main_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.IAP_Down.ResumeLayout(false);
            this.HEX_gBox.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer timer_rec;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton CanSetButton;
        private System.Windows.Forms.ToolStripButton CanCloseButton;
        private System.Windows.Forms.ToolStripButton CanStarButton;
        internal System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton OpenFileButton;
        private System.Windows.Forms.ToolStripButton CleanDataButton;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripButton StopShowButton;
        private System.Windows.Forms.TabPage IAP_Down;
        private System.Windows.Forms.GroupBox HEX_gBox;
        private System.Windows.Forms.Button btnStartIAP;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label lb_curPage;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label_totalPage;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox fileNameText;
        private System.Windows.Forms.Label label_PrjVer;
        private System.Windows.Forms.Label label_Node;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_Version;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label_Line;
        private System.Windows.Forms.Label label_Len;
        private System.Windows.Forms.Button IAP_SelfTest;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ListBox lb_IAPinfo;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.ListBox listBox_Info;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ToolStripButton SaveMessButton;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}

