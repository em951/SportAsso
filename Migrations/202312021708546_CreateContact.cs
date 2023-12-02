namespace SportAssovv.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateContact : DbMigration
    {
        public override void Up()
        {
            CreateTable(
               "dbo.Contacts",
               c => new
               {
                   ID = c.Int(nullable: false, identity: true),
                   Nome = c.String(nullable: false, maxLength: 100),
                   Email = c.String(nullable: false, maxLength: 100),
                   Mensagem = c.String(nullable: false),
               })
               .PrimaryKey(t => t.ID);
        }
        
        public override void Down()
        {
            DropTable("dbo.Contacts");
        }
    }
}
