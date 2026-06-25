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
        string connectionString = @"Data Source=192.168.100.94\AKBARRZHO;Initial Catalog=UCP_PABD_Jadwal;Integrated Security=False;User ID=sa;Password=123;TrustServerCertificate=True";

        // Menambahkan dua variabel ini untuk Data Binding
        private BindingSource bindingSource = new BindingSource();
        private DataTable dtJadwal = new DataTable();

        public AdminDashboard()
        {
            InitializeComponent();
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
                    cmd.Parameters.AddWithValue("@mulai", dtpMulai.Value.ToString("HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@selesai", dtpSelesai.Value.ToString("HH:mm:ss"));
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

            string id = dgvJadwal.CurrentRow.Cells["idJadwal"].Value?.ToString();
            if (string.IsNullOrEmpty(id))
            {
                MessageBox.Show("ID Jadwal tidak valid atau belum terpilih!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

                    cmd.Parameters.AddWithValue("@idJadwal", id);
                    cmd.Parameters.AddWithValue("@hari", cmbHari.Text);
                    cmd.Parameters.AddWithValue("@mulai", dtpMulai.Value.ToString("HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@selesai", dtpSelesai.Value.ToString("HH:mm:ss"));
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
        private void dgvJadwal_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvJadwal.CurrentRow != null && e.RowIndex >= 0)
            {
                cmbHari.Text = dgvJadwal.CurrentRow.Cells["hari"].Value?.ToString() ?? "";
                cmbKelas.Text = dgvJadwal.CurrentRow.Cells["idKelas"].Value?.ToString() ?? "";
                cmbMapel.Text = dgvJadwal.CurrentRow.Cells["MataPelajaran"].Value?.ToString() ?? "";
                cmbGuru.Text = dgvJadwal.CurrentRow.Cells["NamaGuru"].Value?.ToString() ?? "";

                // Sinkronisasi Jam ke DateTimePicker
                var jamMulaiVal = dgvJadwal.CurrentRow.Cells["jamMulai"].Value?.ToString();
                var jamSelesaiVal = dgvJadwal.CurrentRow.Cells["jamSelesai"].Value?.ToString();

                if (!string.IsNullOrEmpty(jamMulaiVal))
                    dtpMulai.Value = DateTime.Parse(jamMulaiVal);
                if (!string.IsNullOrEmpty(jamSelesaiVal))
                    dtpSelesai.Value = DateTime.Parse(jamSelesaiVal);
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
    }
}