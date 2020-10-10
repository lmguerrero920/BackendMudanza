namespace ProjectMudanza.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ValidacionModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Mudanzas",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Document = c.Int(nullable: false),
                    DateProcess = c.DateTime(nullable: false),
                    NumberTrips = c.String(),
                })
                .PrimaryKey(t => t.Id);

        }

        public override void Down()
        {
            DropTable("dbo.Mudanzas");
        }
    }
}
