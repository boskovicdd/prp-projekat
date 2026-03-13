namespace Domen
{
    public class Rezervacija
    {
        public int BrojRezervacije { get; set; }
        public Soba Soba { get; set; }
        public Gost Gost { get; set; }
        public DateTime DatumOd { get; set; }
        public DateTime DatumDo { get; set; }
        public int BrojNoci { get; set; }
        public int BrojGostiju { get; set; }
        public decimal UkupnaCena { get; set; }

        public override string ToString()
        {
            return $"Rezervacija #{BrojRezervacije} | {DatumOd:dd.MM.yyyy} - {DatumDo:dd.MM.yyyy}";
        }
    }
}