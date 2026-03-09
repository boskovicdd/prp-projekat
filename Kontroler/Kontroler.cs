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
        // public void ucitajSveSobe(int HotelId){
        //     ExecuteWithConnection(() => broker.Broker.Instance.UcitajSveSobe(HotelId));
        //  }
    }
}
