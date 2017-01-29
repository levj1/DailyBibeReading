namespace DBReading.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateReadingPlan : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReadingPlans", "ChapterPerDay", c => c.Int(nullable: false));
            AddColumn("dbo.ReadingPlans", "WeekDayOnly", c => c.Boolean(nullable: false));
            AlterColumn("dbo.ReadingPlans", "Name", c => c.String(nullable: false, maxLength: 30));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ReadingPlans", "Name", c => c.String(maxLength: 30));
            DropColumn("dbo.ReadingPlans", "WeekDayOnly");
            DropColumn("dbo.ReadingPlans", "ChapterPerDay");
        }
    }
}
