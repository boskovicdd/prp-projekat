using System;
using System.Drawing;
using System.Windows.Forms;
using Domen;

namespace Forme
{
    public partial class FrmSacuvajGrad : Form
    {
        private Label lblNaslov;
        private Label lblGradId;
        private Label lblNaziv;
        private Label lblDrzava;

        private TextBox txtGradId;
        private TextBox txtNaziv;
        private TextBox txtDrzava;

        private Button btnSacuvaj;
        private Button btnOtkazi;

        public Grad GradZaCuvanje { get; private set; }
        private bool izmena;

        public FrmSacuvajGrad()
        {
            InitializeComponent();
            izmena = false;
            InicijalizujIzgled();
        }

        public FrmSacuvajGrad(Grad grad)
        {
            InitializeComponent();
            izmena = true;
            InicijalizujIzgled();

            txtGradId.Text = grad.GradId.ToString();
            txtGradId.Enabled = false;
            txtNaziv.Text = grad.Naziv;
            txtDrzava.Text = grad.Drzava;
        }

        private void InicijalizujIzgled()
        {
            this.Text = izmena ? "Izmena grada" : "Dodavanje grada";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Size = new Size(500, 420);
            this.MinimumSize = new Size(500, 420);
            this.MaximumSize = new Size(500, 420);
            this.BackColor = Color.FromArgb(245, 247, 250);

            lblNaslov = new Label();
            lblNaslov.Text = izmena ? "Izmena grada" : "Dodavanje grada";
            lblNaslov.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblNaslov.ForeColor = Color.FromArgb(32, 42, 68);
            lblNaslov.AutoSize = true;
            lblNaslov.Location = new Point(30, 20);

            lblGradId = new Label();
            lblGradId.Text = "Grad ID:";
            lblGradId.Location = new Point(35, 80);
            lblGradId.AutoSize = true;
            lblGradId.Font = new Font("Segoe UI", 10);

            txtGradId = new TextBox();
            txtGradId.Location = new Point(35, 105);
            txtGradId.Size = new Size(400, 30);

            lblNaziv = new Label();
            lblNaziv.Text = "Naziv:";
            lblNaziv.Location = new Point(35, 145);
            lblNaziv.AutoSize = true;
            lblNaziv.Font = new Font("Segoe UI", 10);

            txtNaziv = new TextBox();
            txtNaziv.Location = new Point(35, 170);
            txtNaziv.Size = new Size(400, 30);

            lblDrzava = new Label();
            lblDrzava.Text = "Država:";
            lblDrzava.Location = new Point(35, 210);
            lblDrzava.AutoSize = true;
            lblDrzava.Font = new Font("Segoe UI", 10);

            txtDrzava = new TextBox();
            txtDrzava.Location = new Point(35, 235);
            txtDrzava.Size = new Size(400, 30);

            btnSacuvaj = new Button();
            btnSacuvaj.Text = "Sačuvaj";
            btnSacuvaj.Size = new Size(120, 40);
            btnSacuvaj.Location = new Point(175, 300);
            btnSacuvaj.BackColor = Color.FromArgb(32, 42, 68);
            btnSacuvaj.ForeColor = Color.White;
            btnSacuvaj.FlatStyle = FlatStyle.Flat;
            btnSacuvaj.FlatAppearance.BorderSize = 0;
            btnSacuvaj.Click += BtnSacuvaj_Click;

            btnOtkazi = new Button();
            btnOtkazi.Text = "Otkaži";
            btnOtkazi.Size = new Size(120, 40);
            btnOtkazi.Location = new Point(315, 300);
            btnOtkazi.BackColor = Color.Gray;
            btnOtkazi.ForeColor = Color.White;
            btnOtkazi.FlatStyle = FlatStyle.Flat;
            btnOtkazi.FlatAppearance.BorderSize = 0;
            btnOtkazi.Click += BtnOtkazi_Click;

            Controls.Add(lblNaslov);
            Controls.Add(lblGradId);
            Controls.Add(txtGradId);
            Controls.Add(lblNaziv);
            Controls.Add(txtNaziv);
            Controls.Add(lblDrzava);
            Controls.Add(txtDrzava);
            Controls.Add(btnSacuvaj);
            Controls.Add(btnOtkazi);
        }

        private void BtnSacuvaj_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtGradId.Text) ||
                string.IsNullOrWhiteSpace(txtNaziv.Text) ||
                string.IsNullOrWhiteSpace(txtDrzava.Text))
            {
                MessageBox.Show("Sva polja su obavezna.");
                return;
            }

            if (!int.TryParse(txtGradId.Text, out int gradId))
            {
                MessageBox.Show("Grad ID mora biti broj.");
                return;
            }

            GradZaCuvanje = new Grad
            {
                GradId = gradId,
                Naziv = txtNaziv.Text.Trim(),
                Drzava = txtDrzava.Text.Trim()
            };

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