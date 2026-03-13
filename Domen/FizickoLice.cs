namespace Domen
{
    public class FizickoLice : Gost
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string BrojDokumentacije { get; set; }

        public override string ToString()
        {
            return $"{Ime} {Prezime}";
        }
    }
}