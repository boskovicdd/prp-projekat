namespace Domen
{
    public class Soba
    {
        public Hotel Hotel { get; set; }
        public int BrojSobe { get; set; }
        public decimal CenaPoNoci { get; set; }
        public int BrojKreveta { get; set; }

        public override string ToString()
        {
            return $"Soba {BrojSobe} - {BrojKreveta} kreveta - {CenaPoNoci:N2} RSD/noć";
        }
    }
}