namespace DBReading.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeGroup : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Readers", "Group_ID", "dbo.Groups");
            DropForeignKey("dbo.ReadingPlans", "Group_ID", "dbo.Groups");
            DropIndex("dbo.Readers", new[] { "Group_ID" });
            DropIndex("dbo.ReadingPlans", new[] { "Group_ID" });
            DropColumn("dbo.Groups", "ReadingPlanID");
            DropColumn("dbo.Groups", "ReaderID");
            DropColumn("dbo.Readers", "Group_ID");
            DropColumn("dbo.ReadingPlans", "Group_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ReadingPlans", "Group_ID", c => c.Int());
            AddColumn("dbo.Readers", "Group_ID", c => c.Int());
            AddColumn("dbo.Groups", "ReaderID", c => c.Int(nullable: false));
            AddColumn("dbo.Groups", "ReadingPlanID", c => c.Int(nullable: false));
            CreateIndex("dbo.ReadingPlans", "Group_ID");
            CreateIndex("dbo.Readers", "Group_ID");
            AddForeignKey("dbo.ReadingPlans", "Group_ID", "dbo.Groups", "ID");
            AddForeignKey("dbo.Readers", "Group_ID", "dbo.Groups", "ID");
        }
    }
}
