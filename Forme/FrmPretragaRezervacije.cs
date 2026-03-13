using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Domen;

namespace Forme
{
    public partial class FrmPretragaRezervacije : Form
    {
        private Label lblNaslov;
        private Label lblDatumOd;
        private Label lblBrojNoci;
        private Label lblGrad;
        private Label lblBrojGostiju;

        private DateTimePicker dtpDatumOd;
        private NumericUpDown nudBrojNoci;
        private ComboBox cmbGrad;
        private NumericUpDown nudBrojGostiju;

        private Button btnNastavi;
        private Button btnOtkazi;

        public DateTime DatumOd { get; private set; }
        public int BrojNoci { get; private set; }
        public Grad IzabraniGrad { get; private set; }
        public int BrojGostiju { get; private set; }

        public FrmPretragaRezervacije()
        {
            InitializeComponent();
            InicijalizujIzgled();
            UcitajGradove();
        }

        private void InicijalizujIzgled()
        {
            Text = "Nova rezervacija - kriterijumi";
            StartPosition = FormStartPosition.CenterParent;
            Size = new Size(600, 360);
            MinimumSize = new Size(600, 360);
            MaximumSize = new Size(600, 360);
            BackColor = Color.FromArgb(245, 247, 250);

            lblNaslov = new Label();
            lblNaslov.Text = "Unesite kriterijume rezervacije";
            lblNaslov.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblNaslov.ForeColor = Color.FromArgb(32, 42, 68);
            lblNaslov.AutoSize = true;
            lblNaslov.Location = new Point(30, 20);

            lblDatumOd = new Label();
            lblDatumOd.Text = "Datum od:";
            lblDatumOd.Location = new Point(35, 90);
            lblDatumOd.AutoSize = true;

            dtpDatumOd = new DateTimePicker();
            dtpDatumOd.Location = new Point(180, 85);
            dtpDatumOd.Size = new Size(320, 30);
            dtpDatumOd.Value = DateTime.Today.AddDays(1);

            lblBrojNoci = new Label();
            lblBrojNoci.Text = "Broj noći:";
            lblBrojNoci.Location = new Point(35, 135);
            lblBrojNoci.AutoSize = true;

            nudBrojNoci = new NumericUpDown();
            nudBrojNoci.Location = new Point(180, 130);
            nudBrojNoci.Size = new Size(120, 30);
            nudBrojNoci.Minimum = 1;
            nudBrojNoci.Maximum = 60;
            nudBrojNoci.Value = 1;

            lblGrad = new Label();
            lblGrad.Text = "Grad:";
            lblGrad.Location = new Point(35, 180);
            lblGrad.AutoSize = true;

            cmbGrad = new ComboBox();
            cmbGrad.Location = new Point(180, 175);
            cmbGrad.Size = new Size(320, 30);
            cmbGrad.DropDownStyle = ComboBoxStyle.DropDownList;

            lblBrojGostiju = new Label();
            lblBrojGostiju.Text = "Broj gostiju:";
            lblBrojGostiju.Location = new Point(35, 225);
            lblBrojGostiju.AutoSize = true;

            nudBrojGostiju = new NumericUpDown();
            nudBrojGostiju.Location = new Point(180, 220);
            nudBrojGostiju.Size = new Size(120, 30);
            nudBrojGostiju.Minimum = 1;
            nudBrojGostiju.Maximum = 20;
            nudBrojGostiju.Value = 1;

            btnNastavi = new Button();
            btnNastavi.Text = "Prikaži dostupno";
            btnNastavi.Size = new Size(150, 42);
            btnNastavi.Location = new Point(240, 275);
            btnNastavi.BackColor = Color.FromArgb(32, 42, 68);
            btnNastavi.ForeColor = Color.White;
            btnNastavi.FlatStyle = FlatStyle.Flat;
            btnNastavi.FlatAppearance.BorderSize = 0;
            btnNastavi.Click += BtnNastavi_Click;

            btnOtkazi = new Button();
            btnOtkazi.Text = "Otkaži";
            btnOtkazi.Size = new Size(110, 42);
            btnOtkazi.Location = new Point(400, 275);
            btnOtkazi.BackColor = Color.Gray;
            btnOtkazi.ForeColor = Color.White;
            btnOtkazi.FlatStyle = FlatStyle.Flat;
            btnOtkazi.FlatAppearance.BorderSize = 0;
            btnOtkazi.Click += BtnOtkazi_Click;

            Controls.Add(lblNaslov);
            Controls.Add(lblDatumOd);
            Controls.Add(dtpDatumOd);
            Controls.Add(lblBrojNoci);
            Controls.Add(nudBrojNoci);
            Controls.Add(lblGrad);
            Controls.Add(cmbGrad);
            Controls.Add(lblBrojGostiju);
            Controls.Add(nudBrojGostiju);
            Controls.Add(btnNastavi);
            Controls.Add(btnOtkazi);
        }

        private void UcitajGradove()
        {
            try
            {
                List<Grad> gradovi = Kontroler.Kontroler.Instance.DohvatiSveGradove();
                cmbGrad.DataSource = null;
                cmbGrad.DataSource = gradovi;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška pri učitavanju gradova: " + ex.Message);
            }
        }

        private void BtnNastavi_Click(object sender, EventArgs e)
        {
            if (cmbGrad.SelectedItem == null)
            {
                MessageBox.Show("Izaberite grad.");
                return;
            }

            if (dtpDatumOd.Value.Date <= DateTime.Today)
            {
                MessageBox.Show("Datum od mora biti posle današnjeg datuma.");
                return;
            }

            DatumOd = dtpDatumOd.Value.Date;
            BrojNoci = (int)nudBrojNoci.Value;
            IzabraniGrad = cmbGrad.SelectedItem as Grad;
            BrojGostiju = (int)nudBrojGostiju.Value;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnOtkazi_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}