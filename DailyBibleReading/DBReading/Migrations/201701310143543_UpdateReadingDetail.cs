namespace DBReading.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateReadingDetail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReadingPlanDetails", "BookName", c => c.String());
            AddColumn("dbo.ReadingPlanDetails", "StartVerse", c => c.Int(nullable: false));
            AddColumn("dbo.ReadingPlanDetails", "EndVerse", c => c.Int(nullable: false));
            AddColumn("dbo.ReadingPlanDetails", "ReadingDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.ReadingPlanDetails", "PassageDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ReadingPlanDetails", "PassageDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.ReadingPlanDetails", "ReadingDate");
            DropColumn("dbo.ReadingPlanDetails", "EndVerse");
            DropColumn("dbo.ReadingPlanDetails", "StartVerse");
            DropColumn("dbo.ReadingPlanDetails", "BookName");
        }
    }
}
