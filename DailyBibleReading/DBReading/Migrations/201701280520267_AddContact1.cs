namespace DBReading.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddContact1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ReadingGroupBooks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ReadingGroupBooks");
        }
    }
}
