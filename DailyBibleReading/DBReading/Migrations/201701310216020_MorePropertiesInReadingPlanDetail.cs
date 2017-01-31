namespace DBReading.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MorePropertiesInReadingPlanDetail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReadingPlanDetails", "ReadingVersionAbbr", c => c.String(maxLength: 10));
            AddColumn("dbo.ReadingPlanDetails", "ReadingLanguageAbbr", c => c.String());
            DropColumn("dbo.ReadingPlanDetails", "PassageReference");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ReadingPlanDetails", "PassageReference", c => c.String());
            DropColumn("dbo.ReadingPlanDetails", "ReadingLanguageAbbr");
            DropColumn("dbo.ReadingPlanDetails", "ReadingVersionAbbr");
        }
    }
}
