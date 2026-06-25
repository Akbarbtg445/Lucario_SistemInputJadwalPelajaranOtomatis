using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;
using ExcelDataReader;

namespace UCPPABD
{
    public partial class AdminDashboard : Form
    {
        string connectionString = DbHelper.ConnString;

        // Menambahkan dua variabel ini untuk Data Binding
        private BindingSource bindingSource = new BindingSource();
        private DataTable dtJadwal = new DataTable();

        public AdminDashboard()
        {
            InitializeComponent();
            ApplyModernTheme();
            tampilkanData();
            isiPilihanComboBox();
            tampilkanGrafik();
        }

        // --- 1. FUNGSI MENAMPILKAN DATA KE TABEL (READ) ---
        void tampilkanData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM vw_JadwalPelajaran", conn))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            dtJadwal.Clear(); // Bersihkan data lama
                            da.Fill(dtJadwal);

                            bindingSource.DataSource = dtJadwal;
                            dgvJadwal.DataSource = bindingSource;

                            // Hubungkan navigator dengan binding source
                            bindingNavigator1.BindingSource = bindingSource;
                        }
                    }
                }
                catch (Exception ex) { MessageBox.Show("Gagal memuat tabel: " + ex.Message); }
            }
        }


        // --- FUNGSI UNTUK MENAMPILKAN GRAFIK DASHBOARD ---
        void tampilkanGrafik()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    // Query untuk menghitung jumlah jadwal per hari
                    string query = "SELECT hari, COUNT(idJadwal) AS JumlahJadwal FROM Jadwal GROUP BY hari";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    // Bersihkan grafik sebelumnya agar tidak menumpuk saat di-refresh
                    chartJadwal.Series.Clear();
                    chartJadwal.Titles.Clear();

                    // Tambahkan judul grafik
                    chartJadwal.Titles.Add("Statistik Jumlah Jadwal Per Hari");

                    // Buat Series baru (tipe Column/Batang)
                    Series series = new Series("Jadwal");
                    series.ChartType = SeriesChartType.Column; // Bisa diubah ke Pie, Line, dll jika mau

                    // Opsional: Mempercantik warna batang
                    series.Palette = ChartColorPalette.Pastel;

                    chartJadwal.Series.Add(series);

                    // Looping data dari database untuk dimasukkan ke grafik
                    while (reader.Read())
                    {
                        string hari = reader["hari"].ToString();
                        int jumlah = Convert.ToInt32(reader["JumlahJadwal"]);

                        // Masukkan sumbu X (hari) dan sumbu Y (jumlah)
                        series.Points.AddXY(hari, jumlah);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memuat grafik: " + ex.Message);
                }
            }
        }

        // --- 2. FUNGSI ISI PILIHAN DROPDOWN 
        void isiPilihanComboBox()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Load Data Kelas
                    SqlCommand cmdKelas = new SqlCommand("SELECT idKelas FROM Kelas", conn);
                    SqlDataReader drKelas = cmdKelas.ExecuteReader();
                    cmbKelas.Items.Clear();
                    while (drKelas.Read()) { cmbKelas.Items.Add(drKelas["idKelas"].ToString()); }
                    drKelas.Close();

                    // Load Data Guru
                    SqlCommand cmdGuru = new SqlCommand("SELECT nama FROM Guru", conn);
                    SqlDataReader drGuru = cmdGuru.ExecuteReader();
                    cmbGuru.Items.Clear();
                    while (drGuru.Read()) { cmbGuru.Items.Add(drGuru["nama"].ToString()); }
                    drGuru.Close();

                    // Load Data Mata Pelajaran
                    SqlCommand cmdMapel = new SqlCommand("SELECT namaMapel FROM MataPelajaran", conn);
                    SqlDataReader drMapel = cmdMapel.ExecuteReader();
                    cmbMapel.Items.Clear();
                    while (drMapel.Read()) { cmbMapel.Items.Add(drMapel["namaMapel"].ToString()); }
                    drMapel.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memuat pilihan dropdown: " + ex.Message);
                }
            }
        }

        // --- 3. FUNGSI RESET INPUTAN ---
        void resetForm()
        {
            cmbHari.SelectedIndex = -1;
            cmbKelas.SelectedIndex = -1;
            cmbMapel.SelectedIndex = -1;
            cmbGuru.SelectedIndex = -1;
            dtpMulai.Value = DateTime.Now;
            dtpSelesai.Value = DateTime.Now;
        }

        // --- 4. TOMBOL TAMBAH (CREATE) ---
        private void button1_Click(object sender, EventArgs e)
        {
            if (cmbHari.Text == "" || cmbKelas.Text == "" || cmbMapel.Text == "" || cmbGuru.Text == "")
            {
                MessageBox.Show("Mohon lengkapi semua pilihan sebelum menambah data!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            TimeSpan waktuMulai = dtpMulai.Value.TimeOfDay;
            TimeSpan waktuSelesai = dtpSelesai.Value.TimeOfDay;

            if (waktuSelesai <= waktuMulai)
            {
                MessageBox.Show("Jam Selesai harus lebih besar dari Jam Mulai!", "Kesalahan Waktu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            TimeSpan jamBuka = new TimeSpan(7, 0, 0);
            TimeSpan jamTutup = new TimeSpan(15, 0, 0);
            if (waktuMulai < jamBuka || waktuSelesai > jamTutup)
            {
                MessageBox.Show("Jadwal harus berada pada jam kerja sekolah (07:00 - 15:00)!", "Di Luar Jam", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction(); // Mulai Transaksi
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_InsertJadwal", conn, trans); // Tambahkan parameter trans
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@hari", cmbHari.Text);
                    cmd.Parameters.AddWithValue("@mulai", dtpMulai.Value.TimeOfDay);
                    cmd.Parameters.AddWithValue("@selesai", dtpSelesai.Value.TimeOfDay);
                    cmd.Parameters.AddWithValue("@kelas", cmbKelas.Text);
                    cmd.Parameters.AddWithValue("@namaMapel", cmbMapel.Text);
                    cmd.Parameters.AddWithValue("@namaGuru", cmbGuru.Text);

                    cmd.ExecuteNonQuery();

                    trans.Commit(); // Jika sukses, commit permanen

                    MessageBox.Show("Jadwal Berhasil Ditambahkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tampilkanData();
                    tampilkanGrafik(); // Refresh grafik setelah menambah data
                }
                catch (Exception ex)
                {
                    trans.Rollback(); // Jika error, batalkan semua
                    MessageBox.Show("Error Simpan: " + ex.Message);
                }
            }
        }
        
        // --- 5. TOMBOL UBAH (UPDATE) ---
        private void button2_Click(object sender, EventArgs e)
        {
            if (dgvJadwal.CurrentRow == null) { MessageBox.Show("Pilih baris di tabel yang ingin diubah!"); return; }

            if (cmbHari.Text == "" || cmbKelas.Text == "" || cmbMapel.Text == "" || cmbGuru.Text == "")
            {
                MessageBox.Show("Data tidak boleh kosong saat melakukan update!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ... (Validasi waktu tetap sama, biarkan utuh seperti sebelumnya) ...
            TimeSpan waktuMulai = dtpMulai.Value.TimeOfDay;
            TimeSpan waktuSelesai = dtpSelesai.Value.TimeOfDay;
            if (waktuSelesai <= waktuMulai) { MessageBox.Show("Jam Selesai harus lebih besar dari Jam Mulai!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            TimeSpan jamBuka = new TimeSpan(7, 0, 0);
            TimeSpan jamTutup = new TimeSpan(15, 0, 0);
            if (waktuMulai < jamBuka || waktuSelesai > jamTutup) { MessageBox.Show("Jadwal di luar jam sekolah!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction(); // Mulai Transaksi
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_UpdateJadwal", conn, trans);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@idJadwal", dgvJadwal.CurrentRow.Cells["idJadwal"].Value.ToString());
                    cmd.Parameters.AddWithValue("@hari", cmbHari.Text);
                    cmd.Parameters.AddWithValue("@mulai", dtpMulai.Value.TimeOfDay);
                    cmd.Parameters.AddWithValue("@selesai", dtpSelesai.Value.TimeOfDay);
                    cmd.Parameters.AddWithValue("@kelas", cmbKelas.Text);
                    cmd.Parameters.AddWithValue("@namaMapel", cmbMapel.Text);
                    cmd.Parameters.AddWithValue("@namaGuru", cmbGuru.Text);

                    cmd.ExecuteNonQuery();

                    trans.Commit(); // Commit

                    MessageBox.Show("Jadwal Berhasil Di Update!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tampilkanData();
                    tampilkanGrafik(); // Refresh grafik setelah mengubah data
                }
                catch (Exception ex)
                {
                    trans.Rollback(); // Rollback
                    MessageBox.Show("Error Update: " + ex.Message);
                }
            }
        }


        // --- 6. TOMBOL HAPUS (DELETE) ---
        private void button3_Click(object sender, EventArgs e)
        {
            if (dgvJadwal.CurrentRow == null) { MessageBox.Show("Pilih baris yang ingin dihapus!"); return; }

            string id = dgvJadwal.CurrentRow.Cells["idJadwal"].Value.ToString();
            DialogResult dr = MessageBox.Show($"Hapus jadwal dengan ID {id}?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlTransaction trans = conn.BeginTransaction(); // Mulai Transaksi
                    try
                    {
                        SqlCommand cmd = new SqlCommand("sp_DeleteJadwal", conn, trans);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idJadwal", id);

                        cmd.ExecuteNonQuery();
                        trans.Commit(); // Commit

                        MessageBox.Show("Data Berhasil Terhapus!");
                        tampilkanData();
                        tampilkanGrafik(); // Refresh grafik setelah menghapus data
                        resetForm();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback(); // Rollback
                        MessageBox.Show("Error Hapus: " + ex.Message);
                    }
                }
            }
        }
        // --- 7. TOMBOL CETAK (EKSPOR KE CSV/EXCEL) ---
        private void button4_Click(object sender, EventArgs e)
        {
            List<DataJadwal> data = new List<DataJadwal>();

            foreach (DataGridViewRow row in dgvJadwal.Rows)
            {
                if (!row.IsNewRow)
                {
                    data.Add(new DataJadwal()
                    {
                        Hari = row.Cells["hari"].Value?.ToString(),
                        JamMulai = row.Cells["jamMulai"].Value?.ToString(),
                        JamSelesai = row.Cells["jamSelesai"].Value?.ToString(),
                        IdKelas = row.Cells["idKelas"].Value?.ToString(),
                        MataPelajaran = row.Cells["MataPelajaran"].Value?.ToString(),
                        NamaGuru = row.Cells["NamaGuru"].Value?.ToString()
                    });
                }
            }

            FrmLaporanJadwal frm = new FrmLaporanJadwal(data);
            frm.ShowDialog();
        }

        // --- 8. KLIK TABEL ---
        private void dgvJadwal_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvJadwal.CurrentRow != null && e.RowIndex >= 0)
            {
                cmbHari.Text = dgvJadwal.CurrentRow.Cells["hari"].Value.ToString();
                cmbKelas.Text = dgvJadwal.CurrentRow.Cells["idKelas"].Value.ToString();
                cmbMapel.Text = dgvJadwal.CurrentRow.Cells["MataPelajaran"].Value.ToString();
                cmbGuru.Text = dgvJadwal.CurrentRow.Cells["NamaGuru"].Value.ToString();

                // Sinkronisasi Jam ke DateTimePicker
                dtpMulai.Value = DateTime.Parse(dgvJadwal.CurrentRow.Cells["jamMulai"].Value.ToString());
                dtpSelesai.Value = DateTime.Parse(dgvJadwal.CurrentRow.Cells["jamSelesai"].Value.ToString());
            }
        }

        // --- 9. LOGOUT ---
        private void btnLogoutA_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Yakin ingin keluar?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Form1 login = new Form1();
                login.Show();
                this.Hide();
            }
        }

        // Event kosong untuk sinkronisasi designer
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void dateTimePicker2_ValueChanged(object sender, EventArgs e) { }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) { }
        private void label5_Click(object sender, EventArgs e) { }
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e) { }
        private void label7_Click(object sender, EventArgs e) { }
        private void label8_Click(object sender, EventArgs e) { }
        private void label11_Click(object sender, EventArgs e) { }

        private void AdminDashboard_Load(object sender, EventArgs e)
        {

        }

        // --- FITUR IMPORT EXCEL (UCP 3) ---
        private void btnImportGuru_Click(object sender, EventArgs e)
        {
            // Membuka dialog untuk memilih file Excel
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "Excel Workbook|*.xlsx;*.xls", ValidateNames = true })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Membaca file Excel yang dipilih
                        using (var stream = File.Open(ofd.FileName, FileMode.Open, FileAccess.Read))
                        {
                            using (var reader = ExcelReaderFactory.CreateReader(stream))
                            {
                                // Mengonversi isi Excel menjadi DataSet
                                var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                                {
                                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = true }
                                });

                                // Mengambil tabel pertama (Sheet 1) dari Excel
                                DataTable dt = result.Tables[0];
                                int berhasil = 0;

                                using (SqlConnection conn = new SqlConnection(connectionString))
                                {
                                    conn.Open();

                                    // Looping setiap baris di Excel dan masukkan ke Database
                                    foreach (DataRow row in dt.Rows)
                                    {
                                        // Asumsi: Kolom pertama di Excel (kolom A) berisi Nama Guru
                                        string namaGuru = row[0].ToString();

                                        if (!string.IsNullOrEmpty(namaGuru))
                                        {
                                            SqlCommand cmd = new SqlCommand("INSERT INTO Guru (nama) VALUES (@nama)", conn);
                                            cmd.Parameters.AddWithValue("@nama", namaGuru);
                                            cmd.ExecuteNonQuery();
                                            berhasil++;
                                        }
                                    }
                                }

                                MessageBox.Show($"Selesai! Berhasil mengimpor {berhasil} data guru baru.", "Import Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // Refresh pilihan ComboBox Guru agar data baru langsung muncul
                                isiPilihanComboBox();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Gagal melakukan Import Data. Pastikan file Excel sedang tidak dibuka di program lain.\n\nError: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // --- FITUR HAPUS MASTER DATA GURU ---
        private void btnHapusGuru_Click(object sender, EventArgs e)
        {
            // Ambil nama guru yang sedang dipilih di dropdown
            string namaGuru = cmbGuru.Text;

            // Validasi jika kosong
            if (string.IsNullOrEmpty(namaGuru))
            {
                MessageBox.Show("Silakan pilih nama guru di dropdown yang ingin dihapus terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tampilkan konfirmasi
            DialogResult dr = MessageBox.Show($"Apakah Anda yakin ingin menghapus Guru bernama '{namaGuru}' dari sistem secara permanen?", "Konfirmasi Hapus Guru", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        // Query khusus untuk menghapus data di tabel Guru, bukan Jadwal
                        SqlCommand cmd = new SqlCommand("DELETE FROM Guru WHERE nama = @nama", conn);
                        cmd.Parameters.AddWithValue("@nama", namaGuru);

                        int affectedRows = cmd.ExecuteNonQuery();

                        if (affectedRows > 0)
                        {
                            MessageBox.Show("Data Guru berhasil dihapus dari sistem!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Refresh daftar ComboBox agar nama yang terhapus langsung hilang
                            isiPilihanComboBox();
                            cmbGuru.Text = ""; // Kosongkan isian
                        }
                        else
                        {
                            MessageBox.Show("Data guru tidak ditemukan.", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (SqlException ex)
                    {
                        // Menangani error Foreign Key Constraints (jika guru sudah terlanjur punya jadwal)
                        if (ex.Number == 547)
                        {
                            MessageBox.Show("Guru ini tidak bisa dihapus karena sudah memiliki Jadwal Pelajaran di dalam sistem! Silakan hapus jadwalnya terlebih dahulu.", "Gagal Menghapus", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            MessageBox.Show("Terjadi kesalahan database: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void ApplyModernTheme()
        {
            // 1. Form Styling
            this.BackColor = Color.FromArgb(241, 245, 249); // slate-100
            this.Text = "Dashboard Administrator - Sistem Jadwal Pelajaran";
            this.Size = new Size(1150, 750);
            this.StartPosition = FormStartPosition.CenterScreen;

            // 2. Programmatic Header Panel
            Panel panelHeader = new Panel();
            panelHeader.BackColor = Color.FromArgb(79, 70, 229); // Indigo-600
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Height = 70;
            this.Controls.Add(panelHeader);

            Label lblTitle = new Label();
            lblTitle.Text = "ADMINISTRATOR DASHBOARD";
            lblTitle.Font = new Font("Segoe UI Semibold", 15F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(20, 12);
            panelHeader.Controls.Add(lblTitle);

            Label lblSub = new Label();
            lblSub.Text = "Kelola jadwal pelajaran, data guru, mata pelajaran, dan statistik kelas";
            lblSub.Font = new Font("Segoe UI", 9F);
            lblSub.ForeColor = Color.FromArgb(199, 210, 254);
            lblSub.AutoSize = true;
            lblSub.Location = new Point(22, 40);
            panelHeader.Controls.Add(lblSub);

            // Reposition Log Out button onto the header
            panelHeader.Controls.Add(btnLogoutA);
            btnLogoutA.Location = new Point(this.Width - 140, 18);
            btnLogoutA.Size = new Size(100, 34);
            btnLogoutA.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            // 3. Reposition other controls to fit under the new header
            panelinput.Location = new Point(30, 90);
            panelinput.Size = new Size(580, 275);
            panelinput.BackColor = Color.White;
            panelinput.Text = "Input Jadwal Pelajaran";
            panelinput.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            panelinput.ForeColor = Color.FromArgb(15, 23, 42);

            chartJadwal.Location = new Point(630, 90);
            chartJadwal.Size = new Size(470, 275);
            chartJadwal.BorderlineColor = Color.Transparent;
            chartJadwal.BackColor = Color.White;

            // Positioning elements inside panelinput
            pilihhari.Location = new Point(20, 35);
            cmbHari.Location = new Point(110, 32);
            cmbHari.Size = new Size(150, 25);

            pilihkelas.Location = new Point(20, 80);
            cmbKelas.Location = new Point(110, 77);
            cmbKelas.Size = new Size(150, 25);

            matapelajaran.Location = new Point(20, 125);
            cmbMapel.Location = new Point(110, 122);
            cmbMapel.Size = new Size(150, 25);

            jammulai.Location = new Point(285, 35);
            dtpMulai.Location = new Point(385, 32);
            dtpMulai.Size = new Size(170, 25);

            jamselesai.Location = new Point(285, 80);
            dtpSelesai.Location = new Point(385, 77);
            dtpSelesai.Size = new Size(170, 25);

            guru.Location = new Point(285, 125);
            cmbGuru.Location = new Point(385, 122);
            cmbGuru.Size = new Size(170, 25);

            // Positioning buttons inside panelinput
            btnTambah.Location = new Point(20, 190);
            btnTambah.Size = new Size(120, 36);

            btnUbah.Location = new Point(155, 190);
            btnUbah.Size = new Size(120, 36);

            btnHapus.Location = new Point(290, 190);
            btnHapus.Size = new Size(120, 36);

            btnCetak.Location = new Point(425, 190);
            btnCetak.Size = new Size(130, 36);

            // Guru master buttons outside panelinput
            btnImportGuru.Location = new Point(30, 380);
            btnImportGuru.Size = new Size(220, 32);

            btnHapusGuru.Location = new Point(265, 380);
            btnHapusGuru.Size = new Size(120, 32);

            tabeljadwalsaatini.Location = new Point(30, 420);
            tabeljadwalsaatini.Text = "Daftar Jadwal Aktif";
            tabeljadwalsaatini.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            tabeljadwalsaatini.ForeColor = Color.FromArgb(15, 23, 42);

            dgvJadwal.Location = new Point(30, 450);
            dgvJadwal.Size = new Size(1070, 190);
            dgvJadwal.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            bindingNavigator1.Location = new Point(30, 650);
            bindingNavigator1.Size = new Size(1070, 30);
            bindingNavigator1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            // 4. Style Labels inside panelinput
            jammulai.ForeColor = Color.FromArgb(71, 85, 105); jammulai.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            jamselesai.ForeColor = Color.FromArgb(71, 85, 105); jamselesai.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            pilihhari.ForeColor = Color.FromArgb(71, 85, 105); pilihhari.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            pilihkelas.ForeColor = Color.FromArgb(71, 85, 105); pilihkelas.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            matapelajaran.ForeColor = Color.FromArgb(71, 85, 105); matapelajaran.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            guru.ForeColor = Color.FromArgb(71, 85, 105); guru.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);

            // Hide old descriptive text labels that made UI cluttered
            tambahjadwal.Visible = false;
            ubahjadwal.Visible = false;
            hapusjadwal.Visible = false;
            cetakjadwal.Visible = false;

            // 5. Style Form Controls
            cmbHari.Font = new Font("Segoe UI", 9.5F);
            cmbKelas.Font = new Font("Segoe UI", 9.5F);
            cmbMapel.Font = new Font("Segoe UI", 9.5F);
            cmbGuru.Font = new Font("Segoe UI", 9.5F);
            dtpMulai.Font = new Font("Segoe UI", 9.5F);
            dtpSelesai.Font = new Font("Segoe UI", 9.5F);

            // 6. Buttons Styling
            StyleFlatButton(btnTambah, Color.FromArgb(79, 70, 229), Color.White); // Indigo
            StyleFlatButton(btnUbah, Color.FromArgb(245, 158, 11), Color.White); // Amber
            StyleFlatButton(btnHapus, Color.FromArgb(239, 68, 68), Color.White); // Red
            StyleFlatButton(btnCetak, Color.FromArgb(16, 185, 129), Color.White); // Emerald
            StyleFlatButton(btnImportGuru, Color.White, Color.FromArgb(79, 70, 229), true);
            StyleFlatButton(btnHapusGuru, Color.White, Color.FromArgb(239, 68, 68), true);
            StyleFlatButton(btnLogoutA, Color.FromArgb(239, 68, 68), Color.White);

            // 7. Grid styling
            dgvJadwal.BackgroundColor = Color.White;
            dgvJadwal.BorderStyle = BorderStyle.None;
            dgvJadwal.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvJadwal.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvJadwal.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(79, 70, 229);
            dgvJadwal.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvJadwal.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            dgvJadwal.EnableHeadersVisualStyles = false;
            dgvJadwal.RowHeadersVisible = false;
            dgvJadwal.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvJadwal.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            dgvJadwal.DefaultCellStyle.SelectionBackColor = Color.FromArgb(224, 231, 255);
            dgvJadwal.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 41, 59);
            dgvJadwal.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            dgvJadwal.GridColor = Color.FromArgb(226, 232, 240);
            dgvJadwal.RowTemplate.Height = 32;
            dgvJadwal.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // 8. Chart styling
            chartJadwal.Titles.Clear();
            var title = chartJadwal.Titles.Add("Statistik Jumlah Jadwal Per Hari");
            title.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold);
            title.ForeColor = Color.FromArgb(15, 23, 42);
            chartJadwal.ChartAreas[0].BackColor = Color.White;
            chartJadwal.ChartAreas[0].AxisX.LineColor = Color.FromArgb(203, 213, 225);
            chartJadwal.ChartAreas[0].AxisY.LineColor = Color.FromArgb(203, 213, 225);
            chartJadwal.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.FromArgb(241, 245, 249);
            chartJadwal.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.FromArgb(241, 245, 249);
            chartJadwal.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Segoe UI", 8.5F);
            chartJadwal.ChartAreas[0].AxisY.LabelStyle.Font = new Font("Segoe UI", 8.5F);
        }

        private void StyleFlatButton(Button btn, Color bg, Color fg, bool hasBorder = false)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.BackColor = bg;
            btn.ForeColor = fg;
            btn.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
            if (hasBorder)
            {
                btn.FlatAppearance.BorderColor = fg;
                btn.FlatAppearance.BorderSize = 1;
            }
            else
            {
                btn.FlatAppearance.BorderSize = 0;
            }
        }
    }
}