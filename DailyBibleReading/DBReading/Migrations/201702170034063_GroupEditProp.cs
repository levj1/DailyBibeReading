namespace DBReading.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GroupEditProp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Groups", "ReadingPlanID", c => c.Int(nullable: false));
            AddColumn("dbo.Groups", "ReaderID", c => c.Int(nullable: false));
            AddColumn("dbo.Readers", "Group_ID", c => c.Int());
            CreateIndex("dbo.Readers", "Group_ID");
            AddForeignKey("dbo.Readers", "Group_ID", "dbo.Groups", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Readers", "Group_ID", "dbo.Groups");
            DropIndex("dbo.Readers", new[] { "Group_ID" });
            DropColumn("dbo.Readers", "Group_ID");
            DropColumn("dbo.Groups", "ReaderID");
            DropColumn("dbo.Groups", "ReadingPlanID");
        }
    }
}
