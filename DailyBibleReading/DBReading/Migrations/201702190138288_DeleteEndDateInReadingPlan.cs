namespace DBReading.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteEndDateInReadingPlan : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ReadingPlans", "EndDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ReadingPlans", "EndDate", c => c.DateTime(nullable: false));
        }
    }
}
