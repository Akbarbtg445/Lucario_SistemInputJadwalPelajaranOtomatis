using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace UCPPABD
{
    public partial class FormDbConfig : Form
    {
        public FormDbConfig()
        {
            InitializeComponent();
        }

        private void FormDbConfig_Load(object sender, EventArgs e)
        {
            cmbAuthType.Items.Clear();
            cmbAuthType.Items.AddRange(new string[] { "Windows Authentication", "SQL Server Authentication" });
            
            // Parsing existing connection string to pre-fill the form
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(DbHelper.ConnString);
                txtServer.Text = builder.DataSource;
                txtDatabase.Text = builder.InitialCatalog;
                
                if (builder.IntegratedSecurity)
                {
                    cmbAuthType.SelectedIndex = 0;
                    txtUser.Text = "";
                    txtPass.Text = "";
                }
                else
                {
                    cmbAuthType.SelectedIndex = 1;
                    txtUser.Text = builder.UserID;
                    txtPass.Text = builder.Password;
                }
            }
            catch
            {
                // fallback defaults
                txtServer.Text = @".\SQLEXPRESS";
                txtDatabase.Text = "UCP_PABD_Jadwal";
                cmbAuthType.SelectedIndex = 0;
            }
        }

        private void cmbAuthType_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool isSqlAuth = cmbAuthType.SelectedIndex == 1;
            txtUser.Enabled = isSqlAuth;
            txtPass.Enabled = isSqlAuth;
            
            if (!isSqlAuth)
            {
                txtUser.Text = "";
                txtPass.Text = "";
            }
        }

        // Helper to construct connection string from fields
        private string BuildConnectionString()
        {
            string server = txtServer.Text.Trim();
            string db = txtDatabase.Text.Trim();

            if (string.IsNullOrEmpty(server) || string.IsNullOrEmpty(db))
            {
                return null;
            }

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = server;
            builder.InitialCatalog = db;

            if (cmbAuthType.SelectedIndex == 0)
            {
                builder.IntegratedSecurity = true;
            }
            else
            {
                builder.IntegratedSecurity = false;
                builder.UserID = txtUser.Text.Trim();
                builder.Password = txtPass.Text;
            }

            builder.TrustServerCertificate = true;
            // Tambahkan timeout yang cepat agar tes koneksi tidak hang
            builder.ConnectTimeout = 5; 

            return builder.ConnectionString;
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            string connStr = BuildConnectionString();
            if (string.IsNullOrEmpty(connStr))
            {
                MessageBox.Show("Mohon lengkapi alamat server dan database!", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnTest.Text = "Menghubungkan...";
            btnTest.Enabled = false;
            this.Cursor = Cursors.WaitCursor;

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    MessageBox.Show("Sambungan ke database berhasil!", "Sukses",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menyambung ke database!\n\nDetail: " + ex.Message, "Koneksi Gagal",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnTest.Text = "Tes Koneksi";
                btnTest.Enabled = true;
                this.Cursor = Cursors.Default;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string connStr = BuildConnectionString();
            if (string.IsNullOrEmpty(connStr))
            {
                MessageBox.Show("Mohon lengkapi alamat server dan database!", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Bersihkan properti Timeout tambahan untuk pemakaian reguler
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connStr);
            builder.ConnectTimeout = 15; // standard timeout

            DbHelper.ConnString = builder.ConnectionString;
            
            MessageBox.Show("Konfigurasi server berhasil disimpan!", "Sukses",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
