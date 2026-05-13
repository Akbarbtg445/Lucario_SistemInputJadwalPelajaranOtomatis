using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UCPPABD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // 1. Ambil data dari inputan
            string user = txtUsername.Text;
            string pass = txtPassword.Text;
            string role = cmbRole.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(role))
            {
                MessageBox.Show("Silakan pilih Role terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Tentukan tabel target
            string tabel = (role == "Admin") ? "Admin" : "[User]";
            string connectionString = @"Data Source=AOZORA\AKBARRZHO;Initial Catalog=UCP_PABD_Jadwal;Integrated Security=True;TrustServerCertificate=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // 3. QUERY DINAMIS 
                    string query;
                    if (role == "Admin")
                    {
                        // Bahaya: Input langsung dimasukkan ke dalam teks kueri menggunakan tanda kutip satu (' ')
                        query = "SELECT username FROM " + tabel + " WHERE username='" + user + "' AND password='" + pass + "'";
                    }
                    else
                    {
                        // Bahaya: Input langsung dimasukkan ke dalam teks kueri
                        query = "SELECT nama, NIS FROM " + tabel + " WHERE username='" + user + "' AND password='" + pass + "'";
                    }

                    SqlCommand cmd = new SqlCommand(query, conn);

                    // HAPUS ATAU JADIKAN KOMENTAR BARIS PARAMETER DI BAWAH INI
                    // cmd.Parameters.AddWithValue("@user", user);
                    // cmd.Parameters.AddWithValue("@pass", pass);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read()) // Jika akun ditemukan
                    {
                        if (role == "Admin")
                        {
                            // --- LOGIKA LOGIN ADMIN ---
                            MessageBox.Show("Login Admin Berhasil!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            AdminDashboard adminForm = new AdminDashboard();
                            adminForm.Show();
                        }
                        else
                        {
                            // --- LOGIKA LOGIN USER (Siswa) ---
                            string namaLogin = reader["nama"].ToString();
                            string nisLogin = reader["NIS"].ToString();

                            MessageBox.Show($"Selamat Datang, {namaLogin}!", "Login Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            UserDashboard userForm = new UserDashboard();

                            // Kirim data ke label (Pastikan Modifiers label sudah PUBLIC)
                            userForm.lblNama.Text = namaLogin;
                            userForm.lblNIS.Text = nisLogin;

                            userForm.Show();
                        }

                        this.Hide(); // Sembunyikan form login
                    }
                    else
                    {
                        MessageBox.Show("Username atau Password salah!", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan: " + ex.Message);
                }
            }
        }

        // Event-event kosong di bawah tetap biarkan agar Designer tidak error
        private void label1_Click(object sender, EventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void cmbRole_SelectedIndexChanged(object sender, EventArgs e) { }
    }
}