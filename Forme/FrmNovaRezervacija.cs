using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Domen;

namespace Forme
{
    public partial class FrmNovaRezervacija : Form
    {
        private readonly DateTime datumOd;
        private readonly int brojNoci;
        private readonly Grad grad;
        private readonly int brojGostiju;
        private readonly DateTime datumDo;

        private Label lblNaslov;
        private Label lblInfo;
        private Label lblHoteli;
        private Label lblSobe;
        private Label lblGost;

        private DataGridView dgvHoteli;
        private DataGridView dgvSobe;

        private ComboBox cmbGosti;

        private Button btnDodajFizicko;
        private Button btnDodajPravno;
        private Button btnSacuvaj;
        private Button btnOtkazi;

        public FrmNovaRezervacija(DateTime datumOd, int brojNoci, Grad grad, int brojGostiju)
        {
            InitializeComponent();

            this.datumOd = datumOd;
            this.brojNoci = brojNoci;
            this.grad = grad;
            this.brojGostiju = brojGostiju;
            this.datumDo = datumOd.AddDays(brojNoci);

            InicijalizujIzgled();
            UcitajDostupneHotele();
            UcitajGoste();
        }

        private void InicijalizujIzgled()
        {
            Text = "Nova rezervacija";
            StartPosition = FormStartPosition.CenterParent;
            Size = new Size(1250, 760);
            MinimumSize = new Size(1250, 760);
            BackColor = Color.FromArgb(245, 247, 250);

            lblNaslov = new Label();
            lblNaslov.Text = "Nova rezervacija";
            lblNaslov.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            lblNaslov.ForeColor = Color.FromArgb(32, 42, 68);
            lblNaslov.AutoSize = true;
            lblNaslov.Location = new Point(30, 20);

            lblInfo = new Label();
            lblInfo.Text = $"Grad: {grad.Naziv} | Datum od: {datumOd:dd.MM.yyyy} | Datum do: {datumDo:dd.MM.yyyy} | Broj noći: {brojNoci} | Broj gostiju: {brojGostiju}";
            lblInfo.Font = new Font("Segoe UI", 10);
            lblInfo.ForeColor = Color.DimGray;
            lblInfo.AutoSize = true;
            lblInfo.Location = new Point(32, 65);

            lblHoteli = new Label();
            lblHoteli.Text = "Dostupni hoteli";
            lblHoteli.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblHoteli.AutoSize = true;
            lblHoteli.Location = new Point(30, 110);

            dgvHoteli = new DataGridView();
            dgvHoteli.Location = new Point(30, 145);
            dgvHoteli.Size = new Size(560, 280);
            dgvHoteli.BackgroundColor = Color.White;
            dgvHoteli.BorderStyle = BorderStyle.None;
            dgvHoteli.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHoteli.MultiSelect = false;
            dgvHoteli.ReadOnly = true;
            dgvHoteli.AllowUserToAddRows = false;
            dgvHoteli.AllowUserToDeleteRows = false;
            dgvHoteli.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgvHoteli.RowHeadersVisible = false;
            dgvHoteli.SelectionChanged += DgvHoteli_SelectionChanged;

            lblSobe = new Label();
            lblSobe.Text = "Slobodne sobe za izabrani hotel";
            lblSobe.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblSobe.AutoSize = true;
            lblSobe.Location = new Point(620, 110);

            dgvSobe = new DataGridView();
            dgvSobe.Location = new Point(620, 145);
            dgvSobe.Size = new Size(580, 280);
            dgvSobe.BackgroundColor = Color.White;
            dgvSobe.BorderStyle = BorderStyle.None;
            dgvSobe.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSobe.MultiSelect = false;
            dgvSobe.ReadOnly = true;
            dgvSobe.AllowUserToAddRows = false;
            dgvSobe.AllowUserToDeleteRows = false;
            dgvSobe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgvSobe.RowHeadersVisible = false;

            lblGost = new Label();
            lblGost.Text = "Gost";
            lblGost.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblGost.AutoSize = true;
            lblGost.Location = new Point(30, 460);

            cmbGosti = new ComboBox();
            cmbGosti.Location = new Point(30, 495);
            cmbGosti.Size = new Size(520, 30);
            cmbGosti.DropDownStyle = ComboBoxStyle.DropDownList;

            btnDodajFizicko = new Button();
            btnDodajFizicko.Text = "Dodaj fizičko lice";
            btnDodajFizicko.Size = new Size(160, 42);
            btnDodajFizicko.Location = new Point(570, 488);
            btnDodajFizicko.BackColor = Color.FromArgb(32, 42, 68);
            btnDodajFizicko.ForeColor = Color.White;
            btnDodajFizicko.FlatStyle = FlatStyle.Flat;
            btnDodajFizicko.FlatAppearance.BorderSize = 0;
            btnDodajFizicko.Click += BtnDodajFizicko_Click;

            btnDodajPravno = new Button();
            btnDodajPravno.Text = "Dodaj pravno lice";
            btnDodajPravno.Size = new Size(160, 42);
            btnDodajPravno.Location = new Point(740, 488);
            btnDodajPravno.BackColor = Color.FromArgb(54, 74, 114);
            btnDodajPravno.ForeColor = Color.White;
            btnDodajPravno.FlatStyle = FlatStyle.Flat;
            btnDodajPravno.FlatAppearance.BorderSize = 0;
            btnDodajPravno.Click += BtnDodajPravno_Click;

            btnSacuvaj = new Button();
            btnSacuvaj.Text = "Sačuvaj rezervaciju";
            btnSacuvaj.Size = new Size(180, 45);
            btnSacuvaj.Location = new Point(850, 650);
            btnSacuvaj.BackColor = Color.FromArgb(32, 42, 68);
            btnSacuvaj.ForeColor = Color.White;
            btnSacuvaj.FlatStyle = FlatStyle.Flat;
            btnSacuvaj.FlatAppearance.BorderSize = 0;
            btnSacuvaj.Click += BtnSacuvaj_Click;

            btnOtkazi = new Button();
            btnOtkazi.Text = "Otkaži";
            btnOtkazi.Size = new Size(120, 45);
            btnOtkazi.Location = new Point(1040, 650);
            btnOtkazi.BackColor = Color.Gray;
            btnOtkazi.ForeColor = Color.White;
            btnOtkazi.FlatStyle = FlatStyle.Flat;
            btnOtkazi.FlatAppearance.BorderSize = 0;
            btnOtkazi.Click += BtnOtkazi_Click;

            Controls.Add(lblNaslov);
            Controls.Add(lblInfo);
            Controls.Add(lblHoteli);
            Controls.Add(dgvHoteli);
            Controls.Add(lblSobe);
            Controls.Add(dgvSobe);
            Controls.Add(lblGost);
            Controls.Add(cmbGosti);
            Controls.Add(btnDodajFizicko);
            Controls.Add(btnDodajPravno);
            Controls.Add(btnSacuvaj);
            Controls.Add(btnOtkazi);
        }

        private void UcitajDostupneHotele()
        {
            try
            {
                DataTable hoteli = Kontroler.Kontroler.Instance.DohvatiDostupneHotele(
                    grad.GradId, datumOd, brojNoci, brojGostiju);

                if (hoteli.Rows.Count == 0)
                {
                    MessageBox.Show("Nema slobodnih soba za izabrani grad i datume.");
                    Close();
                    return;
                }

                dgvHoteli.DataSource = hoteli;

                foreach (DataGridViewColumn kolona in dgvHoteli.Columns)
                    kolona.SortMode = DataGridViewColumnSortMode.NotSortable;

                dgvHoteli.Columns["HID"].HeaderText = "ID";
                dgvHoteli.Columns["Naziv"].HeaderText = "Naziv";
                dgvHoteli.Columns["Adresa"].HeaderText = "Adresa";
                dgvHoteli.Columns["BrojZvezdica"].HeaderText = "Zvezdice";

                dgvHoteli.Columns["BrojZvezdica"].DefaultCellStyle.Format = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška pri učitavanju hotela: " + ex.Message);
            }
        }

        private void UcitajSobeZaHotel(int hotelId)
        {
            try
            {
                DataTable sobe = Kontroler.Kontroler.Instance.DohvatiDostupneSobeZaHotel(
                    hotelId, datumOd, brojNoci, brojGostiju);

                dgvSobe.DataSource = sobe;

                foreach (DataGridViewColumn kolona in dgvSobe.Columns)
                    kolona.SortMode = DataGridViewColumnSortMode.NotSortable;

                dgvSobe.Columns["BrojSobe"].HeaderText = "Broj sobe";
                dgvSobe.Columns["CenaPoNoci"].HeaderText = "Cena po noći";
                dgvSobe.Columns["BrojKreveta"].HeaderText = "Broj kreveta";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška pri učitavanju soba: " + ex.Message);
            }
        }

        private void UcitajGoste()
        {
            try
            {
                List<Gost> gosti = Kontroler.Kontroler.Instance.DohvatiSveGoste();
                cmbGosti.DataSource = null;
                cmbGosti.DataSource = gosti;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška pri učitavanju gostiju: " + ex.Message);
            }
        }

        private void DgvHoteli_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvHoteli.CurrentRow == null) return;

            int hotelId = Convert.ToInt32(dgvHoteli.CurrentRow.Cells["HID"].Value);
            UcitajSobeZaHotel(hotelId);
        }

        private void BtnDodajFizicko_Click(object sender, EventArgs e)
        {
            FrmSacuvajFizickoLice forma = new FrmSacuvajFizickoLice();
            if (forma.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Kontroler.Kontroler.Instance.UnesiFizickoLice(forma.FizickoLiceZaCuvanje);
                    UcitajGoste();
                    cmbGosti.SelectedItem = cmbGosti.Items.Cast<Gost>().FirstOrDefault(g => g.GostId == forma.FizickoLiceZaCuvanje.GostId);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void BtnDodajPravno_Click(object sender, EventArgs e)
        {
            FrmSacuvajPravnoLice forma = new FrmSacuvajPravnoLice();
            if (forma.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Kontroler.Kontroler.Instance.UnesiPravnoLice(forma.PravnoLiceZaCuvanje);
                    UcitajGoste();
                    cmbGosti.SelectedItem = cmbGosti.Items.Cast<Gost>().FirstOrDefault(g => g.GostId == forma.PravnoLiceZaCuvanje.GostId);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void BtnSacuvaj_Click(object sender, EventArgs e)
        {
            if (dgvHoteli.CurrentRow == null)
            {
                MessageBox.Show("Izaberite hotel.");
                return;
            }

            if (dgvSobe.CurrentRow == null)
            {
                MessageBox.Show("Izaberite sobu.");
                return;
            }

            if (cmbGosti.SelectedItem == null)
            {
                MessageBox.Show("Izaberite gosta.");
                return;
            }

            try
            {
                int hotelId = Convert.ToInt32(dgvHoteli.CurrentRow.Cells["HID"].Value);
                int brojSobe = Convert.ToInt32(dgvSobe.CurrentRow.Cells["BrojSobe"].Value);
                Gost gost = cmbGosti.SelectedItem as Gost;

                Kontroler.Kontroler.Instance.NapraviRezervaciju(
                    hotelId,
                    brojSobe,
                    gost.GostId,
                    datumOd,
                    datumDo,
                    brojGostiju
                );

                MessageBox.Show("Rezervacija je uspešno kreirana.");
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