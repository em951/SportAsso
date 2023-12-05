namespace SportAssovv.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRelationSectionDiscipline : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sections", "DisciplineId", c => c.Int(nullable: false));
            CreateIndex("dbo.Sections", "DisciplineId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Sections", new[] { "DisciplineId" });
            DropColumn("dbo.Sections", "DisciplineId");
        }
    }
}
