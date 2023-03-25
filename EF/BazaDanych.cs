

using Microsoft.EntityFrameworkCore;

namespace EF
{
    public class BazaDanychOsóbDBContext : DbContext
    {
        public DbSet<Osoba> Osoby { get; set; }
        public DbSet<Adres> Adres { get; set; }
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
        public Adres[] Adresy { get => dbc.Adres.ToArray(); }
#endif
        public Osoba PobierzOsobę(int idOsoby)
        {
            return dbc.Osoby.FirstOrDefault(o => o.Id == idOsoby);
        }
        public Osoba this[int idOsoby]
        {
            get => PobierzOsobę(idOsoby);
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

            return 2;
        }
    }
}
