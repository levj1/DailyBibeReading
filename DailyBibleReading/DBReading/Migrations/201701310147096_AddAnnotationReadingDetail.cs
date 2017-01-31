namespace DBReading.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAnnotationReadingDetail : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ReadingPlanDetails", "BookName", c => c.String(maxLength: 30));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ReadingPlanDetails", "BookName", c => c.String());
        }
    }
}
