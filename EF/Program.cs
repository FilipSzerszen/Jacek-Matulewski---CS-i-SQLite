using System;
using EF;


namespace EF
{
    class Program
    {
        static void Main(string[] args)
        {
            Adres adres1 = new Adres()
            {
                Id = 1,
                Miasto = "Wrocław",
                Ulica = "Borowska",
                NumerDomu = 38,
                NumerMieszkania = 10
            };
            Adres adres2 = new Adres()
            {
                Id = 2,
                Miasto = "Wrocław",
                Ulica = "Borowska",
                NumerDomu = 38,
                NumerMieszkania = 10
            };
            Console.WriteLine(adres1.Equals(adres2));
        }

    }
}
