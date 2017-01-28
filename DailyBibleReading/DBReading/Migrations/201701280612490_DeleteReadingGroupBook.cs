namespace DBReading.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteReadingGroupBook : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BibleBooks", "ReadingGroupBookID", "dbo.ReadingGroupBooks");
            DropIndex("dbo.BibleBooks", new[] { "ReadingGroupBookID" });
            DropTable("dbo.ReadingGroupBooks");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ReadingGroupBooks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateIndex("dbo.BibleBooks", "ReadingGroupBookID");
            AddForeignKey("dbo.BibleBooks", "ReadingGroupBookID", "dbo.ReadingGroupBooks", "ID", cascadeDelete: true);
        }
    }
}
