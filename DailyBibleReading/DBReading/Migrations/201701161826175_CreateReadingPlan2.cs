namespace DBReading.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateReadingPlan2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReadingPlans", "StartDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ReadingPlans", "StartDate");
        }
    }
}
