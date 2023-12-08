namespace SportAssovv.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTablePaiement : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Paiements", "DateExpiration", c => c.String());
            AddColumn("dbo.Paiements", "Cvv", c => c.String());
            AlterColumn("dbo.Paiements", "DatePaiement", c => c.DateTime());
            DropColumn("dbo.Paiements", "Valeur");
            DropColumn("dbo.Paiements", "DateValidite");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Paiements", "DateValidite", c => c.String());
            AddColumn("dbo.Paiements", "Valeur", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Paiements", "DatePaiement", c => c.DateTime(nullable: false));
            DropColumn("dbo.Paiements", "Cvv");
            DropColumn("dbo.Paiements", "DateExpiration");
        }
    }
}
