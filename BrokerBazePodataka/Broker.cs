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
    }
}