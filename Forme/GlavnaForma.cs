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
        private DataGridView dgvGosti;
        private DataGridView dgvRezervacije;
        private ComboBox cmbTipGostaFilter;
        private TextBox txtPretragaGostiju;
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

        private void PrikaziStranicuGosti()
        {
            panelSadrzaj.Controls.Clear();

            Panel kartica = new Panel
            {
                BackColor = Color.White,
                Size = new Size(1150, 620),
                Location = new Point(20, 20),
                BorderStyle = BorderStyle.FixedSingle
            };

            Label lblNaslov = new Label
            {
                Text = "Upravljanje gostima",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.FromArgb(32, 42, 68),
                AutoSize = true,
                Location = new Point(30, 25)
            };

            Label lblTip = new Label
            {
                Text = "Tip gosta:",
                Font = new Font("Segoe UI", 10),
                AutoSize = true,
                Location = new Point(32, 85)
            };

            cmbTipGostaFilter = new ComboBox
            {
                Location = new Point(32, 110),
                Size = new Size(200, 30),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbTipGostaFilter.Items.Add("Svi");
            cmbTipGostaFilter.Items.Add("Fizičko lice");
            cmbTipGostaFilter.Items.Add("Pravno lice");
            cmbTipGostaFilter.SelectedIndex = 0;

            Label lblPretraga = new Label
            {
                Text = "Pretraga:",
                Font = new Font("Segoe UI", 10),
                AutoSize = true,
                Location = new Point(255, 85)
            };

            txtPretragaGostiju = new TextBox
            {
                Location = new Point(255, 110),
                Size = new Size(280, 30)
            };

            Button btnPretrazi = new Button
            {
                Text = "Pretraži",
                Size = new Size(120, 40),
                Location = new Point(555, 103),
                BackColor = Color.FromArgb(32, 42, 68),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnPretrazi.FlatAppearance.BorderSize = 0;
            btnPretrazi.Click += BtnPretraziGoste_Click;

            Button btnPonisti = new Button
            {
                Text = "Poništi filter",
                Size = new Size(140, 40),
                Location = new Point(685, 103),
                BackColor = Color.Gray,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnPonisti.FlatAppearance.BorderSize = 0;
            btnPonisti.Click += BtnPonistiFilterGostiju_Click;

            Button btnDodajFizicko = new Button
            {
                Text = "Dodaj fizičko lice",
                Size = new Size(150, 42),
                Location = new Point(32, 165),
                BackColor = Color.FromArgb(32, 42, 68),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnDodajFizicko.FlatAppearance.BorderSize = 0;
            btnDodajFizicko.Click += BtnDodajFizickoLice_Click;

            Button btnDodajPravno = new Button
            {
                Text = "Dodaj pravno lice",
                Size = new Size(150, 42),
                Location = new Point(192, 165),
                BackColor = Color.FromArgb(54, 74, 114),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnDodajPravno.FlatAppearance.BorderSize = 0;
            btnDodajPravno.Click += BtnDodajPravnoLice_Click;

            Button btnIzmeni = new Button
            {
                Text = "Izmeni",
                Size = new Size(130, 42),
                Location = new Point(352, 165),
                BackColor = Color.FromArgb(70, 100, 140),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnIzmeni.FlatAppearance.BorderSize = 0;
            btnIzmeni.Click += BtnIzmeniGosta_Click;

            Button btnObrisi = new Button
            {
                Text = "Obriši",
                Size = new Size(130, 42),
                Location = new Point(492, 165),
                BackColor = Color.FromArgb(160, 50, 50),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnObrisi.FlatAppearance.BorderSize = 0;
            btnObrisi.Click += BtnObrisiGosta_Click;

            Button btnOsvezi = new Button
            {
                Text = "Osveži",
                Size = new Size(130, 42),
                Location = new Point(632, 165),
                BackColor = Color.Gray,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnOsvezi.FlatAppearance.BorderSize = 0;
            btnOsvezi.Click += BtnOsveziGoste_Click;

            dgvGosti = new DataGridView
            {
                Location = new Point(32, 230),
                Size = new Size(1080, 340),
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
            kartica.Controls.Add(lblTip);
            kartica.Controls.Add(cmbTipGostaFilter);
            kartica.Controls.Add(lblPretraga);
            kartica.Controls.Add(txtPretragaGostiju);
            kartica.Controls.Add(btnPretrazi);
            kartica.Controls.Add(btnPonisti);
            kartica.Controls.Add(btnDodajFizicko);
            kartica.Controls.Add(btnDodajPravno);
            kartica.Controls.Add(btnIzmeni);
            kartica.Controls.Add(btnObrisi);
            kartica.Controls.Add(btnOsvezi);
            kartica.Controls.Add(dgvGosti);

            panelSadrzaj.Controls.Add(kartica);

            UcitajGosteUTabelu();
        }

        private void UcitajGosteUTabelu()
        {
            try
            {
                var gosti = Kontroler.Kontroler.Instance.DohvatiSveGoste();

                dgvGosti.DataSource = null;
                dgvGosti.Columns.Clear();

                var podaciZaPrikaz = gosti.Select(g => new
                {
                    ID = g.GostId,
                    VrstaGosta = g is FizickoLice ? "Fizičko lice" : "Pravno lice",
                    Naziv = g is FizickoLice fl ? $"{fl.Ime} {fl.Prezime}" : ((PravnoLice)g).NazivFirme,
                    g.Email,
                    g.Telefon,
                    IdentifikacioniBroj = g is FizickoLice fl2 ? fl2.BrojDokumentacije : ((PravnoLice)g).PIB
                }).ToList();

                dgvGosti.DataSource = podaciZaPrikaz;

                dgvGosti.Columns["ID"].HeaderText = "ID";
                dgvGosti.Columns["VrstaGosta"].HeaderText = "Tip gosta";
                dgvGosti.Columns["Naziv"].HeaderText = "Naziv/Ime prezime";
                dgvGosti.Columns["Email"].HeaderText = "Email";
                dgvGosti.Columns["Telefon"].HeaderText = "Telefon";
                dgvGosti.Columns["IdentifikacioniBroj"].HeaderText = "Dokument / PIB";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška pri učitavanju gostiju: " + ex.Message);
            }
        }

        private Gost VratiSelektovanogGosta()
        {
            if (dgvGosti == null || dgvGosti.CurrentRow == null)
                return null;

            int gostId = Convert.ToInt32(dgvGosti.CurrentRow.Cells["ID"].Value);
            var gosti = Kontroler.Kontroler.Instance.DohvatiSveGoste();

            return gosti.FirstOrDefault(g => g.GostId == gostId);
        }

        private void BtnPretraziGoste_Click(object sender, EventArgs e)
        {
            try
            {
                string tip = cmbTipGostaFilter.SelectedItem?.ToString() ?? "Svi";
                string kriterijum = txtPretragaGostiju.Text.Trim();

                var gosti = Kontroler.Kontroler.Instance.PretraziGoste(tip, kriterijum);

                dgvGosti.DataSource = null;
                dgvGosti.Columns.Clear();

                var podaciZaPrikaz = gosti.Select(g => new
                {
                    ID = g.GostId,
                    VrstaGosta = g is FizickoLice ? "Fizičko lice" : "Pravno lice",
                    Naziv = g is FizickoLice fl ? $"{fl.Ime} {fl.Prezime}" : ((PravnoLice)g).NazivFirme,
                    g.Email,
                    g.Telefon,
                    IdentifikacioniBroj = g is FizickoLice fl2 ? fl2.BrojDokumentacije : ((PravnoLice)g).PIB
                }).ToList();

                dgvGosti.DataSource = podaciZaPrikaz;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška pri pretrazi gostiju: " + ex.Message);
            }
        }

        private void BtnPonistiFilterGostiju_Click(object sender, EventArgs e)
        {
            cmbTipGostaFilter.SelectedIndex = 0;
            txtPretragaGostiju.Text = "";
            UcitajGosteUTabelu();
        }

        private void BtnDodajFizickoLice_Click(object sender, EventArgs e)
        {
            FrmSacuvajFizickoLice forma = new FrmSacuvajFizickoLice();

            if (forma.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Kontroler.Kontroler.Instance.UnesiFizickoLice(forma.FizickoLiceZaCuvanje);
                    UcitajGosteUTabelu();
                    MessageBox.Show("Fizičko lice je uspešno dodato.");
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

        private void BtnDodajPravnoLice_Click(object sender, EventArgs e)
        {
            FrmSacuvajPravnoLice forma = new FrmSacuvajPravnoLice();

            if (forma.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Kontroler.Kontroler.Instance.UnesiPravnoLice(forma.PravnoLiceZaCuvanje);
                    UcitajGosteUTabelu();
                    MessageBox.Show("Pravno lice je uspešno dodato.");
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

        private void BtnIzmeniGosta_Click(object sender, EventArgs e)
        {
            Gost selektovaniGost = VratiSelektovanogGosta();

            if (selektovaniGost == null)
            {
                MessageBox.Show("Selektuj gosta kojeg želiš da izmeniš.");
                return;
            }

            try
            {
                if (selektovaniGost is FizickoLice)
                {
                    FizickoLice fizicko = Kontroler.Kontroler.Instance.DohvatiFizickoLicePoId(selektovaniGost.GostId);
                    FrmSacuvajFizickoLice forma = new FrmSacuvajFizickoLice(fizicko);

                    if (forma.ShowDialog() == DialogResult.OK)
                    {
                        Kontroler.Kontroler.Instance.IzmeniFizickoLice(forma.FizickoLiceZaCuvanje);
                        UcitajGosteUTabelu();
                        MessageBox.Show("Fizičko lice je uspešno izmenjeno.");
                    }
                }
                else if (selektovaniGost is PravnoLice)
                {
                    PravnoLice pravno = Kontroler.Kontroler.Instance.DohvatiPravnoLicePoId(selektovaniGost.GostId);
                    FrmSacuvajPravnoLice forma = new FrmSacuvajPravnoLice(pravno);

                    if (forma.ShowDialog() == DialogResult.OK)
                    {
                        Kontroler.Kontroler.Instance.IzmeniPravnoLice(forma.PravnoLiceZaCuvanje);
                        UcitajGosteUTabelu();
                        MessageBox.Show("Pravno lice je uspešno izmenjeno.");
                    }
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

        private void BtnObrisiGosta_Click(object sender, EventArgs e)
        {
            Gost selektovaniGost = VratiSelektovanogGosta();

            if (selektovaniGost == null)
            {
                MessageBox.Show("Selektuj gosta kojeg želiš da obrišeš.");
                return;
            }

            DialogResult rezultat = MessageBox.Show(
                $"Da li si siguran da želiš da obrišeš gosta sa ID = {selektovaniGost.GostId}?",
                "Potvrda brisanja",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (rezultat == DialogResult.Yes)
            {
                try
                {
                    Kontroler.Kontroler.Instance.ObrisiGosta(selektovaniGost.GostId);
                    UcitajGosteUTabelu();
                    MessageBox.Show("Gost je uspešno obrisan.");
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

        private void BtnOsveziGoste_Click(object sender, EventArgs e)
        {
            UcitajGosteUTabelu();
        }
        private void BtnOsveziRezervacije_Click(object sender, EventArgs e)
        {
            UcitajRezervacije();
        }

        private void BtnIzmeniRezervaciju_Click(object sender, EventArgs e)
        {
            if (dgvRezervacije.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selektuj rezervaciju.");
                return;
            }

            int brojRezervacije = Convert.ToInt32(
    dgvRezervacije.SelectedRows[0].Cells["BrojRezervacije"].Value
);

            DateTime datumOd = Convert.ToDateTime(
                dgvRezervacije.SelectedRows[0].Cells["DatumOd"].Value
            );

            int brojNoci = Convert.ToInt32(
                dgvRezervacije.SelectedRows[0].Cells["BrojNoci"].Value
            );

            FrmIzmeniRezervaciju forma =
                new FrmIzmeniRezervaciju(brojRezervacije, datumOd, brojNoci);

            if (forma.ShowDialog() == DialogResult.OK)
            {
                UcitajRezervacije();
            }
        }

        private void BtnObrisiRezervaciju_Click(object sender, EventArgs e)
        {
            if (dgvRezervacije.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selektuj rezervaciju.");
                return;
            }

            int brojRezervacije = Convert.ToInt32(
                dgvRezervacije.SelectedRows[0].Cells["BrojRezervacije"].Value
            );

            DialogResult rezultat = MessageBox.Show(
                "Da li ste sigurni da želite da obrišete rezervaciju?",
                "Potvrda",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (rezultat == DialogResult.Yes)
            {
                try
                {
                    Kontroler.Kontroler.Instance.ObrisiRezervaciju(brojRezervacije);

                    MessageBox.Show("Rezervacija obrisana.");
                    UcitajRezervacije();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void PrikaziStranicuRezervacije()
        {
            panelSadrzaj.Controls.Clear();

            Panel kartica = new Panel
            {
                BackColor = Color.White,
                Size = new Size(1450, 650),
                Location = new Point(20, 20),
                BorderStyle = BorderStyle.FixedSingle
            };

            Label lblNaslov = new Label
            {
                Text = "Pregled rezervacija",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.FromArgb(32, 42, 68),
                AutoSize = true,
                Location = new Point(30, 25)
            };

            Button btnNova = new Button
            {
                Text = "Nova rezervacija",
                Size = new Size(180, 42),
                Location = new Point(30, 85),
                BackColor = Color.FromArgb(32, 42, 68),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnNova.FlatAppearance.BorderSize = 0;
            btnNova.Click += BtnNovaRezervacija_Click;

            Button btnIzmeni = new Button
            {
                Text = "Izmeni datum",
                Size = new Size(160, 42),
                Location = new Point(220, 85),
                BackColor = Color.FromArgb(70, 100, 140),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnIzmeni.FlatAppearance.BorderSize = 0;
            btnIzmeni.Click += BtnIzmeniRezervaciju_Click;

            Button btnObrisi = new Button
            {
                Text = "Obriši",
                Size = new Size(140, 42),
                Location = new Point(390, 85),
                BackColor = Color.FromArgb(160, 50, 50),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnObrisi.FlatAppearance.BorderSize = 0;
            btnObrisi.Click += BtnObrisiRezervaciju_Click;

            Button btnOsvezi = new Button
            {
                Text = "Osveži",
                Size = new Size(140, 42),
                Location = new Point(540, 85),
                BackColor = Color.Gray,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnOsvezi.FlatAppearance.BorderSize = 0;
            btnOsvezi.Click += BtnOsveziRezervacije_Click;

            dgvRezervacije = new DataGridView
            {
                Location = new Point(30, 150),
                Size = new Size(1290, 450),
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells,
                RowHeadersVisible = false
            };

            kartica.Controls.Add(lblNaslov);
            kartica.Controls.Add(btnNova);
            kartica.Controls.Add(btnIzmeni);
            kartica.Controls.Add(btnObrisi);
            kartica.Controls.Add(btnOsvezi);
            kartica.Controls.Add(dgvRezervacije);

            panelSadrzaj.Controls.Add(kartica);

            UcitajRezervacije();
        }
        private void UcitajRezervacije()
        {
            try
            {
                var rezervacije = Kontroler.Kontroler.Instance.DohvatiSveRezervacije();

                dgvRezervacije.DataSource = null;
                dgvRezervacije.Columns.Clear();
                dgvRezervacije.DataSource = rezervacije;
                dgvRezervacije.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvRezervacije.Columns["BrojRezervacije"].HeaderText = "ID";
                dgvRezervacije.Columns["Grad"].HeaderText = "Grad";
                dgvRezervacije.Columns["Hotel"].HeaderText = "Hotel";
                dgvRezervacije.Columns["Zvezdice"].HeaderText = "Zvezdice";
                dgvRezervacije.Columns["BrojSobe"].HeaderText = "Soba";
                dgvRezervacije.Columns["ImeGosta"].HeaderText = "Naziv firme / Ime gosta";
                dgvRezervacije.Columns["VrstaGosta"].HeaderText = "Tip gosta";
                dgvRezervacije.Columns["DatumOd"].HeaderText = "Datum od";
                dgvRezervacije.Columns["DatumDo"].HeaderText = "Datum do";
                dgvRezervacije.Columns["BrojNoci"].HeaderText = "Broj noći";
                dgvRezervacije.Columns["UkupnaCena"].HeaderText = "Ukupna cena";

                dgvRezervacije.Columns["BrojRezervacije"].FillWeight = 12;
                dgvRezervacije.Columns["Grad"].FillWeight = 18;
                dgvRezervacije.Columns["Hotel"].FillWeight = 28;
                dgvRezervacije.Columns["Zvezdice"].FillWeight = 16;
                dgvRezervacije.Columns["BrojSobe"].FillWeight = 12;
                dgvRezervacije.Columns["ImeGosta"].FillWeight = 24;
                dgvRezervacije.Columns["VrstaGosta"].FillWeight = 18;
                dgvRezervacije.Columns["DatumOd"].FillWeight = 18;
                dgvRezervacije.Columns["DatumDo"].FillWeight = 18;
                dgvRezervacije.Columns["BrojNoci"].FillWeight = 14;
                dgvRezervacije.Columns["UkupnaCena"].FillWeight = 18;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
            FrmPretragaRezervacije formaPretraga = new FrmPretragaRezervacije();

            if (formaPretraga.ShowDialog() == DialogResult.OK)
            {
                FrmNovaRezervacija formaNova = new FrmNovaRezervacija(
                    formaPretraga.DatumOd,
                    formaPretraga.BrojNoci,
                    formaPretraga.IzabraniGrad,
                    formaPretraga.BrojGostiju
                );

                if (formaNova.ShowDialog() == DialogResult.OK)
                {
                    if (dgvRezervacije != null)
                        UcitajRezervacije();
                }
            }
        }

        private void BtnPregledRezervacija_Click(object sender, EventArgs e)
        {
            ResetujBojeDugmadi();
            btnPregledRezervacija.BackColor = Color.FromArgb(54, 74, 114);
            lblNaslovAplikacije.Text = "Pregled rezervacija";

            PrikaziStranicuRezervacije();
        }

        private void BtnGosti_Click(object sender, EventArgs e)
        {
            ResetujBojeDugmadi();
            btnGosti.BackColor = Color.FromArgb(54, 74, 114);
            lblNaslovAplikacije.Text = "Gosti";
            PrikaziStranicuGosti();
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