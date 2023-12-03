namespace SportAssovv.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangementModels : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CreneauxDisciplineSections", "Creneaux_CreneauHoraireId", "dbo.Creneaux");
            DropForeignKey("dbo.CreneauxDisciplineSections", new[] { "DisciplineSection_DisciplineId", "DisciplineSection_SectionId" }, "dbo.DisciplineSections");
            DropForeignKey("dbo.Creneaux", "SectionId", "dbo.Sections");
            DropIndex("dbo.CreneauxDisciplineSections", new[] { "Creneaux_CreneauHoraireId" });
            DropIndex("dbo.CreneauxDisciplineSections", new[] { "DisciplineSection_DisciplineId", "DisciplineSection_SectionId" });
            RenameColumn(table: "dbo.Paiements", name: "DossierInscriptionDossierId", newName: "DossierId");
            RenameIndex(table: "dbo.Paiements", name: "IX_DossierInscriptionDossierId", newName: "IX_DossierId");
            AddColumn("dbo.Disciplines", "PlacesDisponibles", c => c.Int(nullable: false));
            AddColumn("dbo.DisciplineSections", "DisciplineSectionId", c => c.Int(nullable: false));
            AddColumn("dbo.Creneaux", "JourHeureDebut", c => c.DateTime(nullable: false));
            AddColumn("dbo.Creneaux", "Section_SectionId", c => c.Int());
            AddColumn("dbo.Sections", "Creneaux_CreneauHoraireId", c => c.Int());
            AddColumn("dbo.Paiements", "AdherentId", c => c.Int(nullable: false));
            AlterColumn("dbo.Adherents", "DateNaissance", c => c.DateTime());
            AlterColumn("dbo.Adherents", "Role", c => c.String(maxLength: 64));
            CreateIndex("dbo.Sections", "Creneaux_CreneauHoraireId");
            CreateIndex("dbo.Creneaux", "Section_SectionId");
            CreateIndex("dbo.Paiements", "AdherentId");
            AddForeignKey("dbo.Sections", "Creneaux_CreneauHoraireId", "dbo.Creneaux", "CreneauHoraireId");
            AddForeignKey("dbo.Paiements", "AdherentId", "dbo.Adherents", "AdherentId", cascadeDelete: true);
            AddForeignKey("dbo.Creneaux", "Section_SectionId", "dbo.Sections", "SectionId");
            DropColumn("dbo.Creneaux", "Jour");
            DropColumn("dbo.Creneaux", "HeureDebut");
            DropColumn("dbo.Creneaux", "PlacesDisponibles");
           //DropTable("dbo.CreneauxDisciplineSections");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CreneauxDisciplineSections",
                c => new
                    {
                        Creneaux_CreneauHoraireId = c.Int(nullable: false),
                        DisciplineSection_DisciplineId = c.Int(nullable: false),
                        DisciplineSection_SectionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Creneaux_CreneauHoraireId, t.DisciplineSection_DisciplineId, t.DisciplineSection_SectionId });
            
            AddColumn("dbo.Creneaux", "PlacesDisponibles", c => c.Int(nullable: false));
            AddColumn("dbo.Creneaux", "HeureDebut", c => c.Time(nullable: false, precision: 7));
            AddColumn("dbo.Creneaux", "Jour", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.Creneaux", "Section_SectionId", "dbo.Sections");
            DropForeignKey("dbo.Paiements", "AdherentId", "dbo.Adherents");
            DropForeignKey("dbo.Sections", "Creneaux_CreneauHoraireId", "dbo.Creneaux");
            DropIndex("dbo.Paiements", new[] { "AdherentId" });
            DropIndex("dbo.Creneaux", new[] { "Section_SectionId" });
            DropIndex("dbo.Sections", new[] { "Creneaux_CreneauHoraireId" });
            AlterColumn("dbo.Adherents", "Role", c => c.String(nullable: false));
            AlterColumn("dbo.Adherents", "DateNaissance", c => c.DateTime(nullable: false));
            DropColumn("dbo.Paiements", "AdherentId");
            DropColumn("dbo.Sections", "Creneaux_CreneauHoraireId");
            DropColumn("dbo.Creneaux", "Section_SectionId");
            DropColumn("dbo.Creneaux", "JourHeureDebut");
            DropColumn("dbo.DisciplineSections", "DisciplineSectionId");
            DropColumn("dbo.Disciplines", "PlacesDisponibles");
            RenameIndex(table: "dbo.Paiements", name: "IX_DossierId", newName: "IX_DossierInscriptionDossierId");
            RenameColumn(table: "dbo.Paiements", name: "DossierId", newName: "DossierInscriptionDossierId");
            CreateIndex("dbo.CreneauxDisciplineSections", new[] { "DisciplineSection_DisciplineId", "DisciplineSection_SectionId" });
            CreateIndex("dbo.CreneauxDisciplineSections", "Creneaux_CreneauHoraireId");
            AddForeignKey("dbo.Creneaux", "SectionId", "dbo.Sections", "SectionId", cascadeDelete: true);
            AddForeignKey("dbo.CreneauxDisciplineSections", new[] { "DisciplineSection_DisciplineId", "DisciplineSection_SectionId" }, "dbo.DisciplineSections", new[] { "DisciplineId", "SectionId" }, cascadeDelete: true);
            AddForeignKey("dbo.CreneauxDisciplineSections", "Creneaux_CreneauHoraireId", "dbo.Creneaux", "CreneauHoraireId", cascadeDelete: true);
        }
    }
}
