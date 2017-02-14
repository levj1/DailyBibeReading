namespace DBReading.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TwoMoreBookOptionInReadingPlan : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReadingPlans", "GroupBookSelected", c => c.String());
            AddColumn("dbo.ReadingPlans", "SingleBookSelected", c => c.String());
            DropColumn("dbo.ReadingPlans", "SelectedReadingOption");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ReadingPlans", "SelectedReadingOption", c => c.String());
            DropColumn("dbo.ReadingPlans", "SingleBookSelected");
            DropColumn("dbo.ReadingPlans", "GroupBookSelected");
        }
    }
}
