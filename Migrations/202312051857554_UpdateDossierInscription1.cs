namespace SportAssovv.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDossierInscription1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Paiements", "DossierId", "dbo.DossierInscriptions");
            DropIndex("dbo.Paiements", new[] { "DossierId" });
            DropColumn("dbo.DossierInscriptions", "DossierId");
            DropColumn("dbo.Paiements", "DossierId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Paiements", "DossierId", c => c.Int(nullable: false));
            AddColumn("dbo.DossierInscriptions", "DossierId", c => c.Int(nullable: false, identity: true));
            CreateIndex("dbo.Paiements", "DossierId");
            AddForeignKey("dbo.Paiements", "DossierId", "dbo.DossierInscriptions", "AdherentId", cascadeDelete: true);
        }
    }
}
