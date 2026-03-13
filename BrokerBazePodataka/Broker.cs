using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Domen;

namespace BrokerBazePodataka
{
    public class Broker
    {
        private DbConnection connection;
        private static Broker instance;

        private Broker()
        {
            connection = new DbConnection();
        }

        public static Broker Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Broker();
                }
                return instance;
            }
        }

        public void OpenConnection()
        {
            connection.OpenConnection();
        }

        public void CloseConnection()
        {
            connection.CloseConnection();
        }

        public void BeginTransaction()
        {
            connection.BeginTransaction();
        }

        public void Commit()
        {
            connection.Commit();
        }

        public void Rollback()
        {
            connection.Rollback();
        }

        public List<Grad> DohvatiSveGradove()
        {
            List<Grad> gradovi = new List<Grad>();

            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "api.DohvatiSveGradove";
                command.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Grad grad = new Grad
                        {
                            GradId = (int)reader["GrID"],
                            Naziv = (string)reader["Naziv"],
                            Drzava = (string)reader["Drzava"]
                        };

                        gradovi.Add(grad);
                    }
                }
            }

            return gradovi;
        }

        public void UnesiGrad(Grad grad)
        {
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "api.UnetiGrad";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@GrID", grad.GradId);
                command.Parameters.AddWithValue("@Naziv", grad.Naziv);
                command.Parameters.AddWithValue("@Drzava", grad.Drzava);

                command.ExecuteNonQuery();
            }
        }

        public void IzmeniGrad(Grad grad)
        {
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "api.IzmeniGrad";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@GrID", grad.GradId);
                command.Parameters.AddWithValue("@Naziv", grad.Naziv);
                command.Parameters.AddWithValue("@Drzava", grad.Drzava);

                command.ExecuteNonQuery();
            }
        }

        public void ObrisiGrad(int gradId)
        {
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "api.ObrisiGrad";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@GrID", gradId);

                command.ExecuteNonQuery();
            }
        }
        public List<Hotel> DohvatiSveHotele()
        {
            List<Hotel> hoteli = new List<Hotel>();

            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "api.DohvatiSveHotele";
                command.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Hotel hotel = new Hotel
                        {
                            HotelId = (int)reader["HID"],
                            Naziv = (string)reader["Naziv"],
                            Grad = new Grad
                            {
                                GradId = (int)reader["GrID"],
                                Naziv = (string)reader["GradNaziv"],
                                Drzava = (string)reader["Drzava"]
                            },
                            BrojZvezdica = (byte)reader["BrojZvezdica"],
                            Adresa = (string)reader["Adresa"]
                        };

                        hoteli.Add(hotel);
                    }
                }
            }

            return hoteli;
        }

        public List<Soba> DohvatiSobeZaHotel(int hotelId)
        {
            List<Soba> sobe = new List<Soba>();

            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "api.DohvatiSobeZaHotel";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@HID", hotelId);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Soba soba = new Soba
                        {
                            Hotel = new Hotel { HotelId = hotelId },
                            BrojSobe = (int)reader["BrojSobe"],
                            CenaPoNoci = (decimal)reader["CenaPoNoci"],
                            BrojKreveta = (int)reader["BrojKreveta"]
                        };

                        sobe.Add(soba);
                    }
                }
            }

            return sobe;
        }

        public void UnesiHotelSaSobama(Hotel hotel, List<Soba> sobe)
        {
            try
            {
                BeginTransaction();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "api.UnetiHotel";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@HID", hotel.HotelId);
                    command.Parameters.AddWithValue("@GrID", hotel.Grad.GradId);
                    command.Parameters.AddWithValue("@Naziv", hotel.Naziv);
                    command.Parameters.AddWithValue("@BrojZvezdica", hotel.BrojZvezdica);
                    command.Parameters.AddWithValue("@Adresa", hotel.Adresa);
                    command.ExecuteNonQuery();
                }

                foreach (Soba soba in sobe)
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "api.UnetiSobu";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@HID", hotel.HotelId);
                        command.Parameters.AddWithValue("@BrojSobe", soba.BrojSobe);
                        command.Parameters.AddWithValue("@CenaPoNoci", soba.CenaPoNoci);
                        command.Parameters.AddWithValue("@BrojKreveta", soba.BrojKreveta);
                        command.ExecuteNonQuery();
                    }
                }

                Commit();
            }
            catch
            {
                Rollback();
                throw;
            }
        }

        public void IzmeniHotelSaSobama(Hotel hotel, List<Soba> sobe)
        {
            try
            {
                BeginTransaction();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "api.IzmeniHotel";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@HID", hotel.HotelId);
                    command.Parameters.AddWithValue("@GrID", hotel.Grad.GradId);
                    command.Parameters.AddWithValue("@Naziv", hotel.Naziv);
                    command.Parameters.AddWithValue("@BrojZvezdica", hotel.BrojZvezdica);
                    command.Parameters.AddWithValue("@Adresa", hotel.Adresa);
                    command.ExecuteNonQuery();
                }

                List<Soba> postojeceSobe = DohvatiSobeZaHotel(hotel.HotelId);

                foreach (Soba soba in sobe)
                {
                    Soba postojeca = postojeceSobe.FirstOrDefault(s => s.BrojSobe == soba.BrojSobe);

                    if (postojeca == null)
                    {
                        using (SqlCommand command = connection.CreateCommand())
                        {
                            command.CommandText = "api.UnetiSobu";
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@HID", hotel.HotelId);
                            command.Parameters.AddWithValue("@BrojSobe", soba.BrojSobe);
                            command.Parameters.AddWithValue("@CenaPoNoci", soba.CenaPoNoci);
                            command.Parameters.AddWithValue("@BrojKreveta", soba.BrojKreveta);
                            command.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        using (SqlCommand command = connection.CreateCommand())
                        {
                            command.CommandText = "api.IzmeniSobu";
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@HID", hotel.HotelId);
                            command.Parameters.AddWithValue("@BrojSobe", soba.BrojSobe);
                            command.Parameters.AddWithValue("@NovaCena", soba.CenaPoNoci);
                            command.Parameters.AddWithValue("@NoviBrojKreveta", soba.BrojKreveta);
                            command.ExecuteNonQuery();
                        }
                    }
                }

                foreach (Soba postojecaSoba in postojeceSobe)
                {
                    bool iDaljePostoji = sobe.Any(s => s.BrojSobe == postojecaSoba.BrojSobe);

                    if (!iDaljePostoji)
                    {
                        using (SqlCommand command = connection.CreateCommand())
                        {
                            command.CommandText = "api.ObrisiSobu";
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@HID", hotel.HotelId);
                            command.Parameters.AddWithValue("@BrojSobe", postojecaSoba.BrojSobe);
                            command.ExecuteNonQuery();
                        }
                    }
                }

                Commit();
            }
            catch
            {
                Rollback();
                throw;
            }
        }

        public void ObrisiHotel(int hotelId)
        {
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "api.ObrisiHotel";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@HID", hotelId);
                command.ExecuteNonQuery();
            }
        }

        public List<Gost> DohvatiSveGoste()
        {
            List<Gost> gosti = new List<Gost>();

            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "api.DohvatiSveGoste";
                command.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string vrstaGosta = reader["VrstaGosta"].ToString();

                        if (vrstaGosta == "Fizičko lice")
                        {
                            string puniNaziv = reader["PuniNaziv"].ToString();
                            string[] delovi = puniNaziv.Split(' ', 2);

                            FizickoLice fizicko = new FizickoLice
                            {
                                GostId = (int)reader["GID"],
                                Email = reader["Email"]?.ToString(),
                                Telefon = reader["Telefon"]?.ToString(),
                                Ime = delovi.Length > 0 ? delovi[0] : "",
                                Prezime = delovi.Length > 1 ? delovi[1] : "",
                                BrojDokumentacije = reader["IdentifikacioniBroj"].ToString()
                            };

                            gosti.Add(fizicko);
                        }
                        else
                        {
                            PravnoLice pravno = new PravnoLice
                            {
                                GostId = (int)reader["GID"],
                                Email = reader["Email"]?.ToString(),
                                Telefon = reader["Telefon"]?.ToString(),
                                NazivFirme = reader["PuniNaziv"].ToString(),
                                PIB = reader["IdentifikacioniBroj"].ToString()
                            };

                            gosti.Add(pravno);
                        }
                    }
                }
            }

            return gosti;
        }

        public List<Gost> PretraziGoste(string tipGosta, string kriterijum)
        {
            List<Gost> gosti = new List<Gost>();

            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "api.PretraziGoste";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@TipGosta", tipGosta ?? "Svi");
                command.Parameters.AddWithValue("@Kriterijum", kriterijum ?? "");

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string vrstaGosta = reader["VrstaGosta"].ToString();

                        if (vrstaGosta == "Fizičko lice")
                        {
                            string puniNaziv = reader["PuniNaziv"].ToString();
                            string[] delovi = puniNaziv.Split(' ', 2);

                            FizickoLice fizicko = new FizickoLice
                            {
                                GostId = (int)reader["GID"],
                                Email = reader["Email"]?.ToString(),
                                Telefon = reader["Telefon"]?.ToString(),
                                Ime = delovi.Length > 0 ? delovi[0] : "",
                                Prezime = delovi.Length > 1 ? delovi[1] : "",
                                BrojDokumentacije = reader["IdentifikacioniBroj"].ToString()
                            };

                            gosti.Add(fizicko);
                        }
                        else
                        {
                            PravnoLice pravno = new PravnoLice
                            {
                                GostId = (int)reader["GID"],
                                Email = reader["Email"]?.ToString(),
                                Telefon = reader["Telefon"]?.ToString(),
                                NazivFirme = reader["PuniNaziv"].ToString(),
                                PIB = reader["IdentifikacioniBroj"].ToString()
                            };

                            gosti.Add(pravno);
                        }
                    }
                }
            }

            return gosti;
        }

        public void UnesiFizickoLice(FizickoLice fizickoLice)
        {
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "api.UnetiFizickoLice";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@GID", fizickoLice.GostId);
                command.Parameters.AddWithValue("@Email", (object?)fizickoLice.Email ?? DBNull.Value);
                command.Parameters.AddWithValue("@Telefon", (object?)fizickoLice.Telefon ?? DBNull.Value);
                command.Parameters.AddWithValue("@Ime", fizickoLice.Ime);
                command.Parameters.AddWithValue("@Prezime", fizickoLice.Prezime);
                command.Parameters.AddWithValue("@BrojDokumentacije", fizickoLice.BrojDokumentacije);

                command.ExecuteNonQuery();
            }
        }

        public void UnesiPravnoLice(PravnoLice pravnoLice)
        {
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "api.UnetiPravnoLice";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@GID", pravnoLice.GostId);
                command.Parameters.AddWithValue("@Email", (object?)pravnoLice.Email ?? DBNull.Value);
                command.Parameters.AddWithValue("@Telefon", (object?)pravnoLice.Telefon ?? DBNull.Value);
                command.Parameters.AddWithValue("@PIB", pravnoLice.PIB);
                command.Parameters.AddWithValue("@MB", pravnoLice.MB);
                command.Parameters.AddWithValue("@NazivFirme", pravnoLice.NazivFirme);

                command.ExecuteNonQuery();
            }
        }

        public FizickoLice DohvatiFizickoLicePoId(int gostId)
        {
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "api.DohvatiFizickoLicePoId";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@GID", gostId);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new FizickoLice
                        {
                            GostId = (int)reader["GID"],
                            Email = reader["Email"]?.ToString(),
                            Telefon = reader["Telefon"]?.ToString(),
                            Ime = reader["Ime"].ToString(),
                            Prezime = reader["Prezime"].ToString(),
                            BrojDokumentacije = reader["BrojDokumentacije"].ToString()
                        };
                    }
                }
            }

            return null;
        }

        public PravnoLice DohvatiPravnoLicePoId(int gostId)
        {
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "api.DohvatiPravnoLicePoId";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@GID", gostId);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new PravnoLice
                        {
                            GostId = (int)reader["GID"],
                            Email = reader["Email"]?.ToString(),
                            Telefon = reader["Telefon"]?.ToString(),
                            PIB = reader["PIB"].ToString(),
                            MB = reader["MB"].ToString(),
                            NazivFirme = reader["NazivFirme"].ToString()
                        };
                    }
                }
            }

            return null;
        }

        public void IzmeniFizickoLice(FizickoLice fizickoLice)
        {
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "api.IzmeniFizickoLice";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@GID", fizickoLice.GostId);
                command.Parameters.AddWithValue("@Email", (object?)fizickoLice.Email ?? DBNull.Value);
                command.Parameters.AddWithValue("@Telefon", (object?)fizickoLice.Telefon ?? DBNull.Value);
                command.Parameters.AddWithValue("@Ime", fizickoLice.Ime);
                command.Parameters.AddWithValue("@Prezime", fizickoLice.Prezime);
                command.Parameters.AddWithValue("@BrojDokumentacije", fizickoLice.BrojDokumentacije);

                command.ExecuteNonQuery();
            }
        }

        public void IzmeniPravnoLice(PravnoLice pravnoLice)
        {
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "api.IzmeniPravnoLice";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@GID", pravnoLice.GostId);
                command.Parameters.AddWithValue("@Email", (object?)pravnoLice.Email ?? DBNull.Value);
                command.Parameters.AddWithValue("@Telefon", (object?)pravnoLice.Telefon ?? DBNull.Value);
                command.Parameters.AddWithValue("@PIB", pravnoLice.PIB);
                command.Parameters.AddWithValue("@MB", pravnoLice.MB);
                command.Parameters.AddWithValue("@NazivFirme", pravnoLice.NazivFirme);

                command.ExecuteNonQuery();
            }
        }
        public void ObrisiGosta(int gostId)
        {
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "api.ObrisiGosta";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@GID", gostId);

                command.ExecuteNonQuery();
            }
        }
        public DataTable DohvatiSveRezervacije()
        {
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "api.DohvatiSveRezervacije";
                command.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);

                return table;
            }
        }
        

        public void IzmeniRezervaciju(int brojRezervacije, DateTime datumOd, DateTime datumDo)
        {
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "api.IzmeniRezervaciju";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@BrojRezervacije", brojRezervacije);
                command.Parameters.AddWithValue("@NoviDatumOd", datumOd);
                command.Parameters.AddWithValue("@NoviDatumDo", datumDo);

                command.ExecuteNonQuery();
            }
        }

        public void ObrisiRezervaciju(int brojRezervacije)
        {
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "api.OtkaziRezervaciju";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@BrojRezervacije", brojRezervacije);

                command.ExecuteNonQuery();
            }
        }

        public DataTable DohvatiDostupneHotele(int gradId, DateTime datumOd, int brojNoci, int brojGostiju)
        {
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "api.DohvatiDostupneHotele";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@GrID", gradId);
                command.Parameters.AddWithValue("@DatumOd", datumOd);
                command.Parameters.AddWithValue("@BrojNoci", brojNoci);
                command.Parameters.AddWithValue("@BrojGostiju", brojGostiju);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable tabela = new DataTable();
                adapter.Fill(tabela);

                return tabela;
            }
        }

        public DataTable DohvatiDostupneSobeZaHotel(int hotelId, DateTime datumOd, int brojNoci, int brojGostiju)
        {
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "api.DohvatiDostupneSobeZaHotel";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@HID", hotelId);
                command.Parameters.AddWithValue("@DatumOd", datumOd);
                command.Parameters.AddWithValue("@BrojNoci", brojNoci);
                command.Parameters.AddWithValue("@BrojGostiju", brojGostiju);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable tabela = new DataTable();
                adapter.Fill(tabela);

                return tabela;
            }
        }

        public void NapraviRezervaciju(int hotelId, int brojSobe, int gostId, DateTime datumOd, DateTime datumDo, int brojGostiju)
        {
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "api.NapravitiRezervacijuAuto";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@HID", hotelId);
                command.Parameters.AddWithValue("@BrojSobe", brojSobe);
                command.Parameters.AddWithValue("@GID", gostId);
                command.Parameters.AddWithValue("@DatumOd", datumOd);
                command.Parameters.AddWithValue("@DatumDo", datumDo);
                command.Parameters.AddWithValue("@BrojGostiju", brojGostiju);

                command.ExecuteNonQuery();
            }
        }
    }
}