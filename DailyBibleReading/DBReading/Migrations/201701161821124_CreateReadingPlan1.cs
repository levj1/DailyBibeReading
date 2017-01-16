namespace DBReading.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateReadingPlan1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ReadingPlans", "TodayPassage");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ReadingPlans", "TodayPassage", c => c.String());
        }
    }
}
