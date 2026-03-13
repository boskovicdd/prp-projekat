namespace Domen
{
    public class PravnoLice : Gost
    {
        public string PIB { get; set; }
        public string MB { get; set; }
        public string NazivFirme { get; set; }

        public override string ToString()
        {
            return $"{NazivFirme}";
        }
    }
}