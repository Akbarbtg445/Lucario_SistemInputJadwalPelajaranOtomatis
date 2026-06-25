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
        string connectionString = DbHelper.ConnString;

        public UserDashboard()
        {
            InitializeComponent();
            ApplyModernTheme();
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
                    // mengubah menjadi Menggunakan VIEW 
                    SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM vw_JadwalPelajaran", conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvJadwal.DataSource = dt;
                }
                catch (Exception ex) { MessageBox.Show("Gagal memuat jadwal: " + ex.Message); }
            }
        }

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
                    cmd.Parameters.AddWithValue("@mulai", dtpMulai.Value.TimeOfDay);
                    cmd.Parameters.AddWithValue("@selesai", dtpSelesai.Value.TimeOfDay);
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
            try
            {
                FrmLaporanUser frm = new FrmLaporanUser(lblNIS.Text);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal membuka laporan : " + ex.Message);
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

        private void UserDashboard_Load(object sender, EventArgs e)
        {

        }

        private void ApplyModernTheme()
        {
            // 1. Form Styling
            this.BackColor = Color.FromArgb(241, 245, 249); // slate-100
            this.Text = "Dashboard Siswa - Sistem Jadwal Pelajaran";
            this.Size = new Size(1160, 710);
            this.StartPosition = FormStartPosition.CenterScreen;

            // 2. Top Header Panel (panel1)
            panel1.BackColor = Color.FromArgb(79, 70, 229); // Indigo-600
            panel1.Height = 70;
            
            lblNama.ForeColor = Color.White;
            lblNama.Font = new Font("Segoe UI Semibold", 13F, FontStyle.Bold);
            lblNama.Location = new Point(20, 12);
            
            lblNIS.ForeColor = Color.FromArgb(199, 210, 254);
            lblNIS.Font = new Font("Segoe UI", 9.5F);
            lblNIS.Location = new Point(22, 38);

            // Reposition Log Out button onto the header
            panel1.Controls.Add(btnLogout);
            btnLogout.Location = new Point(this.Width - 140, 18);
            btnLogout.Size = new Size(100, 34);
            btnLogout.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            // 3. Reposition other controls to fit under the new header
            groupBox1.Location = new Point(30, 90);
            groupBox1.Size = new Size(320, 120);
            groupBox1.BackColor = Color.White;
            groupBox1.Text = "Cari Jadwal Kelas";
            groupBox1.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            groupBox1.ForeColor = Color.FromArgb(15, 23, 42);

            label1.ForeColor = Color.FromArgb(71, 85, 105);
            label1.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            label1.Location = new Point(20, 42);
            cmbKelas.Font = new Font("Segoe UI", 9.5F);
            cmbKelas.Location = new Point(90, 39);
            cmbKelas.Size = new Size(200, 25);
            btnCari.Location = new Point(90, 75);
            btnCari.Size = new Size(200, 32);

            groupBox2.Location = new Point(370, 90);
            groupBox2.Size = new Size(760, 120);
            groupBox2.BackColor = Color.White;
            groupBox2.Text = "Pengaturan Waktu Pelajaran";
            groupBox2.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            groupBox2.ForeColor = Color.FromArgb(15, 23, 42);

            // Style labels inside groupBox2
            label3.ForeColor = Color.FromArgb(71, 85, 105); label3.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            label4.ForeColor = Color.FromArgb(71, 85, 105); label4.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            Jammulai.ForeColor = Color.FromArgb(71, 85, 105); Jammulai.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            label5.ForeColor = Color.FromArgb(71, 85, 105); label5.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);

            // Row 1
            Jammulai.Location = new Point(20, 35);
            Jammulai.Size = new Size(80, 20);
            dtpMulai.Location = new Point(110, 32);
            dtpMulai.Size = new Size(130, 25);
            
            label3.Location = new Point(265, 35);
            label3.Size = new Size(100, 20);
            cmbMapel.Location = new Point(375, 32);
            cmbMapel.Size = new Size(160, 25);

            // Row 2
            label5.Location = new Point(20, 75);
            label5.Size = new Size(80, 20);
            dtpSelesai.Location = new Point(110, 72);
            dtpSelesai.Size = new Size(130, 25);
            
            label4.Location = new Point(265, 75);
            label4.Size = new Size(100, 20);
            cmbPilihkelas.Location = new Point(375, 72);
            cmbPilihkelas.Size = new Size(160, 25);

            cmbMapel.Font = new Font("Segoe UI", 9.5F);
            cmbPilihkelas.Font = new Font("Segoe UI", 9.5F);
            dtpMulai.Font = new Font("Segoe UI", 9.5F);
            dtpSelesai.Font = new Font("Segoe UI", 9.5F);

            // Adjust button sizes and layout inside GroupBox2
            btnSimpan.Location = new Point(560, 32);
            btnSimpan.Size = new Size(170, 66);

            // 4. Reposition DataGridView
            dgvJadwal.Location = new Point(30, 230);
            dgvJadwal.Size = new Size(1100, 310);
            dgvJadwal.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            // 5. Reposition and style Capacity panel
            groupBox3.Location = new Point(30, 560);
            groupBox3.Size = new Size(510, 100);
            groupBox3.BackColor = Color.White;
            groupBox3.Text = "Kapasitas Kelas Terpilih";
            groupBox3.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            groupBox3.ForeColor = Color.FromArgb(15, 23, 42);

            lblMax.Font = new Font("Segoe UI", 9.5F); lblMax.ForeColor = Color.FromArgb(71, 85, 105);
            lblTerisi.Font = new Font("Segoe UI", 9.5F); lblTerisi.ForeColor = Color.FromArgb(71, 85, 105);
            lblSisa.Font = new Font("Segoe UI", 9.5F); lblSisa.ForeColor = Color.FromArgb(71, 85, 105);
            lblStatus.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);

            lblMax.Location = new Point(20, 30);
            lblTerisi.Location = new Point(20, 60);
            lblSisa.Location = new Point(270, 30);
            lblStatus.Location = new Point(270, 60);

            // 6. Reposition and style Print options
            // Move them from groupBox2 to Form controls to prevent clipping
            if (groupBox2.Controls.Contains(label2))
            {
                groupBox2.Controls.Remove(label2);
            }
            if (groupBox2.Controls.Contains(btnCetak))
            {
                groupBox2.Controls.Remove(btnCetak);
            }
            
            if (!this.Controls.Contains(label2))
            {
                this.Controls.Add(label2);
            }
            if (!this.Controls.Contains(btnCetak))
            {
                this.Controls.Add(btnCetak);
            }

            label2.Location = new Point(560, 568);
            label2.Text = "Cetak Jadwal Pelajaran Pribadi:";
            label2.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label2.ForeColor = Color.FromArgb(15, 23, 42);

            btnCetak.Location = new Point(563, 595);
            btnCetak.Size = new Size(160, 36);

            // 7. Buttons Styling
            StyleFlatButton(btnCari, Color.FromArgb(79, 70, 229), Color.White); // Indigo
            StyleFlatButton(btnSimpan, Color.FromArgb(16, 185, 129), Color.White); // Emerald/Green
            StyleFlatButton(btnCetak, Color.FromArgb(37, 99, 235), Color.White); // Blue
            StyleFlatButton(btnLogout, Color.FromArgb(239, 68, 68), Color.White); // Red

            // 8. Grid styling
            dgvJadwal.BackgroundColor = Color.White;
            dgvJadwal.BorderStyle = BorderStyle.None;
            dgvJadwal.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvJadwal.ColumnHeadersVisible = DataGridViewHeadersSuffixHack(); // force evaluate
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
        }

        private bool DataGridViewHeadersSuffixHack()
        {
            return true;
        }

        private void StyleFlatButton(Button btn, Color bg, Color fg)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.BackColor = bg;
            btn.ForeColor = fg;
            btn.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
            btn.FlatAppearance.BorderSize = 0;
        }
    }
}