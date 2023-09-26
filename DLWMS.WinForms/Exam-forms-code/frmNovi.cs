using DLWMS.WinForms.DB;
using DLWMS.WinForms.Reports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLWMS.WinForms.IB180156
{
    public partial class frmNovi : Form
    {
        KonekcijaNaBazu db = DLWMSdb.Baza;
        List<StudentiKonsultacije> _konsultacije;
        StudentiPredmeti _student;

       
        public frmNovi(StudentiPredmeti student)
        {
            InitializeComponent();
            _student = student;
            
        }

        private void frmNovi_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
            UcitajKonsultacije();
           

        }

        private void UcitajKonsultacije()
        {
            _konsultacije = db.StudentiKonsultacije.Where(s => s.Student.Indeks == _student.Student.Indeks).ToList();
            dataGridView1.DataSource = _konsultacije;
        }

        private void btnPrintaj_Click(object sender, EventArgs e)
        {
            frmIzvjestaj forma = new frmIzvjestaj(_konsultacije);
            forma.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex==3)
            {
                int index = dataGridView1.SelectedCells[0].RowIndex;
                var student = _konsultacije[index];


                var upozorenje = MessageBox.Show("jeste li sgiurni", "upozorenje", MessageBoxButtons.YesNo);
                if(upozorenje== DialogResult.Yes)
                db.StudentiKonsultacije.Remove(student);

                db.SaveChanges();
                UcitajKonsultacije();
            }
        }
    }
 }

