namespace Domen
{
    public class Grad
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Drzava { get; set; }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        public override string? ToString()
        {
            return Naziv + ", " + Drzava;
        }
    }
}
