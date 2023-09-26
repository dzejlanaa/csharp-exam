using DLWMS.WinForms.IB180156;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLWMS.WinForms.Reports
{
    public partial class frmIzvjestaj : Form
    {
        List<StudentiKonsultacije> _konsultacije;
        public frmIzvjestaj(List<StudentiKonsultacije> konsultacije)
        {
            InitializeComponent();
            _konsultacije = konsultacije;
        }

        private void frmIzvjestaj_Load(object sender, EventArgs e)
        {
            if (_konsultacije != null)
            {
                ReportParameterCollection rpc = new ReportParameterCollection();
                //ReportParameter rp1 = new ReportParameter("student", _student.ToString());
                ReportParameter rp2 = new ReportParameter("brojkonsultacija", _konsultacije.Count().ToString());
                //rpc.Add(rp1);
                rpc.Add(rp2);
                var tblKonsultacije = new dsDLWMS.PolozeniPredmetiDataTable();
                for (int i = 0; i < _konsultacije.Count(); i++)
                {
                    var red = tblKonsultacije.NewPolozeniPredmetiRow();
                    red.RB = (i + 1).ToString();
                    red.Predmet = _konsultacije[i].Predmet.ToString();
                    red.Vrijeme = _konsultacije[i].VrijemeOdrzavanja.ToString();
                    red.Napomena = _konsultacije[i].Napomena.Length.ToString();
                    tblKonsultacije.AddPolozeniPredmetiRow(red);
                }
                ReportDataSource rds = new ReportDataSource();
                rds.Name = "dsPolozeni";
                rds.Value = tblKonsultacije;
                reportViewer1.LocalReport.DataSources.Add(rds);
                reportViewer1.LocalReport.SetParameters(rpc);
                this.reportViewer1.RefreshReport();
            }
        }
    }
    
}





