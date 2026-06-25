namespace UCPPABD
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
            this.btnLogin = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.cmbRole = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panelLoginCard = new System.Windows.Forms.Panel();
            this.lnkDbConfig = new System.Windows.Forms.LinkLabel();
            this.lblSubHeader = new System.Windows.Forms.Label();
            this.lblHeader = new System.Windows.Forms.Label();
            this.panelLoginCard.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.btnLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(40, 275);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(280, 42);
            this.btnLogin.TabIndex = 4;
            this.btnLogin.Text = "LOG IN";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.label1.Location = new System.Drawing.Point(37, 138);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Username";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.label2.Location = new System.Drawing.Point(37, 203);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Password";
            // 
            // txtUsername
            // 
            this.txtUsername.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtUsername.Location = new System.Drawing.Point(40, 160);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(280, 26);
            this.txtUsername.TabIndex = 2;
            this.txtUsername.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtPassword.Location = new System.Drawing.Point(40, 225);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '●';
            this.txtPassword.Size = new System.Drawing.Size(280, 26);
            this.txtPassword.TabIndex = 3;
            // 
            // cmbRole
            // 
            this.cmbRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRole.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.cmbRole.FormattingEnabled = true;
            this.cmbRole.Items.AddRange(new object[] {
            "Admin",
            "Siswa"});
            this.cmbRole.Location = new System.Drawing.Point(40, 95);
            this.cmbRole.Name = "cmbRole";
            this.cmbRole.Size = new System.Drawing.Size(280, 27);
            this.cmbRole.TabIndex = 1;
            this.cmbRole.SelectedIndexChanged += new System.EventHandler(this.cmbRole_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.label3.Location = new System.Drawing.Point(37, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = "Login Sebagai";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // panelLoginCard
            // 
            this.panelLoginCard.BackColor = System.Drawing.Color.White;
            this.panelLoginCard.Controls.Add(this.lnkDbConfig);
            this.panelLoginCard.Controls.Add(this.lblSubHeader);
            this.panelLoginCard.Controls.Add(this.lblHeader);
            this.panelLoginCard.Controls.Add(this.label3);
            this.panelLoginCard.Controls.Add(this.cmbRole);
            this.panelLoginCard.Controls.Add(this.btnLogin);
            this.panelLoginCard.Controls.Add(this.txtPassword);
            this.panelLoginCard.Controls.Add(this.label1);
            this.panelLoginCard.Controls.Add(this.txtUsername);
            this.panelLoginCard.Controls.Add(this.label2);
            this.panelLoginCard.Location = new System.Drawing.Point(120, 45);
            this.panelLoginCard.Name = "panelLoginCard";
            this.panelLoginCard.Size = new System.Drawing.Size(360, 385);
            this.panelLoginCard.TabIndex = 8;
            // 
            // lnkDbConfig
            // 
            this.lnkDbConfig.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(102)))), ((int)(((byte)(241)))));
            this.lnkDbConfig.AutoSize = true;
            this.lnkDbConfig.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lnkDbConfig.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.lnkDbConfig.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.lnkDbConfig.Location = new System.Drawing.Point(125, 342);
            this.lnkDbConfig.Name = "lnkDbConfig";
            this.lnkDbConfig.Size = new System.Drawing.Size(111, 15);
            this.lnkDbConfig.TabIndex = 5;
            this.lnkDbConfig.TabStop = true;
            this.lnkDbConfig.Text = "⚙️ Atur Server Database";
            this.lnkDbConfig.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkDbConfig_LinkClicked);
            // 
            // lblSubHeader
            // 
            this.lblSubHeader.AutoSize = true;
            this.lblSubHeader.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSubHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblSubHeader.Location = new System.Drawing.Point(37, 44);
            this.lblSubHeader.Name = "lblSubHeader";
            this.lblSubHeader.Size = new System.Drawing.Size(183, 15);
            this.lblSubHeader.TabIndex = 9;
            this.lblSubHeader.Text = "Sistem Input Jadwal Pelajaran";
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI Semibold", 15F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblHeader.Location = new System.Drawing.Point(35, 15);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(155, 28);
            this.lblHeader.TabIndex = 8;
            this.lblHeader.Text = "Selamat Datang";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(600, 480);
            this.Controls.Add(this.panelLoginCard);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login - Sistem Jadwal Pelajaran";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panelLoginCard.ResumeLayout(false);
            this.panelLoginCard.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.ComboBox cmbRole;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panelLoginCard;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblSubHeader;
        private System.Windows.Forms.LinkLabel lnkDbConfig;
    }
}
