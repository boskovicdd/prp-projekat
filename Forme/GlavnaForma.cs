using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Domen;
using Kontroler;

namespace Forme
{
    public partial class GlavnaForma : Form
    {
        private Panel panelMeni;
        private Panel panelGornji;
        private Panel panelSadrzaj;

        private Label lblNaslovAplikacije;

        private Button btnDashboard;
        private Button btnNovaRezervacija;
        private Button btnPregledRezervacija;
        private Button btnGosti;
        private Button btnHoteli;
        private Button btnGradovi;

        private DataGridView dgvGradovi;
        private DataGridView dgvHoteli;
        public GlavnaForma()
        {
            InitializeComponent();
            InicijalizujIzgled();
        }

        private void InicijalizujIzgled()
        {
            Text = "Hotel rezervacioni sistem";
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Maximized;
            MinimumSize = new Size(1200, 750);
            BackColor = Color.White;

            KreirajPanele();
            KreirajLeviMeni();
            KreirajGornjiDeo();

            ResetujBojeDugmadi();
            btnDashboard.BackColor = Color.FromArgb(54, 74, 114);
            lblNaslovAplikacije.Text = "Dashboard";

            PrikaziDashboard();
        }

        private void KreirajPanele()
        {
            panelMeni = new Panel
            {
                Dock = DockStyle.Left,
                Width = 260,
                BackColor = Color.FromArgb(32, 42, 68)
            };

            panelGornji = new Panel
            {
                Dock = DockStyle.Top,
                Height = 120,
                BackColor = Color.White
            };

            panelSadrzaj = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(245, 247, 250),
                Padding = new Padding(25),
                AutoScroll = true
            };

            Controls.Add(panelSadrzaj);
            Controls.Add(panelGornji);
            Controls.Add(panelMeni);
        }

        private void KreirajLeviMeni()
        {
            Label lblLogo = new Label
            {
                Text = "HOTEL APP",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 100
            };

            btnDashboard = KreirajMeniDugme("Dashboard");
            btnNovaRezervacija = KreirajMeniDugme("Nova rezervacija");
            btnPregledRezervacija = KreirajMeniDugme("Pregled rezervacija");
            btnGosti = KreirajMeniDugme("Gosti");
            btnHoteli = KreirajMeniDugme("Hoteli");
            btnGradovi = KreirajMeniDugme("Gradovi");

            btnDashboard.Click += BtnDashboard_Click;
            btnNovaRezervacija.Click += BtnNovaRezervacija_Click;
            btnPregledRezervacija.Click += BtnPregledRezervacija_Click;
            btnGosti.Click += BtnGosti_Click;
            btnHoteli.Click += BtnHoteli_Click;
            btnGradovi.Click += BtnGradovi_Click;

            panelMeni.Controls.Add(btnGradovi);
            panelMeni.Controls.Add(btnHoteli);
            panelMeni.Controls.Add(btnGosti);
            panelMeni.Controls.Add(btnPregledRezervacija);
            panelMeni.Controls.Add(btnNovaRezervacija);
            panelMeni.Controls.Add(btnDashboard);
            panelMeni.Controls.Add(lblLogo);
        }

        private Button KreirajMeniDugme(string tekst)
        {
            Button btn = new Button
            {
                Text = tekst,
                Dock = DockStyle.Top,
                Height = 68,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(32, 42, 68),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11, FontStyle.Regular),
                Cursor = Cursors.Hand,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(25, 0, 0, 0)
            };

            btn.FlatAppearance.BorderSize = 0;

            btn.MouseEnter += (s, e) =>
            {
                if (btn.BackColor != Color.FromArgb(54, 74, 114))
                    btn.BackColor = Color.FromArgb(42, 54, 84);
            };

            btn.MouseLeave += (s, e) =>
            {
                if (btn.BackColor != Color.FromArgb(54, 74, 114))
                    btn.BackColor = Color.FromArgb(32, 42, 68);
            };

            return btn;
        }

        private void KreirajGornjiDeo()
        {
            lblNaslovAplikacije = new Label
            {
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = Color.FromArgb(32, 42, 68),
                AutoSize = true,
                Location = new Point(25, 18)
            };

            panelGornji.Controls.Add(lblNaslovAplikacije);
        }

        private Panel KreirajKarticu(string naslov, string opis, int x, int y, EventHandler klik)
        {
            Panel panel = new Panel
            {
                Size = new Size(360, 160),
                Location = new Point(x, y),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Cursor = Cursors.Hand
            };

            Label lblNaslov = new Label
            {
                Text = naslov,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(32, 42, 68),
                AutoSize = true,
                Location = new Point(20, 20),
                Cursor = Cursors.Hand
            };

            Label lblOpisKartice = new Label
            {
                Text = opis,
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                ForeColor = Color.DimGray,
                Size = new Size(300, 80),
                Location = new Point(20, 55),
                Cursor = Cursors.Hand
            };

            panel.Controls.Add(lblNaslov);
            panel.Controls.Add(lblOpisKartice);

            panel.Click += klik;
            lblNaslov.Click += klik;
            lblOpisKartice.Click += klik;

            panel.MouseEnter += (s, e) => panel.BackColor = Color.FromArgb(250, 250, 252);
            panel.MouseLeave += (s, e) => panel.BackColor = Color.White;

            return panel;
        }

        private void PrikaziDashboard()
        {
            panelSadrzaj.Controls.Clear();

            Label lblDobrodoslica = new Label
            {
                Text = "Dobrodošli u aplikaciju za booking",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.FromArgb(32, 42, 68),
                AutoSize = true,
                Location = new Point(10, 10)
            };

            Label lblOpis = new Label
            {
                Text = "Izaberite jednu od ponuđenih opcija iz menija sa leve strane ili kliknite na karticu.",
                Font = new Font("Segoe UI", 11, FontStyle.Regular),
                ForeColor = Color.DimGray,
                AutoSize = true,
                Location = new Point(12, 55)
            };

            Panel kartica1 = KreirajKarticu("Nova rezervacija", "Kreiranje nove rezervacije za izabrani hotel i sobu.", 10, 120, BtnNovaRezervacija_Click);
            Panel kartica2 = KreirajKarticu("Pregled rezervacija", "Pregled svih unetih rezervacija i njihovih detalja.", 400, 120, BtnPregledRezervacija_Click);
            Panel kartica3 = KreirajKarticu("Gosti", "Pregled, unos i upravljanje postojećim gostima.", 790, 120, BtnGosti_Click);
            Panel kartica4 = KreirajKarticu("Hoteli", "Dodavanje, izmena i pregled hotela i soba.", 10, 320, BtnHoteli_Click);
            Panel kartica5 = KreirajKarticu("Gradovi", "Dodavanje, izmena i pregled gradova u sistemu.", 400, 320, BtnGradovi_Click);

            panelSadrzaj.Controls.Add(lblDobrodoslica);
            panelSadrzaj.Controls.Add(lblOpis);
            panelSadrzaj.Controls.Add(kartica1);
            panelSadrzaj.Controls.Add(kartica2);
            panelSadrzaj.Controls.Add(kartica3);
            panelSadrzaj.Controls.Add(kartica4);
            panelSadrzaj.Controls.Add(kartica5);
        }

        private void PrikaziStranicuPlaceholder(string naslov, string tekst)
        {
            panelSadrzaj.Controls.Clear();

            Panel kartica = new Panel
            {
                BackColor = Color.White,
                Size = new Size(950, 520),
                Location = new Point(20, 20),
                BorderStyle = BorderStyle.FixedSingle
            };

            Label lblNaslov = new Label
            {
                Text = naslov,
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.FromArgb(32, 42, 68),
                AutoSize = true,
                Location = new Point(30, 30)
            };

            Label lblTekst = new Label
            {
                Text = tekst,
                Font = new Font("Segoe UI", 11, FontStyle.Regular),
                ForeColor = Color.DimGray,
                Size = new Size(830, 320),
                Location = new Point(32, 90)
            };

            kartica.Controls.Add(lblNaslov);
            kartica.Controls.Add(lblTekst);
            panelSadrzaj.Controls.Add(kartica);
        }

        private void PrikaziStranicuGradovi()
        {
            panelSadrzaj.Controls.Clear();

            Panel kartica = new Panel
            {
                BackColor = Color.White,
                Size = new Size(1150, 560),
                Location = new Point(20, 20),
                BorderStyle = BorderStyle.FixedSingle
            };

            Label lblNaslov = new Label
            {
                Text = "Upravljanje gradovima",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.FromArgb(32, 42, 68),
                AutoSize = true,
                Location = new Point(30, 25)
            };

            Button btnDodaj = new Button
            {
                Text = "Dodaj",
                Size = new Size(130, 42),
                Location = new Point(32, 90),
                BackColor = Color.FromArgb(32, 42, 68),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnDodaj.FlatAppearance.BorderSize = 0;
            btnDodaj.Click += BtnDodajGrad_Click;

            Button btnIzmeni = new Button
            {
                Text = "Izmeni",
                Size = new Size(130, 42),
                Location = new Point(172, 90),
                BackColor = Color.FromArgb(54, 74, 114),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnIzmeni.FlatAppearance.BorderSize = 0;
            btnIzmeni.Click += BtnIzmeniGrad_Click;

            Button btnObrisi = new Button
            {
                Text = "Obriši",
                Size = new Size(130, 42),
                Location = new Point(312, 90),
                BackColor = Color.FromArgb(160, 50, 50),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnObrisi.FlatAppearance.BorderSize = 0;
            btnObrisi.Click += BtnObrisiGrad_Click;

            Button btnOsvezi = new Button
            {
                Text = "Osveži",
                Size = new Size(130, 42),
                Location = new Point(452, 90),
                BackColor = Color.Gray,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnOsvezi.FlatAppearance.BorderSize = 0;
            btnOsvezi.Click += BtnOsveziGradove_Click;

            dgvGradovi = new DataGridView
            {
                Location = new Point(32, 155),
                Size = new Size(1080, 360),
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            kartica.Controls.Add(lblNaslov);
            kartica.Controls.Add(btnDodaj);
            kartica.Controls.Add(btnIzmeni);
            kartica.Controls.Add(btnObrisi);
            kartica.Controls.Add(btnOsvezi);
            kartica.Controls.Add(dgvGradovi);

            panelSadrzaj.Controls.Add(kartica);

            UcitajGradoveUTabelu();
        }

        private void UcitajGradoveUTabelu()
        {
            try
            {
                List<Grad> gradovi = Kontroler.Kontroler.Instance.DohvatiSveGradove();
                dgvGradovi.DataSource = null;
                dgvGradovi.DataSource = gradovi;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška pri učitavanju gradova: " + ex.Message);
            }
        }

        private Grad VratiSelektovaniGrad()
        {
            if (dgvGradovi == null || dgvGradovi.CurrentRow == null)
                return null;

            return dgvGradovi.CurrentRow.DataBoundItem as Grad;
        }

        private void BtnDodajGrad_Click(object sender, EventArgs e)
        {
            FrmSacuvajGrad forma = new FrmSacuvajGrad();

            if (forma.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Kontroler.Kontroler.Instance.UnesiGrad(forma.GradZaCuvanje);
                    UcitajGradoveUTabelu();
                    MessageBox.Show("Grad je uspešno dodat.");
                }
                catch (Microsoft.Data.SqlClient.SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnIzmeniGrad_Click(object sender, EventArgs e)
        {
            Grad selektovaniGrad = VratiSelektovaniGrad();
            if (selektovaniGrad == null)
            {
                MessageBox.Show("Selektuj grad koji želiš da izmeniš.");
                return;
            }

            FrmSacuvajGrad forma = new FrmSacuvajGrad(selektovaniGrad);

            if (forma.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Kontroler.Kontroler.Instance.IzmeniGrad(forma.GradZaCuvanje);
                    UcitajGradoveUTabelu();
                    MessageBox.Show("Grad je uspešno izmenjen.");
                }
                catch (Microsoft.Data.SqlClient.SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnObrisiGrad_Click(object sender, EventArgs e)
        {
            Grad selektovaniGrad = VratiSelektovaniGrad();
            if (selektovaniGrad == null)
            {
                MessageBox.Show("Selektuj grad koji želiš da obrišeš.");
                return;
            }

            DialogResult rezultat = MessageBox.Show(
                $"Da li si siguran da želiš da obrišeš grad {selektovaniGrad.Naziv}?",
                "Potvrda brisanja",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (rezultat == DialogResult.Yes)
            {
                try
                {
                    Kontroler.Kontroler.Instance.ObrisiGrad(selektovaniGrad.GradId);
                    UcitajGradoveUTabelu();
                    MessageBox.Show("Grad je uspešno obrisan.");
                }
                catch (Microsoft.Data.SqlClient.SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnOsveziGradove_Click(object sender, EventArgs e)
        {
            UcitajGradoveUTabelu();
        }
        private void PrikaziStranicuHoteli()
        {
            panelSadrzaj.Controls.Clear();

            Panel kartica = new Panel
            {
                BackColor = Color.White,
                Size = new Size(1150, 560),
                Location = new Point(20, 20),
                BorderStyle = BorderStyle.FixedSingle
            };

            Label lblNaslov = new Label
            {
                Text = "Upravljanje hotelima",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.FromArgb(32, 42, 68),
                AutoSize = true,
                Location = new Point(30, 25)
            };

            Button btnDodaj = new Button
            {
                Text = "Dodaj",
                Size = new Size(130, 42),
                Location = new Point(32, 90),
                BackColor = Color.FromArgb(32, 42, 68),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnDodaj.FlatAppearance.BorderSize = 0;
            btnDodaj.Click += BtnDodajHotel_Click;

            Button btnIzmeni = new Button
            {
                Text = "Izmeni",
                Size = new Size(130, 42),
                Location = new Point(172, 90),
                BackColor = Color.FromArgb(54, 74, 114),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnIzmeni.FlatAppearance.BorderSize = 0;
            btnIzmeni.Click += BtnIzmeniHotel_Click;

            Button btnObrisi = new Button
            {
                Text = "Obriši",
                Size = new Size(130, 42),
                Location = new Point(312, 90),
                BackColor = Color.FromArgb(160, 50, 50),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnObrisi.FlatAppearance.BorderSize = 0;
            btnObrisi.Click += BtnObrisiHotel_Click;

            Button btnOsvezi = new Button
            {
                Text = "Osveži",
                Size = new Size(130, 42),
                Location = new Point(452, 90),
                BackColor = Color.Gray,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnOsvezi.FlatAppearance.BorderSize = 0;
            btnOsvezi.Click += BtnOsveziHotele_Click;

            dgvHoteli = new DataGridView
            {
                Location = new Point(32, 155),
                Size = new Size(1080, 360),
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            kartica.Controls.Add(lblNaslov);
            kartica.Controls.Add(btnDodaj);
            kartica.Controls.Add(btnIzmeni);
            kartica.Controls.Add(btnObrisi);
            kartica.Controls.Add(btnOsvezi);
            kartica.Controls.Add(dgvHoteli);

            panelSadrzaj.Controls.Add(kartica);

            UcitajHoteleUTabelu();
        }

        private void UcitajHoteleUTabelu()
        {
            try
            {
                List<Hotel> hoteli = Kontroler.Kontroler.Instance.DohvatiSveHotele();

                dgvHoteli.DataSource = null;
                dgvHoteli.Columns.Clear();

                var podaciZaPrikaz = hoteli.Select(h => new
                {
                    ID = h.HotelId,
                    Naziv = h.Naziv,
                    Adresa = h.Adresa,
                    Grad = h.Grad != null ? h.Grad.Naziv : "",
                    BrojZvezdica = new string('⭐', h.BrojZvezdica)
                }).ToList();

                dgvHoteli.DataSource = podaciZaPrikaz;

                dgvHoteli.Columns["ID"].HeaderText = "ID";
                dgvHoteli.Columns["Naziv"].HeaderText = "Naziv";
                dgvHoteli.Columns["Adresa"].HeaderText = "Adresa";
                dgvHoteli.Columns["Grad"].HeaderText = "Grad";
                dgvHoteli.Columns["BrojZvezdica"].HeaderText = "Broj zvezdica";

                dgvHoteli.Columns["ID"].FillWeight = 15;
                dgvHoteli.Columns["Naziv"].FillWeight = 30;
                dgvHoteli.Columns["Adresa"].FillWeight = 30;
                dgvHoteli.Columns["Grad"].FillWeight = 20;
                dgvHoteli.Columns["BrojZvezdica"].FillWeight = 20;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška pri učitavanju hotela: " + ex.Message);
            }
        }

        private Hotel VratiSelektovaniHotel()
        {
            if (dgvHoteli == null || dgvHoteli.CurrentRow == null)
                return null;

            int hotelId = Convert.ToInt32(dgvHoteli.CurrentRow.Cells["ID"].Value);

            List<Hotel> hoteli = Kontroler.Kontroler.Instance.DohvatiSveHotele();
            return hoteli.FirstOrDefault(h => h.HotelId == hotelId);
        }

        private void BtnDodajHotel_Click(object sender, EventArgs e)
        {
            FrmSacuvajHotel forma = new FrmSacuvajHotel();

            if (forma.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Kontroler.Kontroler.Instance.UnesiHotelSaSobama(forma.HotelZaCuvanje, forma.SobeZaCuvanje);
                    UcitajHoteleUTabelu();
                    MessageBox.Show("Hotel je uspešno dodat.");
                }
                catch (Microsoft.Data.SqlClient.SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnIzmeniHotel_Click(object sender, EventArgs e)
        {
            Hotel selektovaniHotel = VratiSelektovaniHotel();

            if (selektovaniHotel == null)
            {
                MessageBox.Show("Selektuj hotel koji želiš da izmeniš.");
                return;
            }

            try
            {
                List<Soba> sobe = Kontroler.Kontroler.Instance.DohvatiSobeZaHotel(selektovaniHotel.HotelId);

                FrmSacuvajHotel forma = new FrmSacuvajHotel(selektovaniHotel, sobe);

                if (forma.ShowDialog() == DialogResult.OK)
                {
                    Kontroler.Kontroler.Instance.IzmeniHotelSaSobama(forma.HotelZaCuvanje, forma.SobeZaCuvanje);
                    UcitajHoteleUTabelu();
                    MessageBox.Show("Hotel je uspešno izmenjen.");
                }
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnObrisiHotel_Click(object sender, EventArgs e)
        {
            Hotel selektovaniHotel = VratiSelektovaniHotel();

            if (selektovaniHotel == null)
            {
                MessageBox.Show("Selektuj hotel koji želiš da obrišeš.");
                return;
            }

            DialogResult rezultat = MessageBox.Show(
                $"Da li si siguran da želiš da obrišeš hotel na adresi {selektovaniHotel.Adresa}?",
                "Potvrda brisanja",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (rezultat == DialogResult.Yes)
            {
                try
                {
                    Kontroler.Kontroler.Instance.ObrisiHotel(selektovaniHotel.HotelId);
                    UcitajHoteleUTabelu();
                    MessageBox.Show("Hotel je uspešno obrisan.");
                }
                catch (Microsoft.Data.SqlClient.SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnOsveziHotele_Click(object sender, EventArgs e)
        {
            UcitajHoteleUTabelu();
        }
        private void ResetujBojeDugmadi()
        {
            btnDashboard.BackColor = Color.FromArgb(32, 42, 68);
            btnNovaRezervacija.BackColor = Color.FromArgb(32, 42, 68);
            btnPregledRezervacija.BackColor = Color.FromArgb(32, 42, 68);
            btnGosti.BackColor = Color.FromArgb(32, 42, 68);
            btnHoteli.BackColor = Color.FromArgb(32, 42, 68);
            btnGradovi.BackColor = Color.FromArgb(32, 42, 68);
        }

        private void BtnDashboard_Click(object sender, EventArgs e)
        {
            ResetujBojeDugmadi();
            btnDashboard.BackColor = Color.FromArgb(54, 74, 114);
            lblNaslovAplikacije.Text = "Dashboard";
            PrikaziDashboard();
        }

        private void BtnNovaRezervacija_Click(object sender, EventArgs e)
        {
            ResetujBojeDugmadi();
            btnNovaRezervacija.BackColor = Color.FromArgb(54, 74, 114);
            lblNaslovAplikacije.Text = "Nova rezervacija";
            PrikaziStranicuPlaceholder("Nova rezervacija",
                "Ovde ćemo sledeće napraviti ceo flow:\n\n- unos datuma i broja gostiju\n- izbor hotela\n- izbor sobe\n- izbor postojećeg ili novog gosta\n- potvrda rezervacije");
        }

        private void BtnPregledRezervacija_Click(object sender, EventArgs e)
        {
            ResetujBojeDugmadi();
            btnPregledRezervacija.BackColor = Color.FromArgb(54, 74, 114);
            lblNaslovAplikacije.Text = "Pregled rezervacija";
            PrikaziStranicuPlaceholder("Pregled rezervacija",
                "Ovde ćemo napraviti tabelarni pregled rezervacija sa filtriranjem i detaljima.");
        }

        private void BtnGosti_Click(object sender, EventArgs e)
        {
            ResetujBojeDugmadi();
            btnGosti.BackColor = Color.FromArgb(54, 74, 114);
            lblNaslovAplikacije.Text = "Gosti";
            PrikaziStranicuPlaceholder("Gosti",
                "Ovde ćemo napraviti pregled gostiju i posebne forme za dodavanje i izmenu fizičkog i pravnog lica.");
        }

        private void BtnHoteli_Click(object sender, EventArgs e)
        {
            ResetujBojeDugmadi();
            btnHoteli.BackColor = Color.FromArgb(54, 74, 114);
            lblNaslovAplikacije.Text = "Hoteli";
            PrikaziStranicuHoteli();
        }

        private void BtnGradovi_Click(object sender, EventArgs e)
        {
            ResetujBojeDugmadi();
            btnGradovi.BackColor = Color.FromArgb(54, 74, 114);
            lblNaslovAplikacije.Text = "Gradovi";
            PrikaziStranicuGradovi();
        }
    }
}