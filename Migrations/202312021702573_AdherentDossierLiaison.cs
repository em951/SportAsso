namespace SportAssovv.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdherentDossierLiaison : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DossierInscriptions", "AdherentId", "dbo.Adherents");
            DropForeignKey("dbo.Paiements", "DossierInscriptionDossierId", "dbo.DossierInscriptions");
            DropPrimaryKey("dbo.DossierInscriptions");
            AlterColumn("dbo.DossierInscriptions", "DossierId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.DossierInscriptions", "AdherentId");
            AddForeignKey("dbo.DossierInscriptions", "AdherentId", "dbo.Adherents", "AdherentId");
            AddForeignKey("dbo.Paiements", "DossierInscriptionDossierId", "dbo.DossierInscriptions", "AdherentId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Paiements", "DossierInscriptionDossierId", "dbo.DossierInscriptions");
            DropForeignKey("dbo.DossierInscriptions", "AdherentId", "dbo.Adherents");
            DropPrimaryKey("dbo.DossierInscriptions");
            AlterColumn("dbo.DossierInscriptions", "DossierId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.DossierInscriptions", "DossierId");
            AddForeignKey("dbo.Paiements", "DossierInscriptionDossierId", "dbo.DossierInscriptions", "DossierId", cascadeDelete: true);
            AddForeignKey("dbo.DossierInscriptions", "AdherentId", "dbo.Adherents", "AdherentId", cascadeDelete: true);
        }
    }
}
