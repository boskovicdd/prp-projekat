namespace Domen
{
    public class Hotel
    {
        public int HotelId { get; set; }
        public string Naziv { get; set; }
        public Grad Grad { get; set; }
        public byte BrojZvezdica { get; set; }
        public string Adresa { get; set; }

        public override string ToString()
        {
            return $"{Adresa} ({BrojZvezdica}★, {Grad?.Naziv})";
        }
    }
}