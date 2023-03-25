using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF
{
    public class Osoba
    {
        public int Id { get; set; }
        public string Imię { get; set; }
        public string Nazwisko { get; set; }
        public int? NumerTelefonu { get; set; }

        public Adres Adres { get; set; }

        public override string ToString()
        {
            return $"{Id}. {Imię} {Nazwisko} ({(NumerTelefonu.HasValue ? NumerTelefonu.Value.ToString() : "---")})";
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (!(obj is Osoba)) return false;
            Osoba innaOsoba = obj as Osoba;
            return
                Id == innaOsoba.Id &&
                Imię.Equals(innaOsoba.Imię) &&
                Nazwisko.Equals(innaOsoba.Nazwisko);
        }
        public override int GetHashCode()
        {
            return Id ^ Imię.GetHashCode() ^ Nazwisko.GetHashCode() ^ NumerTelefonu.GetHashCode();
        }

    }
    public class Adres
    {
        public int Id { get; set; }
        public string Miasto { get; set; }
        public string Ulica { get; set; }
        public int NumerDomu { get; set; }
        public int? NumerMieszkania { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (!(obj is Adres)) return false;
            Adres innyAdres = obj as Adres;
            return
                Miasto.Equals(innyAdres.Miasto) &&
                Ulica.Equals(innyAdres.Ulica) &&
                NumerDomu == innyAdres.NumerDomu &&
                NumerMieszkania == innyAdres.NumerMieszkania;

        }
        public override int GetHashCode()
        {
            return Id ^ Miasto.GetHashCode() ^ Ulica.GetHashCode() ^ NumerDomu.GetHashCode() ^ NumerMieszkania.GetHashCode();
        }
        public override string ToString()
        {
            return $"{Id}. {Miasto} {Ulica} {NumerDomu}/{NumerMieszkania}".TrimEnd('/');
        }
    }
}
