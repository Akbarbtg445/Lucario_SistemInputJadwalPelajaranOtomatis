namespace UCPPABD
{
    partial class UserDashboard
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
            this.btnLogout = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblNIS = new System.Windows.Forms.Label();
            this.lblNama = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCari = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbKelas = new System.Windows.Forms.ComboBox();
            this.dgvJadwal = new System.Windows.Forms.DataGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblSisa = new System.Windows.Forms.Label();
            this.lblTerisi = new System.Windows.Forms.Label();
            this.lblMax = new System.Windows.Forms.Label();
            this.btnCetak = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbPilihkelas = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnSimpan = new System.Windows.Forms.Button();
            this.Jammulai = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpMulai = new System.Windows.Forms.DateTimePicker();
            this.dtpSelesai = new System.Windows.Forms.DateTimePicker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmbMapel = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvJadwal)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(808, 102);
            this.btnLogout.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(105, 34);
            this.btnLogout.TabIndex = 0;
            this.btnLogout.Text = "LOG OUT";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.lblNIS);
            this.panel1.Controls.Add(this.lblNama);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1515, 55);
            this.panel1.TabIndex = 1;
            // 
            // lblNIS
            // 
            this.lblNIS.AutoSize = true;
            this.lblNIS.Location = new System.Drawing.Point(566, 19);
            this.lblNIS.Name = "lblNIS";
            this.lblNIS.Size = new System.Drawing.Size(36, 20);
            this.lblNIS.TabIndex = 1;
            this.lblNIS.Text = "NIS";
            // 
            // lblNama
            // 
            this.lblNama.AutoSize = true;
            this.lblNama.Location = new System.Drawing.Point(310, 19);
            this.lblNama.Name = "lblNama";
            this.lblNama.Size = new System.Drawing.Size(51, 20);
            this.lblNama.TabIndex = 0;
            this.lblNama.Text = "Nama";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCari);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cmbKelas);
            this.groupBox1.Location = new System.Drawing.Point(29, 102);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(280, 114);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lihat Jadwal Kelas";
            // 
            // btnCari
            // 
            this.btnCari.Location = new System.Drawing.Point(134, 74);
            this.btnCari.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCari.Name = "btnCari";
            this.btnCari.Size = new System.Drawing.Size(75, 34);
            this.btnCari.TabIndex = 2;
            this.btnCari.Text = "CARI";
            this.btnCari.UseVisualStyleBackColor = true;
            this.btnCari.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Kelas";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // cmbKelas
            // 
            this.cmbKelas.FormattingEnabled = true;
            this.cmbKelas.Location = new System.Drawing.Point(134, 35);
            this.cmbKelas.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbKelas.Name = "cmbKelas";
            this.cmbKelas.Size = new System.Drawing.Size(121, 28);
            this.cmbKelas.TabIndex = 0;
            // 
            // dgvJadwal
            // 
            this.dgvJadwal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvJadwal.Location = new System.Drawing.Point(29, 232);
            this.dgvJadwal.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvJadwal.Name = "dgvJadwal";
            this.dgvJadwal.ReadOnly = true;
            this.dgvJadwal.RowHeadersWidth = 62;
            this.dgvJadwal.RowTemplate.Height = 28;
            this.dgvJadwal.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvJadwal.Size = new System.Drawing.Size(1114, 333);
            this.dgvJadwal.TabIndex = 3;
            this.dgvJadwal.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvJadwal_CellClick);
            this.dgvJadwal.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvJadwal_CellClick);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblStatus);
            this.groupBox3.Controls.Add(this.lblSisa);
            this.groupBox3.Controls.Add(this.lblTerisi);
            this.groupBox3.Controls.Add(this.lblMax);
            this.groupBox3.Location = new System.Drawing.Point(29, 569);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Size = new System.Drawing.Size(386, 205);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Cek Kapasitas";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(205, 112);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(56, 20);
            this.lblStatus.TabIndex = 5;
            this.lblStatus.Text = "Status";
            // 
            // lblSisa
            // 
            this.lblSisa.AutoSize = true;
            this.lblSisa.Location = new System.Drawing.Point(11, 149);
            this.lblSisa.Name = "lblSisa";
            this.lblSisa.Size = new System.Drawing.Size(86, 20);
            this.lblSisa.TabIndex = 2;
            this.lblSisa.Text = "Sisa Kuota";
            // 
            // lblTerisi
            // 
            this.lblTerisi.AutoSize = true;
            this.lblTerisi.Location = new System.Drawing.Point(11, 90);
            this.lblTerisi.Name = "lblTerisi";
            this.lblTerisi.Size = new System.Drawing.Size(147, 20);
            this.lblTerisi.TabIndex = 1;
            this.lblTerisi.Text = "Jumlah Siswa Terisi";
            this.lblTerisi.Click += new System.EventHandler(this.label9_Click);
            // 
            // lblMax
            // 
            this.lblMax.AutoSize = true;
            this.lblMax.Location = new System.Drawing.Point(11, 46);
            this.lblMax.Name = "lblMax";
            this.lblMax.Size = new System.Drawing.Size(149, 20);
            this.lblMax.TabIndex = 0;
            this.lblMax.Text = "Kapasitas Maksimal";
            this.lblMax.Click += new System.EventHandler(this.label8_Click);
            // 
            // btnCetak
            // 
            this.btnCetak.Location = new System.Drawing.Point(213, 227);
            this.btnCetak.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCetak.Name = "btnCetak";
            this.btnCetak.Size = new System.Drawing.Size(75, 30);
            this.btnCetak.TabIndex = 0;
            this.btnCetak.Text = "Cetak";
            this.btnCetak.UseVisualStyleBackColor = true;
            this.btnCetak.Click += new System.EventHandler(this.btnCetak_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(157, 192);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(156, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Cetak Jadwal Pribadi";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Mata Pelajaran";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 149);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 20);
            this.label4.TabIndex = 4;
            this.label4.Text = "Pilih Kelas";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // cmbPilihkelas
            // 
            this.cmbPilihkelas.FormattingEnabled = true;
            this.cmbPilihkelas.Location = new System.Drawing.Point(135, 141);
            this.cmbPilihkelas.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbPilihkelas.Name = "cmbPilihkelas";
            this.cmbPilihkelas.Size = new System.Drawing.Size(129, 28);
            this.cmbPilihkelas.TabIndex = 5;
            this.cmbPilihkelas.SelectedIndexChanged += new System.EventHandler(this.cmbPilihkelas_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 192);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(145, 20);
            this.label7.TabIndex = 6;
            this.label7.Text = "Simpan Perubahan";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // btnSimpan
            // 
            this.btnSimpan.Location = new System.Drawing.Point(10, 227);
            this.btnSimpan.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSimpan.Name = "btnSimpan";
            this.btnSimpan.Size = new System.Drawing.Size(75, 36);
            this.btnSimpan.TabIndex = 7;
            this.btnSimpan.Text = "Update";
            this.btnSimpan.UseVisualStyleBackColor = true;
            this.btnSimpan.Click += new System.EventHandler(this.btnSimpan_Click);
            // 
            // Jammulai
            // 
            this.Jammulai.AutoSize = true;
            this.Jammulai.Location = new System.Drawing.Point(6, 46);
            this.Jammulai.Name = "Jammulai";
            this.Jammulai.Size = new System.Drawing.Size(80, 20);
            this.Jammulai.TabIndex = 9;
            this.Jammulai.Text = "Jam Mulai";
            this.Jammulai.Click += new System.EventHandler(this.label5_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(228, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 20);
            this.label5.TabIndex = 10;
            this.label5.Text = "Jam Selesai";
            // 
            // dtpMulai
            // 
            this.dtpMulai.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpMulai.Location = new System.Drawing.Point(10, 76);
            this.dtpMulai.Name = "dtpMulai";
            this.dtpMulai.ShowUpDown = true;
            this.dtpMulai.Size = new System.Drawing.Size(141, 26);
            this.dtpMulai.TabIndex = 11;
            this.dtpMulai.ValueChanged += new System.EventHandler(this.dtpMulai_ValueChanged);
            // 
            // dtpSelesai
            // 
            this.dtpSelesai.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpSelesai.Location = new System.Drawing.Point(232, 76);
            this.dtpSelesai.Name = "dtpSelesai";
            this.dtpSelesai.ShowUpDown = true;
            this.dtpSelesai.Size = new System.Drawing.Size(134, 26);
            this.dtpSelesai.TabIndex = 12;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmbMapel);
            this.groupBox2.Controls.Add(this.dtpSelesai);
            this.groupBox2.Controls.Add(this.dtpMulai);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.Jammulai);
            this.groupBox2.Controls.Add(this.btnSimpan);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.cmbPilihkelas);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.btnCetak);
            this.groupBox2.Location = new System.Drawing.Point(633, 569);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(510, 298);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Edit Jadwal Kelas";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // cmbMapel
            // 
            this.cmbMapel.FormattingEnabled = true;
            this.cmbMapel.Location = new System.Drawing.Point(136, 109);
            this.cmbMapel.Name = "cmbMapel";
            this.cmbMapel.Size = new System.Drawing.Size(187, 28);
            this.cmbMapel.TabIndex = 13;
            // 
            // UserDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1515, 898);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.dgvJadwal);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnLogout);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "UserDashboard";
            this.Text = "UserDashboard";
            this.Load += new System.EventHandler(this.UserDashboard_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvJadwal)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvJadwal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbKelas;
        private System.Windows.Forms.Button btnCari;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblMax;
        private System.Windows.Forms.Label lblSisa;
        private System.Windows.Forms.Label lblTerisi;
        private System.Windows.Forms.Label lblStatus;
        public System.Windows.Forms.Label lblNIS;
        public System.Windows.Forms.Label lblNama;
        private System.Windows.Forms.Button btnCetak;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbPilihkelas;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnSimpan;
        private System.Windows.Forms.Label Jammulai;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpMulai;
        private System.Windows.Forms.DateTimePicker dtpSelesai;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cmbMapel;
    }
}