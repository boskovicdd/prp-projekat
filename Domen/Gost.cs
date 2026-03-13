namespace Domen
{
    public class Gost
    {
        public int GostId { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }

        public override string ToString()
        {
            return $"{Email} / {Telefon}";
        }
    }
}