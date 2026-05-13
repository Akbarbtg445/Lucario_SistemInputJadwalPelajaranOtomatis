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

namespace UCPPABD
{
    public partial class AdminDashboard : Form
    {
        // String koneksi ke database SQL Server
        string connectionString = @"Data Source=AOZORA\AKBARRZHO;Initial Catalog=UCP_PABD_Jadwal;Integrated Security=True;TrustServerCertificate=True";

        public AdminDashboard()
        {
            InitializeComponent();
            tampilkanData();      // Load tabel jadwal saat dibuka
            isiPilihanComboBox(); // Load data Guru, Mapel, dan Kelas ke dropdown
        }

        // --- 1. FUNGSI MENAMPILKAN DATA KE TABEL (READ) ---
        void tampilkanData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Jadwal", conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvJadwal.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memuat tabel: " + ex.Message);
                }
            }
        }

        // --- 2. FUNGSI ISI PILIHAN DROPDOWN (OTOMATIS DARI DATABASE) ---
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
                try
                {
                    conn.Open();
                    string query = "INSERT INTO Jadwal (hari, jamMulai, jamSelesai, idKelas, idKeahlian, idGuru) " +
                                   "VALUES (@hari, @mulai, @selesai, @kelas, " +
                                   "(SELECT TOP 1 idMapel FROM MataPelajaran WHERE namaMapel = @mapel), " +
                                   "(SELECT TOP 1 idGuru FROM Guru WHERE nama = @guru))";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@hari", cmbHari.Text);
                    cmd.Parameters.AddWithValue("@mulai", dtpMulai.Value.ToString("HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@selesai", dtpSelesai.Value.ToString("HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@kelas", cmbKelas.Text);
                    cmd.Parameters.AddWithValue("@mapel", cmbMapel.Text);
                    cmd.Parameters.AddWithValue("@guru", cmbGuru.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Jadwal Berhasil Ditambahkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tampilkanData();
                    resetForm();
                }
                catch (Exception ex)
                {
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
                try
                {
                    conn.Open();
                    string id = dgvJadwal.CurrentRow.Cells["idJadwal"].Value.ToString();

                    string query = "UPDATE Jadwal SET hari=@hari, jamMulai=@mulai, jamSelesai=@selesai, idKelas=@kelas, " +
                                   "idKeahlian=(SELECT TOP 1 idMapel FROM MataPelajaran WHERE namaMapel=@mapel), " +
                                   "idGuru=(SELECT TOP 1 idGuru FROM Guru WHERE nama=@guru) " +
                                   "WHERE idJadwal=@id";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@hari", cmbHari.Text);
                    cmd.Parameters.AddWithValue("@mulai", dtpMulai.Value.ToString("HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@selesai", dtpSelesai.Value.ToString("HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@kelas", cmbKelas.Text);
                    cmd.Parameters.AddWithValue("@mapel", cmbMapel.Text);
                    cmd.Parameters.AddWithValue("@guru", cmbGuru.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data Berhasil Diperbarui!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tampilkanData();
                }
                catch (Exception ex)
                {
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
                    try
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("DELETE FROM Jadwal WHERE idJadwal = @id", conn);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Data Berhasil Terhapus!");
                        tampilkanData();
                        resetForm();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error Hapus: " + ex.Message);
                    }
                }
            }
        }

        // --- 7. TOMBOL CETAK (EKSPOR KE CSV/EXCEL) ---
        private void button4_Click(object sender, EventArgs e)
        {
            if (dgvJadwal.Rows.Count > 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "CSV (*.csv)|*.csv";
                sfd.FileName = "Laporan_Jadwal_Pelajaran.csv";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string header = "";
                        for (int i = 0; i < dgvJadwal.Columns.Count; i++)
                        {
                            header += dgvJadwal.Columns[i].HeaderText + ",";
                        }

                        List<string> lines = new List<string>();
                        lines.Add(header);

                        foreach (DataGridViewRow row in dgvJadwal.Rows)
                        {
                            if (!row.IsNewRow)
                            {
                                string line = "";
                                for (int i = 0; i < dgvJadwal.Columns.Count; i++)
                                {
                                    line += row.Cells[i].Value?.ToString() + ",";
                                }
                                lines.Add(line);
                            }
                        }

                        System.IO.File.WriteAllLines(sfd.FileName, lines, Encoding.UTF8);
                        MessageBox.Show("Jadwal Berhasil Dicetak ke CSV!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Gagal mencetak: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Tabel kosong, tidak ada data untuk dicetak!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // --- 8. KLIK TABEL ---
        private void dgvJadwal_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvJadwal.CurrentRow != null)
            {
                cmbHari.Text = dgvJadwal.CurrentRow.Cells["hari"].Value.ToString();
                cmbKelas.Text = dgvJadwal.CurrentRow.Cells["idKelas"].Value.ToString();
                cmbMapel.Text = dgvJadwal.CurrentRow.Cells["idKeahlian"].Value.ToString();
                cmbGuru.Text = dgvJadwal.CurrentRow.Cells["idGuru"].Value.ToString();
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
    }
}