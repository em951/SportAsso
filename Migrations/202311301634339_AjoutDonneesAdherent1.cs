namespace SportAssovv.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AjoutDonneesAdherent1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DossierInscriptions", "Certificat_medical", c => c.Boolean(nullable: false));
            AddColumn("dbo.DossierInscriptions", "Assurance", c => c.Boolean(nullable: false));
            AddColumn("dbo.DossierInscriptions", "Dossier_complet", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DossierInscriptions", "Dossier_complet");
            DropColumn("dbo.DossierInscriptions", "Assurance");
            DropColumn("dbo.DossierInscriptions", "Certificat_medical");
        }
    }
}
