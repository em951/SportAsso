namespace SportAssovv.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Creneaux", "SectionId", "dbo.Sections");
            DropIndex("dbo.Creneaux", new[] { "SectionId" });
            AddColumn("dbo.Sections", "Jour", c => c.DateTime());
            AddColumn("dbo.Sections", "HeureDebut", c => c.Time(nullable: false, precision: 7));
            AddColumn("dbo.Sections", "HeureFin", c => c.Time(nullable: false, precision: 7));
            DropTable("dbo.Creneaux");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Creneaux",
                c => new
                    {
                        CreneauHoraireId = c.Int(nullable: false, identity: true),
                        JourHeureDebut = c.DateTime(nullable: false),
                        HeureFin = c.Time(nullable: false, precision: 7),
                        SectionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CreneauHoraireId);
            
            DropColumn("dbo.Sections", "HeureFin");
            DropColumn("dbo.Sections", "HeureDebut");
            DropColumn("dbo.Sections", "Jour");
            CreateIndex("dbo.Creneaux", "SectionId");
            AddForeignKey("dbo.Creneaux", "SectionId", "dbo.Sections", "SectionId", cascadeDelete: true);
        }
    }
}
