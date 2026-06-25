using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace UCPPABD
{
    public partial class FrmLaporanUser : Form
    {
        string connectionString = DbHelper.ConnString;

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

                    // MENGGUNAKAN FILTER JADWAL PRIBADI SISWA BERDASARKAN NIS
                    string query = @"SELECT 
    v.hari AS Hari,
    v.jamMulai AS JamMulai,
    v.jamSelesai AS JamSelesai,
    v.idKelas AS IdKelas,
    v.MataPelajaran AS MataPelajaran,
    v.NamaGuru AS NamaGuru
FROM vw_JadwalPelajaran v
INNER JOIN Jadwal_Pribadi jp ON v.idJadwal = jp.idJadwal
WHERE jp.NIS = @nis";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@nis", nisUser);

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