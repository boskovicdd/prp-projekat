using BrokerBazePodataka;
using Domen;
using System;
using System.Collections.Generic;
using System.Data;

namespace Kontroler
{
    public class Kontroler
    {
        private static Kontroler instance;

        private Kontroler()
        {
        }

        public static Kontroler Instance
        {
            get
            {
                if (instance == null)
                    instance = new Kontroler();
                return instance;
            }
        }

        private T ExecuteWithConnection<T>(Func<T> operation)
        {
            try
            {
                Broker.Instance.OpenConnection();
                return operation();
            }
            finally
            {
                Broker.Instance.CloseConnection();
            }
        }

        private void ExecuteWithConnection(Action operation)
        {
            try
            {
                Broker.Instance.OpenConnection();
                operation();
            }
            finally
            {
                Broker.Instance.CloseConnection();
            }
        }

        public List<Grad> DohvatiSveGradove()
        {
            return ExecuteWithConnection(() => Broker.Instance.DohvatiSveGradove());
        }

        public void UnesiGrad(Grad grad)
        {
            ExecuteWithConnection(() => Broker.Instance.UnesiGrad(grad));
        }

        public void IzmeniGrad(Grad grad)
        {
            ExecuteWithConnection(() => Broker.Instance.IzmeniGrad(grad));
        }

        public void ObrisiGrad(int gradId)
        {
            ExecuteWithConnection(() => Broker.Instance.ObrisiGrad(gradId));
        }

        public List<Hotel> DohvatiSveHotele()
        {
            return ExecuteWithConnection(() => Broker.Instance.DohvatiSveHotele());
        }

        public List<Soba> DohvatiSobeZaHotel(int hotelId)
        {
            return ExecuteWithConnection(() => Broker.Instance.DohvatiSobeZaHotel(hotelId));
        }

        public void UnesiHotelSaSobama(Hotel hotel, List<Soba> sobe)
        {
            ExecuteWithConnection(() => Broker.Instance.UnesiHotelSaSobama(hotel, sobe));
        }

        public void IzmeniHotelSaSobama(Hotel hotel, List<Soba> sobe)
        {
            ExecuteWithConnection(() => Broker.Instance.IzmeniHotelSaSobama(hotel, sobe));
        }

        public void ObrisiHotel(int hotelId)
        {
            ExecuteWithConnection(() => Broker.Instance.ObrisiHotel(hotelId));
        }
        public List<Gost> DohvatiSveGoste()
        {
            return ExecuteWithConnection(() => Broker.Instance.DohvatiSveGoste());
        }

        public List<Gost> PretraziGoste(string tipGosta, string kriterijum)
        {
            return ExecuteWithConnection(() => Broker.Instance.PretraziGoste(tipGosta, kriterijum));
        }

        public void UnesiFizickoLice(FizickoLice fizickoLice)
        {
            ExecuteWithConnection(() => Broker.Instance.UnesiFizickoLice(fizickoLice));
        }

        public void UnesiPravnoLice(PravnoLice pravnoLice)
        {
            ExecuteWithConnection(() => Broker.Instance.UnesiPravnoLice(pravnoLice));
        }

        public FizickoLice DohvatiFizickoLicePoId(int gostId)
        {
            return ExecuteWithConnection(() => Broker.Instance.DohvatiFizickoLicePoId(gostId));
        }

        public PravnoLice DohvatiPravnoLicePoId(int gostId)
        {
            return ExecuteWithConnection(() => Broker.Instance.DohvatiPravnoLicePoId(gostId));
        }

        public void IzmeniFizickoLice(FizickoLice fizickoLice)
        {
            ExecuteWithConnection(() => Broker.Instance.IzmeniFizickoLice(fizickoLice));
        }

        public void IzmeniPravnoLice(PravnoLice pravnoLice)
        {
            ExecuteWithConnection(() => Broker.Instance.IzmeniPravnoLice(pravnoLice));
        }
        public void ObrisiGosta(int gostId)
        {
            ExecuteWithConnection(() => Broker.Instance.ObrisiGosta(gostId));
        }
        public DataTable DohvatiSveRezervacije()
        {
            return ExecuteWithConnection(() => Broker.Instance.DohvatiSveRezervacije());
        }
        

        public void IzmeniRezervaciju(int brojRezervacije, DateTime datumOd, DateTime datumDo)
        {
            ExecuteWithConnection(() => Broker.Instance.IzmeniRezervaciju(brojRezervacije, datumOd, datumDo));
        }

        public void ObrisiRezervaciju(int brojRezervacije)
        {
            ExecuteWithConnection(() => Broker.Instance.ObrisiRezervaciju(brojRezervacije));
        }

        public DataTable DohvatiDostupneHotele(int gradId, DateTime datumOd, int brojNoci, int brojGostiju)
        {
            return ExecuteWithConnection(() => Broker.Instance.DohvatiDostupneHotele(gradId, datumOd, brojNoci, brojGostiju));
        }

        public DataTable DohvatiDostupneSobeZaHotel(int hotelId, DateTime datumOd, int brojNoci, int brojGostiju)
        {
            return ExecuteWithConnection(() => Broker.Instance.DohvatiDostupneSobeZaHotel(hotelId, datumOd, brojNoci, brojGostiju));
        }

        public void NapraviRezervaciju(int hotelId, int brojSobe, int gostId, DateTime datumOd, DateTime datumDo, int brojGostiju)
        {
            ExecuteWithConnection(() => Broker.Instance.NapraviRezervaciju(hotelId, brojSobe, gostId, datumOd, datumDo, brojGostiju));
        }
    }
}