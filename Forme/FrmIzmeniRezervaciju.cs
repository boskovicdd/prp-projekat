using System;
using System.Drawing;
using System.Windows.Forms;
using Kontroler;

namespace Forme
{
    public partial class FrmIzmeniRezervaciju : Form
    {
        private int brojRezervacije;

        private Label lblNaslov;
        private Label lblDatumOd;
        private Label lblBrojNoci;

        private DateTimePicker dtpDatumOd;
        private NumericUpDown numBrojNoci;

        private Button btnSacuvaj;
        private Button btnOtkazi;

        public FrmIzmeniRezervaciju(int brojRezervacije, DateTime datumOd, int brojNoci)
        {
            InitializeComponent();
            InicijalizujIzgled();

            this.brojRezervacije = brojRezervacije;

            dtpDatumOd.Value = datumOd;
            numBrojNoci.Value = brojNoci;
        }

        private void InicijalizujIzgled()
        {
            Text = "Izmena rezervacije";
            StartPosition = FormStartPosition.CenterParent;
            Size = new Size(500, 280);
            MinimumSize = new Size(500, 280);
            MaximumSize = new Size(500, 280);
            BackColor = Color.FromArgb(245, 247, 250);

            lblNaslov = new Label();
            lblNaslov.Text = "Izmena rezervacije";
            lblNaslov.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblNaslov.ForeColor = Color.FromArgb(32, 42, 68);
            lblNaslov.AutoSize = true;
            lblNaslov.Location = new Point(30, 20);

            lblDatumOd = new Label();
            lblDatumOd.Text = "Datum od:";
            lblDatumOd.Location = new Point(35, 80);
            lblDatumOd.AutoSize = true;

            dtpDatumOd = new DateTimePicker();
            dtpDatumOd.Location = new Point(160, 75);
            dtpDatumOd.Size = new Size(260, 30);

            lblBrojNoci = new Label();
            lblBrojNoci.Text = "Broj noći:";
            lblBrojNoci.Location = new Point(35, 130);
            lblBrojNoci.AutoSize = true;

            numBrojNoci = new NumericUpDown();
            numBrojNoci.Location = new Point(160, 125);
            numBrojNoci.Size = new Size(120, 30);
            numBrojNoci.Minimum = 1;
            numBrojNoci.Maximum = 365;

            btnSacuvaj = new Button();
            btnSacuvaj.Text = "Sačuvaj";
            btnSacuvaj.Size = new Size(120, 40);
            btnSacuvaj.Location = new Point(170, 185);
            btnSacuvaj.BackColor = Color.FromArgb(32, 42, 68);
            btnSacuvaj.ForeColor = Color.White;
            btnSacuvaj.FlatStyle = FlatStyle.Flat;
            btnSacuvaj.FlatAppearance.BorderSize = 0;
            btnSacuvaj.Click += BtnSacuvaj_Click;

            btnOtkazi = new Button();
            btnOtkazi.Text = "Otkaži";
            btnOtkazi.Size = new Size(120, 40);
            btnOtkazi.Location = new Point(300, 185);
            btnOtkazi.BackColor = Color.Gray;
            btnOtkazi.ForeColor = Color.White;
            btnOtkazi.FlatStyle = FlatStyle.Flat;
            btnOtkazi.FlatAppearance.BorderSize = 0;
            btnOtkazi.Click += BtnOtkazi_Click;

            Controls.Add(lblNaslov);
            Controls.Add(lblDatumOd);
            Controls.Add(dtpDatumOd);
            Controls.Add(lblBrojNoci);
            Controls.Add(numBrojNoci);
            Controls.Add(btnSacuvaj);
            Controls.Add(btnOtkazi);
        }

        private void BtnSacuvaj_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime datumOd = dtpDatumOd.Value.Date;
                int brojNoci = (int)numBrojNoci.Value;

                DateTime datumDo = datumOd.AddDays(brojNoci);

                Kontroler.Kontroler.Instance.IzmeniRezervaciju(
                    brojRezervacije,
                    datumOd,
                    datumDo
                );

                MessageBox.Show("Rezervacija uspešno izmenjena.");

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnOtkazi_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}