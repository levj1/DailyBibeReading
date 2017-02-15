namespace DBReading.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addReader : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Readers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 30),
                        LastName = c.String(nullable: false, maxLength: 30),
                        MiddleName = c.String(maxLength: 30),
                        Email = c.String(nullable: false, maxLength: 30),
                        ReaderDteCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Readers");
        }
    }
}
