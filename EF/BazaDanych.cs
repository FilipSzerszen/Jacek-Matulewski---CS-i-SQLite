using Microsoft.EntityFrameworkCore;

namespace EF
{
    public class BazaDanychOsóbDBContext : DbContext
    {
        public DbSet<Osoba> Osoby { get; set; }
        public DbSet<Adres> Adresy { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=osoby.db");
        }
    }
    public class BazaDanychOsób : IDisposable
    {
        private BazaDanychOsóbDBContext dbc = new BazaDanychOsóbDBContext();
        public BazaDanychOsób()
        {
            dbc.Database.EnsureCreated();
        }
        public void Dispose()
        {
            dbc.Dispose();
        }
#if DEBUG
        public Osoba[] Osoby { get => dbc.Osoby.ToArray(); }
        public Adres[] Adresy { get => dbc.Adresy.ToArray(); }
#endif
        public Osoba? PobierzOsobę(int idOsoby)
        {
            return dbc.Osoby.FirstOrDefault(o => o.Id == idOsoby);
        }
        public Osoba? this[int idOsoby]
        {
            get => PobierzOsobę(idOsoby);
            set => ZmieńDaneOsoby(idOsoby, value);
        }
        public int[] IdentyfikatoryOsób
        {
            get
            {
                return dbc.Osoby.Select(o => o.Id).ToArray();
            }
        }
        public int DodajOsobę(Osoba osoba)
        {
            if (osoba == null) throw new ArgumentNullException("Podano pustą referencję jako argument");

            if (osoba.Adres == null) throw new ArgumentNullException("Osoba nie ma adresu, nie jest zameldowana");

            if (dbc.Osoby.ToArray().Any(o => o.Equals(osoba))) return osoba.Id;
            else  if (dbc.Osoby.Any(o => o.Id == osoba.Id)) osoba.Id = dbc.Osoby.Max(o => o.Id) + 1; 

            Adres adres = dbc.Adresy.ToArray().FirstOrDefault(a=>a.Equals(osoba.Adres));
            if (adres != null) osoba.Adres = adres;

            dbc.Osoby.Add(osoba);
            dbc.SaveChanges();
            return osoba.Id;
        }
        private int[] PobierzIdentyfikatoryUżywanychAdresów()
        {
            return dbc.Osoby.Select(o => o.Adres.Id).Distinct().ToArray();
        }
        private Adres[] PobierzNieużywaneAdresy()
        {
            int[] używaneIdentyfikatoryAdresów = PobierzIdentyfikatoryUżywanychAdresów();
            List<Adres> nieużywaneAdresy = new List<Adres>();
            foreach(Adres adres in dbc.Adresy)
            {
                if (!używaneIdentyfikatoryAdresów.Contains(adres.Id)) nieużywaneAdresy.Add(adres);
            }
            return nieużywaneAdresy.ToArray();
        }
        private void usuńNieużywaneAdresy()
        {
            dbc.Adresy.RemoveRange(PobierzNieużywaneAdresy());
            dbc.SaveChanges();
        }
        public void UsuńOsobę(int idOsoby)
        {
            Osoba osoba = PobierzOsobę(idOsoby);
            if (osoba != null)
            {
                dbc.Osoby.Remove(osoba);
                dbc.SaveChanges();
                usuńNieużywaneAdresy();
            }
        }
        public void ZmieńDaneOsoby(int idOsoby,Osoba noweDane)
        {
            Osoba osoba = PobierzOsobę(idOsoby);
            if (osoba == null) throw new Exception("Nie ma osoby o podanym identyfikatorze");
            osoba.Imię = noweDane.Imię;
            osoba.Nazwisko = noweDane.Nazwisko;
            osoba.NumerTelefonu = noweDane.NumerTelefonu;
            osoba.Adres = noweDane.Adres;

            dbc.SaveChanges();
        }
    }
}
