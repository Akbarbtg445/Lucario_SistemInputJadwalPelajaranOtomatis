using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace UCPPABD
{
    public partial class FrmLaporanUser : Form
    {
        string connectionString =
            @"Data Source=192.168.100.94\AKBARRZHO;
              Initial Catalog=UCP_PABD_Jadwal;
              Integrated Security=False;
              User ID=sa;
              Password=123;
              TrustServerCertificate=True";

        string nisUser;

        public FrmLaporanUser(string nis)
        {
            InitializeComponent();
            nisUser = nis;
        }

        private void FrmLaporanUser_Load(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // MENGGUNAKAN VIEW LANGSUNG tanpa filter tabel Jadwal_Pribadi
                    // Ini akan menarik seluruh jadwal yang ada di database
                    string query = @"SELECT 
    hari AS Hari,
    jamMulai AS JamMulai,
    jamSelesai AS JamSelesai,
    idKelas AS IdKelas,
    MataPelajaran AS MataPelajaran,
    NamaGuru AS NamaGuru
FROM vw_JadwalPelajaran";


                    SqlCommand cmd = new SqlCommand(query, conn);

                    // cmd.Parameters("@nis") dihapus karena kita mencetak semua data tanpa memandang NIS siswa

                    SqlDataReader dr = cmd.ExecuteReader();

                    // MEMBUAT TABEL VIRTUAL MANUAL (Semua tipe dipaksa jadi String)
                    DataTable dt = new DataTable("UCPPABD_DataJadwal");
                    dt.Columns.Add("Hari", typeof(string));
                    dt.Columns.Add("JamMulai", typeof(string));
                    dt.Columns.Add("JamSelesai", typeof(string));
                    dt.Columns.Add("IdKelas", typeof(string));
                    dt.Columns.Add("MataPelajaran", typeof(string));
                    dt.Columns.Add("NamaGuru", typeof(string));

                    int jumlahData = 0;

                    // Mengisi tabel secara manual
                    while (dr.Read())
                    {
                        dt.Rows.Add(
                            dr["Hari"].ToString(),
                            dr["JamMulai"].ToString(),
                            dr["JamSelesai"].ToString(),
                            dr["IdKelas"].ToString(),
                            dr["MataPelajaran"].ToString(),
                            dr["NamaGuru"].ToString()
                        );
                        jumlahData++;
                    }
                    dr.Close();

                    // Pesan pop-up disesuaikan jika keseluruhan jadwal masih kosong
                    if (jumlahData == 0)
                    {
                        MessageBox.Show("Data jadwal kelas belum tersedia di database.", "Info Database", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    // SUNTIKKAN KE CRYSTAL REPORTS
                    ReportUser rpt = new ReportUser();
                    rpt.Database.Tables[0].SetDataSource(dt);

                    crystalReportViewer1.ReportSource = rpt;
                    crystalReportViewer1.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat laporan: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}