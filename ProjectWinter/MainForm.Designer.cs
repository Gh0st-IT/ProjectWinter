namespace ProjectWinter
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.sysTray = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.operatingSystemVersion = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.user = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.osBuild = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.osVersion = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.checkWindowsUpdate = new System.Windows.Forms.Button();
            this.pcNameLabel = new System.Windows.Forms.Label();
            this.appVersion = new System.Windows.Forms.Label();
            this.btnOtherInformation = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // sysTray
            // 
            this.sysTray.ContextMenuStrip = this.contextMenuStrip1;
            this.sysTray.Icon = ((System.Drawing.Icon)(resources.GetObject("sysTray.Icon")));
            this.sysTray.Text = "Winter";
            this.sysTray.Visible = true;
            this.sysTray.Click += new System.EventHandler(this.sysTray_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(94, 26);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(93, 22);
            this.toolStripMenuItem1.Text = "Exit";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ProjectWinter.Properties.Resources.snowflake;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(64, 64);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // operatingSystemVersion
            // 
            this.operatingSystemVersion.AutoSize = true;
            this.operatingSystemVersion.BackColor = System.Drawing.SystemColors.Control;
            this.operatingSystemVersion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.operatingSystemVersion.Font = new System.Drawing.Font("Arial Rounded MT Bold", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.operatingSystemVersion.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.operatingSystemVersion.Location = new System.Drawing.Point(82, 25);
            this.operatingSystemVersion.Name = "operatingSystemVersion";
            this.operatingSystemVersion.Size = new System.Drawing.Size(157, 37);
            this.operatingSystemVersion.TabIndex = 2;
            this.operatingSystemVersion.Text = "Windows";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.user);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.operatingSystemVersion);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(527, 86);
            this.panel1.TabIndex = 3;
            // 
            // user
            // 
            this.user.BackColor = System.Drawing.SystemColors.Control;
            this.user.Font = new System.Drawing.Font("Arial Rounded MT Bold", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.user.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.user.Location = new System.Drawing.Point(326, 32);
            this.user.Name = "user";
            this.user.Size = new System.Drawing.Size(190, 24);
            this.user.TabIndex = 3;
            this.user.Text = "User";
            this.user.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.Control;
            this.panel3.Controls.Add(this.osBuild);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(4, 4);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(8);
            this.panel3.Size = new System.Drawing.Size(240, 75);
            this.panel3.TabIndex = 5;
            // 
            // osBuild
            // 
            this.osBuild.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.osBuild.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.osBuild.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.osBuild.Location = new System.Drawing.Point(17, 45);
            this.osBuild.Name = "osBuild";
            this.osBuild.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.osBuild.Size = new System.Drawing.Size(212, 22);
            this.osBuild.TabIndex = 1;
            this.osBuild.Text = "Build";
            this.osBuild.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial Rounded MT Bold", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.label5.Location = new System.Drawing.Point(11, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 24);
            this.label5.TabIndex = 0;
            this.label5.Text = "OS Build";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.panel3);
            this.panel4.Location = new System.Drawing.Point(266, 92);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(4);
            this.panel4.Size = new System.Drawing.Size(250, 85);
            this.panel4.TabIndex = 6;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.panel6);
            this.panel5.Location = new System.Drawing.Point(10, 92);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(4);
            this.panel5.Size = new System.Drawing.Size(250, 85);
            this.panel5.TabIndex = 7;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.SystemColors.Control;
            this.panel6.Controls.Add(this.osVersion);
            this.panel6.Controls.Add(this.label7);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(4, 4);
            this.panel6.Name = "panel6";
            this.panel6.Padding = new System.Windows.Forms.Padding(8);
            this.panel6.Size = new System.Drawing.Size(240, 75);
            this.panel6.TabIndex = 5;
            // 
            // osVersion
            // 
            this.osVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.osVersion.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.osVersion.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.osVersion.Location = new System.Drawing.Point(17, 45);
            this.osVersion.Name = "osVersion";
            this.osVersion.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.osVersion.Size = new System.Drawing.Size(212, 22);
            this.osVersion.TabIndex = 1;
            this.osVersion.Text = "Version";
            this.osVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial Rounded MT Bold", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.label7.Location = new System.Drawing.Point(11, 8);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(124, 24);
            this.label7.TabIndex = 0;
            this.label7.Text = "OS Version";
            // 
            // checkWindowsUpdate
            // 
            this.checkWindowsUpdate.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.checkWindowsUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkWindowsUpdate.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkWindowsUpdate.ForeColor = System.Drawing.SystemColors.Control;
            this.checkWindowsUpdate.Location = new System.Drawing.Point(10, 191);
            this.checkWindowsUpdate.Name = "checkWindowsUpdate";
            this.checkWindowsUpdate.Padding = new System.Windows.Forms.Padding(8);
            this.checkWindowsUpdate.Size = new System.Drawing.Size(162, 62);
            this.checkWindowsUpdate.TabIndex = 8;
            this.checkWindowsUpdate.Text = "Check Windows Update";
            this.checkWindowsUpdate.UseVisualStyleBackColor = false;
            this.checkWindowsUpdate.Click += new System.EventHandler(this.checkWindowsUpdate_Click);
            // 
            // pcNameLabel
            // 
            this.pcNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pcNameLabel.BackColor = System.Drawing.SystemColors.Control;
            this.pcNameLabel.Font = new System.Drawing.Font("Arial Rounded MT Bold", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pcNameLabel.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.pcNameLabel.Location = new System.Drawing.Point(326, 208);
            this.pcNameLabel.Name = "pcNameLabel";
            this.pcNameLabel.Size = new System.Drawing.Size(190, 24);
            this.pcNameLabel.TabIndex = 9;
            this.pcNameLabel.Text = "PC Name";
            this.pcNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // appVersion
            // 
            this.appVersion.AutoSize = true;
            this.appVersion.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.appVersion.Location = new System.Drawing.Point(476, 241);
            this.appVersion.Name = "appVersion";
            this.appVersion.Size = new System.Drawing.Size(40, 13);
            this.appVersion.TabIndex = 10;
            this.appVersion.Text = "1.0.0.0";
            // 
            // btnOtherInformation
            // 
            this.btnOtherInformation.BackColor = System.Drawing.Color.Transparent;
            this.btnOtherInformation.FlatAppearance.BorderSize = 0;
            this.btnOtherInformation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOtherInformation.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOtherInformation.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnOtherInformation.Location = new System.Drawing.Point(178, 228);
            this.btnOtherInformation.Name = "btnOtherInformation";
            this.btnOtherInformation.Size = new System.Drawing.Size(114, 23);
            this.btnOtherInformation.TabIndex = 11;
            this.btnOtherInformation.Text = "Other Information";
            this.btnOtherInformation.UseVisualStyleBackColor = false;
            this.btnOtherInformation.Click += new System.EventHandler(this.btnOtherInformation_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(527, 263);
            this.Controls.Add(this.btnOtherInformation);
            this.Controls.Add(this.appVersion);
            this.Controls.Add(this.pcNameLabel);
            this.Controls.Add(this.checkWindowsUpdate);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon sysTray;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label operatingSystemVersion;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label osBuild;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label osVersion;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button checkWindowsUpdate;
        private System.Windows.Forms.Label user;
        private System.Windows.Forms.Label pcNameLabel;
        private System.Windows.Forms.Label appVersion;
        private System.Windows.Forms.Button btnOtherInformation;
    }
}

