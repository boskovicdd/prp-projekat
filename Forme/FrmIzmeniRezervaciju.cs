using System;
using System.Drawing;
using System.Windows.Forms;

namespace Forme
{
    public partial class FrmIzmeniRezervaciju : Form
    {
        private Label lblNaslov;
        private Label lblDatumOd;
        private Label lblDatumDo;

        private DateTimePicker dtpDatumOd;
        private DateTimePicker dtpDatumDo;

        private Button btnSacuvaj;
        private Button btnOtkazi;

        public DateTime NoviDatumOd { get; private set; }
        public DateTime NoviDatumDo { get; private set; }

        public FrmIzmeniRezervaciju(DateTime datumOd, DateTime datumDo)
        {
            InitializeComponent();
            InicijalizujIzgled();

            dtpDatumOd.Value = datumOd;
            dtpDatumDo.Value = datumDo;
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
            lblNaslov.Text = "Izmena datuma rezervacije";
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

            lblDatumDo = new Label();
            lblDatumDo.Text = "Datum do:";
            lblDatumDo.Location = new Point(35, 130);
            lblDatumDo.AutoSize = true;

            dtpDatumDo = new DateTimePicker();
            dtpDatumDo.Location = new Point(160, 125);
            dtpDatumDo.Size = new Size(260, 30);

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
            Controls.Add(lblDatumDo);
            Controls.Add(dtpDatumDo);
            Controls.Add(btnSacuvaj);
            Controls.Add(btnOtkazi);
        }

        private void BtnSacuvaj_Click(object sender, EventArgs e)
        {
            if (dtpDatumDo.Value.Date <= dtpDatumOd.Value.Date)
            {
                MessageBox.Show("Datum do mora biti veći od datuma od.");
                return;
            }

            NoviDatumOd = dtpDatumOd.Value.Date;
            NoviDatumDo = dtpDatumDo.Value.Date;

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