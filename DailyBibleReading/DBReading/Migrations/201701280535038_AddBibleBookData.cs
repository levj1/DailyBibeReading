namespace DBReading.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBibleBookData : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BibleBooks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ReadingGroupBookID = c.Int(nullable: false),
                        MaxChapter = c.Int(nullable: false),
                        Testament = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.BibleBooks");
        }
    }
}
