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
    public partial class UserDashboard : Form
    {
        string connectionString = @"Data Source=AOZORA\AKBARRZHO;Initial Catalog=UCP_PABD_Jadwal;Integrated Security=True;TrustServerCertificate=True";

        public UserDashboard()
        {
            InitializeComponent();
            tampilkanData();
            isiPilihanKelas();
            isiPilihanMapel(); // PANGGIL DI SINI AGAR OTOMATIS TERISI

            // Setting Default Jam 07:00
            DateTime hariIni = DateTime.Now;
            dtpMulai.Value = new DateTime(hariIni.Year, hariIni.Month, hariIni.Day, 7, 0, 0);
            dtpSelesai.Value = new DateTime(hariIni.Year, hariIni.Month, hariIni.Day, 8, 0, 0);
        }

        // --- 1. FUNGSI ISI SEMUA DROPDOWN KELAS ---
        void isiPilihanKelas()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT idKelas FROM Kelas", conn);
                    SqlDataReader dr = cmd.ExecuteReader();

                    cmbKelas.Items.Clear();
                    cmbPilihkelas.Items.Clear();

                    while (dr.Read())
                    {
                        string dataKelas = dr["idKelas"].ToString();
                        cmbKelas.Items.Add(dataKelas);
                        cmbPilihkelas.Items.Add(dataKelas);
                    }
                    dr.Close();

                    if (cmbKelas.Items.Count == 0)
                    {
                        cmbKelas.Items.Add("KLS01");
                        cmbPilihkelas.Items.Add("KLS01");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memuat daftar kelas: " + ex.Message);
                }
            }
        }

        // --- FUNGSI ISI DROPDOWN MATA PELAJARAN ---
        void isiPilihanMapel()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT namaMapel FROM MataPelajaran", conn);
                    SqlDataReader dr = cmd.ExecuteReader();

                    cmbMapel.Items.Clear();
                    while (dr.Read())
                    {
                        cmbMapel.Items.Add(dr["namaMapel"].ToString());
                    }
                    dr.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memuat daftar mata pelajaran: " + ex.Message);
                }
            }
        }

        // --- 2. FUNGSI TAMPILKAN DATA JADWAL (PAKAI VIEW) ---
        void tampilkanData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    // Menggunakan VIEW (Poin 2)
                    SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM vw_JadwalPelajaran", conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvJadwal.DataSource = dt;
                }
                catch (Exception ex) { MessageBox.Show("Gagal memuat jadwal: " + ex.Message); }
            }
        }

        // --- 3. TOMBOL CARI ---
        // --- 3. TOMBOL CARI (PAKAI STORED PROCEDURE) ---
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbKelas.Text)) return;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    // Menggunakan SP Cari 
                    SqlCommand cmd = new SqlCommand("sp_SearchJadwal", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@kelas", cmbKelas.Text);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvJadwal.DataSource = dt;
                }
                catch (Exception ex) { MessageBox.Show("Error Cari: " + ex.Message); }
            }
        }

        // --- 4. KLIK TABEL (SYNC KE FORM EDIT & CEK KAPASITAS) ---
        private void dgvJadwal_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvJadwal.CurrentRow != null && e.RowIndex >= 0)
            {
                try
                {
                    string idJadwal = dgvJadwal.CurrentRow.Cells["idJadwal"].Value.ToString();
                    string idKelas = dgvJadwal.CurrentRow.Cells["idKelas"].Value.ToString();

                    // Langsung ambil nama mapel dari tabel yang sudah di-JOIN
                    cmbMapel.Text = dgvJadwal.CurrentRow.Cells["MataPelajaran"].Value.ToString();

                    cmbPilihkelas.Text = idKelas;
                    dtpMulai.Value = DateTime.Parse(dgvJadwal.CurrentRow.Cells["jamMulai"].Value.ToString());
                    dtpSelesai.Value = DateTime.Parse(dgvJadwal.CurrentRow.Cells["jamSelesai"].Value.ToString());

                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();

                        // Ambil Kapasitas Maksimal
                        SqlCommand cmdMax = new SqlCommand("SELECT kapasitas FROM Kelas WHERE idKelas = @kelas", conn);
                        cmdMax.Parameters.AddWithValue("@kelas", idKelas);
                        object res = cmdMax.ExecuteScalar();
                        int max = (res != null) ? Convert.ToInt32(res) : 0;

                        // Hitung Jumlah Siswa Terisi
                        SqlCommand cmdTerisi = new SqlCommand("SELECT COUNT(*) FROM Jadwal_Pribadi WHERE idJadwal = @id", conn);
                        cmdTerisi.Parameters.AddWithValue("@id", idJadwal);
                        int terisi = Convert.ToInt32(cmdTerisi.ExecuteScalar());

                        int sisa = max - terisi;

                        // --- PERBAIKAN TAMPILAN LABEL ---
                        lblMax.Text = "Kapasitas Maksimal : " + max.ToString();
                        lblTerisi.Text = "Jumlah Siswa Terisi : " + terisi.ToString();
                        lblSisa.Text = "Sisa Kuota : " + sisa.ToString();

                        if (sisa > 0)
                        {
                            lblStatus.Text = "Status : TERSEDIA";
                            lblStatus.ForeColor = Color.Green;
                            btnSimpan.Enabled = true;
                        }
                        else
                        {
                            lblStatus.Text = "Status : PENUH";
                            lblStatus.ForeColor = Color.Red;
                            btnSimpan.Enabled = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal pilih data: " + ex.Message);
                }
            }
        }

        // --- 5. FUNGSI UPDATE JADWAL ---
        // --- 5. FUNGSI UPDATE JADWAL (PAKAI STORED PROCEDURE) ---
        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (dgvJadwal.CurrentRow == null)
            {
                MessageBox.Show("Pilih jadwal dari tabel terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    string idJadwal = dgvJadwal.CurrentRow.Cells["idJadwal"].Value.ToString();

                    // Menggunakan SP Update 
                    SqlCommand cmd = new SqlCommand("sp_UpdateJadwalUser", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@idJadwal", idJadwal);
                    cmd.Parameters.AddWithValue("@mulai", dtpMulai.Value.ToString("HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@selesai", dtpSelesai.Value.ToString("HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@kelas", cmbPilihkelas.Text);
                    cmd.Parameters.AddWithValue("@namaMapel", cmbMapel.Text);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data Jadwal Berhasil Diupdate!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    tampilkanData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal Update: " + ex.Message);
                }
            }
        }

        // --- 6. CETAK JADWAL PRIBADI (TOMBOL CETAK) ---
        private void btnCetak_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"SELECT j.hari, j.jamMulai, j.jamSelesai, m.namaMapel, g.nama AS namaGuru, k.idKelas 
                                     FROM Jadwal_Pribadi jp 
                                     JOIN Jadwal j ON jp.idJadwal = j.idJadwal 
                                     JOIN MataPelajaran m ON j.idKeahlian = m.idMapel 
                                     JOIN Guru g ON j.idGuru = g.idGuru 
                                     WHERE jp.idUser = @nis";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@nis", lblNIS.Text);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.Filter = "CSV (*.csv)|*.csv";
                        sfd.FileName = $"Jadwal_Pribadi_{lblNama.Text}.csv";

                        if (sfd.ShowDialog() == DialogResult.OK)
                        {
                            List<string> lines = new List<string>();
                            lines.Add("Hari,Jam Mulai,Jam Selesai,Mata Pelajaran,Guru,Kelas");

                            foreach (DataRow row in dt.Rows)
                            {
                                lines.Add($"{row["hari"]},{row["jamMulai"]},{row["jamSelesai"]},{row["namaMapel"]},{row["namaGuru"]},{row["idKelas"]}");
                            }

                            System.IO.File.WriteAllLines(sfd.FileName, lines, Encoding.UTF8);
                            MessageBox.Show("Jadwal Pribadi Berhasil Dicetak!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Kamu belum memiliki jadwal pribadi untuk dicetak.", "Kosong", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex) { MessageBox.Show("Gagal mencetak: " + ex.Message); }
            }
        }

        // --- 7. LOGOUT ---
        private void btnLogout_Click(object sender, EventArgs e)
        {
            Form1 login = new Form1();
            login.Show();
            this.Hide();
        }

        // Event handler kosong agar tidak error di Designer
        private void label1_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void label7_Click(object sender, EventArgs e) { }
        private void label8_Click(object sender, EventArgs e) { }
        private void label11_Click(object sender, EventArgs e) { }
        private void label9_Click(object sender, EventArgs e) { }
        private void label12_Click(object sender, EventArgs e) { }
        private void groupBox2_Enter(object sender, EventArgs e) { }
        private void cmbPilihkelas_SelectedIndexChanged(object sender, EventArgs e) { }
        private void button3_Click(object sender, EventArgs e) { }
        private void cmbJadwalLama_SelectedIndexChanged(object sender, EventArgs e) { }
        private void txtMapel_TextChanged(object sender, EventArgs e) { }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void dtpMulai_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}