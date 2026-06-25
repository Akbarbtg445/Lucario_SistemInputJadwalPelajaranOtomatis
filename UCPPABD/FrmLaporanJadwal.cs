using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;


namespace UCPPABD
{
    public partial class FrmLaporanJadwal : Form
    {
        public FrmLaporanJadwal(List<DataJadwal> data)
        {
            InitializeComponent();

            ReportAdmin rpt = new ReportAdmin();

            rpt.SetDataSource(data);

            crystalReportViewer1.ReportSource = rpt;
            crystalReportViewer1.Refresh();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }

    }
}
        

    
