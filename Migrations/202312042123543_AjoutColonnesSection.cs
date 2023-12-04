namespace SportAssovv.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AjoutColonnesSection : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sections", "Encadrant", c => c.String(nullable: false));
            AddColumn("dbo.Sections", "Lieu", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sections", "Lieu");
            DropColumn("dbo.Sections", "Encadrant");
        }
    }
}
