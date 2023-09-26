using DLWMS.WinForms.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLWMS.WinForms.IB180156
{
    public partial class pretragaGodinaStudija : Form
    {
        KonekcijaNaBazu db = DLWMSdb.Baza;
        List<StudentiPredmeti> _studenti;
        

        int godina;
        string filterNaziv;
        int ocjena;
        public pretragaGodinaStudija()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            UcitajSpolove();
        }
        private void UcitajSpolove()
        {
            comboBox3.DataSource = db.Spolovi.ToList();
            comboBox3.DisplayMember = "Naziv";
            comboBox3.ValueMember = "Id";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UcitajPodatke();
        }

        private void UcitajPodatke()
        {
            filterNaziv = string.IsNullOrEmpty(textBox1.Text) ? "" : textBox1.Text;

            godina = string.IsNullOrEmpty(comboBox1.Text) ? 0 : int.Parse(comboBox1.SelectedItem.ToString());

            ocjena = string.IsNullOrEmpty(comboBox2.Text) ? 5 : int.Parse(comboBox2.SelectedItem.ToString());

            var odabraniSpol = comboBox3.SelectedItem as Spolovi;

            var datum = dateTimePicker1.Value;

            int aktivnost = string.IsNullOrEmpty(comboBox4.Text) ? 5 : int.Parse(comboBox4.SelectedItem.ToString());

            bool AKTIVNOST;

            if (aktivnost == 0)
                AKTIVNOST = false;
            else
                AKTIVNOST = true;
          

            _studenti = db.StudentiPredmeti.Where(s => s.Student.GodinaStudija == godina ||
            s.Student.Ime == filterNaziv || s.Student.Prezime == filterNaziv
            || s.Ocjena == ocjena || s.Student.Spol.Id == odabraniSpol.Id || s.DatumPolaganja == datum
            || s.Student.Aktivan == AKTIVNOST
            ).ToList();

            if (_studenti != null)
            {
                DataTable tblStudenti = new DataTable();
                tblStudenti.Columns.Add("ImePrezime");
                tblStudenti.Columns.Add("GodinaStudija");
                tblStudenti.Columns.Add("NazivPredmeta");
                // tblStudenti.Columns.Add("BrojPolozenih");
                // tblStudenti.Columns.Add("ProsjecnaOcjena");
                tblStudenti.Columns.Add("Ocjenas");
                tblStudenti.Columns.Add("Spol");
                tblStudenti.Columns.Add("Datum");
                tblStudenti.Columns.Add("Aktivnost");

                for (int i = 0; i < _studenti.Count(); i++)
                {
                    var student = _studenti[i];
                    List<StudentiPredmeti> studentiPredmeti = db.StudentiPredmeti.Where(x => x.Student.Id == student.Id).ToList();

                    DataRow red = tblStudenti.NewRow();
                    red["ImePrezime"] = student.Student;
                    red["GodinaStudija"] = student.Student.GodinaStudija;
                    red["NazivPredmeta"] = student.Predmet;
                    // red["BrojPolozenih"] = studentiPredmeti.Count();
                    //red["ProsjecnaOcjena"] = studentiPredmeti.Count()==0 ? 5 : studentiPredmeti.Average(o=>o.Ocjena);
                    red["Ocjenas"] = student.Ocjena;
                    red["Spol"] = student.Student.Spol;
                    red["Datum"] = student.DatumPolaganja;
                    red["Aktivnost"] = student.Student.Aktivan;



                    tblStudenti.Rows.Add(red);
                }

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = tblStudenti;


            }
            else
            {
                dataGridView1.DataSource = null;
            }


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            UcitajPodatke();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            UcitajPodatke();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            UcitajPodatke();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            UcitajPodatke();
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            UcitajPodatke();
        }

       

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex==7)
            {
                int index = dataGridView1.SelectedCells[0].RowIndex;
                var student = _studenti[index];
                frmNovi forma = new frmNovi(student);
                forma.ShowDialog();
            }
        }
    }
}
