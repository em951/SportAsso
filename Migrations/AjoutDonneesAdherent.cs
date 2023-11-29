namespace SportAssovv.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AjoutDonneesAdherent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Adherents", "Role", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Adherents", "Role");
        }
    }
}
