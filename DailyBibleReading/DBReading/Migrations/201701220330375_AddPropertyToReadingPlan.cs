namespace DBReading.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPropertyToReadingPlan : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ReadingPlans", "Name", c => c.String(maxLength: 30));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ReadingPlans", "Name", c => c.String());
        }
    }
}
