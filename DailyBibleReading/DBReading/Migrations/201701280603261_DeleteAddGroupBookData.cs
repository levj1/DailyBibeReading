namespace DBReading.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteAddGroupBookData : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.BibleBooks", "ReadingGroupBookID");
            AddForeignKey("dbo.BibleBooks", "ReadingGroupBookID", "dbo.ReadingGroupBooks", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BibleBooks", "ReadingGroupBookID", "dbo.ReadingGroupBooks");
            DropIndex("dbo.BibleBooks", new[] { "ReadingGroupBookID" });
        }
    }
}
