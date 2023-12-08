namespace SportAssovv.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropDetailsPaiement : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DetailsPaiements", "PaiementId", "dbo.Paiements");
            DropIndex("dbo.DetailsPaiements", new[] { "PaiementId" });
            AddColumn("dbo.Paiements", "NumeroCarte", c => c.String());
            AddColumn("dbo.Paiements", "Valeur", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Paiements", "NomTitulaire", c => c.String());
            AddColumn("dbo.Paiements", "DateValidite", c => c.String());
            DropColumn("dbo.Paiements", "StatutPaiement");
            DropTable("dbo.DetailsPaiements");
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.PaiementId);
            
            AddColumn("dbo.Paiements", "StatutPaiement", c => c.String(maxLength: 64));
            DropColumn("dbo.Paiements", "DateValidite");
            DropColumn("dbo.Paiements", "NomTitulaire");
            DropColumn("dbo.Paiements", "Valeur");
            DropColumn("dbo.Paiements", "NumeroCarte");
            CreateIndex("dbo.DetailsPaiements", "PaiementId");
            AddForeignKey("dbo.DetailsPaiements", "PaiementId", "dbo.Paiements", "PaiementId");
        }
    }
}
