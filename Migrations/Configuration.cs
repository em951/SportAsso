namespace SportAssovv.Migrations
{
    using SportAssovv.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SportAssovv.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SportAssovv.Models.ApplicationDbContext context)
        {
        context.Adherents.AddOrUpdate(
        new Adherent { Nom = "Jackson", Prenom = "Lise" },
        new Adherent { Nom = "Park", Prenom = "Seojun" }
         );

        }

    }

}
