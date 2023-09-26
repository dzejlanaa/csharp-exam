
using DLWMS.WinForms.IB180156;
using System.Data.Entity;

namespace DLWMS.WinForms.DB
{

    //DLWMSContext
    public class KonekcijaNaBazu : DbContext
    {
        public KonekcijaNaBazu() : base("DLWMSPutanja")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Student>().ToTable("Studenti");
            modelBuilder.Entity<Predmeti>().ToTable("Predmeti");
            modelBuilder.Entity<StudentiPredmeti>().ToTable("StudentiPredmeti");
            modelBuilder.Entity<Spolovi>().ToTable("Spolovi");
            modelBuilder.Entity<StudentiKonsultacije>().ToTable("StudentiKonsultacije");
        }       
        public DbSet<Student> Studenti { get; set; }
        public DbSet<Predmeti> Predmeti { get; set; }

        public DbSet<StudentiPredmeti> StudentiPredmeti { get; set; }
        public DbSet<Spolovi> Spolovi { get; set; }
        public DbSet<StudentiKonsultacije> StudentiKonsultacije { get; set; }

    }
}