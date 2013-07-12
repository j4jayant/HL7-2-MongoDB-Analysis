namespace j4jayant.HL7toMongoDB
{
    partial class LoadData
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
            this.grpFile = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtMsgPath = new System.Windows.Forms.TextBox();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblCurrent = new System.Windows.Forms.Label();
            this.pbStatus = new System.Windows.Forms.ProgressBar();
            this.label7 = new System.Windows.Forms.Label();
            this.txtFeed = new System.Windows.Forms.TextBox();
            this.txtActivity = new System.Windows.Forms.RichTextBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.txtMongoPass = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtMongoUser = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtMongoPort = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMongoServer = new System.Windows.Forms.TextBox();
            this.grpFile.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpFile
            // 
            this.grpFile.Controls.Add(this.label1);
            this.grpFile.Controls.Add(this.btnBrowse);
            this.grpFile.Controls.Add(this.txtMsgPath);
            this.grpFile.Location = new System.Drawing.Point(10, 159);
            this.grpFile.Margin = new System.Windows.Forms.Padding(4);
            this.grpFile.Name = "grpFile";
            this.grpFile.Padding = new System.Windows.Forms.Padding(4);
            this.grpFile.Size = new System.Drawing.Size(468, 70);
            this.grpFile.TabIndex = 34;
            this.grpFile.TabStop = false;
            this.grpFile.Text = "File Source";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 27);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 16);
            this.label1.TabIndex = 7;
            this.label1.Text = "Select Path:";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(406, 14);
            this.btnBrowse.Margin = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(49, 41);
            this.btnBrowse.TabIndex = 6;
            this.btnBrowse.Text = "...";
            this.btnBrowse.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtMsgPath
            // 
            this.txtMsgPath.Location = new System.Drawing.Point(88, 25);
            this.txtMsgPath.Margin = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtMsgPath.Name = "txtMsgPath";
            this.txtMsgPath.Size = new System.Drawing.Size(310, 22);
            this.txtMsgPath.TabIndex = 5;
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(415, 237);
            this.lblTotal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(0, 16);
            this.lblTotal.TabIndex = 33;
            // 
            // lblCurrent
            // 
            this.lblCurrent.AutoSize = true;
            this.lblCurrent.Location = new System.Drawing.Point(253, 237);
            this.lblCurrent.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCurrent.Name = "lblCurrent";
            this.lblCurrent.Size = new System.Drawing.Size(0, 16);
            this.lblCurrent.TabIndex = 32;
            // 
            // pbStatus
            // 
            this.pbStatus.Location = new System.Drawing.Point(241, 262);
            this.pbStatus.Margin = new System.Windows.Forms.Padding(4);
            this.pbStatus.Name = "pbStatus";
            this.pbStatus.Size = new System.Drawing.Size(237, 47);
            this.pbStatus.TabIndex = 31;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 14);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 16);
            this.label7.TabIndex = 30;
            this.label7.Text = "Feed: ";
            // 
            // txtFeed
            // 
            this.txtFeed.Location = new System.Drawing.Point(98, 9);
            this.txtFeed.Margin = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtFeed.Name = "txtFeed";
            this.txtFeed.Size = new System.Drawing.Size(140, 22);
            this.txtFeed.TabIndex = 29;
            this.txtFeed.Text = "ADT";
            // 
            // txtActivity
            // 
            this.txtActivity.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtActivity.Location = new System.Drawing.Point(488, 6);
            this.txtActivity.Margin = new System.Windows.Forms.Padding(4);
            this.txtActivity.Name = "txtActivity";
            this.txtActivity.ReadOnly = true;
            this.txtActivity.Size = new System.Drawing.Size(436, 303);
            this.txtActivity.TabIndex = 28;
            this.txtActivity.Text = "";
            this.txtActivity.WordWrap = false;
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(123, 262);
            this.btnStop.Margin = new System.Windows.Forms.Padding(4);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(103, 50);
            this.btnStop.TabIndex = 27;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(10, 262);
            this.btnStart.Margin = new System.Windows.Forms.Padding(4);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(103, 50);
            this.btnStart.TabIndex = 26;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(240, 69);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(74, 16);
            this.label10.TabIndex = 18;
            this.label10.Text = "Password: ";
            // 
            // txtMongoPass
            // 
            this.txtMongoPass.Location = new System.Drawing.Point(315, 65);
            this.txtMongoPass.Margin = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtMongoPass.Name = "txtMongoPass";
            this.txtMongoPass.Size = new System.Drawing.Size(140, 22);
            this.txtMongoPass.TabIndex = 17;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(5, 69);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(80, 16);
            this.label9.TabIndex = 16;
            this.label9.Text = "User Name:";
            // 
            // txtMongoUser
            // 
            this.txtMongoUser.Location = new System.Drawing.Point(86, 65);
            this.txtMongoUser.Margin = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtMongoUser.Name = "txtMongoUser";
            this.txtMongoUser.Size = new System.Drawing.Size(140, 22);
            this.txtMongoUser.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(228, 33);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(11, 16);
            this.label6.TabIndex = 12;
            this.label6.Text = ":";
            // 
            // txtMongoPort
            // 
            this.txtMongoPort.Location = new System.Drawing.Point(242, 30);
            this.txtMongoPort.Margin = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtMongoPort.Name = "txtMongoPort";
            this.txtMongoPort.Size = new System.Drawing.Size(72, 22);
            this.txtMongoPort.TabIndex = 11;
            this.txtMongoPort.Text = "27017";
            this.txtMongoPort.Enter += new System.EventHandler(this.txtMongoPort_Enter);
            this.txtMongoPort.Leave += new System.EventHandler(this.txtMongoPort_Leave);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtMongoPass);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtMongoUser);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtMongoPort);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtMongoServer);
            this.groupBox1.Location = new System.Drawing.Point(10, 52);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.groupBox1.Size = new System.Drawing.Size(466, 105);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Database Settings";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 33);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 16);
            this.label4.TabIndex = 10;
            this.label4.Text = "Connection:";
            // 
            // txtMongoServer
            // 
            this.txtMongoServer.Location = new System.Drawing.Point(86, 30);
            this.txtMongoServer.Margin = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtMongoServer.Name = "txtMongoServer";
            this.txtMongoServer.Size = new System.Drawing.Size(140, 22);
            this.txtMongoServer.TabIndex = 9;
            this.txtMongoServer.Text = "localhost";
            this.txtMongoServer.Enter += new System.EventHandler(this.txtMongoServer_Enter);
            this.txtMongoServer.Leave += new System.EventHandler(this.txtMongoServer_Leave);
            // 
            // LoadData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 318);
            this.Controls.Add(this.grpFile);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.lblCurrent);
            this.Controls.Add(this.pbStatus);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtFeed);
            this.Controls.Add(this.txtActivity);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "LoadData";
            this.Text = "LoadData";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LoadData_FormClosing);
            this.Load += new System.EventHandler(this.LoadData_Load);
            this.grpFile.ResumeLayout(false);
            this.grpFile.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtMsgPath;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblCurrent;
        private System.Windows.Forms.ProgressBar pbStatus;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtFeed;
        private System.Windows.Forms.RichTextBox txtActivity;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtMongoPass;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtMongoUser;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtMongoPort;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtMongoServer;
    }
}

