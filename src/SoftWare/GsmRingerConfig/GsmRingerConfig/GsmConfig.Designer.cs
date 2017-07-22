namespace GsmRingerConfig
{
    partial class GsmConfig
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GsmConfig));
            this.portConfigBox = new System.Windows.Forms.GroupBox();
            this.portNameBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.deviceIdLbl = new System.Windows.Forms.Label();
            this.user1NumBox = new System.Windows.Forms.MaskedTextBox();
            this.user2NumBox = new System.Windows.Forms.MaskedTextBox();
            this.user3NumBox = new System.Windows.Forms.MaskedTextBox();
            this.user4NumBox = new System.Windows.Forms.MaskedTextBox();
            this.user5NumBox = new System.Windows.Forms.MaskedTextBox();
            this.smsUser1CheckBox = new System.Windows.Forms.CheckBox();
            this.smsUser2CheckBox = new System.Windows.Forms.CheckBox();
            this.smsUser3CheckBox = new System.Windows.Forms.CheckBox();
            this.smsUser4CheckBox = new System.Windows.Forms.CheckBox();
            this.smsUser5CheckBox = new System.Windows.Forms.CheckBox();
            this.callUser5CheckBox = new System.Windows.Forms.CheckBox();
            this.callUser4CheckBox = new System.Windows.Forms.CheckBox();
            this.callUser3CheckBox = new System.Windows.Forms.CheckBox();
            this.callUser2CheckBox = new System.Windows.Forms.CheckBox();
            this.callUser1CheckBox = new System.Windows.Forms.CheckBox();
            this.userNumGroupBox = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.readUser1NumBtn = new System.Windows.Forms.Button();
            this.writeUser1NumBtn = new System.Windows.Forms.Button();
            this.readUser2NumBtn = new System.Windows.Forms.Button();
            this.writeUser2NumBtn = new System.Windows.Forms.Button();
            this.readUser3NumBtn = new System.Windows.Forms.Button();
            this.writeUser3NumBtn = new System.Windows.Forms.Button();
            this.readUser4NumBtn = new System.Windows.Forms.Button();
            this.writeUser4NumBtn = new System.Windows.Forms.Button();
            this.readUser5NumBtn = new System.Windows.Forms.Button();
            this.writeUser5NumBtn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.readCallParamsBtn = new System.Windows.Forms.Button();
            this.writeCallParamsBtn = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.deactivationTimeBox = new System.Windows.Forms.TextBox();
            this.activationTimeBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.writeDeactivationTimeBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.readDeactivationTimeBtn = new System.Windows.Forms.Button();
            this.readActivationTimeBtn = new System.Windows.Forms.Button();
            this.writeActivationTimeBtn = new System.Windows.Forms.Button();
            this.exitBtn = new System.Windows.Forms.Button();
            this.readDeviceVersionBtn = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.sendUSSDBtn = new System.Windows.Forms.Button();
            this.ussdBox = new System.Windows.Forms.TextBox();
            this.setDefaultsBtn = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.zone1EnableCheckBox = new System.Windows.Forms.CheckBox();
            this.zone4EnableCheckBox = new System.Windows.Forms.CheckBox();
            this.zone3EnableCheckBox = new System.Windows.Forms.CheckBox();
            this.zone2EnableCheckBox = new System.Windows.Forms.CheckBox();
            this.readActiveZonesBtn = new System.Windows.Forms.Button();
            this.writeActiveZonesBtn = new System.Windows.Forms.Button();
            this.readDeviceIdBtn = new System.Windows.Forms.Button();
            this.writeAllBtn = new System.Windows.Forms.Button();
            this.readAllBtn = new System.Windows.Forms.Button();
            this.portConfigBox.SuspendLayout();
            this.userNumGroupBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // portConfigBox
            // 
            this.portConfigBox.Controls.Add(this.portNameBox);
            this.portConfigBox.Location = new System.Drawing.Point(244, 35);
            this.portConfigBox.Name = "portConfigBox";
            this.portConfigBox.Size = new System.Drawing.Size(153, 77);
            this.portConfigBox.TabIndex = 5;
            this.portConfigBox.TabStop = false;
            this.portConfigBox.Text = "Конфигурация порта";
            // 
            // portNameBox
            // 
            this.portNameBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.portNameBox.FormattingEnabled = true;
            this.portNameBox.Location = new System.Drawing.Point(15, 32);
            this.portNameBox.Name = "portNameBox";
            this.portNameBox.Size = new System.Drawing.Size(121, 21);
            this.portNameBox.TabIndex = 0;
            this.portNameBox.SelectedIndexChanged += new System.EventHandler(this.portNameBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Идентификатор устройства:";
            // 
            // deviceIdLbl
            // 
            this.deviceIdLbl.AutoSize = true;
            this.deviceIdLbl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.deviceIdLbl.Location = new System.Drawing.Point(169, 12);
            this.deviceIdLbl.Name = "deviceIdLbl";
            this.deviceIdLbl.Size = new System.Drawing.Size(2, 15);
            this.deviceIdLbl.TabIndex = 7;
            // 
            // user1NumBox
            // 
            this.user1NumBox.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.user1NumBox.Location = new System.Drawing.Point(67, 35);
            this.user1NumBox.Mask = "(999) 000-0000";
            this.user1NumBox.Name = "user1NumBox";
            this.user1NumBox.Size = new System.Drawing.Size(81, 20);
            this.user1NumBox.TabIndex = 9;
            // 
            // user2NumBox
            // 
            this.user2NumBox.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.user2NumBox.Location = new System.Drawing.Point(67, 61);
            this.user2NumBox.Mask = "(999) 000-0000";
            this.user2NumBox.Name = "user2NumBox";
            this.user2NumBox.Size = new System.Drawing.Size(81, 20);
            this.user2NumBox.TabIndex = 10;
            // 
            // user3NumBox
            // 
            this.user3NumBox.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.user3NumBox.Location = new System.Drawing.Point(67, 87);
            this.user3NumBox.Mask = "(999) 000-0000";
            this.user3NumBox.Name = "user3NumBox";
            this.user3NumBox.Size = new System.Drawing.Size(81, 20);
            this.user3NumBox.TabIndex = 11;
            // 
            // user4NumBox
            // 
            this.user4NumBox.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.user4NumBox.Location = new System.Drawing.Point(67, 113);
            this.user4NumBox.Mask = "(999) 000-0000";
            this.user4NumBox.Name = "user4NumBox";
            this.user4NumBox.Size = new System.Drawing.Size(81, 20);
            this.user4NumBox.TabIndex = 12;
            // 
            // user5NumBox
            // 
            this.user5NumBox.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.user5NumBox.Location = new System.Drawing.Point(67, 139);
            this.user5NumBox.Mask = "(999) 000-0000";
            this.user5NumBox.Name = "user5NumBox";
            this.user5NumBox.Size = new System.Drawing.Size(81, 20);
            this.user5NumBox.TabIndex = 13;
            // 
            // smsUser1CheckBox
            // 
            this.smsUser1CheckBox.AutoSize = true;
            this.smsUser1CheckBox.Location = new System.Drawing.Point(67, 59);
            this.smsUser1CheckBox.Name = "smsUser1CheckBox";
            this.smsUser1CheckBox.Size = new System.Drawing.Size(15, 14);
            this.smsUser1CheckBox.TabIndex = 22;
            this.smsUser1CheckBox.UseVisualStyleBackColor = true;
            // 
            // smsUser2CheckBox
            // 
            this.smsUser2CheckBox.AutoSize = true;
            this.smsUser2CheckBox.Location = new System.Drawing.Point(88, 59);
            this.smsUser2CheckBox.Name = "smsUser2CheckBox";
            this.smsUser2CheckBox.Size = new System.Drawing.Size(15, 14);
            this.smsUser2CheckBox.TabIndex = 23;
            this.smsUser2CheckBox.UseVisualStyleBackColor = true;
            // 
            // smsUser3CheckBox
            // 
            this.smsUser3CheckBox.AutoSize = true;
            this.smsUser3CheckBox.Location = new System.Drawing.Point(109, 59);
            this.smsUser3CheckBox.Name = "smsUser3CheckBox";
            this.smsUser3CheckBox.Size = new System.Drawing.Size(15, 14);
            this.smsUser3CheckBox.TabIndex = 24;
            this.smsUser3CheckBox.UseVisualStyleBackColor = true;
            // 
            // smsUser4CheckBox
            // 
            this.smsUser4CheckBox.AutoSize = true;
            this.smsUser4CheckBox.Location = new System.Drawing.Point(130, 59);
            this.smsUser4CheckBox.Name = "smsUser4CheckBox";
            this.smsUser4CheckBox.Size = new System.Drawing.Size(15, 14);
            this.smsUser4CheckBox.TabIndex = 25;
            this.smsUser4CheckBox.UseVisualStyleBackColor = true;
            // 
            // smsUser5CheckBox
            // 
            this.smsUser5CheckBox.AutoSize = true;
            this.smsUser5CheckBox.Location = new System.Drawing.Point(151, 59);
            this.smsUser5CheckBox.Name = "smsUser5CheckBox";
            this.smsUser5CheckBox.Size = new System.Drawing.Size(15, 14);
            this.smsUser5CheckBox.TabIndex = 26;
            this.smsUser5CheckBox.UseVisualStyleBackColor = true;
            // 
            // callUser5CheckBox
            // 
            this.callUser5CheckBox.AutoSize = true;
            this.callUser5CheckBox.Location = new System.Drawing.Point(151, 84);
            this.callUser5CheckBox.Name = "callUser5CheckBox";
            this.callUser5CheckBox.Size = new System.Drawing.Size(15, 14);
            this.callUser5CheckBox.TabIndex = 31;
            this.callUser5CheckBox.UseVisualStyleBackColor = true;
            // 
            // callUser4CheckBox
            // 
            this.callUser4CheckBox.AutoSize = true;
            this.callUser4CheckBox.Location = new System.Drawing.Point(130, 84);
            this.callUser4CheckBox.Name = "callUser4CheckBox";
            this.callUser4CheckBox.Size = new System.Drawing.Size(15, 14);
            this.callUser4CheckBox.TabIndex = 30;
            this.callUser4CheckBox.UseVisualStyleBackColor = true;
            // 
            // callUser3CheckBox
            // 
            this.callUser3CheckBox.AutoSize = true;
            this.callUser3CheckBox.Location = new System.Drawing.Point(109, 84);
            this.callUser3CheckBox.Name = "callUser3CheckBox";
            this.callUser3CheckBox.Size = new System.Drawing.Size(15, 14);
            this.callUser3CheckBox.TabIndex = 29;
            this.callUser3CheckBox.UseVisualStyleBackColor = true;
            // 
            // callUser2CheckBox
            // 
            this.callUser2CheckBox.AutoSize = true;
            this.callUser2CheckBox.Location = new System.Drawing.Point(88, 84);
            this.callUser2CheckBox.Name = "callUser2CheckBox";
            this.callUser2CheckBox.Size = new System.Drawing.Size(15, 14);
            this.callUser2CheckBox.TabIndex = 28;
            this.callUser2CheckBox.UseVisualStyleBackColor = true;
            // 
            // callUser1CheckBox
            // 
            this.callUser1CheckBox.AutoSize = true;
            this.callUser1CheckBox.Location = new System.Drawing.Point(67, 84);
            this.callUser1CheckBox.Name = "callUser1CheckBox";
            this.callUser1CheckBox.Size = new System.Drawing.Size(15, 14);
            this.callUser1CheckBox.TabIndex = 27;
            this.callUser1CheckBox.UseVisualStyleBackColor = true;
            // 
            // userNumGroupBox
            // 
            this.userNumGroupBox.Controls.Add(this.label8);
            this.userNumGroupBox.Controls.Add(this.label7);
            this.userNumGroupBox.Controls.Add(this.label6);
            this.userNumGroupBox.Controls.Add(this.label5);
            this.userNumGroupBox.Controls.Add(this.label4);
            this.userNumGroupBox.Controls.Add(this.user1NumBox);
            this.userNumGroupBox.Controls.Add(this.readUser1NumBtn);
            this.userNumGroupBox.Controls.Add(this.writeUser1NumBtn);
            this.userNumGroupBox.Controls.Add(this.user2NumBox);
            this.userNumGroupBox.Controls.Add(this.user3NumBox);
            this.userNumGroupBox.Controls.Add(this.user4NumBox);
            this.userNumGroupBox.Controls.Add(this.user5NumBox);
            this.userNumGroupBox.Controls.Add(this.readUser2NumBtn);
            this.userNumGroupBox.Controls.Add(this.writeUser2NumBtn);
            this.userNumGroupBox.Controls.Add(this.readUser3NumBtn);
            this.userNumGroupBox.Controls.Add(this.writeUser3NumBtn);
            this.userNumGroupBox.Controls.Add(this.readUser4NumBtn);
            this.userNumGroupBox.Controls.Add(this.writeUser4NumBtn);
            this.userNumGroupBox.Controls.Add(this.readUser5NumBtn);
            this.userNumGroupBox.Controls.Add(this.writeUser5NumBtn);
            this.userNumGroupBox.Location = new System.Drawing.Point(15, 35);
            this.userNumGroupBox.Name = "userNumGroupBox";
            this.userNumGroupBox.Size = new System.Drawing.Size(223, 183);
            this.userNumGroupBox.TabIndex = 42;
            this.userNumGroupBox.TabStop = false;
            this.userNumGroupBox.Text = "Номера оповещения";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(11, 142);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 13);
            this.label8.TabIndex = 26;
            this.label8.Text = "Номер 5";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 116);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 13);
            this.label7.TabIndex = 25;
            this.label7.Text = "Номер 4";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 90);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 13);
            this.label6.TabIndex = 24;
            this.label6.Text = "Номер 3";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 64);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 13);
            this.label5.TabIndex = 23;
            this.label5.Text = "Номер 2";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "Номер 1";
            // 
            // readUser1NumBtn
            // 
            this.readUser1NumBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("readUser1NumBtn.BackgroundImage")));
            this.readUser1NumBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.readUser1NumBtn.Location = new System.Drawing.Point(155, 31);
            this.readUser1NumBtn.Name = "readUser1NumBtn";
            this.readUser1NumBtn.Size = new System.Drawing.Size(24, 26);
            this.readUser1NumBtn.TabIndex = 0;
            this.readUser1NumBtn.UseVisualStyleBackColor = true;
            this.readUser1NumBtn.Click += new System.EventHandler(this.readUser1NumBtn_Click);
            // 
            // writeUser1NumBtn
            // 
            this.writeUser1NumBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("writeUser1NumBtn.BackgroundImage")));
            this.writeUser1NumBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.writeUser1NumBtn.Location = new System.Drawing.Point(185, 31);
            this.writeUser1NumBtn.Name = "writeUser1NumBtn";
            this.writeUser1NumBtn.Size = new System.Drawing.Size(24, 26);
            this.writeUser1NumBtn.TabIndex = 3;
            this.writeUser1NumBtn.UseVisualStyleBackColor = true;
            this.writeUser1NumBtn.Click += new System.EventHandler(this.writeUser1NumBtn_Click);
            // 
            // readUser2NumBtn
            // 
            this.readUser2NumBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("readUser2NumBtn.BackgroundImage")));
            this.readUser2NumBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.readUser2NumBtn.Location = new System.Drawing.Point(155, 57);
            this.readUser2NumBtn.Name = "readUser2NumBtn";
            this.readUser2NumBtn.Size = new System.Drawing.Size(24, 26);
            this.readUser2NumBtn.TabIndex = 14;
            this.readUser2NumBtn.UseVisualStyleBackColor = true;
            this.readUser2NumBtn.Click += new System.EventHandler(this.readUser2NumBtn_Click);
            // 
            // writeUser2NumBtn
            // 
            this.writeUser2NumBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("writeUser2NumBtn.BackgroundImage")));
            this.writeUser2NumBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.writeUser2NumBtn.Location = new System.Drawing.Point(185, 57);
            this.writeUser2NumBtn.Name = "writeUser2NumBtn";
            this.writeUser2NumBtn.Size = new System.Drawing.Size(24, 26);
            this.writeUser2NumBtn.TabIndex = 15;
            this.writeUser2NumBtn.UseVisualStyleBackColor = true;
            this.writeUser2NumBtn.Click += new System.EventHandler(this.writeUser2NumBtn_Click);
            // 
            // readUser3NumBtn
            // 
            this.readUser3NumBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("readUser3NumBtn.BackgroundImage")));
            this.readUser3NumBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.readUser3NumBtn.Location = new System.Drawing.Point(155, 83);
            this.readUser3NumBtn.Name = "readUser3NumBtn";
            this.readUser3NumBtn.Size = new System.Drawing.Size(24, 26);
            this.readUser3NumBtn.TabIndex = 16;
            this.readUser3NumBtn.UseVisualStyleBackColor = true;
            this.readUser3NumBtn.Click += new System.EventHandler(this.readUser3NumBtn_Click);
            // 
            // writeUser3NumBtn
            // 
            this.writeUser3NumBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("writeUser3NumBtn.BackgroundImage")));
            this.writeUser3NumBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.writeUser3NumBtn.Location = new System.Drawing.Point(185, 83);
            this.writeUser3NumBtn.Name = "writeUser3NumBtn";
            this.writeUser3NumBtn.Size = new System.Drawing.Size(24, 26);
            this.writeUser3NumBtn.TabIndex = 17;
            this.writeUser3NumBtn.UseVisualStyleBackColor = true;
            this.writeUser3NumBtn.Click += new System.EventHandler(this.writeUser3NumBtn_Click);
            // 
            // readUser4NumBtn
            // 
            this.readUser4NumBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("readUser4NumBtn.BackgroundImage")));
            this.readUser4NumBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.readUser4NumBtn.Location = new System.Drawing.Point(155, 109);
            this.readUser4NumBtn.Name = "readUser4NumBtn";
            this.readUser4NumBtn.Size = new System.Drawing.Size(24, 26);
            this.readUser4NumBtn.TabIndex = 18;
            this.readUser4NumBtn.UseVisualStyleBackColor = true;
            this.readUser4NumBtn.Click += new System.EventHandler(this.readUser4NumBtn_Click);
            // 
            // writeUser4NumBtn
            // 
            this.writeUser4NumBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("writeUser4NumBtn.BackgroundImage")));
            this.writeUser4NumBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.writeUser4NumBtn.Location = new System.Drawing.Point(185, 109);
            this.writeUser4NumBtn.Name = "writeUser4NumBtn";
            this.writeUser4NumBtn.Size = new System.Drawing.Size(24, 26);
            this.writeUser4NumBtn.TabIndex = 19;
            this.writeUser4NumBtn.UseVisualStyleBackColor = true;
            this.writeUser4NumBtn.Click += new System.EventHandler(this.writeUser4NumBtn_Click);
            // 
            // readUser5NumBtn
            // 
            this.readUser5NumBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("readUser5NumBtn.BackgroundImage")));
            this.readUser5NumBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.readUser5NumBtn.Location = new System.Drawing.Point(155, 135);
            this.readUser5NumBtn.Name = "readUser5NumBtn";
            this.readUser5NumBtn.Size = new System.Drawing.Size(24, 26);
            this.readUser5NumBtn.TabIndex = 20;
            this.readUser5NumBtn.UseVisualStyleBackColor = true;
            this.readUser5NumBtn.Click += new System.EventHandler(this.readUser5NumBtn_Click);
            // 
            // writeUser5NumBtn
            // 
            this.writeUser5NumBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("writeUser5NumBtn.BackgroundImage")));
            this.writeUser5NumBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.writeUser5NumBtn.Location = new System.Drawing.Point(185, 135);
            this.writeUser5NumBtn.Name = "writeUser5NumBtn";
            this.writeUser5NumBtn.Size = new System.Drawing.Size(24, 26);
            this.writeUser5NumBtn.TabIndex = 21;
            this.writeUser5NumBtn.UseVisualStyleBackColor = true;
            this.writeUser5NumBtn.Click += new System.EventHandler(this.writeUser5NumBtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.readCallParamsBtn);
            this.groupBox1.Controls.Add(this.writeCallParamsBtn);
            this.groupBox1.Controls.Add(this.smsUser1CheckBox);
            this.groupBox1.Controls.Add(this.smsUser5CheckBox);
            this.groupBox1.Controls.Add(this.smsUser4CheckBox);
            this.groupBox1.Controls.Add(this.smsUser3CheckBox);
            this.groupBox1.Controls.Add(this.smsUser2CheckBox);
            this.groupBox1.Controls.Add(this.callUser1CheckBox);
            this.groupBox1.Controls.Add(this.callUser5CheckBox);
            this.groupBox1.Controls.Add(this.callUser4CheckBox);
            this.groupBox1.Controls.Add(this.callUser2CheckBox);
            this.groupBox1.Controls.Add(this.callUser3CheckBox);
            this.groupBox1.Location = new System.Drawing.Point(15, 224);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(223, 110);
            this.groupBox1.TabIndex = 43;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Способы оповещения";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(151, 33);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(13, 13);
            this.label16.TabIndex = 47;
            this.label16.Text = "5";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(130, 33);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(13, 13);
            this.label15.TabIndex = 46;
            this.label15.Text = "4";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(109, 33);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(13, 13);
            this.label14.TabIndex = 45;
            this.label14.Text = "3";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(88, 33);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(13, 13);
            this.label13.TabIndex = 44;
            this.label13.Text = "2";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(67, 33);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(13, 13);
            this.label12.TabIndex = 43;
            this.label12.Text = "1";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 32);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(47, 13);
            this.label11.TabIndex = 27;
            this.label11.Text = "Номера";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 59);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(30, 13);
            this.label10.TabIndex = 42;
            this.label10.Text = "SMS";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 84);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(44, 13);
            this.label9.TabIndex = 27;
            this.label9.Text = "Звонок";
            // 
            // readCallParamsBtn
            // 
            this.readCallParamsBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("readCallParamsBtn.BackgroundImage")));
            this.readCallParamsBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.readCallParamsBtn.Location = new System.Drawing.Point(155, 0);
            this.readCallParamsBtn.Name = "readCallParamsBtn";
            this.readCallParamsBtn.Size = new System.Drawing.Size(24, 26);
            this.readCallParamsBtn.TabIndex = 40;
            this.readCallParamsBtn.UseVisualStyleBackColor = true;
            this.readCallParamsBtn.Click += new System.EventHandler(this.readCallParamsBtn_Click);
            // 
            // writeCallParamsBtn
            // 
            this.writeCallParamsBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("writeCallParamsBtn.BackgroundImage")));
            this.writeCallParamsBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.writeCallParamsBtn.Location = new System.Drawing.Point(185, 0);
            this.writeCallParamsBtn.Name = "writeCallParamsBtn";
            this.writeCallParamsBtn.Size = new System.Drawing.Size(24, 26);
            this.writeCallParamsBtn.TabIndex = 41;
            this.writeCallParamsBtn.UseVisualStyleBackColor = true;
            this.writeCallParamsBtn.Click += new System.EventHandler(this.writeCallParamsBtn_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.deactivationTimeBox);
            this.groupBox2.Controls.Add(this.activationTimeBox);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.writeDeactivationTimeBtn);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.readDeactivationTimeBtn);
            this.groupBox2.Controls.Add(this.readActivationTimeBtn);
            this.groupBox2.Controls.Add(this.writeActivationTimeBtn);
            this.groupBox2.Location = new System.Drawing.Point(244, 118);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(153, 133);
            this.groupBox2.TabIndex = 44;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Установки времени";
            // 
            // deactivationTimeBox
            // 
            this.deactivationTimeBox.Location = new System.Drawing.Point(9, 91);
            this.deactivationTimeBox.Name = "deactivationTimeBox";
            this.deactivationTimeBox.Size = new System.Drawing.Size(64, 20);
            this.deactivationTimeBox.TabIndex = 41;
            this.deactivationTimeBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.deactivationTimeBox.TextChanged += new System.EventHandler(this.activationTimeBox_TextChanged);
            // 
            // activationTimeBox
            // 
            this.activationTimeBox.Location = new System.Drawing.Point(9, 45);
            this.activationTimeBox.Name = "activationTimeBox";
            this.activationTimeBox.Size = new System.Drawing.Size(64, 20);
            this.activationTimeBox.TabIndex = 40;
            this.activationTimeBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.activationTimeBox.TextChanged += new System.EventHandler(this.activationTimeBox_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 13);
            this.label3.TabIndex = 38;
            this.label3.Text = "Отсрочка оповещения, с";
            // 
            // writeDeactivationTimeBtn
            // 
            this.writeDeactivationTimeBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("writeDeactivationTimeBtn.BackgroundImage")));
            this.writeDeactivationTimeBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.writeDeactivationTimeBtn.Location = new System.Drawing.Point(123, 87);
            this.writeDeactivationTimeBtn.Name = "writeDeactivationTimeBtn";
            this.writeDeactivationTimeBtn.Size = new System.Drawing.Size(24, 26);
            this.writeDeactivationTimeBtn.TabIndex = 39;
            this.writeDeactivationTimeBtn.UseVisualStyleBackColor = true;
            this.writeDeactivationTimeBtn.Click += new System.EventHandler(this.writeDeactivationTimeBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Отсрочка активации, с";
            // 
            // readDeactivationTimeBtn
            // 
            this.readDeactivationTimeBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("readDeactivationTimeBtn.BackgroundImage")));
            this.readDeactivationTimeBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.readDeactivationTimeBtn.Location = new System.Drawing.Point(93, 87);
            this.readDeactivationTimeBtn.Name = "readDeactivationTimeBtn";
            this.readDeactivationTimeBtn.Size = new System.Drawing.Size(24, 26);
            this.readDeactivationTimeBtn.TabIndex = 38;
            this.readDeactivationTimeBtn.UseVisualStyleBackColor = true;
            this.readDeactivationTimeBtn.Click += new System.EventHandler(this.readDeactivationTimeBtn_Click);
            // 
            // readActivationTimeBtn
            // 
            this.readActivationTimeBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("readActivationTimeBtn.BackgroundImage")));
            this.readActivationTimeBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.readActivationTimeBtn.Location = new System.Drawing.Point(93, 41);
            this.readActivationTimeBtn.Name = "readActivationTimeBtn";
            this.readActivationTimeBtn.Size = new System.Drawing.Size(24, 26);
            this.readActivationTimeBtn.TabIndex = 36;
            this.readActivationTimeBtn.UseVisualStyleBackColor = true;
            this.readActivationTimeBtn.Click += new System.EventHandler(this.readActivationTimeBtn_Click);
            // 
            // writeActivationTimeBtn
            // 
            this.writeActivationTimeBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("writeActivationTimeBtn.BackgroundImage")));
            this.writeActivationTimeBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.writeActivationTimeBtn.Location = new System.Drawing.Point(123, 41);
            this.writeActivationTimeBtn.Name = "writeActivationTimeBtn";
            this.writeActivationTimeBtn.Size = new System.Drawing.Size(24, 26);
            this.writeActivationTimeBtn.TabIndex = 37;
            this.writeActivationTimeBtn.UseVisualStyleBackColor = true;
            this.writeActivationTimeBtn.Click += new System.EventHandler(this.writeActivationTimeBtn_Click);
            // 
            // exitBtn
            // 
            this.exitBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.exitBtn.Location = new System.Drawing.Point(229, 426);
            this.exitBtn.Name = "exitBtn";
            this.exitBtn.Size = new System.Drawing.Size(168, 44);
            this.exitBtn.TabIndex = 45;
            this.exitBtn.Text = "Выход";
            this.exitBtn.UseVisualStyleBackColor = true;
            this.exitBtn.Click += new System.EventHandler(this.exitBtn_Click);
            // 
            // readDeviceVersionBtn
            // 
            this.readDeviceVersionBtn.Location = new System.Drawing.Point(14, 426);
            this.readDeviceVersionBtn.Name = "readDeviceVersionBtn";
            this.readDeviceVersionBtn.Size = new System.Drawing.Size(210, 21);
            this.readDeviceVersionBtn.TabIndex = 46;
            this.readDeviceVersionBtn.Text = "Версия устройства";
            this.readDeviceVersionBtn.UseVisualStyleBackColor = true;
            this.readDeviceVersionBtn.Click += new System.EventHandler(this.readDeviceVersionBtn_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.sendUSSDBtn);
            this.groupBox3.Controls.Add(this.ussdBox);
            this.groupBox3.Location = new System.Drawing.Point(15, 340);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(382, 80);
            this.groupBox3.TabIndex = 47;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "USSD Команда";
            // 
            // sendUSSDBtn
            // 
            this.sendUSSDBtn.Location = new System.Drawing.Point(155, 45);
            this.sendUSSDBtn.Name = "sendUSSDBtn";
            this.sendUSSDBtn.Size = new System.Drawing.Size(75, 23);
            this.sendUSSDBtn.TabIndex = 1;
            this.sendUSSDBtn.Text = "Отправить";
            this.sendUSSDBtn.UseVisualStyleBackColor = true;
            this.sendUSSDBtn.Click += new System.EventHandler(this.sendUSSDBtn_Click);
            // 
            // ussdBox
            // 
            this.ussdBox.Location = new System.Drawing.Point(17, 19);
            this.ussdBox.Name = "ussdBox";
            this.ussdBox.Size = new System.Drawing.Size(349, 20);
            this.ussdBox.TabIndex = 0;
            // 
            // setDefaultsBtn
            // 
            this.setDefaultsBtn.Location = new System.Drawing.Point(14, 449);
            this.setDefaultsBtn.Name = "setDefaultsBtn";
            this.setDefaultsBtn.Size = new System.Drawing.Size(209, 21);
            this.setDefaultsBtn.TabIndex = 49;
            this.setDefaultsBtn.Text = "Параметры по умолчанию";
            this.setDefaultsBtn.UseVisualStyleBackColor = true;
            this.setDefaultsBtn.Click += new System.EventHandler(this.setDefaultsBtn_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label22);
            this.groupBox5.Controls.Add(this.label21);
            this.groupBox5.Controls.Add(this.label17);
            this.groupBox5.Controls.Add(this.label18);
            this.groupBox5.Controls.Add(this.label19);
            this.groupBox5.Controls.Add(this.label20);
            this.groupBox5.Controls.Add(this.zone1EnableCheckBox);
            this.groupBox5.Controls.Add(this.zone4EnableCheckBox);
            this.groupBox5.Controls.Add(this.zone3EnableCheckBox);
            this.groupBox5.Controls.Add(this.zone2EnableCheckBox);
            this.groupBox5.Controls.Add(this.readActiveZonesBtn);
            this.groupBox5.Controls.Add(this.writeActiveZonesBtn);
            this.groupBox5.Location = new System.Drawing.Point(245, 257);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(152, 77);
            this.groupBox5.TabIndex = 50;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Активные зоны";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(1, 57);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(57, 13);
            this.label22.TabIndex = 56;
            this.label22.Text = "Включена";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(1, 31);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(32, 13);
            this.label21.TabIndex = 55;
            this.label21.Text = "Зона";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(127, 31);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(13, 13);
            this.label17.TabIndex = 54;
            this.label17.Text = "4";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(106, 31);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(13, 13);
            this.label18.TabIndex = 53;
            this.label18.Text = "3";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(85, 31);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(13, 13);
            this.label19.TabIndex = 52;
            this.label19.Text = "2";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(64, 31);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(13, 13);
            this.label20.TabIndex = 51;
            this.label20.Text = "1";
            // 
            // zone1EnableCheckBox
            // 
            this.zone1EnableCheckBox.AutoSize = true;
            this.zone1EnableCheckBox.Location = new System.Drawing.Point(64, 57);
            this.zone1EnableCheckBox.Name = "zone1EnableCheckBox";
            this.zone1EnableCheckBox.Size = new System.Drawing.Size(15, 14);
            this.zone1EnableCheckBox.TabIndex = 47;
            this.zone1EnableCheckBox.UseVisualStyleBackColor = true;
            // 
            // zone4EnableCheckBox
            // 
            this.zone4EnableCheckBox.AutoSize = true;
            this.zone4EnableCheckBox.Location = new System.Drawing.Point(127, 57);
            this.zone4EnableCheckBox.Name = "zone4EnableCheckBox";
            this.zone4EnableCheckBox.Size = new System.Drawing.Size(15, 14);
            this.zone4EnableCheckBox.TabIndex = 50;
            this.zone4EnableCheckBox.UseVisualStyleBackColor = true;
            // 
            // zone3EnableCheckBox
            // 
            this.zone3EnableCheckBox.AutoSize = true;
            this.zone3EnableCheckBox.Location = new System.Drawing.Point(106, 57);
            this.zone3EnableCheckBox.Name = "zone3EnableCheckBox";
            this.zone3EnableCheckBox.Size = new System.Drawing.Size(15, 14);
            this.zone3EnableCheckBox.TabIndex = 49;
            this.zone3EnableCheckBox.UseVisualStyleBackColor = true;
            // 
            // zone2EnableCheckBox
            // 
            this.zone2EnableCheckBox.AutoSize = true;
            this.zone2EnableCheckBox.Location = new System.Drawing.Point(85, 57);
            this.zone2EnableCheckBox.Name = "zone2EnableCheckBox";
            this.zone2EnableCheckBox.Size = new System.Drawing.Size(15, 14);
            this.zone2EnableCheckBox.TabIndex = 48;
            this.zone2EnableCheckBox.UseVisualStyleBackColor = true;
            // 
            // readActiveZonesBtn
            // 
            this.readActiveZonesBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("readActiveZonesBtn.BackgroundImage")));
            this.readActiveZonesBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.readActiveZonesBtn.Location = new System.Drawing.Point(92, 0);
            this.readActiveZonesBtn.Name = "readActiveZonesBtn";
            this.readActiveZonesBtn.Size = new System.Drawing.Size(24, 26);
            this.readActiveZonesBtn.TabIndex = 42;
            this.readActiveZonesBtn.UseVisualStyleBackColor = true;
            this.readActiveZonesBtn.Click += new System.EventHandler(this.readActiveZonesBtn_Click);
            // 
            // writeActiveZonesBtn
            // 
            this.writeActiveZonesBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("writeActiveZonesBtn.BackgroundImage")));
            this.writeActiveZonesBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.writeActiveZonesBtn.Location = new System.Drawing.Point(122, 0);
            this.writeActiveZonesBtn.Name = "writeActiveZonesBtn";
            this.writeActiveZonesBtn.Size = new System.Drawing.Size(24, 26);
            this.writeActiveZonesBtn.TabIndex = 43;
            this.writeActiveZonesBtn.UseVisualStyleBackColor = true;
            this.writeActiveZonesBtn.Click += new System.EventHandler(this.writeActiveZonesBtn_Click);
            // 
            // readDeviceIdBtn
            // 
            this.readDeviceIdBtn.BackgroundImage = global::GsmRingerConfig.Properties.Resources.readBtn16;
            this.readDeviceIdBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.readDeviceIdBtn.Location = new System.Drawing.Point(214, 5);
            this.readDeviceIdBtn.Name = "readDeviceIdBtn";
            this.readDeviceIdBtn.Size = new System.Drawing.Size(24, 26);
            this.readDeviceIdBtn.TabIndex = 8;
            this.readDeviceIdBtn.UseVisualStyleBackColor = true;
            this.readDeviceIdBtn.Click += new System.EventHandler(this.readDeviceIdBtn_Click);
            // 
            // writeAllBtn
            // 
            this.writeAllBtn.BackgroundImage = global::GsmRingerConfig.Properties.Resources.writeAllBtn16;
            this.writeAllBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.writeAllBtn.Location = new System.Drawing.Point(373, 5);
            this.writeAllBtn.Name = "writeAllBtn";
            this.writeAllBtn.Size = new System.Drawing.Size(24, 26);
            this.writeAllBtn.TabIndex = 2;
            this.writeAllBtn.UseVisualStyleBackColor = true;
            this.writeAllBtn.Click += new System.EventHandler(this.writeAllBtn_Click);
            // 
            // readAllBtn
            // 
            this.readAllBtn.BackgroundImage = global::GsmRingerConfig.Properties.Resources.readAllBtn16;
            this.readAllBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.readAllBtn.Location = new System.Drawing.Point(343, 5);
            this.readAllBtn.Name = "readAllBtn";
            this.readAllBtn.Size = new System.Drawing.Size(24, 26);
            this.readAllBtn.TabIndex = 1;
            this.readAllBtn.UseVisualStyleBackColor = true;
            this.readAllBtn.Click += new System.EventHandler(this.readAllBtn_Click);
            // 
            // GsmConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.exitBtn;
            this.ClientSize = new System.Drawing.Size(411, 483);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.setDefaultsBtn);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.readDeviceVersionBtn);
            this.Controls.Add(this.exitBtn);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.readDeviceIdBtn);
            this.Controls.Add(this.deviceIdLbl);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.portConfigBox);
            this.Controls.Add(this.writeAllBtn);
            this.Controls.Add(this.readAllBtn);
            this.Controls.Add(this.userNumGroupBox);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GsmConfig";
            this.Text = "GSM Ringer Lite - Конфигуратор";
            this.Load += new System.EventHandler(this.GsmConfig_Load);
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.GsmConfig_HelpButtonClicked);
            this.portConfigBox.ResumeLayout(false);
            this.userNumGroupBox.ResumeLayout(false);
            this.userNumGroupBox.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button readUser1NumBtn;
        private System.Windows.Forms.Button readAllBtn;
        private System.Windows.Forms.Button writeAllBtn;
        private System.Windows.Forms.Button writeUser1NumBtn;
        private System.Windows.Forms.GroupBox portConfigBox;
        private System.Windows.Forms.ComboBox portNameBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label deviceIdLbl;
        private System.Windows.Forms.Button readDeviceIdBtn;
        private System.Windows.Forms.MaskedTextBox user1NumBox;
        private System.Windows.Forms.MaskedTextBox user2NumBox;
        private System.Windows.Forms.MaskedTextBox user3NumBox;
        private System.Windows.Forms.MaskedTextBox user4NumBox;
        private System.Windows.Forms.MaskedTextBox user5NumBox;
        private System.Windows.Forms.Button writeUser2NumBtn;
        private System.Windows.Forms.Button readUser2NumBtn;
        private System.Windows.Forms.Button writeUser3NumBtn;
        private System.Windows.Forms.Button readUser3NumBtn;
        private System.Windows.Forms.Button writeUser4NumBtn;
        private System.Windows.Forms.Button readUser4NumBtn;
        private System.Windows.Forms.Button writeUser5NumBtn;
        private System.Windows.Forms.Button readUser5NumBtn;
        private System.Windows.Forms.CheckBox smsUser1CheckBox;
        private System.Windows.Forms.CheckBox smsUser2CheckBox;
        private System.Windows.Forms.CheckBox smsUser3CheckBox;
        private System.Windows.Forms.CheckBox smsUser4CheckBox;
        private System.Windows.Forms.CheckBox smsUser5CheckBox;
        private System.Windows.Forms.CheckBox callUser5CheckBox;
        private System.Windows.Forms.CheckBox callUser4CheckBox;
        private System.Windows.Forms.CheckBox callUser3CheckBox;
        private System.Windows.Forms.CheckBox callUser2CheckBox;
        private System.Windows.Forms.CheckBox callUser1CheckBox;
        private System.Windows.Forms.Button writeActivationTimeBtn;
        private System.Windows.Forms.Button readActivationTimeBtn;
        private System.Windows.Forms.Button writeDeactivationTimeBtn;
        private System.Windows.Forms.Button readDeactivationTimeBtn;
        private System.Windows.Forms.Button writeCallParamsBtn;
        private System.Windows.Forms.Button readCallParamsBtn;
        private System.Windows.Forms.GroupBox userNumGroupBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button exitBtn;
        private System.Windows.Forms.Button readDeviceVersionBtn;
        private System.Windows.Forms.TextBox activationTimeBox;
        private System.Windows.Forms.TextBox deactivationTimeBox;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox ussdBox;
        private System.Windows.Forms.Button sendUSSDBtn;
        private System.Windows.Forms.Button setDefaultsBtn;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button readActiveZonesBtn;
        private System.Windows.Forms.Button writeActiveZonesBtn;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.CheckBox zone1EnableCheckBox;
        private System.Windows.Forms.CheckBox zone4EnableCheckBox;
        private System.Windows.Forms.CheckBox zone3EnableCheckBox;
        private System.Windows.Forms.CheckBox zone2EnableCheckBox;

    }
}

