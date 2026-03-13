using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Domen;

namespace Forme
{
    public partial class FrmSacuvajHotel : Form
    {
        private Label lblNaslov;
        private Label lblHotelId;
        private Label lblNaziv;
        private Label lblGrad;
        private Label lblBrojZvezdica;
        private Label lblAdresa;

        private TextBox txtHotelId;
        private TextBox txtNaziv;
        private ComboBox cmbGrad;
        private NumericUpDown nudBrojZvezdica;
        private TextBox txtAdresa;

        private Label lblSobeNaslov;
        private Label lblBrojSobe;
        private Label lblCenaPoNoci;
        private Label lblBrojKreveta;

        private NumericUpDown nudBrojSobe;
        private NumericUpDown nudCenaPoNoci;
        private NumericUpDown nudBrojKreveta;

        private Button btnDodajSobu;
        private Button btnObrisiSobu;
        private Button btnSacuvaj;
        private Button btnOtkazi;

        private DataGridView dgvSobe;

        private List<Soba> sobe = new List<Soba>();
        private bool izmena;

        public Hotel HotelZaCuvanje { get; private set; }
        public List<Soba> SobeZaCuvanje { get; private set; }

        public FrmSacuvajHotel()
        {
            InitializeComponent();
            izmena = false;
            InicijalizujIzgled();
            UcitajGradove();
        }

        public FrmSacuvajHotel(Hotel hotel, List<Soba> postojeceSobe)
        {
            InitializeComponent();
            izmena = true;
            InicijalizujIzgled();
            UcitajGradove();

            txtHotelId.Text = hotel.HotelId.ToString();
            txtHotelId.Enabled = false;
            txtNaziv.Text = hotel.Naziv;
            nudBrojZvezdica.Value = hotel.BrojZvezdica;
            txtAdresa.Text = hotel.Adresa;

            if (hotel.Grad != null)
            {
                for (int i = 0; i < cmbGrad.Items.Count; i++)
                {
                    Grad g = cmbGrad.Items[i] as Grad;
                    if (g != null && g.GradId == hotel.Grad.GradId)
                    {
                        cmbGrad.SelectedIndex = i;
                        break;
                    }
                }
            }

            sobe = postojeceSobe
                .Select(s => new Soba
                {
                    Hotel = s.Hotel,
                    BrojSobe = s.BrojSobe,
                    CenaPoNoci = s.CenaPoNoci,
                    BrojKreveta = s.BrojKreveta
                })
                .ToList();

            OsveziTabeluSoba();
        }

        private void InicijalizujIzgled()
        {
            this.Text = izmena ? "Izmena hotela" : "Dodavanje hotela";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Size = new Size(950, 700);
            this.MinimumSize = new Size(950, 700);
            this.BackColor = Color.FromArgb(245, 247, 250);

            lblNaslov = new Label();
            lblNaslov.Text = izmena ? "Izmena hotela" : "Dodavanje hotela";
            lblNaslov.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblNaslov.ForeColor = Color.FromArgb(32, 42, 68);
            lblNaslov.AutoSize = true;
            lblNaslov.Location = new Point(30, 20);

            lblHotelId = new Label();
            lblHotelId.Text = "Hotel ID:";
            lblHotelId.Font = new Font("Segoe UI", 10);
            lblHotelId.AutoSize = true;
            lblHotelId.Location = new Point(35, 80);

            txtHotelId = new TextBox();
            txtHotelId.Location = new Point(35, 105);
            txtHotelId.Size = new Size(180, 30);

            lblNaziv = new Label();
            lblNaziv.Text = "Naziv:";
            lblNaziv.Font = new Font("Segoe UI", 10);
            lblNaziv.AutoSize = true;
            lblNaziv.Location = new Point(240, 80);

            txtNaziv = new TextBox();
            txtNaziv.Location = new Point(240, 105);
            txtNaziv.Size = new Size(250, 30);

            lblGrad = new Label();
            lblGrad.Text = "Grad:";
            lblGrad.Font = new Font("Segoe UI", 10);
            lblGrad.AutoSize = true;
            lblGrad.Location = new Point(520, 80);

            cmbGrad = new ComboBox();
            cmbGrad.Location = new Point(520, 105);
            cmbGrad.Size = new Size(250, 30);
            cmbGrad.DropDownStyle = ComboBoxStyle.DropDownList;

            lblBrojZvezdica = new Label();
            lblBrojZvezdica.Text = "Broj zvezdica:";
            lblBrojZvezdica.Font = new Font("Segoe UI", 10);
            lblBrojZvezdica.AutoSize = true;
            lblBrojZvezdica.Location = new Point(790, 80);

            nudBrojZvezdica = new NumericUpDown();
            nudBrojZvezdica.Location = new Point(790, 105);
            nudBrojZvezdica.Size = new Size(120, 30);
            nudBrojZvezdica.Minimum = 1;
            nudBrojZvezdica.Maximum = 5;
            nudBrojZvezdica.Value = 1;

            lblAdresa = new Label();
            lblAdresa.Text = "Adresa:";
            lblAdresa.Font = new Font("Segoe UI", 10);
            lblAdresa.AutoSize = true;
            lblAdresa.Location = new Point(35, 150);

            txtAdresa = new TextBox();
            txtAdresa.Location = new Point(35, 175);
            txtAdresa.Size = new Size(875, 30);

            lblSobeNaslov = new Label();
            lblSobeNaslov.Text = "Sobe";
            lblSobeNaslov.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblSobeNaslov.ForeColor = Color.FromArgb(32, 42, 68);
            lblSobeNaslov.AutoSize = true;
            lblSobeNaslov.Location = new Point(35, 235);

            lblBrojSobe = new Label();
            lblBrojSobe.Text = "Broj sobe:";
            lblBrojSobe.Font = new Font("Segoe UI", 10);
            lblBrojSobe.AutoSize = true;
            lblBrojSobe.Location = new Point(35, 285);

            nudBrojSobe = new NumericUpDown();
            nudBrojSobe.Location = new Point(35, 310);
            nudBrojSobe.Size = new Size(150, 30);
            nudBrojSobe.Minimum = 1;
            nudBrojSobe.Maximum = 10000;

            lblCenaPoNoci = new Label();
            lblCenaPoNoci.Text = "Cena po noći:";
            lblCenaPoNoci.Font = new Font("Segoe UI", 10);
            lblCenaPoNoci.AutoSize = true;
            lblCenaPoNoci.Location = new Point(210, 285);

            nudCenaPoNoci = new NumericUpDown();
            nudCenaPoNoci.Location = new Point(210, 310);
            nudCenaPoNoci.Size = new Size(180, 30);
            nudCenaPoNoci.Minimum = 1;
            nudCenaPoNoci.Maximum = 1000000;
            nudCenaPoNoci.DecimalPlaces = 2;

            lblBrojKreveta = new Label();
            lblBrojKreveta.Text = "Broj kreveta:";
            lblBrojKreveta.Font = new Font("Segoe UI", 10);
            lblBrojKreveta.AutoSize = true;
            lblBrojKreveta.Location = new Point(420, 285);

            nudBrojKreveta = new NumericUpDown();
            nudBrojKreveta.Location = new Point(420, 310);
            nudBrojKreveta.Size = new Size(150, 30);
            nudBrojKreveta.Minimum = 1;
            nudBrojKreveta.Maximum = 20;
            nudBrojKreveta.Value = 1;

            btnDodajSobu = new Button();
            btnDodajSobu.Text = "Dodaj sobu";
            btnDodajSobu.Size = new Size(150, 42);
            btnDodajSobu.Location = new Point(600, 302);
            btnDodajSobu.BackColor = Color.FromArgb(32, 42, 68);
            btnDodajSobu.ForeColor = Color.White;
            btnDodajSobu.FlatStyle = FlatStyle.Flat;
            btnDodajSobu.FlatAppearance.BorderSize = 0;
            btnDodajSobu.Click += BtnDodajSobu_Click;

            btnObrisiSobu = new Button();
            btnObrisiSobu.Text = "Obriši sobu";
            btnObrisiSobu.Size = new Size(150, 42);
            btnObrisiSobu.Location = new Point(765, 302);
            btnObrisiSobu.BackColor = Color.FromArgb(160, 50, 50);
            btnObrisiSobu.ForeColor = Color.White;
            btnObrisiSobu.FlatStyle = FlatStyle.Flat;
            btnObrisiSobu.FlatAppearance.BorderSize = 0;
            btnObrisiSobu.Click += BtnObrisiSobu_Click;

            dgvSobe = new DataGridView();
            dgvSobe.Location = new Point(35, 365);
            dgvSobe.Size = new Size(885, 220);
            dgvSobe.BackgroundColor = Color.White;
            dgvSobe.BorderStyle = BorderStyle.None;
            dgvSobe.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSobe.MultiSelect = false;
            dgvSobe.ReadOnly = true;
            dgvSobe.AllowUserToAddRows = false;
            dgvSobe.AllowUserToDeleteRows = false;
            dgvSobe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            btnSacuvaj = new Button();
            btnSacuvaj.Text = "Sačuvaj";
            btnSacuvaj.Size = new Size(130, 42);
            btnSacuvaj.Location = new Point(650, 610);
            btnSacuvaj.BackColor = Color.FromArgb(32, 42, 68);
            btnSacuvaj.ForeColor = Color.White;
            btnSacuvaj.FlatStyle = FlatStyle.Flat;
            btnSacuvaj.FlatAppearance.BorderSize = 0;
            btnSacuvaj.Click += BtnSacuvaj_Click;

            btnOtkazi = new Button();
            btnOtkazi.Text = "Otkaži";
            btnOtkazi.Size = new Size(130, 42);
            btnOtkazi.Location = new Point(790, 610);
            btnOtkazi.BackColor = Color.Gray;
            btnOtkazi.ForeColor = Color.White;
            btnOtkazi.FlatStyle = FlatStyle.Flat;
            btnOtkazi.FlatAppearance.BorderSize = 0;
            btnOtkazi.Click += BtnOtkazi_Click;

            Controls.Add(lblHotelId);
            Controls.Add(txtHotelId);
            Controls.Add(lblNaziv);
            Controls.Add(txtNaziv);
            Controls.Add(lblGrad);
            Controls.Add(cmbGrad);
            Controls.Add(lblBrojZvezdica);
            Controls.Add(nudBrojZvezdica);
            Controls.Add(lblAdresa);
            Controls.Add(txtAdresa);
            Controls.Add(lblSobeNaslov);
            Controls.Add(lblBrojSobe);
            Controls.Add(nudBrojSobe);
            Controls.Add(lblCenaPoNoci);
            Controls.Add(nudCenaPoNoci);
            Controls.Add(lblBrojKreveta);
            Controls.Add(nudBrojKreveta);
            Controls.Add(btnDodajSobu);
            Controls.Add(btnObrisiSobu);
            Controls.Add(dgvSobe);
            Controls.Add(btnSacuvaj);
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

        private void OsveziTabeluSoba()
        {
            dgvSobe.DataSource = null;
            dgvSobe.DataSource = sobe.Select(s => new
            {
                s.BrojSobe,
                s.CenaPoNoci,
                s.BrojKreveta
            }).ToList();
        }

        private void BtnDodajSobu_Click(object sender, EventArgs e)
        {
            int brojSobe = (int)nudBrojSobe.Value;

            if (sobe.Any(s => s.BrojSobe == brojSobe))
            {
                MessageBox.Show("Soba sa tim brojem je već dodata.");
                return;
            }

            Soba soba = new Soba
            {
                BrojSobe = brojSobe,
                CenaPoNoci = nudCenaPoNoci.Value,
                BrojKreveta = (int)nudBrojKreveta.Value
            };

            sobe.Add(soba);
            OsveziTabeluSoba();

            nudBrojSobe.Value = 1;
            nudCenaPoNoci.Value = 1;
            nudBrojKreveta.Value = 1;
        }

        private void BtnObrisiSobu_Click(object sender, EventArgs e)
        {
            if (dgvSobe.CurrentRow == null)
            {
                MessageBox.Show("Selektuj sobu koju želiš da obrišeš.");
                return;
            }

            int brojSobe = Convert.ToInt32(dgvSobe.CurrentRow.Cells["BrojSobe"].Value);
            Soba sobaZaBrisanje = sobe.FirstOrDefault(s => s.BrojSobe == brojSobe);

            if (sobaZaBrisanje != null)
            {
                sobe.Remove(sobaZaBrisanje);
                OsveziTabeluSoba();
            }
        }

        private void BtnSacuvaj_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtHotelId.Text) ||
                string.IsNullOrWhiteSpace(txtNaziv.Text) ||
                string.IsNullOrWhiteSpace(txtAdresa.Text) ||
                cmbGrad.SelectedItem == null)
            {
                MessageBox.Show("Sva polja za hotel su obavezna.");
                return;
            }

            if (!int.TryParse(txtHotelId.Text, out int hotelId))
            {
                MessageBox.Show("Hotel ID mora biti broj.");
                return;
            }

            if (sobe.Count == 0)
            {
                MessageBox.Show("Hotel mora imati bar jednu sobu.");
                return;
            }

            Grad izabraniGrad = cmbGrad.SelectedItem as Grad;

            HotelZaCuvanje = new Hotel
            {
                HotelId = hotelId,
                Naziv = txtNaziv.Text.Trim(),
                Grad = izabraniGrad,
                BrojZvezdica = (byte)nudBrojZvezdica.Value,
                Adresa = txtAdresa.Text.Trim()
            };

            foreach (Soba soba in sobe)
            {
                soba.Hotel = HotelZaCuvanje;
            }

            SobeZaCuvanje = sobe;

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