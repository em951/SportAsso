namespace SportAssovv.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangementPaiement : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DetailsPaiements",
                c => new
                    {
                        PaiementId = c.Int(nullable: false),
                        NumeroCarte = c.String(),
                        Valeur = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NomTitulaire = c.String(),
                        DateValidite = c.String(),
                    })
                .PrimaryKey(t => t.PaiementId)
                .ForeignKey("dbo.Paiements", t => t.PaiementId)
                .Index(t => t.PaiementId);
            
            AddColumn("dbo.DossierInscriptions", "Certificat_medical_data", c => c.Binary());
            AddColumn("dbo.DossierInscriptions", "Certificat_medical_contentType", c => c.String());
            AddColumn("dbo.DossierInscriptions", "Assurance_data", c => c.Binary());
            AddColumn("dbo.DossierInscriptions", "Assurance_contentType", c => c.String());
            AddColumn("dbo.Paiements", "PaiementDetailsId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DetailsPaiements", "PaiementId", "dbo.Paiements");
            DropIndex("dbo.DetailsPaiements", new[] { "PaiementId" });
            DropColumn("dbo.Paiements", "PaiementDetailsId");
            DropColumn("dbo.DossierInscriptions", "Assurance_contentType");
            DropColumn("dbo.DossierInscriptions", "Assurance_data");
            DropColumn("dbo.DossierInscriptions", "Certificat_medical_contentType");
            DropColumn("dbo.DossierInscriptions", "Certificat_medical_data");
            DropTable("dbo.DetailsPaiements");
        }
    }
}
