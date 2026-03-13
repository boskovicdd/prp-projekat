using System;
using System.Collections.Generic;
using Domen;
using BrokerBazePodataka;

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
    }
}