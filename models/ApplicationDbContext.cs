using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using static System.Collections.Specialized.BitVector32;

namespace SportAssovv.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("MaConnexionSqlServer")
        {
      
        }

        public DbSet<Adherent> Adherents { get; set; }
        public DbSet<Creneaux> Creneaux { get; set; }
        public DbSet<Discipline> Disciplines { get; set; }
        public DbSet<DisciplineAdherent> DisciplineAdherents { get; set; }
        public DbSet<DisciplineSection> DisciplineSections { get; set; }
        public DbSet<DossierInscription> DossiersInscription { get; set; }
        public DbSet<Paiement> Paiements { get; set; }
        public DbSet<Section> Sections { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Configure la clé primaire composite pour DisciplineAdherent
            modelBuilder.Entity<DisciplineAdherent>().HasKey(da => new { da.DisciplineId, da.AdherentId });

            // Configure la clé primaire composite pour DisciplineSection
            modelBuilder.Entity<DisciplineSection>().HasKey(ds => new { ds.DisciplineId, ds.SectionId });

           
        }

    }
}