using System;
using System.Drawing;
using System.Windows.Forms;
using Domen;

namespace Forme
{
    public partial class FrmSacuvajPravnoLice : Form
    {
        private Label lblNaslov;
        private Label lblGostId;
        private Label lblNazivFirme;
        private Label lblPIB;
        private Label lblMB;
        private Label lblEmail;
        private Label lblTelefon;

        private TextBox txtGostId;
        private TextBox txtNazivFirme;
        private TextBox txtPIB;
        private TextBox txtMB;
        private TextBox txtEmail;
        private TextBox txtTelefon;

        private Button btnSacuvaj;
        private Button btnOtkazi;

        private bool izmena;
        public PravnoLice PravnoLiceZaCuvanje { get; private set; }

        public FrmSacuvajPravnoLice()
        {
            InitializeComponent();
            izmena = false;
            InicijalizujIzgled();
        }

        public FrmSacuvajPravnoLice(PravnoLice pravnoLice)
        {
            InitializeComponent();
            izmena = true;
            InicijalizujIzgled();

            txtGostId.Text = pravnoLice.GostId.ToString();
            txtGostId.Enabled = false;
            txtNazivFirme.Text = pravnoLice.NazivFirme;
            txtPIB.Text = pravnoLice.PIB;
            txtMB.Text = pravnoLice.MB;
            txtEmail.Text = pravnoLice.Email;
            txtTelefon.Text = pravnoLice.Telefon;
        }

        private void InicijalizujIzgled()
        {
            Text = izmena ? "Izmena pravnog lica" : "Dodavanje pravnog lica";
            StartPosition = FormStartPosition.CenterParent;
            Size = new Size(560, 620);
            MinimumSize = new Size(560, 620);
            MaximumSize = new Size(560, 620);
            BackColor = Color.FromArgb(245, 247, 250);

            lblNaslov = new Label();
            lblNaslov.Text = izmena ? "Izmena pravnog lica" : "Dodavanje pravnog lica";
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

            lblNazivFirme = new Label();
            lblNazivFirme.Text = "Naziv firme:";
            lblNazivFirme.Location = new Point(35, 145);
            lblNazivFirme.AutoSize = true;
            lblNazivFirme.Font = new Font("Segoe UI", 10);

            txtNazivFirme = new TextBox();
            txtNazivFirme.Location = new Point(35, 170);
            txtNazivFirme.Size = new Size(470, 30);

            lblPIB = new Label();
            lblPIB.Text = "PIB:";
            lblPIB.Location = new Point(35, 210);
            lblPIB.AutoSize = true;
            lblPIB.Font = new Font("Segoe UI", 10);

            txtPIB = new TextBox();
            txtPIB.Location = new Point(35, 235);
            txtPIB.Size = new Size(470, 30);

            lblMB = new Label();
            lblMB.Text = "Matični broj:";
            lblMB.Location = new Point(35, 275);
            lblMB.AutoSize = true;
            lblMB.Font = new Font("Segoe UI", 10);

            txtMB = new TextBox();
            txtMB.Location = new Point(35, 300);
            txtMB.Size = new Size(470, 30);

            lblEmail = new Label();
            lblEmail.Text = "Email:";
            lblEmail.Location = new Point(35, 340);
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Segoe UI", 10);

            txtEmail = new TextBox();
            txtEmail.Location = new Point(35, 365);
            txtEmail.Size = new Size(470, 30);

            lblTelefon = new Label();
            lblTelefon.Text = "Telefon:";
            lblTelefon.Location = new Point(35, 405);
            lblTelefon.AutoSize = true;
            lblTelefon.Font = new Font("Segoe UI", 10);

            txtTelefon = new TextBox();
            txtTelefon.Location = new Point(35, 430);
            txtTelefon.Size = new Size(470, 30);

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
            Controls.Add(lblNazivFirme);
            Controls.Add(txtNazivFirme);
            Controls.Add(lblPIB);
            Controls.Add(txtPIB);
            Controls.Add(lblMB);
            Controls.Add(txtMB);
            Controls.Add(lblEmail);
            Controls.Add(txtEmail);
            Controls.Add(lblTelefon);
            Controls.Add(txtTelefon);
            Controls.Add(btnSacuvaj);
            Controls.Add(btnOtkazi);
        }

        private void BtnSacuvaj_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtGostId.Text) ||
                string.IsNullOrWhiteSpace(txtNazivFirme.Text) ||
                string.IsNullOrWhiteSpace(txtPIB.Text) ||
                string.IsNullOrWhiteSpace(txtMB.Text))
            {
                MessageBox.Show("Gost ID, naziv firme, PIB i matični broj su obavezni.");
                return;
            }

            if (!int.TryParse(txtGostId.Text, out int gostId))
            {
                MessageBox.Show("Gost ID mora biti broj.");
                return;
            }

            PravnoLiceZaCuvanje = new PravnoLice
            {
                GostId = gostId,
                NazivFirme = txtNazivFirme.Text.Trim(),
                PIB = txtPIB.Text.Trim(),
                MB = txtMB.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Telefon = txtTelefon.Text.Trim()
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