namespace SportAssovv.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDossierAdherentPaiement : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DossierInscriptions", "DossierId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Paiements", "StatutPaiement", c => c.String(maxLength: 64));
            DropColumn("dbo.DossierInscriptions", "StatutInscription");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DossierInscriptions", "StatutInscription", c => c.String(nullable: false, maxLength: 64));
            AlterColumn("dbo.Paiements", "StatutPaiement", c => c.String(nullable: false, maxLength: 64));
            AlterColumn("dbo.DossierInscriptions", "DossierId", c => c.Int(nullable: false));
        }
    }
}
