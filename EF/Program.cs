using System;
using EF;


namespace EF
{
    class Program
    {
        static void Main(string[] args)
        {
            string dbName = "osoby.db";
            if (File.Exists(dbName)) File.Delete(dbName);

            BazaDanychOsób db = new BazaDanychOsób();

            dodajPrzykładoweOsoby(db);
            podglądBazyDanych(db);

            pokażIdentyfikatoryOsób(db);
            pokażOsoby(db);



            db[1] = new Osoba() { Imię = "Jan", Nazwisko = "Nowak", Adres = new Adres { Miasto = "Toruń", Ulica = "Mostowa", NumerDomu = 3, NumerMieszkania = 5 } };
            podglądBazyDanych(db);

            int idUsuwanejOsoby = 3;
            Console.WriteLine($"Usuwam osobę numer {idUsuwanejOsoby}...");
            db.UsuńOsobę(idUsuwanejOsoby);
            podglądBazyDanych(db);
        }
        static void podglądBazyDanych(BazaDanychOsób db)
        {
            try
            {
                Console.WriteLine("Podgląd danych: ");
                Console.WriteLine("Osoby: ");
                foreach (Osoba osoba in db.Osoby) Console.WriteLine(osoba);
                Console.WriteLine("Adresy: ");
                foreach (Adres adres in db.Adresy) Console.WriteLine(adres);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Błąd podglądu bazy danych: " + ex.ToString());
            }

        }
        static void dodajPrzykładoweOsoby(BazaDanychOsób db)
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
                Ulica = "Bardzka",
                NumerDomu = 39,
                NumerMieszkania = 11
            };
            Osoba osoba1 = new Osoba()
            {
                Imię = "Marian",
                Nazwisko = "Paździoch",
                NumerTelefonu = 123456,
                Adres = adres1
            };
            Osoba osoba2 = new Osoba()
            {
                Imię = "Ferdynand",
                Nazwisko = "Kiepski",
                NumerTelefonu = 987654,
                Adres = adres2
            };
            Osoba osoba3 = new Osoba()
            {
                Imię = "Halina",
                Nazwisko = "Kiepska",
                NumerTelefonu = 3456789,
                Adres = adres2
            };
            db.DodajOsobę(osoba1);
            db.DodajOsobę(osoba2);
            db.DodajOsobę(osoba3);
        }
        static void pokażIdentyfikatoryOsób(BazaDanychOsób db)
        {
            string s = "Identyfikatory osób: ";
            foreach (int idOsoby in db.IdentyfikatoryOsób) s += idOsoby.ToString() + "; ";
            Console.WriteLine(s.TrimEnd(' ', ';'));
        }
        static void pokażOsoby(BazaDanychOsób db)
        {
            foreach (int idOsoby in db.IdentyfikatoryOsób)
            {
                Console.WriteLine(db[idOsoby].ToString());
            }
        }
    }
}
