namespace UCPPABD
{
    partial class AdminDashboard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminDashboard));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.dgvJadwal = new System.Windows.Forms.DataGridView();
            this.panelinput = new System.Windows.Forms.GroupBox();
            this.cmbGuru = new System.Windows.Forms.ComboBox();
            this.guru = new System.Windows.Forms.Label();
            this.cetakjadwal = new System.Windows.Forms.Label();
            this.hapusjadwal = new System.Windows.Forms.Label();
            this.ubahjadwal = new System.Windows.Forms.Label();
            this.tambahjadwal = new System.Windows.Forms.Label();
            this.btnCetak = new System.Windows.Forms.Button();
            this.btnHapus = new System.Windows.Forms.Button();
            this.btnUbah = new System.Windows.Forms.Button();
            this.btnTambah = new System.Windows.Forms.Button();
            this.matapelajaran = new System.Windows.Forms.Label();
            this.cmbMapel = new System.Windows.Forms.ComboBox();
            this.dtpSelesai = new System.Windows.Forms.DateTimePicker();
            this.jamselesai = new System.Windows.Forms.Label();
            this.cmbKelas = new System.Windows.Forms.ComboBox();
            this.pilihkelas = new System.Windows.Forms.Label();
            this.jammulai = new System.Windows.Forms.Label();
            this.pilihhari = new System.Windows.Forms.Label();
            this.dtpMulai = new System.Windows.Forms.DateTimePicker();
            this.cmbHari = new System.Windows.Forms.ComboBox();
            this.tabeljadwalsaatini = new System.Windows.Forms.Label();
            this.btnLogoutA = new System.Windows.Forms.Button();
            this.bindingNavigator1 = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.chartJadwal = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnImportGuru = new System.Windows.Forms.Button();
            this.btnHapusGuru = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvJadwal)).BeginInit();
            this.panelinput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).BeginInit();
            this.bindingNavigator1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartJadwal)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvJadwal
            // 
            this.dgvJadwal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvJadwal.Location = new System.Drawing.Point(156, 451);
            this.dgvJadwal.Name = "dgvJadwal";
            this.dgvJadwal.RowHeadersWidth = 62;
            this.dgvJadwal.RowTemplate.Height = 28;
            this.dgvJadwal.Size = new System.Drawing.Size(1070, 204);
            this.dgvJadwal.TabIndex = 0;
            this.dgvJadwal.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvJadwal_CellContentClick);
            // 
            // panelinput
            // 
            this.panelinput.Controls.Add(this.cmbGuru);
            this.panelinput.Controls.Add(this.guru);
            this.panelinput.Controls.Add(this.cetakjadwal);
            this.panelinput.Controls.Add(this.hapusjadwal);
            this.panelinput.Controls.Add(this.ubahjadwal);
            this.panelinput.Controls.Add(this.tambahjadwal);
            this.panelinput.Controls.Add(this.btnCetak);
            this.panelinput.Controls.Add(this.btnHapus);
            this.panelinput.Controls.Add(this.btnUbah);
            this.panelinput.Controls.Add(this.btnTambah);
            this.panelinput.Controls.Add(this.matapelajaran);
            this.panelinput.Controls.Add(this.cmbMapel);
            this.panelinput.Controls.Add(this.dtpSelesai);
            this.panelinput.Controls.Add(this.jamselesai);
            this.panelinput.Controls.Add(this.cmbKelas);
            this.panelinput.Controls.Add(this.pilihkelas);
            this.panelinput.Controls.Add(this.jammulai);
            this.panelinput.Controls.Add(this.pilihhari);
            this.panelinput.Controls.Add(this.dtpMulai);
            this.panelinput.Controls.Add(this.cmbHari);
            this.panelinput.Location = new System.Drawing.Point(156, 84);
            this.panelinput.Name = "panelinput";
            this.panelinput.Size = new System.Drawing.Size(611, 296);
            this.panelinput.TabIndex = 1;
            this.panelinput.TabStop = false;
            this.panelinput.Text = "Form Input Jadwal";
            // 
            // cmbGuru
            // 
            this.cmbGuru.FormattingEnabled = true;
            this.cmbGuru.Location = new System.Drawing.Point(373, 119);
            this.cmbGuru.Name = "cmbGuru";
            this.cmbGuru.Size = new System.Drawing.Size(121, 28);
            this.cmbGuru.TabIndex = 19;
            // 
            // guru
            // 
            this.guru.AutoSize = true;
            this.guru.Location = new System.Drawing.Point(316, 124);
            this.guru.Name = "guru";
            this.guru.Size = new System.Drawing.Size(45, 20);
            this.guru.TabIndex = 18;
            this.guru.Text = "Guru";
            this.guru.Click += new System.EventHandler(this.label11_Click);
            // 
            // cetakjadwal
            // 
            this.cetakjadwal.AutoSize = true;
            this.cetakjadwal.Location = new System.Drawing.Point(458, 212);
            this.cetakjadwal.Name = "cetakjadwal";
            this.cetakjadwal.Size = new System.Drawing.Size(104, 20);
            this.cetakjadwal.TabIndex = 17;
            this.cetakjadwal.Text = "Cetak Jadwal";
            // 
            // hapusjadwal
            // 
            this.hapusjadwal.AutoSize = true;
            this.hapusjadwal.Location = new System.Drawing.Point(316, 212);
            this.hapusjadwal.Name = "hapusjadwal";
            this.hapusjadwal.Size = new System.Drawing.Size(109, 20);
            this.hapusjadwal.TabIndex = 16;
            this.hapusjadwal.Text = "Hapus Jadwal";
            // 
            // ubahjadwal
            // 
            this.ubahjadwal.AutoSize = true;
            this.ubahjadwal.Location = new System.Drawing.Point(184, 212);
            this.ubahjadwal.Name = "ubahjadwal";
            this.ubahjadwal.Size = new System.Drawing.Size(101, 20);
            this.ubahjadwal.TabIndex = 15;
            this.ubahjadwal.Text = "Ubah Jadwal";
            this.ubahjadwal.Click += new System.EventHandler(this.label8_Click);
            // 
            // tambahjadwal
            // 
            this.tambahjadwal.AutoSize = true;
            this.tambahjadwal.Location = new System.Drawing.Point(48, 212);
            this.tambahjadwal.Name = "tambahjadwal";
            this.tambahjadwal.Size = new System.Drawing.Size(120, 20);
            this.tambahjadwal.TabIndex = 14;
            this.tambahjadwal.Text = "Tambah Jadwal";
            this.tambahjadwal.Click += new System.EventHandler(this.label7_Click);
            // 
            // btnCetak
            // 
            this.btnCetak.Location = new System.Drawing.Point(462, 244);
            this.btnCetak.Name = "btnCetak";
            this.btnCetak.Size = new System.Drawing.Size(121, 39);
            this.btnCetak.TabIndex = 13;
            this.btnCetak.Text = "Cetak";
            this.btnCetak.UseVisualStyleBackColor = true;
            this.btnCetak.Click += new System.EventHandler(this.button4_Click);
            // 
            // btnHapus
            // 
            this.btnHapus.Location = new System.Drawing.Point(320, 244);
            this.btnHapus.Name = "btnHapus";
            this.btnHapus.Size = new System.Drawing.Size(121, 39);
            this.btnHapus.TabIndex = 12;
            this.btnHapus.Text = "Hapus";
            this.btnHapus.UseVisualStyleBackColor = true;
            this.btnHapus.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnUbah
            // 
            this.btnUbah.Location = new System.Drawing.Point(188, 244);
            this.btnUbah.Name = "btnUbah";
            this.btnUbah.Size = new System.Drawing.Size(121, 39);
            this.btnUbah.TabIndex = 11;
            this.btnUbah.Text = "Ubah";
            this.btnUbah.UseVisualStyleBackColor = true;
            this.btnUbah.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnTambah
            // 
            this.btnTambah.Location = new System.Drawing.Point(47, 244);
            this.btnTambah.Name = "btnTambah";
            this.btnTambah.Size = new System.Drawing.Size(121, 39);
            this.btnTambah.TabIndex = 10;
            this.btnTambah.Text = "Tambah";
            this.btnTambah.UseVisualStyleBackColor = true;
            this.btnTambah.Click += new System.EventHandler(this.button1_Click);
            // 
            // matapelajaran
            // 
            this.matapelajaran.AutoSize = true;
            this.matapelajaran.Location = new System.Drawing.Point(248, 161);
            this.matapelajaran.Name = "matapelajaran";
            this.matapelajaran.Size = new System.Drawing.Size(115, 20);
            this.matapelajaran.TabIndex = 9;
            this.matapelajaran.Text = "Mata Pelajaran";
            // 
            // cmbMapel
            // 
            this.cmbMapel.FormattingEnabled = true;
            this.cmbMapel.Location = new System.Drawing.Point(373, 153);
            this.cmbMapel.Name = "cmbMapel";
            this.cmbMapel.Size = new System.Drawing.Size(121, 28);
            this.cmbMapel.TabIndex = 8;
            this.cmbMapel.SelectedIndexChanged += new System.EventHandler(this.comboBox3_SelectedIndexChanged);
            // 
            // dtpSelesai
            // 
            this.dtpSelesai.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpSelesai.Location = new System.Drawing.Point(373, 76);
            this.dtpSelesai.Name = "dtpSelesai";
            this.dtpSelesai.ShowUpDown = true;
            this.dtpSelesai.Size = new System.Drawing.Size(121, 26);
            this.dtpSelesai.TabIndex = 7;
            this.dtpSelesai.ValueChanged += new System.EventHandler(this.dateTimePicker2_ValueChanged);
            // 
            // jamselesai
            // 
            this.jamselesai.AutoSize = true;
            this.jamselesai.Location = new System.Drawing.Point(268, 81);
            this.jamselesai.Name = "jamselesai";
            this.jamselesai.Size = new System.Drawing.Size(95, 20);
            this.jamselesai.TabIndex = 6;
            this.jamselesai.Text = "Jam Selesai";
            this.jamselesai.Click += new System.EventHandler(this.label4_Click);
            // 
            // cmbKelas
            // 
            this.cmbKelas.FormattingEnabled = true;
            this.cmbKelas.Location = new System.Drawing.Point(120, 124);
            this.cmbKelas.Name = "cmbKelas";
            this.cmbKelas.Size = new System.Drawing.Size(121, 28);
            this.cmbKelas.TabIndex = 5;
            this.cmbKelas.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // pilihkelas
            // 
            this.pilihkelas.AutoSize = true;
            this.pilihkelas.Location = new System.Drawing.Point(11, 132);
            this.pilihkelas.Name = "pilihkelas";
            this.pilihkelas.Size = new System.Drawing.Size(80, 20);
            this.pilihkelas.TabIndex = 4;
            this.pilihkelas.Text = "Pilih Kelas";
            this.pilihkelas.Click += new System.EventHandler(this.label3_Click);
            // 
            // jammulai
            // 
            this.jammulai.AutoSize = true;
            this.jammulai.Location = new System.Drawing.Point(268, 42);
            this.jammulai.Name = "jammulai";
            this.jammulai.Size = new System.Drawing.Size(80, 20);
            this.jammulai.TabIndex = 3;
            this.jammulai.Text = "Jam Mulai";
            this.jammulai.Click += new System.EventHandler(this.label2_Click);
            // 
            // pilihhari
            // 
            this.pilihhari.AutoSize = true;
            this.pilihhari.Location = new System.Drawing.Point(11, 42);
            this.pilihhari.Name = "pilihhari";
            this.pilihhari.Size = new System.Drawing.Size(67, 20);
            this.pilihhari.TabIndex = 2;
            this.pilihhari.Text = "Pilih hari";
            this.pilihhari.Click += new System.EventHandler(this.label1_Click);
            // 
            // dtpMulai
            // 
            this.dtpMulai.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpMulai.Location = new System.Drawing.Point(373, 35);
            this.dtpMulai.Name = "dtpMulai";
            this.dtpMulai.ShowUpDown = true;
            this.dtpMulai.Size = new System.Drawing.Size(121, 26);
            this.dtpMulai.TabIndex = 1;
            // 
            // cmbHari
            // 
            this.cmbHari.FormattingEnabled = true;
            this.cmbHari.Items.AddRange(new object[] {
            "Senin",
            "Selasa",
            "Rabu",
            "Kamis",
            "Jumat",
            "Sabtu"});
            this.cmbHari.Location = new System.Drawing.Point(120, 33);
            this.cmbHari.Name = "cmbHari";
            this.cmbHari.Size = new System.Drawing.Size(121, 28);
            this.cmbHari.TabIndex = 0;
            // 
            // tabeljadwalsaatini
            // 
            this.tabeljadwalsaatini.AutoSize = true;
            this.tabeljadwalsaatini.Location = new System.Drawing.Point(152, 405);
            this.tabeljadwalsaatini.Name = "tabeljadwalsaatini";
            this.tabeljadwalsaatini.Size = new System.Drawing.Size(155, 20);
            this.tabeljadwalsaatini.TabIndex = 2;
            this.tabeljadwalsaatini.Text = "Tabel Jadwal saat ini";
            this.tabeljadwalsaatini.Click += new System.EventHandler(this.label5_Click);
            // 
            // btnLogoutA
            // 
            this.btnLogoutA.Location = new System.Drawing.Point(796, 83);
            this.btnLogoutA.Name = "btnLogoutA";
            this.btnLogoutA.Size = new System.Drawing.Size(103, 30);
            this.btnLogoutA.TabIndex = 3;
            this.btnLogoutA.Text = "LOG OUT";
            this.btnLogoutA.UseVisualStyleBackColor = true;
            this.btnLogoutA.Click += new System.EventHandler(this.btnLogoutA_Click);
            // 
            // bindingNavigator1
            // 
            this.bindingNavigator1.AddNewItem = this.bindingNavigatorAddNewItem;
            this.bindingNavigator1.CountItem = this.bindingNavigatorCountItem;
            this.bindingNavigator1.DeleteItem = this.bindingNavigatorDeleteItem;
            this.bindingNavigator1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.bindingNavigator1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.bindingNavigatorAddNewItem,
            this.bindingNavigatorDeleteItem});
            this.bindingNavigator1.Location = new System.Drawing.Point(0, 0);
            this.bindingNavigator1.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.bindingNavigator1.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.bindingNavigator1.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.bindingNavigator1.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.bindingNavigator1.Name = "bindingNavigator1";
            this.bindingNavigator1.PositionItem = this.bindingNavigatorPositionItem;
            this.bindingNavigator1.Size = new System.Drawing.Size(1421, 33);
            this.bindingNavigator1.TabIndex = 4;
            this.bindingNavigator1.Text = "bindingNavigator1";
            // 
            // bindingNavigatorAddNewItem
            // 
            this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
            this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(34, 28);
            this.bindingNavigatorAddNewItem.Text = "Add new";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(54, 28);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
            // 
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(34, 28);
            this.bindingNavigatorDeleteItem.Text = "Delete";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(34, 28);
            this.bindingNavigatorMoveFirstItem.Text = "Move first";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(34, 28);
            this.bindingNavigatorMovePreviousItem.Text = "Move previous";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 33);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Position";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 31);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Current position";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 33);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(34, 28);
            this.bindingNavigatorMoveNextItem.Text = "Move next";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(34, 28);
            this.bindingNavigatorMoveLastItem.Text = "Move last";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 33);
            // 
            // chartJadwal
            // 
            chartArea1.Name = "ChartArea1";
            this.chartJadwal.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartJadwal.Legends.Add(legend1);
            this.chartJadwal.Location = new System.Drawing.Point(796, 119);
            this.chartJadwal.Name = "chartJadwal";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartJadwal.Series.Add(series1);
            this.chartJadwal.Size = new System.Drawing.Size(443, 306);
            this.chartJadwal.TabIndex = 5;
            this.chartJadwal.Text = "chart1";
            // 
            // btnImportGuru
            // 
            this.btnImportGuru.BackColor = System.Drawing.Color.Honeydew;
            this.btnImportGuru.Location = new System.Drawing.Point(375, 386);
            this.btnImportGuru.Name = "btnImportGuru";
            this.btnImportGuru.Size = new System.Drawing.Size(298, 35);
            this.btnImportGuru.TabIndex = 6;
            this.btnImportGuru.Text = "Import Data Guru (Excel)";
            this.btnImportGuru.UseVisualStyleBackColor = false;
            this.btnImportGuru.Click += new System.EventHandler(this.btnImportGuru_Click);
            // 
            // btnHapusGuru
            // 
            this.btnHapusGuru.BackColor = System.Drawing.Color.RosyBrown;
            this.btnHapusGuru.Location = new System.Drawing.Point(679, 387);
            this.btnHapusGuru.Name = "btnHapusGuru";
            this.btnHapusGuru.Size = new System.Drawing.Size(111, 34);
            this.btnHapusGuru.TabIndex = 7;
            this.btnHapusGuru.Text = "Hapus Guru";
            this.btnHapusGuru.UseVisualStyleBackColor = false;
            this.btnHapusGuru.Click += new System.EventHandler(this.btnHapusGuru_Click);
            // 
            // AdminDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1421, 733);
            this.Controls.Add(this.btnHapusGuru);
            this.Controls.Add(this.btnImportGuru);
            this.Controls.Add(this.chartJadwal);
            this.Controls.Add(this.bindingNavigator1);
            this.Controls.Add(this.btnLogoutA);
            this.Controls.Add(this.tabeljadwalsaatini);
            this.Controls.Add(this.panelinput);
            this.Controls.Add(this.dgvJadwal);
            this.Name = "AdminDashboard";
            this.Text = "AdminDashboard";
            this.Load += new System.EventHandler(this.AdminDashboard_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvJadwal)).EndInit();
            this.panelinput.ResumeLayout(false);
            this.panelinput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).EndInit();
            this.bindingNavigator1.ResumeLayout(false);
            this.bindingNavigator1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartJadwal)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvJadwal;
        private System.Windows.Forms.GroupBox panelinput;
        private System.Windows.Forms.DateTimePicker dtpMulai;
        private System.Windows.Forms.ComboBox cmbHari;
        private System.Windows.Forms.Label jammulai;
        private System.Windows.Forms.Label pilihhari;
        private System.Windows.Forms.Label pilihkelas;
        private System.Windows.Forms.ComboBox cmbKelas;
        private System.Windows.Forms.Label jamselesai;
        private System.Windows.Forms.DateTimePicker dtpSelesai;
        private System.Windows.Forms.Label tabeljadwalsaatini;
        private System.Windows.Forms.Label matapelajaran;
        private System.Windows.Forms.ComboBox cmbMapel;
        private System.Windows.Forms.Button btnCetak;
        private System.Windows.Forms.Button btnHapus;
        private System.Windows.Forms.Button btnUbah;
        private System.Windows.Forms.Button btnTambah;
        private System.Windows.Forms.Label tambahjadwal;
        private System.Windows.Forms.Label cetakjadwal;
        private System.Windows.Forms.Label hapusjadwal;
        private System.Windows.Forms.Label ubahjadwal;
        private System.Windows.Forms.ComboBox cmbGuru;
        private System.Windows.Forms.Label guru;
        private System.Windows.Forms.Button btnLogoutA;
        private System.Windows.Forms.BindingNavigator bindingNavigator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorDeleteItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartJadwal;
        private System.Windows.Forms.Button btnImportGuru;
        private System.Windows.Forms.Button btnHapusGuru;
    }
}