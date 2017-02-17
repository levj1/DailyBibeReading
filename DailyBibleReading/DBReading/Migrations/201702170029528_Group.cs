namespace DBReading.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Group : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.ReadingPlans", "Group_ID", c => c.Int());
            CreateIndex("dbo.ReadingPlans", "Group_ID");
            AddForeignKey("dbo.ReadingPlans", "Group_ID", "dbo.Groups", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReadingPlans", "Group_ID", "dbo.Groups");
            DropIndex("dbo.ReadingPlans", new[] { "Group_ID" });
            DropColumn("dbo.ReadingPlans", "Group_ID");
            DropTable("dbo.Groups");
        }
    }
}
