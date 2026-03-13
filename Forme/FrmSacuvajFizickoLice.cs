using System;
using System.Drawing;
using System.Windows.Forms;
using Domen;

namespace Forme
{
    public partial class FrmSacuvajFizickoLice : Form
    {
        private Label lblNaslov;
        private Label lblGostId;
        private Label lblIme;
        private Label lblPrezime;
        private Label lblEmail;
        private Label lblTelefon;
        private Label lblBrojDokumentacije;

        private TextBox txtGostId;
        private TextBox txtIme;
        private TextBox txtPrezime;
        private TextBox txtEmail;
        private TextBox txtTelefon;
        private TextBox txtBrojDokumentacije;

        private Button btnSacuvaj;
        private Button btnOtkazi;

        private bool izmena;
        public FizickoLice FizickoLiceZaCuvanje { get; private set; }

        public FrmSacuvajFizickoLice()
        {
            InitializeComponent();
            izmena = false;
            InicijalizujIzgled();
        }

        public FrmSacuvajFizickoLice(FizickoLice fizickoLice)
        {
            InitializeComponent();
            izmena = true;
            InicijalizujIzgled();

            txtGostId.Text = fizickoLice.GostId.ToString();
            txtGostId.Enabled = false;
            txtIme.Text = fizickoLice.Ime;
            txtPrezime.Text = fizickoLice.Prezime;
            txtEmail.Text = fizickoLice.Email;
            txtTelefon.Text = fizickoLice.Telefon;
            txtBrojDokumentacije.Text = fizickoLice.BrojDokumentacije;
        }

        private void InicijalizujIzgled()
        {
            Text = izmena ? "Izmena fizičkog lica" : "Dodavanje fizičkog lica";
            StartPosition = FormStartPosition.CenterParent;
            Size = new Size(560, 620);
            MinimumSize = new Size(560, 620);
            MaximumSize = new Size(560, 620);
            BackColor = Color.FromArgb(245, 247, 250);

            lblNaslov = new Label();
            lblNaslov.Text = izmena ? "Izmena fizičkog lica" : "Dodavanje fizičkog lica";
            lblNaslov.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblNaslov.ForeColor = Color.FromArgb(32, 42, 68);
            lblNaslov.AutoSize = true;
            lblNaslov.Location = new Point(30, 20);

            lblGostId = new Label();
            lblGostId.Text = "Gost ID:";
            lblGostId.Location = new Point(35, 80);
            lblGostId.AutoSize = true;
            lblGostId.Font = new Font("Segoe UI", 10);

            txtGostId = new TextBox();
            txtGostId.Location = new Point(35, 105);
            txtGostId.Size = new Size(470, 30);

            lblIme = new Label();
            lblIme.Text = "Ime:";
            lblIme.Location = new Point(35, 145);
            lblIme.AutoSize = true;
            lblIme.Font = new Font("Segoe UI", 10);

            txtIme = new TextBox();
            txtIme.Location = new Point(35, 170);
            txtIme.Size = new Size(470, 30);

            lblPrezime = new Label();
            lblPrezime.Text = "Prezime:";
            lblPrezime.Location = new Point(35, 210);
            lblPrezime.AutoSize = true;
            lblPrezime.Font = new Font("Segoe UI", 10);

            txtPrezime = new TextBox();
            txtPrezime.Location = new Point(35, 235);
            txtPrezime.Size = new Size(470, 30);

            lblEmail = new Label();
            lblEmail.Text = "Email:";
            lblEmail.Location = new Point(35, 275);
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Segoe UI", 10);

            txtEmail = new TextBox();
            txtEmail.Location = new Point(35, 300);
            txtEmail.Size = new Size(470, 30);

            lblTelefon = new Label();
            lblTelefon.Text = "Telefon:";
            lblTelefon.Location = new Point(35, 340);
            lblTelefon.AutoSize = true;
            lblTelefon.Font = new Font("Segoe UI", 10);

            txtTelefon = new TextBox();
            txtTelefon.Location = new Point(35, 365);
            txtTelefon.Size = new Size(470, 30);

            lblBrojDokumentacije = new Label();
            lblBrojDokumentacije.Text = "Broj dokumentacije:";
            lblBrojDokumentacije.Location = new Point(35, 405);
            lblBrojDokumentacije.AutoSize = true;
            lblBrojDokumentacije.Font = new Font("Segoe UI", 10);

            txtBrojDokumentacije = new TextBox();
            txtBrojDokumentacije.Location = new Point(35, 430);
            txtBrojDokumentacije.Size = new Size(470, 30);

            btnSacuvaj = new Button();
            btnSacuvaj.Text = "Sačuvaj";
            btnSacuvaj.Size = new Size(120, 40);
            btnSacuvaj.Location = new Point(255, 470);
            btnSacuvaj.BackColor = Color.FromArgb(32, 42, 68);
            btnSacuvaj.ForeColor = Color.White;
            btnSacuvaj.FlatStyle = FlatStyle.Flat;
            btnSacuvaj.FlatAppearance.BorderSize = 0;
            btnSacuvaj.Click += BtnSacuvaj_Click;

            btnOtkazi = new Button();
            btnOtkazi.Text = "Otkaži";
            btnOtkazi.Size = new Size(120, 40);
            btnOtkazi.Location = new Point(385, 470);
            btnOtkazi.BackColor = Color.Gray;
            btnOtkazi.ForeColor = Color.White;
            btnOtkazi.FlatStyle = FlatStyle.Flat;
            btnOtkazi.FlatAppearance.BorderSize = 0;
            btnOtkazi.Click += BtnOtkazi_Click;

            Controls.Add(lblNaslov);
            Controls.Add(lblGostId);
            Controls.Add(txtGostId);
            Controls.Add(lblIme);
            Controls.Add(txtIme);
            Controls.Add(lblPrezime);
            Controls.Add(txtPrezime);
            Controls.Add(lblEmail);
            Controls.Add(txtEmail);
            Controls.Add(lblTelefon);
            Controls.Add(txtTelefon);
            Controls.Add(lblBrojDokumentacije);
            Controls.Add(txtBrojDokumentacije);
            Controls.Add(btnSacuvaj);
            Controls.Add(btnOtkazi);
        }

        private void BtnSacuvaj_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtGostId.Text) ||
                string.IsNullOrWhiteSpace(txtIme.Text) ||
                string.IsNullOrWhiteSpace(txtPrezime.Text) ||
                string.IsNullOrWhiteSpace(txtBrojDokumentacije.Text))
            {
                MessageBox.Show("Gost ID, ime, prezime i broj dokumentacije su obavezni.");
                return;
            }

            if (!int.TryParse(txtGostId.Text, out int gostId))
            {
                MessageBox.Show("Gost ID mora biti broj.");
                return;
            }

            FizickoLiceZaCuvanje = new FizickoLice
            {
                GostId = gostId,
                Ime = txtIme.Text.Trim(),
                Prezime = txtPrezime.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Telefon = txtTelefon.Text.Trim(),
                BrojDokumentacije = txtBrojDokumentacije.Text.Trim()
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