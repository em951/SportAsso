namespace SportAssovv.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropColumnPaiement : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Paiements", "PaiementDetailsId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Paiements", "PaiementDetailsId", c => c.Int(nullable: false));
        }
    }
}
