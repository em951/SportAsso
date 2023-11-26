namespace SportAssovv.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Adherents",
                c => new
                    {
                        AdherentId = c.Int(nullable: false, identity: true),
                        Nom = c.String(nullable: false, maxLength: 64),
                        Prenom = c.String(nullable: false, maxLength: 64),
                        Adresse = c.String(nullable: false),
                        Email = c.String(nullable: false, maxLength: 64),
                        Telephone = c.String(nullable: false, maxLength: 20),
                        DateNaissance = c.DateTime(nullable: false),
                        MotDePasse = c.String(nullable: false, maxLength: 64),
                    })
                .PrimaryKey(t => t.AdherentId);
            
            CreateTable(
                "dbo.DisciplineAdherents",
                c => new
                    {
                        DisciplineId = c.Int(nullable: false),
                        AdherentId = c.Int(nullable: false),
                        Adherent_AdherentId = c.Int(),
                        Adherent_AdherentId1 = c.Int(),
                    })
                .PrimaryKey(t => new { t.DisciplineId, t.AdherentId })
                .ForeignKey("dbo.Adherents", t => t.AdherentId, cascadeDelete: true)
                .ForeignKey("dbo.Disciplines", t => t.DisciplineId, cascadeDelete: true)
                .ForeignKey("dbo.Adherents", t => t.Adherent_AdherentId)
                .ForeignKey("dbo.Adherents", t => t.Adherent_AdherentId1)
                .Index(t => t.DisciplineId)
                .Index(t => t.AdherentId)
                .Index(t => t.Adherent_AdherentId)
                .Index(t => t.Adherent_AdherentId1);
            
            CreateTable(
                "dbo.Disciplines",
                c => new
                    {
                        DisciplineId = c.Int(nullable: false, identity: true),
                        NomDiscipline = c.String(nullable: false, maxLength: 64),
                    })
                .PrimaryKey(t => t.DisciplineId);
            
            CreateTable(
                "dbo.DisciplineSections",
                c => new
                    {
                        DisciplineId = c.Int(nullable: false),
                        SectionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DisciplineId, t.SectionId })
                .ForeignKey("dbo.Sections", t => t.SectionId, cascadeDelete: true)
                .ForeignKey("dbo.Disciplines", t => t.DisciplineId, cascadeDelete: true)
                .Index(t => t.DisciplineId)
                .Index(t => t.SectionId);
            
            CreateTable(
                "dbo.Creneaux",
                c => new
                    {
                        CreneauHoraireId = c.Int(nullable: false, identity: true),
                        Jour = c.DateTime(nullable: false),
                        HeureDebut = c.Time(nullable: false, precision: 7),
                        HeureFin = c.Time(nullable: false, precision: 7),
                        PlacesDisponibles = c.Int(nullable: false),
                        SectionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CreneauHoraireId)
                .ForeignKey("dbo.Sections", t => t.SectionId, cascadeDelete: true)
                .Index(t => t.SectionId);
            
            CreateTable(
                "dbo.Sections",
                c => new
                    {
                        SectionId = c.Int(nullable: false, identity: true),
                        NomSection = c.String(nullable: false, maxLength: 64),
                    })
                .PrimaryKey(t => t.SectionId);
            
            CreateTable(
                "dbo.DossierInscriptions",
                c => new
                    {
                        DossierId = c.Int(nullable: false, identity: true),
                        StatutInscription = c.String(nullable: false, maxLength: 64),
                        AdherentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DossierId)
                .ForeignKey("dbo.Adherents", t => t.AdherentId, cascadeDelete: true)
                .Index(t => t.AdherentId);
            
            CreateTable(
                "dbo.Paiements",
                c => new
                    {
                        PaiementId = c.Int(nullable: false, identity: true),
                        MontantPaye = c.Int(nullable: false),
                        DatePaiement = c.DateTime(nullable: false),
                        StatutPaiement = c.String(nullable: false, maxLength: 64),
                        DossierInscriptionDossierId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PaiementId)
                .ForeignKey("dbo.DossierInscriptions", t => t.DossierInscriptionDossierId, cascadeDelete: true)
                .Index(t => t.DossierInscriptionDossierId);


        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Paiements", "DossierInscriptionDossierId", "dbo.DossierInscriptions");
            DropForeignKey("dbo.DossierInscriptions", "AdherentId", "dbo.Adherents");
            DropForeignKey("dbo.DisciplineAdherents", "Adherent_AdherentId1", "dbo.Adherents");
            DropForeignKey("dbo.DisciplineAdherents", "Adherent_AdherentId", "dbo.Adherents");
            DropForeignKey("dbo.DisciplineSections", "DisciplineId", "dbo.Disciplines");
            DropForeignKey("dbo.DisciplineSections", "SectionId", "dbo.Sections");
            DropForeignKey("dbo.Creneaux", "SectionId", "dbo.Sections");
            DropForeignKey("dbo.CreneauxDisciplineSections", new[] { "DisciplineSection_DisciplineId", "DisciplineSection_SectionId" }, "dbo.DisciplineSections");
            DropForeignKey("dbo.CreneauxDisciplineSections", "Creneaux_CreneauHoraireId", "dbo.Creneaux");
            DropForeignKey("dbo.DisciplineAdherents", "DisciplineId", "dbo.Disciplines");
            DropForeignKey("dbo.DisciplineAdherents", "AdherentId", "dbo.Adherents");
            DropIndex("dbo.CreneauxDisciplineSections", new[] { "DisciplineSection_DisciplineId", "DisciplineSection_SectionId" });
            DropIndex("dbo.CreneauxDisciplineSections", new[] { "Creneaux_CreneauHoraireId" });
            DropIndex("dbo.Paiements", new[] { "DossierInscriptionDossierId" });
            DropIndex("dbo.DossierInscriptions", new[] { "AdherentId" });
            DropIndex("dbo.Creneaux", new[] { "SectionId" });
            DropIndex("dbo.DisciplineSections", new[] { "SectionId" });
            DropIndex("dbo.DisciplineSections", new[] { "DisciplineId" });
            DropIndex("dbo.DisciplineAdherents", new[] { "Adherent_AdherentId1" });
            DropIndex("dbo.DisciplineAdherents", new[] { "Adherent_AdherentId" });
            DropIndex("dbo.DisciplineAdherents", new[] { "AdherentId" });
            DropIndex("dbo.DisciplineAdherents", new[] { "DisciplineId" });
            DropTable("dbo.CreneauxDisciplineSections");
            DropTable("dbo.Paiements");
            DropTable("dbo.DossierInscriptions");
            DropTable("dbo.Sections");
            DropTable("dbo.Creneaux");
            DropTable("dbo.DisciplineSections");
            DropTable("dbo.Disciplines");
            DropTable("dbo.DisciplineAdherents");
            DropTable("dbo.Adherents");

            // Supprime les clés étrangères associées à CreneauxDisciplineSections
            DropForeignKey("dbo.CreneauxDisciplineSections", "Creneaux_CreneauHoraireId", "dbo.Creneaux");
            DropForeignKey("dbo.CreneauxDisciplineSections", new[] { "DisciplineSection_DisciplineId", "DisciplineSection_SectionId" }, "dbo.DisciplineSections");

            // Supprime les index associés à CreneauxDisciplineSections
            DropIndex("dbo.CreneauxDisciplineSections", new[] { "Creneaux_CreneauHoraireId" });
            DropIndex("dbo.CreneauxDisciplineSections", new[] { "DisciplineSection_DisciplineId", "DisciplineSection_SectionId" });

            // Supprime la table CreneauxDisciplineSections
            DropTable("dbo.CreneauxDisciplineSections");
        }
    }
}
