namespace Domen
{
    public class Grad
    {
        public int GradId { get; set; }
        public string Naziv { get; set; }
        public string Drzava { get; set; }

        public override string ToString()
        {
            return $"{Naziv}, {Drzava}";
        }
    }
}